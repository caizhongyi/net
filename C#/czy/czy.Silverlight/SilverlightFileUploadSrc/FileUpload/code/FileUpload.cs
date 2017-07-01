using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FluxJpeg.Core.Decoder;
using FluxJpeg.Core;
using System.IO;
using FluxJpeg.Core.Filtering;
using FluxJpeg.Core.Encoder;
using System.Threading;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;

namespace DC.FileUpload
{
    public enum FileUploadStatus
    {
        Pending,
        Uploading,
        Complete,
        Error,
        Canceled,
        Removed,
        Resizing
    }

    public class FileUpload : INotifyPropertyChanged
    {
        public event ProgressChangedEvent UploadProgressChanged;
        public event EventHandler StatusChanged;

        public long ChunkSize = 4194304;

        public Uri UploadUrl { get; set; }
        private FileInfo file;
        public FileInfo File
        {
            get { return file; }
            set
            {
                file = value;
                Stream temp = file.OpenRead();
                FileLength = temp.Length;
                temp.Close();
            }
        }
        public string Name { get { return File.Name; } }
        private long fileLength;
        public long FileLength
        {
            get { return fileLength; }
            set
            {
                fileLength = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FileLength"));
                });
            }
        }

        private MemoryStream resizeStream;
        public bool ResizeImage { get; set; }
        public int ImageSize { get; set; }
        
        private long bytesUploaded;
        public long BytesUploaded
        {
            get { return bytesUploaded; }
            set
            {
                bytesUploaded = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("BytesUploaded"));
                });
            }
        }

        private int uploadPercent;
        public int UploadPercent
        {
            get { return uploadPercent; }
            set
            {
                uploadPercent = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("UploadPercent"));
                });
            }
        }

        private FileUploadStatus status;
        public FileUploadStatus Status 
        {
            get { return status; }
            set
            {
                status = value;
                
                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                    if (StatusChanged != null)
                        StatusChanged(this, null);
                });
            }
        }

        private Dispatcher Dispatcher;

        private bool cancel;
        private bool remove;

        private bool displayThumbnail;
        public bool DisplayThumbnail
        {
            get { return displayThumbnail; }
            set
            {
                displayThumbnail = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("DisplayThumbnail"));
                });
            }
        }

        public FileUpload(Dispatcher dispatcher)
        {            
            Dispatcher = dispatcher;
            Status = FileUploadStatus.Pending;
        }

        public FileUpload(Dispatcher dispatcher,Uri uploadUrl)
            : this(dispatcher)
        {
            UploadUrl = uploadUrl;
        }

        public FileUpload(Dispatcher dispatcher, Uri uploadUrl, FileInfo fileToUpload)
            : this(dispatcher,uploadUrl)
        {
            File = fileToUpload;
        }

        public void Upload()
        {
            if (File == null || UploadUrl == null)
                return;
            Status = FileUploadStatus.Uploading;
            cancel = false;

            if (ResizeImage && file.Name.ToLower().EndsWith("jpg") && resizeStream == null && ImageSize > 0)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

                worker.RunWorkerAsync();
            }
            else
                CheckFileOnServer();           
        }

        private void CheckFileOnServer()
        {
            UriBuilder ub = new UriBuilder(UploadUrl);
            ub.Query = string.Format("{1}filename={0}&GetBytes=true", File.Name, string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0, 1) + "&");
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(ub.Uri);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            long lengthtemp = 0;
            if (!string.IsNullOrEmpty(e.Result))
            {
                lengthtemp = long.Parse(e.Result);
            }

            if (lengthtemp > 0)
            {
                MessageBoxResult result;
                if (lengthtemp == FileLength)
                {
                    result = MessageBox.Show("File already exists, overwrite?", "Overwrite?", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                        lengthtemp = 0;
                    else
                    {

                        UploadProgressChangedEventArgs args = new UploadProgressChangedEventArgs(100, FileLength - BytesUploaded, BytesUploaded, FileLength, file.Name);
                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            UploadProgressChanged(this, args);
                        });
                        BytesUploaded = FileLength;
                        Status = FileUploadStatus.Complete;
                        return;
                    }
                }
                else
                {
                    result = MessageBox.Show("File already exists, continue upload?", "Continue?", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                        lengthtemp = 0;
                }
            }
            
            UploadFileEx();
        }

        public void CancelUpload()
        {
            cancel = true;
        }

        public void RemoveUpload()
        {
            cancel = true;
            remove = true;
            if(Status != FileUploadStatus.Uploading)
                Status = FileUploadStatus.Removed;
        }

        public void UploadFileEx()
        {
            Status = FileUploadStatus.Uploading;
            long temp = FileLength - BytesUploaded;

            UriBuilder ub = new UriBuilder(UploadUrl);
            bool complete = temp <= ChunkSize;            
            ub.Query = string.Format("{3}filename={0}&StartByte={1}&Complete={2}", File.Name, BytesUploaded, complete, string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0,1) + "&");

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(ub.Uri);
            webrequest.Method = "POST";
            webrequest.BeginGetRequestStream(new AsyncCallback(WriteCallback), webrequest);
        }

        private void WriteCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            // End the operation.
            Stream requestStream = webrequest.EndGetRequestStream(asynchronousResult);

            byte[] buffer = new Byte[4096];
            int bytesRead = 0;
            int tempTotal = 0;

            Stream fileStream = resizeStream != null ? (Stream)resizeStream : File.OpenRead();

            //using (FileStream fileStream = File.OpenRead())
            //{
                fileStream.Position = BytesUploaded;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 && tempTotal + bytesRead < ChunkSize && !cancel)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                    requestStream.Flush();
                    BytesUploaded += bytesRead;
                    tempTotal += bytesRead;
                    if (UploadProgressChanged != null)
                    {
                        int percent = (int)(((double)BytesUploaded / (double)FileLength) * 100);
                        UploadProgressChangedEventArgs args = new UploadProgressChangedEventArgs(percent, bytesRead, BytesUploaded, FileLength, file.Name);
                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            UploadProgressChanged(this, args);
                        });
                    }
                }
            //}

            // only close the stream if it came from the file, don't close resizestream so we don't have to resize it over again.
                if (resizeStream == null)
                    fileStream.Close();
            requestStream.Close();
            webrequest.BeginGetResponse(new AsyncCallback(ReadCallback), webrequest);

        }
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)webrequest.EndGetResponse(asynchronousResult);
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string responsestring = reader.ReadToEnd();
            reader.Close();

            if (cancel)
            {
                if (resizeStream != null)
                    resizeStream.Close();
                if (remove)
                    Status = FileUploadStatus.Removed;
                else
                    Status = FileUploadStatus.Canceled;
            }
            else if (BytesUploaded < FileLength)
                UploadFileEx();
            else
            {
                if (resizeStream != null)
                    resizeStream.Close();

                Status = FileUploadStatus.Complete;
            }

        }

        void Resize()
        {
            Status = FileUploadStatus.Resizing;
            Stream fileStream = file.OpenRead();
            using (fileStream)
            {
                // Decode
                DecodedJpeg jpegIn = new JpegDecoder(fileStream).Decode();

                if (!ImageResizer.ResizeNeeded(jpegIn.Image, ImageSize))
                {
                    return;
                }
                else
                {

                    // Resize
                    DecodedJpeg jpegOut = new DecodedJpeg(
                        new ImageResizer(jpegIn.Image)
                            .Resize(ImageSize, ResamplingFilters.NearestNeighbor),
                        jpegIn.MetaHeaders); // Retain EXIF details

                    // Encode
                    resizeStream = new MemoryStream();
                    new JpegEncoder(jpegOut, 90, resizeStream).Encode();
                    // Display 
                    resizeStream.Seek(0, SeekOrigin.Begin);
                    FileLength = resizeStream.Length;
                }
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CheckFileOnServer();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Resize();
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace czy.MyClass.Web
{

    public class ResponseFilter : Stream
    {
        private Stream m_sink;
        private long m_position;
        private FileStream fs;

        public ResponseFilter(Stream sink)
        {
            m_sink = sink;
            fs = new FileStream(@"C:\FilterOutput\response.htm", FileMode.OpenOrCreate, FileAccess.Write);
        }

        // The following members of Stream must be overriden.
        public override bool CanRead
        { get { return true; } }

        public override bool CanSeek
        { get { return false; } }

        public override bool CanWrite
        { get { return false; } }

        public override long Length
        { get { return 0; } }

        public override long Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public override long Seek(long offset, System.IO.SeekOrigin direction)
        {
            return 0;
        }

        public override void SetLength(long length)
        {
            m_sink.SetLength(length);
        }

        public override void Close()
        {
            m_sink.Close();
            fs.Close();
        }

        public override void Flush()
        {
            m_sink.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return m_sink.Read(buffer, offset, count);
        }

        // Override the Write method to filter Response to a file.
        public override void Write(byte[] buffer, int offset, int count)
        {
            //Write out the response to the browser.
            m_sink.Write(buffer, 0, count);

            //Write out the response to the file.
            fs.Write(buffer, 0, count);
        }
    }

}

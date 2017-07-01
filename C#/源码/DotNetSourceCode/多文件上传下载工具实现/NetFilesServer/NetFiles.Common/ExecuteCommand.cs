using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace NetFiles.Common
{
    public class ExecuteCommand
    {
        public static int DataPageSize(FileStream stream)
        {
            int pagesize = 1024;
            


                if (stream.Length > 100000)
                {
                    pagesize *= 10;
                }
                else if (stream.Length > 1000000)
                {
                    pagesize *= 100;
                }
                else if (stream.Length > 10000000)
                {
                    pagesize *= 1000;
                }
            
            return pagesize;
        }
        public static int PackageCount(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                int pagesize = DataPageSize(stream);
                return NetFiles.Command.SEND_FILE.PackageCount(stream, pagesize);
            }
        }
        public static void SEND_FILE(HFSoft.Net.IChannel channel, string filename, NetFiles.Command.Record folder,string name)
        {
            int pagesize = 1024;
            using (HFSoft.Net.Messages.MessageChannel mc = new HFSoft.Net.Messages.MessageChannel(channel))
            {
                FileStream stream = File.OpenRead(filename);
                pagesize = DataPageSize(stream);
               NetFiles.Command.SEND_FILE sf;
                byte[] data = new byte[pagesize];
                int count = NetFiles.Command.SEND_FILE.PackageCount(stream, pagesize);
                int pageindex = 0;

                mc.Channel.Completed += delegate(object source, EventArgs e_)
                {
                    if (mc.Channel.State == HFSoft.Net.ChannelState.Closed)
                    {
                        stream.Close();
                        return;
                    }
                    if (pageindex == count)
                    {
                        stream.Close();
                        return;
                    }
                    sf = NetFiles.Command.SEND_FILE.FilePackage(stream, pagesize, pageindex, count, data);
                    pageindex++;
                    System.Threading.Thread.Sleep(1);
                    mc.Send(sf);


                };
                sf = NetFiles.Command.SEND_FILE.FilePackage(stream, pagesize, pageindex, count, data);
                sf.Folder = folder;
                sf.Name = name;
                pageindex++;
                mc.Send(sf);
                sf.Data = null;
                sf = null;



            }
        }
        public static void SEND_FILE(HFSoft.Net.IChannel channel, string filename)
        {
            SEND_FILE(channel, filename, null,null);
        }

        public static void LIST_FILES(HFSoft.Net.IChannel channel, NetFiles.Command.LIST_FILE_REQUEST request)
        {
            NetFiles.Command.LIST_FILE_RESPONSE response = new NetFiles.Command.LIST_FILE_RESPONSE();
            NetFiles.Command.Record record;
            if (request.Folder.FullName == NetFiles.Command.Record.BootTag)
            {
                NetFiles.DataAccess.IBoot boot = NetFiles.DataAccess.AccessFactory.CreateBoot();
                foreach (NetFiles.DataAccess.Entities.BootInfo item in boot.List())
                {
                    record = new NetFiles.Command.Record(item.BootDirectory);
                    record.Name = Path.GetFileName(item.BootDirectory);
                    record.Type = NetFiles.Command.RecoreType.Folder;
                    response.Records.Add(record);
                }
            }
            else
            {
                foreach (string item in Directory.GetDirectories(request.Folder.FullName))
                {
                    record = new NetFiles.Command.Record(item);
                    record.Name = Path.GetFileName(item);
                    record.Type = NetFiles.Command.RecoreType.Folder;
                    response.Records.Add(record);
                }
                foreach (string item in Directory.GetFiles(request.Folder.FullName))
                {
                    record = new NetFiles.Command.Record(item);
                    record.Name = Path.GetFileName(item);
                    record.Type = NetFiles.Command.RecoreType.File;
                    try
                    {
                        using (FileStream stream = File.OpenRead(item))
                        {
                            record.Size = stream.Length;
                        }
                    }
                    catch
                    {
                    }
                    response.Records.Add(record);

                }
            }
            using (HFSoft.Net.Messages.MessageChannel mc = new HFSoft.Net.Messages.MessageChannel(channel))
            {
                mc.Send(response);
            }
        }

       
    }
}

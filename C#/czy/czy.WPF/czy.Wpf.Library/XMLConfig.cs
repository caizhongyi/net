using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace czy.Wpf.Library
{
    public  class XMLConfig
    {
        XmlDocument doc=new XmlDocument ();
        public XMLConfig()
        { }

        public void CreateXML(string path,string[,] node)
        {
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"); 
                sw.WriteLine("<root>");
                for (int i = 0; i < node.Length / 2;i++ )
                {
                    for (int j = 0; j < node.Length / 2; j++)
                    {
                         

                    
                    }
                }
                sw.WriteLine("</root>");
            }

        }
    }
}

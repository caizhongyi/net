using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace ReadText
{
    class Program
    {  
        static void Main(string[] args)
        {
            //StreamReader sr = new StreamReader(@"D:\reader.txt", System.Text.Encoding.GetEncoding("GB2312"));
            //string line;
            //string[] t;
            //while(sr.Peek()>=0)
            //{
            //    line = sr.ReadLine();
            //    t= line.Split('#'); 
            //Console.WriteLine(t[0]);
            //Console.ReadKey(); 
            //}     


            //string strSol=string.Empty;
            //List<string> listFile=new List<string>();
            //StreamReader objSr = new StreamReader(@"D:\reader.txt", System.Text.Encoding.GetEncoding("GB2312"));
            //while(!objSr.EndOfStream){
            //    listFile.Add(objSr.ReadLine());
            //}
            //if(listFile.Count>0)
            //{
            //    for (int i = listFile.Count; i < listFile.Count-1; i++)
            //    {
            //        strSol+=listFile+Environment.NewLine;
            //    }
            //}else
            //    for(int i=0;i<listFile.Count;i++)
            //    {
            //        strSol+=listFile+Environment.NewLine;
            //    }
            //Console.WriteLine(strSol + listFile[listFile.Count-1]);
            //Console.ReadKey();

            //int counter = 0;
            //string line;
            //// Read the file and display it line by line.
            //StreamReader file =new StreamReader(@"D:\reader.txt", System.Text.Encoding.GetEncoding("GB2312"));
            //file.BaseStream.Seek(2, SeekOrigin.Begin);
            //while ((line = file.ReadLine()) != null)
            //{
            //    Console.WriteLine(line);
            //    counter++;
            //}
            //file.Close();
            //// Suspend the screen.
            //Console.ReadLine();

            StreamReader objReader = new StreamReader(@"D:\reader.txt", System.Text.Encoding.GetEncoding("GB2312"));
            string sLine = "";
            ArrayList arrText = new ArrayList();
            bool IsRead = true;
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    if (IsRead)
                    {
                        Console.WriteLine("标题："+sLine + "\n");
                        IsRead = false;
                    }
                    else
                    {
                        arrText.Add(sLine);
                    }
                }

            }
            objReader.Close();

            foreach (string sOutput in arrText)
                Console.WriteLine(sOutput);
            Console.ReadLine();         
        }
    }
}

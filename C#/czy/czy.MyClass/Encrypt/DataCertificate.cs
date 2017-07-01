using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.IO;

namespace czy.MyClass.Encrypt
{
    /// <summary>
    /// 证书类 用于创建程序集验证证书(保证不被创改)
    /// </summary>
    public sealed class DataCertificate
    {
        #region 使用事例
        /// <summary>
        /// 将证书从证书存储区导出，并存储为pfx文件，同时为pfx文件指定打开的密码   
        /// 本函数同时也演示如何用公钥进行加密，私钥进行解密   
        /// </summary>  
        /// </summary>
        /// <param name="enstr"></param>
        /// <param name="destr"></param>
        private void GetEncryptString()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
            foreach (X509Certificate2 x509 in storecollection)
            {
                if (x509.Subject == "CN=luminji")
                {
                    Debug.Print(string.Format("certificate name: {0}", x509.Subject));
                    byte[] pfxByte = x509.Export(X509ContentType.Pfx, "123");
                    using (FileStream fileStream = new FileStream("luminji.pfx", FileMode.Create))
                    {
                        // Write the data to the file, byte by byte.   
                        for (int i = 0; i < pfxByte.Length; i++)
                            fileStream.WriteByte(pfxByte[i]);
                        // Set the stream position to the beginning of the file.   
                        fileStream.Seek(0, SeekOrigin.Begin);
                        // Read and verify the data.   
                        for (int i = 0; i < fileStream.Length; i++)
                        {
                            if (pfxByte[i] != fileStream.ReadByte())
                            {
                                Debug.Print("Error writing data.");
                                return;
                            }
                        }
                        fileStream.Close();
                        Debug.Print("The data was written to {0} " +
                             "and verified.", fileStream.Name);
                    }
                    string myname = "my name is luminji! and i love huzhonghua!";
                    string enStr = czy.MyClass.Encrypt.RSAEncryption.RSAEncrypt(x509.PublicKey.Key.ToXmlString(false), myname);
                    // MessageBox.Show("密文是：" + enStr);   
                    string deStr = czy.MyClass.Encrypt.RSAEncryption.RSADecrypt(x509.PrivateKey.ToXmlString(true), enStr);
                    // MessageBox.Show("明文是：" + deStr);  
                   // enstr = enStr;
                    //destr = deStr;
                }
            }
            store.Close();
            store = null;
            storecollection = null;
        }
        /// <summary>   
        /// 创建还有私钥的证书   
        /// </summary>        
        /// <param name="sender"></param>   
        /// <param name="e"></param>   
        private void btn_createPfx_Click()
        {
            string MakeCert = "C:\\Program Files\\Microsoft Visual Studio 8\\SDK\\v2.0\\Bin\\makecert.exe";
            string x509Name = "CN=luminji";
            string param = " -pe -ss my -n \"" + x509Name + "\" ";
            Process p = Process.Start(MakeCert, param); p.WaitForExit();
            p.Close();
            //MessageBox.Show("over");   
        }
        /// <summary>   
        /// 从pfx文件读取证书信息   
        /// </summary>   
        /// <param name="sender"></param>   
        /// <param name="e"></param>   
        private void btn_readFromPfxFile()
        {
            X509Certificate2 pc = new X509Certificate2("luminji.pfx", "123");
            // MessageBox.Show("name:" + pc.SubjectName.Name);   
            //  MessageBox.Show("public:" + pc.PublicKey.ToString());   
            // MessageBox.Show("private:" + pc.PrivateKey.ToString());   
            pc = null;
        }
        #endregion

        #region 生成证书
        /// <summary>   
        /// 根据指定的证书名和makecert全路径生成证书（包含公钥和私钥，并保存在MY存储区）   
        /// </summary>   
        /// <param name="subjectName">证书名</param>   
        /// <param name="makecertPath">makecert全路径</param>   
        /// <returns></returns>   
        public static bool CreateCertWithPrivateKey(string subjectName, string makecertPath)
        {
            //string markecer = "C:\\Program Files\\Microsoft Visual Studio 8\\SDK\\v2.0\\Bin\\makecert.exe";
          //  string markecer = "C:\\Program Files\\Microsoft Visual Studio 8\\SDK\\v2.0\\Bin\\sn.exe";
          
            subjectName = "CN=" + subjectName;
            string param = " -pe -ss my -n \"" + subjectName + "\" ";
            try
            {
                Process p = Process.Start(makecertPath, param);
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                Log.Write("error",e.ToString());  
                return false;
            }
            return true;
        }
     
        #endregion

        #region 文件导入导出
        /// <summary>   
        /// 从WINDOWS证书存储区的个人MY区找到主题为subjectName的证书，   
        /// 并导出为pfx文件，同时为其指定一个密码   
        /// 并将证书从个人区删除(如果isDelFromstor为true)   
        /// </summary>   
        /// <param name="subjectName">证书主题，不包含CN=</param>   
        /// <param name="pfxFileName">pfx文件名</param>   
        /// <param name="password">pfx文件密码</param>   
        /// <param name="isDelFromStore">是否从存储区删除</param>   
        /// <returns></returns>   
        public static bool ExportToPfxFile(string subjectName, string pfxFileName, string password, bool isDelFromStore)
        {
            subjectName = "CN=" + subjectName;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
            foreach (X509Certificate2 x509 in storecollection)
            {
                if (x509.Subject == subjectName)
                {
                    Debug.Print(string.Format("certificate name: {0}", x509.Subject));

                    byte[] pfxByte = x509.Export(X509ContentType.Pfx, password);
                    using (FileStream fileStream = new FileStream(pfxFileName, FileMode.Create))
                    {
                        // Write the data to the file, byte by byte.   
                        for (int i = 0; i < pfxByte.Length; i++)
                            fileStream.WriteByte(pfxByte[i]);
                        // Set the stream position to the beginning of the file.   
                        fileStream.Seek(0, SeekOrigin.Begin);
                        // Read and verify the data.   
                        for (int i = 0; i < fileStream.Length; i++)
                        {
                            if (pfxByte[i] != fileStream.ReadByte())
                            {
                                // LogRecord.putErrorLog("Export pfx error while verify the pfx file!", "ExportToPfxFile");   
                                fileStream.Close();
                                return false;
                            }
                        }
                        fileStream.Close();
                    }
                    if (isDelFromStore == true)
                        store.Remove(x509);
                }
            }
            store.Close();
            store = null;
            storecollection = null;
            return true;
        }
        /// <summary>   
        /// 从WINDOWS证书存储区的个人MY区找到主题为subjectName的证书，   
        /// 并导出为CER文件（即，只含公钥的）   
        /// </summary>   
        /// <param name="subjectName">subjectName的证书</param>   
        /// <param name="cerFileName">文件名称</param>   
        /// <returns></returns>   
        public static bool ExportToCerFile(string subjectName, string cerFileName)
        {
            subjectName = "CN=" + subjectName;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
            foreach (X509Certificate2 x509 in storecollection)
            {
                if (x509.Subject == subjectName)
                {
                    Debug.Print(string.Format("certificate name: {0}", x509.Subject));
                    //byte[] pfxByte = x509.Export(X509ContentType.Pfx, password);   
                    byte[] cerByte = x509.Export(X509ContentType.Cert);
                    using (FileStream fileStream = new FileStream(cerFileName, FileMode.Create))
                    {
                        // Write the data to the file, byte by byte.   
                        for (int i = 0; i < cerByte.Length; i++)
                            fileStream.WriteByte(cerByte[i]);
                        // Set the stream position to the beginning of the file.   
                        fileStream.Seek(0, SeekOrigin.Begin);
                        // Read and verify the data.   
                        for (int i = 0; i < fileStream.Length; i++)
                        {
                            if (cerByte[i] != fileStream.ReadByte())
                            {
                                // LogRecord.putErrorLog("Export CER error while verify the CERT file!", "ExportToCERFile");   
                                fileStream.Close();
                                return false;
                            }
                        }
                        fileStream.Close();
                    }
                }
            }
            store.Close();
            store = null;
            storecollection = null;
            return true;
        }
        #endregion

        #region 从证书中获取信息
        /// <summary>   
        /// 根据私钥证书得到证书实体，得到实体后可以根据其公钥和私钥进行加解密   
        /// 加解密函数使用DEncrypt的RSACryption类   
        /// </summary>   
        /// <param name="pfxFileName">文件名称</param>   
        /// <param name="password">密码</param>   
        /// <returns></returns>   
        public static X509Certificate2 GetCertificateFromPfxFile(string pfxFileName, string password,ref string error)
        {
            try
            {
                return new X509Certificate2(pfxFileName, password, X509KeyStorageFlags.Exportable);
            }
            catch (Exception e)
            {
                // LogRecord.putErrorLog("get certificate from pfx" + pfxFileName + " error:" + e.ToString(),   
                //    "GetCertificateFromPfxFile");   
                error = e.ToString();
                return null;
            }
        }
        /// <summary>   
        /// 根据私钥证书得到证书实体，得到实体后可以根据其公钥和私钥进行加解密   
        /// 加解密函数使用DEncrypt的RSACryption类   
        /// </summary>   
        /// <param name="pfxFileName">文件名称</param>   
        /// <param name="password">密码</param>   
        /// <returns></returns>   
        public static X509Certificate2 GetCertificateFromPfxFile(string pfxFileName, string password)
        {
            try
            {
                return new X509Certificate2(pfxFileName, password, X509KeyStorageFlags.Exportable);
            }
            catch 
            {
                // LogRecord.putErrorLog("get certificate from pfx" + pfxFileName + " error:" + e.ToString(),   
                //    "GetCertificateFromPfxFile");   

                return null;
            }
        }
        /// <summary>   
        /// 到存储区获取证书   
        /// </summary>   
        /// <param name="subjectName">证书名称</param>   
        /// <returns></returns>   
        public static X509Certificate2 GetCertificateFromStore(string subjectName)
        {
            subjectName = "CN=" + subjectName;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
            foreach (X509Certificate2 x509 in storecollection)
            {
                if (x509.Subject == subjectName)
                {
                    return x509;
                }
            }
            store.Close();
            store = null;
            storecollection = null;
            return null;
        }
        /// <summary>   
        /// 根据公钥证书，返回证书实体   
        /// </summary>   
        /// <param name="cerPath">证书路径</param>   
        public static X509Certificate2 GetCertFromCerFile(string cerPath,ref string error)
        {
            try
            {
                return new X509Certificate2(cerPath);
            }
            catch (Exception e)
            {
                //LogRecord.putErrorLog(e.ToString(), "DataCertificate.LoadStudentPublicKey");   
                error = e.ToString();
                return null;
            }
        }
        /// <summary>   
        /// 根据公钥证书，返回证书实体   
        /// </summary>   
        /// <param name="cerPath">证书路径</param>   
        public static X509Certificate2 GetCertFromCerFile(string cerPath)
        {
            try
            {
                return new X509Certificate2(cerPath);
            }
            catch
            {
                //LogRecord.putErrorLog(e.ToString(), "DataCertificate.LoadStudentPublicKey");   
                return null;
            }
        }
        #endregion
    }


}

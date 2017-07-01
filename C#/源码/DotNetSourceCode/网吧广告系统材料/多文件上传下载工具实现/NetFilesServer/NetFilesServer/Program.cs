using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetFilesServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            HFSoft.Data.MappingContainer.ConfigContainer.Config.ConnectionString
                = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath+"\\NetFileData.mdb;User ID=;Password=;";
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

 


namespace TestIcon
{

    public partial class Form1 : Form
    {
        public static uint SHGFI_ICON = 0x100;
        public static uint SHGFI_DISPLAYNAME = 0x200;
        public static uint SHGFI_TYPENAME = 0x400;
        public static uint SHGFI_ATTRIBUTES = 0x800;
        public static uint SHGFI_ICONLOCATION = 0x1000;
        public static uint SHGFI_EXETYPE = 0x2000;
        public static uint SHGFI_SYSICONINDEX = 0x4000;
        public static uint SHGFI_LINKOVERLAY = 0x8000;
        public static uint SHGFI_SELECTED = 0x10000;
        public static uint SHGFI_LARGEICON = 0x0;
        public static uint SHGFI_SMALLICON = 0x1;
        public static uint SHGFI_OPENICON = 0x2;
        public static uint SHGFI_SHELLICONSIZE = 0x4;
        public static uint SHGFI_PIDL = 0x8;
        public static uint SHGFI_USEFILEATTRIBUTES = 0x10;

        public static uint FILE_ATTRIBUTE_NORMAL = 0x80;
        public static uint LVM_FIRST = 0x1000;
        public static uint LVM_SETIMAGELIST = LVM_FIRST + 3;
        public static uint LVSIL_NORMAL = 0;
        public static uint LVSIL_SMALL = 1;

        [DllImport("Shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath,
            uint dwFileAttributes, ref SHFILEINFO psfi,
            int cbfileInfo, uint uFlags);

        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;
            public string szDisplayName;
            public string szTypeName;
        }

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd,
            uint Msg, IntPtr wParam, IntPtr lParam);

        public void ListViewSysImages(ListView AListView)
        {
            SHFILEINFO vFileInfo = new SHFILEINFO();
            IntPtr vImageList = SHGetFileInfo("", 0, ref vFileInfo,
                Marshal.SizeOf(vFileInfo), SHGFI_SHELLICONSIZE |
                SHGFI_SYSICONINDEX | SHGFI_LARGEICON);

            SendMessage(AListView.Handle, LVM_SETIMAGELIST, (IntPtr)LVSIL_NORMAL,
                vImageList);
        }

        public int FileIconIndex(string AFileName)
        {
            SHFILEINFO vFileInfo = new SHFILEINFO();
            SHGetFileInfo(AFileName, 0, ref vFileInfo,
                Marshal.SizeOf(vFileInfo), SHGFI_SYSICONINDEX);
            return vFileInfo.iIcon;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewSysImages(listView1);
            listView1.Items.Add("test.txt",FileIconIndex("c:\text.txt"));
        }
    }
}
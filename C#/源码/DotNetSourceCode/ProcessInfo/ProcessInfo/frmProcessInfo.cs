using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; 

namespace ProcessInfo
{
    public partial class frmProcessInfo : Form
    {
        public frmProcessInfo()
        {
            InitializeComponent();
        }

        private void btnGetProcessList_Click(object sender, EventArgs e)
        {
            string str = "";
            Process[] processes;
            //Get the list of current active processes.
            processes = System.Diagnostics.Process.GetProcesses();
            //Grab some basic information for each process.
            Process process;
            for (int i = 0; i < processes.Length - 1; i++)
            {
                process = processes[i];
                str = str + Convert.ToString(process.Id) + " : " +
                process.ProcessName + "\r\n";
            }
            //Display the process information to the user
            System.Windows.Forms.MessageBox.Show(str);
            //Default the TextBox value to the first process ID - for the GetByID button
            txtProcessID.Text = processes[0].Id.ToString(); 

        }

        private void btnGetProcessByID_Click(object sender, EventArgs e)
        {
            try
            {

                string s = "";
                System.Int32 processid;
                Process process;
                //Retrieve the additional information about a specific process
                processid = Int32.Parse(txtProcessID.Text);
                process = System.Diagnostics.Process.GetProcessById(processid);
                s = s + "该进程的总体优先级类别:" + Convert.ToString(process.PriorityClass) + " \r\n";
                s = s + "由该进程打开的句柄数:" + process.HandleCount + "\r\n";
                s = s + "该进程的主窗口标题:" + process.MainWindowTitle + "\r\n";
                s = s + " 该进程允许的最小工作集大小:" + process.MinWorkingSet.ToString() + " \r\n";
                s = s + "该进程允许的最大工作集大小:" + process.MaxWorkingSet.ToString() + " \r\n";
                s = s + "该进程的分页内存大小:" + process.PagedMemorySize + "\r\n";
                s = s + "该进程的峰值分页内存大小:" + process.PeakPagedMemorySize + "\r\n";
                System.Windows.Forms.MessageBox.Show(s);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("不合法的进程ID！");
            } 

        }
    }
}
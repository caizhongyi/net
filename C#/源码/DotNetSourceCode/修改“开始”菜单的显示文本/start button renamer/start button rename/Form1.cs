using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace start_button_rename
{
    public partial class Form1 : Form
    {
        string windir = "";
        string appdata = "";

        public Form1()
        {
            InitializeComponent();
        }

        private string getpath()
        {
            string path = "";
            string explorerdir = "";

            Process[] proc=Process.GetProcessesByName("explorer");

            path = proc[0].MainModule.FileName;
            string folderdir = skinRadioButton2.Checked ? appdata : windir;
            path=Path.Combine(folderdir, path);
            string folderpath = Path.GetDirectoryName(path);

            if (skinRadioButton1.Checked)
            {
                explorerdir = folderpath == windir + "\\explorers\\1" ? windir + "\\explorers\\2" : windir + "\\explorers\\1";
            }
            else
            {
                explorerdir = folderpath == appdata + "\\explorers\\1" ? appdata + "\\explorers\\2" : appdata + "\\explorers\\1";
            }
            
            Directory.CreateDirectory(explorerdir);

            return explorerdir;
        }

        private bool rename(string newname)
        {
            if (!File.Exists("begin.exe") || !File.Exists("end.exe"))
            {
                MessageBox.Show("Source files not found", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string explorerpath = "";
            windir = Environment.GetEnvironmentVariable("WINDIR");
            appdata = Environment.GetEnvironmentVariable("AppData") + "\\start button rename";

            try
            {
                explorerpath = getpath();
                if (File.Exists(explorerpath + "\\explorer.exe"))
                {
                    File.Delete(explorerpath + "\\explorer.exe");
                }
                File.Copy("begin.exe", explorerpath + "\\explorer.exe");

                FileStream outstr = new FileStream(explorerpath + "\\explorer.exe", FileMode.Append, FileAccess.Write, FileShare.None);
                BinaryWriter outbr = new BinaryWriter(outstr);

                //Insert new length
                outbr.Write((short)newname.Length);  // comment for 5 length text

                //insert new text
                byte[] input = Encoding.Unicode.GetBytes(newname);
                outbr.Write(input);

                //insert some rubbish that won't be displayed
                for (int j = 0; j < 20 - newname.Length; j++)
                {
                    outbr.Write((short)j);          // comment for 5 length text
                }

                FileStream infs = new FileStream("end.exe", FileMode.Open, FileAccess.Read, FileShare.None);
                BinaryReader inbr = new BinaryReader(infs);

                for (int i = 0; i < 1620; i++)      //1622 for 5 length text
                {
                    input = inbr.ReadBytes(16);
                    outbr.Write(input);
                }

                input = inbr.ReadBytes(2);
                outbr.Write(input);

                inbr.Close();
                infs.Close();
                outbr.Close();
                outstr.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Error during accessing files", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Unkown error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Shell", "");
                if (skinRadioButton1.Checked)
                {
                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Shell", explorerpath + "\\explorer.exe");
                }
                else
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Shell", explorerpath + "\\explorer.exe");
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Error accessing registry", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("Error accessing registry. Make sure you have enough rigths.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }
        
        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.status.Visible = false;

            if (this.textBox1.Text==string.Empty)
            {
                MessageBox.Show("You must enter new text", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool success = rename(this.textBox1.Text);
            if (success)
            {
                if (MessageBox.Show("   Renamed! Do you wish to restart windows?" + Environment.NewLine
                    + "Changes will take effect after restarting windows", "Renamed!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    == DialogResult.OK)
                {
                    try
                    {
                        Org.Mentalis.Utilities.WindowsController.ExitWindows(Org.Mentalis.Utilities.RestartOptions.Reboot, true);
                    }
                    catch (Org.Mentalis.Utilities.PrivilegeException)
                    {
                        MessageBox.Show("Error restarting windows. Make sure you have enough rigths.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.status.Visible = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            About.AboutBox box = new About.AboutBox("Start Button Renamer");
            box.BackColor = Color.Gold;
            box.programname = box.Text;
            box.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
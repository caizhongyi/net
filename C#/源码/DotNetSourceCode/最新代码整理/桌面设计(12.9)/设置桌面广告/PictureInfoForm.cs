using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SetWallpaper;
using WindowsApplication1.SendInfo;
using System.Collections;

namespace WbSystem
{
    public partial class PictureInfoForm : Form
    {
        //转换图片
        IChangeAdPictureService icaps = Factory.GetNewAdPicture();
        //保存图片
        private static ArrayList alAll;
        private static ArrayList alAllTitle;
        private static ArrayList alAllContent;
        private static ArrayList alAllCompanyURL;
        //图片位置
        private static int adA;
        private static int adB;
        private static int adC;
        private static int adD;
        private static int adE;
        //图片上下移动
        private static int pagecountDown;
        private static int pagecountUp;
        //保存图片URL
        private static string ImageURL=null;

        public PictureInfoForm()
        {
            InitializeComponent();
            ReadPicture();
        }

        private void PictureInfoForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - BackForm.Formwidth-this.Width, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            
            this.BackColor = Color.FromArgb(255,200,0);
            this.TransparencyKey = BackColor;
            //保存预览图片
            KeepPicture();
            try
            {
                //存储图片路径
                PbImage.Image = icaps.ChanageAdSize(AdForm.ImageUrl, 150, 50);
                lklbTitle.Text = AdForm.ImageTitle;
                lbContent.Text = AdForm.ImageContent;
                ImageURL = AdForm.CompanyURL;
            }
            catch
            {
                Bitmap MyBitmap = new Bitmap(Application.StartupPath + @"\Adpictures(YunZhou)\右侧广告5\YunZhou.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 150, 50);
                PbImage.Image = (Image)NewBitmap;
            }  
        }
        //显示图片
        void KeepPicture()
        {
            pagecountDown = 5;
            pagecountUp = alAll.Count;
            adA = 0;
            adB = 1;
            adC = 2;
            adD = 3;
            adE = 4;
            btnUp.Text = "第一页";
            btnUp.Enabled = false;

            ChangeImage(0,pbA);
            ChangeImage(1, pbB);
            ChangeImage(2, pbC);
            ChangeImage(3, pbD);
            ChangeImage(4, pbE);

        }
        void ReadPicture()
        {
            alAll = new ArrayList();
            alAllContent = new ArrayList();
            alAllTitle = new ArrayList();
            alAllCompanyURL = new ArrayList();

            DataSet ds = new DataSet();

            ds.ReadXml("AdvInfo.xml");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string type = ds.Tables[0].Rows[i][6].ToString();
                if (type == "2" || type == "3" || type == "5")
                    continue;
                else
                {
                    alAll.Add(ds.Tables[0].Rows[i][2].ToString());
                    alAllTitle.Add(ds.Tables[0].Rows[i][1].ToString());
                    alAllContent.Add(ds.Tables[0].Rows[i][4].ToString());
                    alAllCompanyURL.Add(ds.Tables[0].Rows[i][7].ToString());
                  
                }
            }
        }
        private void btnClosed_Click(object sender, EventArgs e)
        {
            WbSystem.AdForm.IsForm = true;
            this.Close();
        }
        #region ChangeImage 图片变化
        /// <summary>
        /// 图片变化
        /// </summary>
        /// <param name="i">所要显示的图片的所在位置</param>
        /// <param name="PictureFile">文件夹下所有图片地址储存的数组</param>
        /// <param name="pb">所要显示的PictureBox的ID</param>
        private void ChangeImage(int i, PictureBox pb)
        {
            try
            {
                string PicturePath = Directory.GetCurrentDirectory() + @"\Adpictures\" + alAll[i].ToString().Substring(2);
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pb.Image = (Image)NewBitmap;
            }
            catch
            {
                return;
            }
        }
        #endregion
        //向下读取图片
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (pagecountDown < alAll.Count)
            {
                if (pagecountDown < alAll.Count)
                {
                    ChangeImage(pagecountDown, pbA);
                    pagecountUp = pagecountDown-1;
                    adA = pagecountDown;

                    pagecountDown = pagecountDown + 1;

                    btnUp.Enabled = true;
                    btnUp.Text = "上一页";
                }
                else
                {
                    pbA.Image = null;
                    pbA.Enabled = false;
                    pagecountDown = pagecountDown + 1;
                }
                if (pagecountDown < alAll.Count)
                {
                    ChangeImage(pagecountDown, pbB);
                    adB = pagecountDown;

                    pagecountDown = pagecountDown + 1;
                }
                else
                {
                    pbB.Image = null;
                    pbB.Enabled = false;
                    pagecountDown = pagecountDown + 1;
                }
                if (pagecountDown < alAll.Count)
                {
                    ChangeImage(pagecountDown, pbC);
                    adC = pagecountDown;

                    pagecountDown = pagecountDown + 1;
                }
                else
                {
                    pbC.Image = null;
                    pbC.Enabled = false;
                    pagecountDown = pagecountDown + 1;
                }
                if (pagecountDown < alAll.Count)
                {
                    ChangeImage(pagecountDown, pbD);
                    adD = pagecountDown;

                    pagecountDown = pagecountDown + 1;
                }
                else
                {
                    pbD.Image = null;
                    pbD.Enabled = false;
                    pagecountDown = pagecountDown + 1;
                }
                if (pagecountDown < alAll.Count)
                {
                    ChangeImage(pagecountDown, pbE);
                    adE = pagecountDown;

                    pagecountDown = pagecountDown + 1;
                }
                else
                {
                    pbE.Image = null;
                    pbE.Enabled = false;
                    pagecountDown = pagecountDown + 1;
                }
            }
            else
            {
                btnDown.Enabled = false;
                btnDown.Text = "最后一页";
            }
        }
        //向上读取图片
        private void btnUp_Click(object sender, EventArgs e)
        {
            pbA.Enabled = true;
            pbB.Enabled = true;
            pbC.Enabled = true;
            pbD.Enabled = true;
            pbE.Enabled = true;
            if (pagecountUp > 0)
            {
                ChangeImage(pagecountUp, pbE);
                adE = pagecountUp;

                pagecountDown = pagecountUp+1;
                pagecountUp = pagecountUp - 1;
                ChangeImage(pagecountUp, pbD);
                adD = pagecountUp;

                pagecountUp = pagecountUp - 1;
                ChangeImage(pagecountUp, pbC);
                adC = pagecountUp;

                pagecountUp = pagecountUp - 1;
                ChangeImage(pagecountUp, pbB);
                adB = pagecountUp;

                pagecountUp = pagecountUp - 1;
                ChangeImage(pagecountUp, pbA);
                adA = pagecountUp;

                pagecountUp = pagecountUp - 1;

                btnDown.Enabled = true;
                btnDown.Text = "下一页";
            }
            else
            {
                btnUp.Enabled = false;
                btnUp.Text = "第一页";
            }
        }

        private void pbA_Click(object sender, EventArgs e)
        {
            lklbTitle.Text=alAllTitle[adA].ToString();
            lbContent.Text = alAllContent[adA].ToString();
            ImageURL = alAllCompanyURL[adA].ToString();
            PbImage.Image = icaps.ChanageAdSize(Directory.GetCurrentDirectory() + @"\Adpictures\" + alAll[adA].ToString().Substring(2), 150, 50);
        }

        private void pbB_Click(object sender, EventArgs e)
        {
            lklbTitle.Text = alAllTitle[adB].ToString();
            lbContent.Text = alAllContent[adB].ToString();
            ImageURL = alAllCompanyURL[adB].ToString();
            PbImage.Image = icaps.ChanageAdSize(Directory.GetCurrentDirectory() + @"\Adpictures\" + alAll[adB].ToString().Substring(2), 150, 50);
        }

        private void pbC_Click(object sender, EventArgs e)
        {
            lklbTitle.Text = alAllTitle[adC].ToString();
            lbContent.Text = alAllContent[adC].ToString();
            ImageURL = alAllCompanyURL[adC].ToString();
            PbImage.Image = icaps.ChanageAdSize(Directory.GetCurrentDirectory() + @"\Adpictures\" + alAll[adC].ToString().Substring(2), 150, 50);
        }

        private void pbD_Click(object sender, EventArgs e)
        {
            lklbTitle.Text = alAllTitle[adD].ToString();
            lbContent.Text = alAllContent[adD].ToString();
            ImageURL = alAllCompanyURL[adD].ToString();
            PbImage.Image = icaps.ChanageAdSize(Directory.GetCurrentDirectory() + @"\Adpictures\" + alAll[adD].ToString().Substring(2), 150, 50);
        }

        private void pbE_Click(object sender, EventArgs e)
        {
            lklbTitle.Text = alAllTitle[adE].ToString();
            lbContent.Text = alAllContent[adE].ToString();
            ImageURL = alAllCompanyURL[adE].ToString();
            PbImage.Image = icaps.ChanageAdSize(Directory.GetCurrentDirectory() + @"\Adpictures\" + alAll[adE].ToString().Substring(2), 150, 50);
        }

        private void lklbTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("IEXPLORE.EXE", ImageURL);
            }
            catch(Exception ex)
            {
               MessageBox.Show("该网址不存在");
               return;
            }
        }
    }
}
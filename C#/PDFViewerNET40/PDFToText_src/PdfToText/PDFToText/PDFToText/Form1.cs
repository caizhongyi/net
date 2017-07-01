using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfToText;

namespace PDFToText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                PDFParser pdfParser = new PDFParser();
                pdfParser.ExtractText(openFileDialog1.FileName, Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ".txt");
            }
        }
    }
}

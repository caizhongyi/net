using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace czy.MyClass
{
 
    public class Caculator
    {
        private long remain;//计算结果
        private string previousOper;//符号
        private bool numBegin;//是否开始按下数字
        int judge = 0;//
       
        #region 计算器代码
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitMembers(TextBox textBox1)
        {
            remain = 0;
            previousOper = "=";
            numBegin = true;
            textBox1.Text = "0";
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.White;
        }
        /// <summary>
        /// 打开关于主题---调用windows xp中计算器的帮助
        /// </summary>
        /// <param name="form">form</param>
        private void 帮助主题ToolStripMenuItem_Click(Form form)
        {
            Help.ShowHelp(form, "C:\\WINDOWS\\Help\\calc.chm");
        }
        /// <summary>
        /// 调用系统计算器
        /// </summary>
        /// <param name="form">form</param>
        private void openWinComputorToolStripMenuItem_Click(Form form)
        {
            Help.ShowHelp(form, "C:\\WINDOWS\\system32\\calc.exe");
        }
        /// <summary>
        /// 数字键
        /// </summary>
        /// <param name="textBox1">TextBox</param>
        /// <param name="sender">sender</param>
        private void Num_Click(TextBox textBox1,object sender)
        {
            if (textBox1.Text == "Error")
            {
                return;
            }
            Button numButton = (Button)sender;
            if (numBegin)
            {
                textBox1.Text = numButton.Text;
                numBegin = false;
            }
            else if (Convert.ToInt64(textBox1.Text) == 0)
            {
                textBox1.Text = numButton.Text;
            }
            else
            {
                textBox1.Text += numButton.Text;
            }

        }
        /// <summary>
        /// 符号键
        /// </summary>
        /// <param name="textBox1">textBox1</param>
        /// <param name="sender">sender</param>
        private void oper_Click(TextBox textBox1, object sender)
        {
            if (textBox1.Text == "Error")
            {
                return;
            }
            long currNum = long.Parse(textBox1.Text);
            string currOper = ((Button)sender).Text;
            if (currOper == "CE")
            {
                InitMembers(textBox1);
                return;
            }
            switch (previousOper)
            {
                case "+":
                    remain += currNum;
                    break;
                case "-":
                    remain -= currNum;
                    break;
                case "*":
                    remain *= currNum;
                    break;
                case "/":
                    if (currNum == 0)
                    {
                        judge = 1;
                    }
                    else
                    {
                        remain /= currNum;
                    }
                    break;
                case "=":
                    remain = currNum;
                    break;

            }
            //输出结果
            if (judge != 1)
            {
                textBox1.Text = remain.ToString();
            }
            else
            {
                textBox1.Text = "Error";
                judge = 0;
                return;
            }
            //保存当下的运算符号
            previousOper = currOper;
            //准备接收下一个数
            numBegin = true;
        }
        #endregion
    }
}

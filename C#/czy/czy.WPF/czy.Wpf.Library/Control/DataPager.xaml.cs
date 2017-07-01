using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace czy.Wpf.Library.Control
{
   
    /// <summary>
    /// UCPaging.xaml 的交互逻辑
    /// </summary>
    public partial class DataPager : UserControl
    {
        #region Pirvate Member
        //System.Windows.Forms.NumericUpDown nudPageIndex=new System.Windows.Forms.NumericUpDown ();
        private Pager pager = new Pager();
        /// <summary>
        /// 申明委托
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public delegate void EventPagingHandler(EventPagingArg e);
        public event EventPagingHandler EventPaging;
        NumberDownUp nudPageIndex = new NumberDownUp();

        
        private bool _showLabel = true;

        public bool ShowLabel
        {
            get { return _showLabel; }
            set { _showLabel = value; }
        }


        public Pager Pager
        {
            get { return pager; }
            set { pager = value;  }
        }
    
        

        #endregion
        public DataPager(Pager pager)
        {
            this.pager = pager;
            this.pager.ChangePropty += new EventHandler(pager_ChangePropty);
            InitializeComponent();
            nudPageIndex.Value= 1;
            NumberDU.Child = nudPageIndex;
            Bind();
        }

        void pager_ChangePropty(object sender, EventArgs e)
        {
            pager.PageIndex = pager.PageIndex > pager.PageCount ? pager.PageCount : pager.PageIndex;
            Bind();
        }
        public DataPager()
        {
            nudPageIndex.Value = 1;
            InitializeComponent();
            NumberDU.Child = nudPageIndex;
            Bind();
        } 
        /// <summary>
        /// 得到数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fldName"></param>
        /// <param name="fldSort"></param>
        /// <param name="strCondition"></param>
        /// <returns></returns>
        public void Bind()
        {
            if (this.EventPaging != null)
                this.EventPaging(new EventPagingArg(pager.PageIndex));
   
            if (pager.RecorderCount == 0)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }
            else
                this.Visibility = Visibility.Visible;

         
            //--控制
            if (_showLabel)
            {

                this.txbInfo.Text = "第" + (pager.PageSize * (pager.PageIndex - 1)+1) + "-" + pager.PageSize * pager.PageIndex + "条  共" + pager.RecorderCount + "条 | 第" + pager.PageIndex + "页  共" + pager.PageCount + "页";
                this.nudPageIndex.MinValue = 1;
                this.nudPageIndex.MaxValue = pager.PageCount;
                this.nudPageIndex.Value = pager.PageIndex;
                this.txbTotalPageCount.Text = " / " + pager.PageCount;
            }

            if (pager.PageCount > 1 && pager.PageCount > pager.PageIndex)
            {
                this.iNext.Source = new BitmapImage(new Uri("images/next.gif", UriKind.Relative));
                this.iLast.Source = new BitmapImage(new Uri("images/last.gif", UriKind.Relative));
                this.iNext.IsEnabled = true;
                this.iLast.IsEnabled = true;
            }
            else
            {
                this.iNext.Source = new BitmapImage(new Uri("images/next1.gif", UriKind.Relative));
                this.iLast.Source = new BitmapImage(new Uri("images/last1.gif", UriKind.Relative));
                this.iNext.IsEnabled = false;
                this.iLast.IsEnabled = false;
            }

            if (pager.PageIndex > 1 && pager.PageIndex <= pager.PageCount)
            {
                this.iFirst.IsEnabled = true;
                this.iPrev.IsEnabled = true;
                this.iFirst.Source = new BitmapImage(new Uri("images/first.gif", UriKind.Relative));
                this.iPrev.Source = new BitmapImage(new Uri("images/previous.gif", UriKind.Relative));
            }
            else
            {
                this.iFirst.Source = new BitmapImage(new Uri("images/first1.gif", UriKind.Relative));
                this.iPrev.Source = new BitmapImage(new Uri("images/previous1.gif", UriKind.Relative));
                this.iFirst.IsEnabled = false;
                this.iPrev.IsEnabled = false;
            }
        }

        ///// <summary>
        ///// 得到数据
        ///// </summary>
        ///// <param name="tableName">表名</param>
        ///// <param name="fldName">要查询的字段（所有为*）</param>
        ///// <param name="fldSort">排序</param>
        ///// <param name="strCondition">Where条件</param>
        ///// <returns>返回DataTable，如要绑定控件，可自己转换成IList</returns>
        //public DataTable GetData(string tableName, string fldName, string fldSort,
        //      string strCondition)
        //{
        //    int pageCount = 0;
        //    int recordCount = 0;
        //    dt = PagingManage.GetDataSet(tableName, fldName, pager.PageSize, pager.PageIndex, fldSort,
        //        strCondition, out pageCount, out recordCount);
        //    pager.PageCount = pageCount;
        //    pager.RecorderCount = recordCount;

        //    return dt;
        //}

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void iFirst_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.pager.PageIndex = 1;
            this.Bind();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void iPrev_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.pager.PageIndex--;
            this.Bind();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void iNext_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.pager.PageIndex++;
            this.Bind();
        }

        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void iLast_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.pager.PageIndex = pager.PageCount;
            this.Bind();
        }

        /// <summary>
        /// 确定导航到指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudPageIndex_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Return)
            {
                this.pager.PageIndex = int.Parse(this.nudPageIndex.Value.ToString());
                this.Bind();
            }
        }

        /// <summary>
        /// 确定导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbGO_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.pager.PageIndex = int.Parse(this.nudPageIndex.Value.ToString());
            this.Bind();
        }
    }

    /// <summary>
    /// 自定义事件数据基类
    /// </summary>
    public class EventPagingArg : EventArgs
    {
        public int Index
        {
            get;
            set;
        }
        public EventPagingArg() { }
        public EventPagingArg(int _index) { this.Index = _index; }
    }
}

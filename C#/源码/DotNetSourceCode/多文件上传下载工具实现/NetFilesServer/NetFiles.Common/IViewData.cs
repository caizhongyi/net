using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Common
{
    /// <summary>
    /// 基于ListView控件的数据显示描述接口
    /// </summary>
    /// <typeparam name="T">相关数据实体类型</typeparam>
    public interface IViewData<T>
    {
        /// <summary>
        /// 数据绑定过程
        /// </summary>
        /// <param name="items">数据实体对象集</param>
        void DataBind(IList<T> items);
        /// <summary>
        /// 根据实体对象构造相关显示项
        /// </summary>
        /// <param name="item">数据实体对象</param>
        /// <returns>ListViewItem</returns>
        System.Windows.Forms.ListViewItem CreateItem(T item);
        /// <summary>
        /// 创建相关列信息
        /// </summary>
        void CreateColumns();
        /// <summary>
        /// 获取当前选择的数据项
        /// </summary>
        T SelectItem
        {
            get;
        }
        /// <summary>
        /// 数据绑定事件,每一项数据绑定会引发这个事件
        /// </summary>
        event EventViewDataItem<T> ViewDataBound;
        /// <summary>
        /// 获取相关的ListView控件
        /// </summary>
        System.Windows.Forms.ListView ListView
        {
            get;
        }
        event EventViewDataItem<T> SelectChange;
    }

    /// <summary>
    /// 数据绑定委托描述
    /// </summary>
    /// <typeparam name="T">类型实体类型</typeparam>
    /// <param name="item">实体对象</param>
    /// <param name="viewitem">列表项对象</param>
    public delegate void EventViewDataItem<T>(T item,System.Windows.Forms.ListViewItem viewitem);

    

    
    /// <summary>
    /// 数据显示适配器对象
    /// 抽象基本通过功能,简化派生类的实现
    /// </summary>
    /// <typeparam name="T">相关数据实体类型</typeparam>
    public abstract class ViewDataAdapter<T>:IViewData<T>
    {
        public ViewDataAdapter(System.Windows.Forms.ListView view)
        {
            mListView = view;
            CreateColumns();
            ListView.FullRowSelect = true;
            ListView.SelectedIndexChanged += new EventHandler(viewChangeIndex);
            ListView.MouseUp += new System.Windows.Forms.MouseEventHandler(viewMouseUp);
        }
        #region IViewData<T> 成员
        public void DataBind(IList<T> items)
        {
            System.Windows.Forms.ListViewItem vi;
            ListView.Items.Clear();
            foreach (T item in items)
            {
                vi = CreateItem(item);
                if (ViewDataBound != null)
                    ViewDataBound(item, vi);
                ListView.Items.Add(vi);
            }
        }
        public System.Windows.Forms.ListViewItem CreateItem(T item)
        {
            return  OnCreateItem(item);
                  
        }
        protected abstract System.Windows.Forms.ListViewItem OnCreateItem(T item);
        public void CreateColumns()
        {
            if (ListView.Columns.Count > 0)
                return;
            OnCreateColumns();
        }
        protected abstract void OnCreateColumns();
        public T SelectItem
        {
            get
            {
                if (ListView.SelectedItems.Count == 0)
                    return default(T);
                return (T)ListView.SelectedItems[0].Tag;
            }
        }
        public event EventViewDataItem<T> ViewDataBound;
        public event EventViewDataItem<T> SelectChange;
        private System.Windows.Forms.ListView mListView;
        public System.Windows.Forms.ListView ListView
        {
            get { return mListView; }
        }
        private void viewChangeIndex(object source, EventArgs e)
        {
            if (SelectChange != null)
            {
                if (ListView.SelectedItems.Count == 0)
                    SelectChange(default(T),null);
                else
                    SelectChange(SelectItem, ListView.SelectedItems[0]);
            }
        }
        private void viewMouseUp(object source, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.ListViewItem item = ListView.GetItemAt(e.X, e.Y);
            if (SelectChange != null)
            {
                if (item != null)
                {
                    if (ListView.SelectedItems.Count == 0)
                        SelectChange(default(T), null);
                    else
                        SelectChange(SelectItem, ListView.SelectedItems[0]);
                }
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Common
{
    /// <summary>
    /// ����ListView�ؼ���������ʾ�����ӿ�
    /// </summary>
    /// <typeparam name="T">�������ʵ������</typeparam>
    public interface IViewData<T>
    {
        /// <summary>
        /// ���ݰ󶨹���
        /// </summary>
        /// <param name="items">����ʵ�����</param>
        void DataBind(IList<T> items);
        /// <summary>
        /// ����ʵ������������ʾ��
        /// </summary>
        /// <param name="item">����ʵ�����</param>
        /// <returns>ListViewItem</returns>
        System.Windows.Forms.ListViewItem CreateItem(T item);
        /// <summary>
        /// �����������Ϣ
        /// </summary>
        void CreateColumns();
        /// <summary>
        /// ��ȡ��ǰѡ���������
        /// </summary>
        T SelectItem
        {
            get;
        }
        /// <summary>
        /// ���ݰ��¼�,ÿһ�����ݰ󶨻���������¼�
        /// </summary>
        event EventViewDataItem<T> ViewDataBound;
        /// <summary>
        /// ��ȡ��ص�ListView�ؼ�
        /// </summary>
        System.Windows.Forms.ListView ListView
        {
            get;
        }
        event EventViewDataItem<T> SelectChange;
    }

    /// <summary>
    /// ���ݰ�ί������
    /// </summary>
    /// <typeparam name="T">����ʵ������</typeparam>
    /// <param name="item">ʵ�����</param>
    /// <param name="viewitem">�б������</param>
    public delegate void EventViewDataItem<T>(T item,System.Windows.Forms.ListViewItem viewitem);

    

    
    /// <summary>
    /// ������ʾ����������
    /// �������ͨ������,���������ʵ��
    /// </summary>
    /// <typeparam name="T">�������ʵ������</typeparam>
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
        #region IViewData<T> ��Ա
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

using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Common.DataViews
{
    public class RecordView:ViewDataAdapter<NetFiles.Command.Record>
    {
        public RecordView(System.Windows.Forms.ListView view) : base(view) { }
        protected override void OnCreateColumns()
        {
            System.Windows.Forms.ColumnHeader col;
            col = new System.Windows.Forms.ColumnHeader();
            col.Text = "Ãû³Æ";
            col.Width = 300;
            ListView.Columns.Add(col);
            col = new System.Windows.Forms.ColumnHeader();
            col.Text = "´óÐ¡";
            col.Width = 200;
            col.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            ListView.Columns.Add(col);
        }
        protected override System.Windows.Forms.ListViewItem OnCreateItem(NetFiles.Command.Record item)
        {
            System.Windows.Forms.ListViewItem vi = new System.Windows.Forms.ListViewItem(
                new string[] {item.Name,item.Size.ToString("###,###,###byte") });
            vi.Tag = item;
            return vi;
        }
    }
}

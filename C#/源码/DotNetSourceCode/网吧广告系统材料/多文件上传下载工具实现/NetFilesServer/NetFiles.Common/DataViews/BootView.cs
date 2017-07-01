using System;
using System.Collections.Generic;
using System.Text;
using NetFiles.DataAccess.Entities;
namespace NetFiles.Common.DataViews
{
    public class BootView:ViewDataAdapter<BootInfo>
    {
        public BootView(System.Windows.Forms.ListView view) : base(view) { }
        protected override void OnCreateColumns()
        {
            System.Windows.Forms.ColumnHeader col;
            col = new System.Windows.Forms.ColumnHeader();
            col.Text = "·þÎñÄ¿Â¼";
            col.Width = 200;
            ListView.Columns.Add(col);
            col = new System.Windows.Forms.ColumnHeader();
            col.Text = "ÃèÊö";
            col.Width = 500;
            ListView.Columns.Add(col);
        }
        protected override System.Windows.Forms.ListViewItem OnCreateItem(BootInfo item)
        {
            System.Windows.Forms.ListViewItem vi = new System.Windows.Forms.ListViewItem(
                new string[] {item.BootDirectory,item.Remark });
            vi.Tag = item;
            vi.ToolTipText = item.BootDirectory;
            return vi;
        }
       
    }
}

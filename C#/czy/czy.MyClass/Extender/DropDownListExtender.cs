using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;

/// <summary>
/// DropDownList操作控件类
 /// </summary>
public static class DropDownListExtender
{
   
    /// <summary>
    /// DropDownList附值
    /// </summary>
    /// <param name="drlist">DropDownList</param>
    /// <param name="value">Txt值</param>
    public static void SetIndexByText(this DropDownList dataList, string text)
    {
        dataList.SelectedIndex = dataList.Items.IndexOf(dataList.Items.FindByText(text));
    }
    /// <summary>
    /// DropDownList附值
    /// </summary>
    /// <param name="drlist">DropDownList</param>
    /// <param name="value">Value值</param>
    public static void SetIndexByValue(this DropDownList dataList, string value)
    {
        int index = dataList.Items.IndexOf(dataList.Items.FindByValue(value));
        dataList.SelectedIndex = index;
    }
}


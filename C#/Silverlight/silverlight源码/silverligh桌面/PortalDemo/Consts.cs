using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PortalDemo
{
    public static class Consts
    {
        /// <summary>
        /// 宿主站点的虚拟目录
        /// </summary>
        public static string BaseUrl { get; set; }

        /// <summary>
        /// 整个宿主区的宽度
        /// </summary>
        public static double HostWidth { get; set; }

        /// <summary>
        /// 根据区域的标题来得到它的DataServiceUrl
        /// </summary>
        /// <param name="areaTitle"></param>
        /// <returns></returns>
        public static string GetDataServiceUrl(string areaTitle)
        {
            switch (areaTitle)
            {
                case "标题1":
                    return "/IndexHelp.aspx?SubSystemName=Notify";
                case "标题2":
                    return "/IndexHelp.aspx?SubSystemName=JBDocument";
                case "标题3":
                    return "/IndexHelp.aspx?SubSystemName=SendDocument";
                case "标题4":
                    return "/IndexHelp.aspx?SubSystemName=ReceiveDocument";
                case "标题5":
                    return "/IndexHelp.aspx?SubSystemName=QSDJDocument";
                case "标题6":
                    return "/IndexHelp.aspx?SubSystemName=KSQSDJDocument";
                case "标题7":
                    return "/IndexHelp.aspx?SubSystemName=YFBGDDocument";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 最原始的布局信息
        /// </summary>
        public const string InitAreaLayout = "<Columns>\r\n" +
            "<Column ColumnId='0e2b4700-5023-498d-af39-1ca9c5d1f73b' LeftPercentage='0' WidthPercentage='0.33'>\r\n" +
            "<Areas>\r\n" +
            "<Area AreaId='566d0ab6-0886-43eb-aecb-6d0cb507deb9' Top='400' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题7'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "<Area AreaId='bb4d1567-b236-4c57-ba16-c3914ec59257' Top='200' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题1'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "<Area AreaId='8ca5fa4f-3449-4951-83cb-128010c54cb9' Top='0' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题2'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "</Areas>\r\n" +
            "</Column>\r\n" +
            "<Column ColumnId='144ed3ea-6aff-4b8c-b48c-e29ad7d9ca2a' LeftPercentage='0.33' WidthPercentage='0.33'>\r\n" +
            "<Areas>\r\n" +
            "<Area AreaId='c5b6dbcf-2e58-4a1f-bc94-332ea0e3bff8' Top='0' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题3'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "<Area AreaId='c76f7669-5c7c-45d7-a412-3a9fe2ad4381' Top='200' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题4'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "</Areas>\r\n" +
            "</Column>\r\n" +
            "<Column ColumnId='759426b3-2311-4d7a-a06b-7f9ad91cc534' LeftPercentage='0.66' WidthPercentage='0.33'>\r\n" +
            "<Areas>\r\n" +
            "<Area AreaId='160ba4c7-f546-48dd-84e1-2496d1149421' Top='0' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题5'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "<Area AreaId='522d04ca-095a-4088-88e3-959ea226d13d' Top='200' Height='200' HeadStyleKey='BlueHead' InBorderStyleKey='InBorder' State='1'>\r\n" +
            "<AreaHead Title='标题6'>\r\n" +
            "</AreaHead>\r\n" +
            "</Area>\r\n" +
            "</Areas>\r\n" +
            "</Column>\r\n" +
            "</Columns>\r\n";
    }
}

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Defalut.aspx.cs" Inherits="GMap.Defalut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ditu.google.cn/maps?file=api&v=2&sensor=false&key=ABQIAAAA3J9pU8zS35uzqNdZ9bV8DBSLnJcAVdDkxesvCe__rTN4jQ8oGxTX9S37gCFE2xvKQj7m5GmXobK4Sg" type="text/javascript"></script>
    <script src="js/labeledmarker.js" type="text/javascript"></script>
    <script src="js/jBox-1.1.js" type="text/javascript"></script>
    <script src="js/extinfowindow.js" type="text/javascript"></script>
    <link type="text/css" rel="Stylesheet" media="screen" href="js/redInfoWindow.css"/>
    <link href="js/jBox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
     //地图对像
     var map = null;
     //以获取用户指定地址的地址解析
     var geocoder = new GClientGeocoder(); 
     function loadMap() {

         //初始化与定位
         var isCompatible = new GBrowserIsCompatible();
         if (isCompatible) {
             //容器ID
             var containId = "map_canvas";
             // 要设置的标点 
             var point = new GLatLng(31.2318423, 121.476867);
             // 地图默认的比例尺级别
             var perviewLevel = 14;
             // 大的地图缩放级别控件
             var largeMapControl = new GLargeMapControl();
             // 地图缩略图控件
             var overviewMapControl = new GOverviewMapControl();
             // 比例尺控件
             var scaleControl = new GScaleControl();
             // 地图类形选择控件
             var mapTypeControl = new GMapTypeControl();
             // 地址-坐标转换器
             var geocoder = new GClientGeocoder();
             // 上一次的查询地址
             var lastAddress = '';


             map = new GMap2(document.getElementById(containId));
             map.setCenter(point, perviewLevel);

             var customUI = map.getDefaultUI();
             customUI.maptypes.physical = false;
             map.setUI(customUI);

             // point = new GLatLng(lat, lng);
             map.addMapType(G_PHYSICAL_MAP);
             map.setCenter(point, perviewLevel);

             map.enableDoubleClickZoom();
             map.enableScrollWheelZoom();
             map.enableContinuousZoom();

             map.addControl(largeMapControl)
             map.addControl(overviewMapControl);
             map.addControl(mapTypeControl);
             map.addControl(scaleControl);

             MarkerAddress("海沧中学", "<div></div>");
         }
         else {
             alert("对不起，您的浏览器不支持创建地图！");
         }
     }

     //标注地址
     function MarkerAddress(address, html) {
        
         //map.clearOverlays();  //清除所有标注
         geocoder.getLocations(address,
          function (response) {
              if (!response || response.Status.code != 200)
               { alert("\"" + address + "\" not found"); } 
               else 
               {   
                  for(var i=0;i<response.Placemark.length;i++)
                  {
                     var place = response.Placemark[i]; //集合
                     var point = new GLatLng(place.Point.coordinates[1], place.Point.coordinates[0]);//座标
                     var address=place.address;//地址
                     //var countryNameCode=place.AddressDetails.Country.CountryNameCode;
                     OverMarker(point, address, html);
                  }
                }
          }
         );

//          geocoder.getLatLng(address, function (point) {
//              if (!point) {
//                  alert("地址座标错误!");
//              } else {
         //              OverMarker(point,"",html);
//              }
//          }
//       );
    }

     function OverMarker(point,name,html) {
         //地图定位
         map.setCenter(point, 13);

         //标注图标
         var icon = new GIcon();
         icon.image = 'red_marker.png';
         icon.iconSize = new GSize(32, 32);
         icon.iconAnchor = new GPoint(16, 16);
         icon.infoWindowAnchor = new GPoint(25, 7);

         //图标参数自定义标记选项
         /* =========================================================================================================================================================================================
         参数说明：
         常数：G_DEFAULT_ICON     标记使用的默认图标。
         image                 String     图标的前景图像 URL。
         shadow                 String     图标的阴影图像 URL。
         iconSize             GSize     图标前景图像的像素大小。
         shadowSize             GSize     阴影图像的像素大小。
         iconAnchor             GPoint     此图标在地图上的锚定点相对于图标图像左上角的像素坐标。
         infoWindowAnchor     GPoint     信息窗口在此图标上的锚定点相对于图标图像左上角的像素坐标。
         printImage             String     打印地图所用的前景图标图像的 URL。其大小必须与 image 提供的主图标图像的大小相同。
         mozPrintImage         String     用 Firefox/Mozilla 打印地图时所用的前景图标图像的 URL。其大小必须与 image 提供的主图标图像的大小相同。
         printShadow             String     打印地图时所用的阴影图像的 URL。由于大多数浏览器都无法打印 PNG 图像，所以图像格式应该为 GIF。
         transparent             String     在 Internet Explorer 中捕获点击事件时，所用的透明前景图标图像的 URL。此图像应是具有 1% 不透明性的 24 位 PNG 格式的主图标图像，但其大小和形状同主图标相同。
         imageMap             Array of Number  表示图像地图 x/y 坐标的整数数组，用它指定浏览器（非 Internet Explorer）中图标图像的可点击部分。
         maxHeight             Integer 指定拖动标记时视觉上垂直“上升”的距离（以像素表示）。（自 2.79 开始）
         dragCrossImage         String     指定拖动图标时十字交叉图像的 URL。（自 2.79 开始）
         dragCrossSize         GSize     指定拖动图标时十字交叉图像的像素大小。（自 2.79 开始）
         dragCrossAnchor         GPoint     指定拖动图标时十字交叉图像的像素坐标偏移量（相对于 iconAnchor）。（自 2.79 开始）
         ========================================================================================================================================================================================= */
         opts = {
             "icon": icon,
             "labelText": "<div style='width:200px;font-size:11px;background-color:#ffffff;padding:3px;border:solid 1px #000000'><span>" + name + "</span></div>",
             "clickable": true,
             "labelOffset": new GSize(16, -16)
         };


         //创建标注
         var marker = new LabeledMarker(point, opts);
         //图标点击事件
         GEvent.addListener(marker, "click", function () {
             marker.openExtInfoWindow(map, "custom_info_window_red", html, { beakOffset: 1 });
         });
         //标注
         map.addOverlay(marker);
     }
     
    
</script>
</head>
<body onLoad="loadMap()">
    <form id="form1" runat="server">
    <div>
    <div id="map_canvas"   style="height:1024px; "></div>
    </div>
    </form>
</body>
</html>

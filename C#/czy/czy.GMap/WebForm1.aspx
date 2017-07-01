<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GMap.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml"> 
 <head> 
  <title>My Googel Map</title> 
  <script src="http://ditu.google.cn/maps?file=api&v=2&sensor=false&key=ABQIAAAA3J9pU8zS35uzqNdZ9bV8DBSLnJcAVdDkxesvCe__rTN4jQ8oGxTX9S37gCFE2xvKQj7m5GmXobK4Sg" type="text/javascript"></script>
    <script src="js/labeledmarker.js" type="text/javascript"></script>
    <script src="js/extinfowindow.js" type="text/javascript"></script>
    <link type="text/css" rel="Stylesheet" media="screen" href="js/redInfoWindow.css"/>
    <link href="css/map_style.css" rel="stylesheet" type="text/css" />
    <link href="js/jBox.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript"> 
  if(typeof GoogleMap === 'undefined'){
    var GoogleMap = {};
}
(function(){
    if (false) {
        return false;
    }
    else {
        var isCompatible = new GBrowserIsCompatible();
        if (isCompatible) {
            var mapContainer = document.getElementById("map");
            // 创建GoogleMAP地图实例
            var map = new GMap2(mapContainer);
            // 要设置的标点 
            var point = null;    
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
            
            GoogleMap.AdddressMsg = [];
            
            // 创建地图
            GoogleMap.Map = function(lat, lng){
                point = new GLatLng(lat, lng);        
                map.addMapType(G_PHYSICAL_MAP);
                map.setCenter(point, perviewLevel);
                
                map.enableDoubleClickZoom();
                map.enableScrollWheelZoom();
                map.enableContinuousZoom();
                
                map.addControl(largeMapControl)
                map.addControl(overviewMapControl);
                map.addControl(mapTypeControl);
                map.addControl(scaleControl);
            };
            
            // 创建标记
            GoogleMap.createMarker = function(latlng, markerOptions){
                var marker = new GMarker(latlng, markerOptions) || new GMarker(latlng);
                marker.value = 0;
                return marker;
            };
            
            // 自定义标记选项
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
            GoogleMap.setCustomIcon = function(IconOptions){
                var myIcon = new GIcon(), i;
                for (i in IconOptions) {
                    switch (i) {
                        case 'iconSize':
                        case 'shadowSize':
                        case 'dragCrossSize':
                            myIcon[i] = new GSize(IconOptions[i][0], IconOptions[i][1]);
                            break;
                        case 'iconAnchor':    
                        case 'infoWindowAnchor':
                        case 'infoShadowAnchor':
                        case 'dragCrossAnchor':
                            myIcon.iconAnchor = new GPoint(IconOptions[i][0], IconOptions[i][1]);
                            break;
                        default:
                            myIcon[i] = IconOptions[i];
                            break;
                    }    
                    
                }    
                return myIcon;    
            };
            
            // 通过地址获得坐标
            GoogleMap.getAddresslatlng = function(response){
                var place = response.Placemark[0];
                point = new GLatLng(place.Point.coordinates[1], place.Point.coordinates[0]);
                return [place.Point.coordinates[1], place.Point.coordinates[0], point, place];
            };
            
            // 标注坐标和相应的说明信息
            GoogleMap.MarkerMap = function(lat, lng){
                var marker = null;
                GoogleMap.Map(lat, lng);
                
                marker = this.createMarker(point);
                if (GoogleMap.AdddressMsg) {
                    GEvent.addListener(marker, "click", function(){
                        var msg = '<span id="fgmap_markerMsg">', j;
                        msg += '<h4>' + GoogleMap.AdddressMsg[1][0] + '</h4>';
                        for (var j = 1; j < GoogleMap.AdddressMsg[1].length; j++) {
                            msg += GoogleMap.AdddressMsg[1][j] + "";
                        }
                        msg += "</span>";
                        map.openInfoWindowHtml(point, msg);
                    });
                }
                map.addOverlay(marker);
            };
            
            // 将查询地址添加到地图
            GoogleMap.addAddressToMap = function(response){
                map.clearOverlays();
                if (!response || response.Status.code != 200) {
                    alert("对不起, 我们解析不到该地址的经纬度坐标！");
                }
                else {
                    var marker = null, point = GoogleMap.getAddresslatlng(response);
                    var address = point[3].address, lat = point[0], lng = point[1];
                    GoogleMap.AdddressMsg = (GoogleMap.AdddressMsg !== '' && (lastAddress == GoogleMap.AdddressMsg[0])) ? GoogleMap.AdddressMsg : [address, [point[3].address, ('经度：' + point[1]), ('纬度：' + point[0])]];
                    GoogleMap.MarkerMap(lat, lng);
                }
            };
            
            // 通过地址查询坐标
            GoogleMap.showLocation = function(address){
                lastAddress = address;
                geocoder.getLocations(address, this.addAddressToMap);
            };
            
            GEvent.addListener(map, "click", function(overlay,point) {
               var myHtml = "GPoint 值（在缩放级别）是 : " + map.fromLatLngToDivPixel(point) + " 和 " + map.getZoom();
               map.openInfoWindow(point, myHtml);
            });
            GEvent.addListener(window, 'unload', GUnload);
        }
        else {
            alert("对不起，您的浏览器不支持创建地图！");
        }
    }
})();
  </script> 
 </head> 

 <body> 
  <div id="map"></div>
 </body> 
</html>  
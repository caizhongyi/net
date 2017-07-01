/****************************
 * 地图操作相关js调用代码
 * 
 * zero-cn@126.com
 * 
 * **************************
 */
//根据城市获取数据
var maxPf=10;
function getHotels(page,pageIndex,pageCount,orderType,province,city)
{
	
	//if(document.getElementById("selProvince").value==0)
    //	{
	//	province='*';
		//showCity('上海');
	//	map.setCenter(new GLatLng(31.2318423, 121.476867), 10); 
	//}
	//else
	//{
	//	province=document.getElementById("selProvince").value;
	//	city=document.getElementById("selCity").value;
		
		//showCity(city);
		
	//	map.setCenter(new GLatLng(31.2318423, 121.476867), 10); 
		
	//	province=encodeURI(province);
	//	city=encodeURI(city);
	//}
	//if(province=='*')
	//{
	//	province="";
	//	city="";
	//}
	
	
	
	//document.getElementById("test").innerHTML="pageChange.aspx?page="+page+"&province="+province+"&city="+city+"&hotelName=&address=";

    GDownloadUrl("NcMapXMLServer.aspx?pageCount=" + pageCount + "&orderType=" + orderType + "&pageIndex=" + pageIndex + "&province=" + encodeURI(province) + "&city=" + encodeURI(city), function(data, responseCode) {

        //清空
        map.clearOverlays();
        //定义数据
        var NcNo = ""; //id
        var NutritionClubName = ""; //名称
        var ApplyID = ""; //申请号
        var DoorType = ""; //门头类型
        var Province = ""; //省
        var City = ""; //城市
        var latitude = ""; //纬度
        var longitude = ""; //经度
        var NutritionClubAddress = ""; //地址
        var NcPhone = ""; //电话
        var NcMobilePhone = ""; //手机
        var Store_Desc = ""; //简介
        var Store_Prom = ""; //物语
        var NutritionClubStatus = ""; //状态
        var NUTRITIONCLUBSTATUSDETAIL = ""; //说明
        var CustoRank = 0; //热门推荐 
        var ClickRank = 0; //客户评分 
        var NCRank = 0; //HBL热度
        var CreatedOn = ""; //创建时间
        var CreatedBy = ""; //创建者
        var UpdatedOn = ""; //修改时间
        var UpdatedBy = ""; //修改人
        var MetaData = ""; //图片

        var xml = GXml.parse(data);

        //	var html="<table width=\"777\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">"+
        //			  "<tr>"+
        //				"<td>&nbsp;</td>"+
        //				"<td>&nbsp;</td>"+
        //			  "</tr>";

        //	var currentpage=xml.documentElement.childNodes[0].text; //当前页数			

        //	var pagecount=xml.documentElement.childNodes[1].text; //总页数

        //获取酒店列表
        var hotels_list = xml.documentElement.getElementsByTagName("Table");
        //alert(hotels_list.length);
        for (var i = 0; i < hotels_list.length; i++) {
            if (hotels_list[i].selectSingleNode("NcNo") != null) {
                NcNo = hotels_list[i].selectSingleNode("NcNo").text;
                if (NcNo == "NULL") {
                    NcNo = "";
                }
            }
            if (hotels_list[i].selectSingleNode("NutritionClubName") != null) {
                NutritionClubName = hotels_list[i].selectSingleNode("NutritionClubName").text;
                if (NutritionClubName == "NULL") {
                    NutritionClubName = "";
                }
            }
            if (hotels_list[i].selectSingleNode("ApplyID") != null) {
                ApplyID = hotels_list[i].selectSingleNode("ApplyID").text;
                if (ApplyID == "NULL") {
                    ApplyID = "";
                }
            }
            if (hotels_list[i].selectSingleNode("DoorType") != null) {
                DoorType = hotels_list[i].selectSingleNode("DoorType").text;
                if (DoorType == "NULL") {
                    DoorType = "";
                }
            }
            if (hotels_list[i].selectSingleNode("Province") != null) {
                Province = hotels_list[i].selectSingleNode("Province").text;
                if (Province == "NULL") {
                    Province = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("City") != null) {
                City = hotels_list[i].selectSingleNode("City").text;
                if (City == "NULL") {
                    City = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("latitude") != null) {
                latitude = hotels_list[i].selectSingleNode("latitude").text;
                if (latitude == "NULL") {
                    latitude = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("longitude") != null) {
                longitude = hotels_list[i].selectSingleNode("longitude").text;
                if (longitude == "NULL") {
                    longitude = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("NutritionClubAddress") != null) {
                NutritionClubAddress = hotels_list[i].selectSingleNode("NutritionClubAddress").text;
                if (NutritionClubAddress == "NULL") {
                    NutritionClubAddress = "";
                }
            }
            if (hotels_list[i].selectSingleNode("NcPhone") != null) {
                NcPhone = hotels_list[i].selectSingleNode("NcPhone").text;
                if (NcPhone == "NULL") {
                    NcPhone = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("NcMobilePhone") != null) {
                NcMobilePhone = hotels_list[i].selectSingleNode("NcMobilePhone").text;
                if (NcMobilePhone == "NULL") {
                    NcMobilePhone = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("Store_Desc") != null) {
                Store_Desc = hotels_list[i].selectSingleNode("Store_Desc").text;
                if (Store_Desc == "NULL") {
                    Store_Desc = "";
                }
            }
            if (hotels_list[i].selectSingleNode("Store_Prom") != null) {
                Store_Prom = hotels_list[i].selectSingleNode("Store_Prom").text;
                if (Store_Prom == "NULL") {
                    Store_Prom = "";
                }
            }
            if (hotels_list[i].selectSingleNode("NutritionClubStatus") != null) {
                NutritionClubStatus = hotels_list[i].selectSingleNode("NutritionClubStatus").text;
                if (NutritionClubStatus == "NULL") {
                    NutritionClubStatus = "";
                }
            }
            if (hotels_list[i].selectSingleNode("NUTRITIONCLUBSTATUSDETAIL") != null) {
                NUTRITIONCLUBSTATUSDETAIL = hotels_list[i].selectSingleNode("NUTRITIONCLUBSTATUSDETAIL").text;
                if (NUTRITIONCLUBSTATUSDETAIL == "NULL") {
                    NUTRITIONCLUBSTATUSDETAIL = "";
                }
            }
            if (hotels_list[i].selectSingleNode("CustoRank") != null) {
                CustoRank = hotels_list[i].selectSingleNode("CustoRank").text;
                if (CustoRank == "NULL") {
                    CustoRank = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("ClickRank") != null) {
                ClickRank = hotels_list[i].selectSingleNode("ClickRank").text;
                if (ClickRank == "NULL") {
                    ClickRank = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("NCRank") != null) {
                NCRank = hotels_list[i].selectSingleNode("NCRank").text;
                if (NCRank == "NULL") {
                    NCRank = 0;
                }
            }
            if (hotels_list[i].selectSingleNode("CreatedOn") != null) {
                CreatedOn = hotels_list[i].selectSingleNode("CreatedOn").text;
            }
            if (hotels_list[i].selectSingleNode("CreatedBy") != null) {
                CreatedBy = hotels_list[i].selectSingleNode("CreatedBy").text;
            }
            if (hotels_list[i].selectSingleNode("UpdatedOn") != null) {
                UpdatedOn = hotels_list[i].selectSingleNode("UpdatedOn").text;
            }
            if (hotels_list[i].selectSingleNode("UpdatedBy") != null) {
                UpdatedBy = hotels_list[i].selectSingleNode("UpdatedBy").text;
            }
            if (hotels_list[i].selectSingleNode("MetaData") != null) {
                MetaData = hotels_list[i].selectSingleNode("MetaData").text;
                if (MetaData == "NULL") {
                    MetaData = "";
                }
            }

            //NcNo = hotels_list[i].childNodes[0].text; //id
            //            NutritionClubName = hotels_list[i].childNodes[1].text; //名称
            //            ApplyID = hotels_list[i].childNodes[2].text; //申请号
            //            DoorType = hotels_list[i].childNodes[3].text; //门头类型
            //            Province = hotels_list[i].childNodes[4].text; //省
            //            City = hotels_list[i].childNodes[5].text; //城市
            //            latitude = hotels_list[i].childNodes[6].text; //纬度
            //            longitude = hotels_list[i].childNodes[7].text; //经度
            //            NutritionClubAddress = hotels_list[i].childNodes[8].text; //地址
            //            NcPhone = hotels_list[i].childNodes[9].text; //电话
            //            NcMobilePhone = hotels_list[i].childNodes[10].text; //手机
            //            Store_Desc = hotels_list[i].childNodes[11].text; //简介
            //            Store_Prom = hotels_list[i].childNodes[12].text; //物语
            //            NutritionClubStatus = hotels_list[i].childNodes[13].text; //状态
            //            NUTRITIONCLUBSTATUSDETAIL = hotels_list[i].childNodes[14].text; //说明
            //            CustoRank = hotels_list[i].childNodes[15].text; //热门推荐 
            //            ClickRank = hotels_list[i].childNodes[16].text; //客户评分 
            //            NCRank = hotels_list[i].childNodes[17].text; //HBL热度
            //            CreatedOn = hotels_list[i].childNodes[18].text; //创建时间
            //            CreatedBy = hotels_list[i].childNodes[19].text; //创建者
            //            UpdatedOn = hotels_list[i].childNodes[20].text; //修改时间
            //            UpdatedBy = hotels_list[i].childNodes[21].text; //修改人
            //            MetaData = hotels_list[i].childNodes[22].text; //图片

            //html=html+"<tr>"+
            //			"<td width=\"120\">&nbsp;</td>"+
            //			"<td width=\"415\">&nbsp;</td>"+
            //		  "</tr>"+
            //		  "<tr>"+
            //			"<td height=\"101\" valign=\"top\"><a href=\"javascript:ShowWindows('gmap','gmap','GMap.aspx?shop_idx="+NcNo+"','665','510')\">"+NutritionClubName+"</a></td>"+
            //			"<td><table width=\"366\" border=\"0\" cellpadding=\"1\" cellspacing=\"1\" style=\"background-color:#000\">"+
            //			  "<tr>"+
            //				"<td width=\"89\" height=\"30\" bgcolor=\"#FFFFFF\">所属省：</td>"+
            //				"<td width=\"277\" bgcolor=\"#FFFFFF\">"+Province+"</td>"+
            //			  "</tr>"+
            //			  "<tr>"+
            //				"<td height=\"30\" bgcolor=\"#FFFFFF\">所属城市：</td>"+
            //				"<td bgcolor=\"#FFFFFF\">"+City+"</td>"+
            //			  "</tr>"+
            //			  "<tr>"+
            //				"<td height=\"30\" bgcolor=\"#FFFFFF\">地址：</td>"+
            //				"<td bgcolor=\"#FFFFFF\">"+address+"</td>"+
            //			  "</tr>"+
            //			"</table></td>"+
            //		  "</tr>"+
            //		  "<tr>"+
            //			"<td>&nbsp;</td>"+
            //			"<td>&nbsp;</td>"+
            //		  "</tr>";
            var markerHtmls = markerHtml(NcNo,
                                            NutritionClubName,
                                            ApplyID,
                                            DoorType,
                                            Province,
                                            City,
                                            latitude,
                                            longitude,
                                            NutritionClubAddress,
                                            NcPhone,
                                            NcMobilePhone,
                                            Store_Desc,
                                            Store_Prom,
                                            NutritionClubStatus,
                                            NUTRITIONCLUBSTATUSDETAIL,
                                            CustoRank,
                                            ClickRank,
                                            NCRank,
                                            CreatedOn,
                                            CreatedBy,
                                            UpdatedOn,
                                            UpdatedBy,
                                            MetaData);


            if (latitude <= 0 && longitude <= 0) {
                var postAddress = document.getElementById("TxtProvince").value  + document.getElementById("TxtCity").value + NutritionClubAddress;

                showAddress(postAddress, markerHtmls);
             
            }
        }
        showCity(document.getElementById("TxtCity").value);
        //获取当前搜索条件

        ///var province=document.getElementById("selProvince").value;
        //var city=document.getElementById("selCity").value;

        //if(province==0)
        //{
        //	province="*";
        //	city="*";
        //}



        //var pagehtml="<table width=\"157\" border=\"0\">"
        //				"<tr>"+
        //				  "<td>";
        //				  if(currentpage>1)
        //				  {
        //					 var uppage=currentpage;
        //					 --uppage;
        //				  	pagehtml=pagehtml+"<a href=\"javascript:getHotels("+uppage+",'"+province+"','"+city+"')\">上一页</a>";
        //				  }
        //				  if(pagecount>1&&currentpage<pagecount)
        //				  {
        //					var downpage=currentpage;
        //					++downpage;
        //				  	pagehtml=pagehtml+"<a href=\"javascript:getHotels("+downpage+",'"+province+"','"+city+"')\">下一页</a>";
        //				  }
        //	pagehtml=pagehtml+ "</td>"+
        //					"</tr>"+
        //				"</table>";


        //document.getElementById("datalist").innerHTML=html;

        //document.getElementById("page").innerHTML=pagehtml;




    });
			
			

}
//打开地图窗口
function ShowWindows(id,name,url,width,height)
{
	jBox.open(id,'iframe',url,name,'width='+width+',height='+height+',center=true,minimizable=true,resize=true,draggable=true,model=true,scrolling=true');
} 
//显示当前位置，并添加一个单击事件
//function showAddress(address,html) { 

//  geocoder.getLatLng( 
//    address, 
//    function(point) { 
//      if (!point) { 
//        //alert("无法解析:" + address);  
//      } else { 
//        
//        

//		var icon = new GIcon();
//		icon.image = 'red_marker.png';
//		icon.iconSize = new GSize(32, 32);
//		icon.iconAnchor = new GPoint(16, 16);
//		icon.infoWindowAnchor = new GPoint(25, 7);
//		
//		opts = { 
//		  "icon": icon,
//		  "clickable": true,
//		  "labelOffset": new GSize(16, -16)
//		};
//		var marker = new LabeledMarker(point, opts);
//		
//		GEvent.addListener(marker, "click", function() {  
//	      marker.openExtInfoWindow(
//              map, 
//              "custom_info_window_red",
//             html,
//              {beakOffset: 5}
//            ); 
//		  });
//		
//        map.addOverlay(marker); 
//        //marker.openInfoWindowHtml(address); 
//		
//      } 
//    } 
//  ); 
//}
function showAddress(address, html) {
    geocoder.getLatLng(
    address,
    function(point) {
        if (!point) {

        } else {


            var icon = new GIcon();
            icon.image = 'red_marker.png';
            icon.iconSize = new GSize(32, 32);
            icon.iconAnchor = new GPoint(16, 16);
            icon.infoWindowAnchor = new GPoint(25, 7);

            opts = {
                "icon": icon,
                // "labelText": NutritionClubName,
                "clickable": true,
                "labelOffset": new GSize(16, -16)
            };



            var marker = new LabeledMarker(point, opts);
            
            //alert(point + "," + opts);
            GEvent.addListener(marker, "click", function() {
                marker.openExtInfoWindow(
              map,
              "custom_info_window_red",
             html,
              { beakOffset: 1 }
            );

            });
            map.addOverlay(marker);


        }
    }
  );
}
//根据城市设置中心点
function showCity(city) { 

  geocoder.getLatLng( 
    city, 
    function(point) { 
      if (!point) { 
        //alert("无法解析:" + city);  
      } else { 
        map.setCenter(point, 10); 

      } 
    } 
  ); 
}
  //跟据评分获取钻石图片的个数
    function GetJsImg( num)
    {
        var html = "";
        for (var i = 0; i < num; i++)
        {
            html += "<img alt=\"\" src=\"images/map02_zs.gif\"/>";
        }
        return html;
    }

    //执度线的长度
    function GetLine( num, totalNum)
    {
        
        var html = "";
        for (var i = 0; i < num; i++)
        {
            html += "<img alt=\"\" src=\"images/map02_line.jpg\"/>";
        }
        if (totalNum - num >= 0)
        {
            for (var j = 0; j < totalNum - num; j++)
            {
                html += "<img alt=\"\" src=\"images/map02_line1.jpg\"/>";
            }
        }
        return html;
    }
		

//标注的html模板
function markerHtml(
NcNo,
NutritionClubName,
ApplyID,
DoorType,
Province,
City,
latitude,
longitude,
NutritionClubAddress,
NcPhone,
NcMobilePhone,
Store_Desc,
Store_Prom,
NutritionClubStatus,
NUTRITIONCLUBSTATUSDETAIL,
CustoRank,
ClickRank,
NCRank,
CreatedOn,
CreatedBy,
UpdatedOn,
UpdatedBy,
MetaData
)
{

    
    //md = "http://" + window.location.host + "/ncrelease/" + md
    var md = "";
    if (MetaData=="") {
        md="images/clubpic/No_images.jpg";
    }　
    else
    {
        md=MetaData;
    }
    
    var	html="";
    //html+="<div style=\"marign-left:10px;\"><img alt=\"\" src=\"images/map03_03.jpg\"></div>";
   	html += "<div style=\" margin-top:10px;text-align:left\">";
	html += "<div style=\"float:left\">";
	html += "<div style=\" border-color:#999999;  border-style:solid; border-width:1px;padding:5px 5px 5px 5px;\"><div><img alt=\"\" width=\"100\" height=\"100\" src=\"" + md + "\"></div>";
	html += "<div>";
	//alert(html)
	// html+= "<img alt=\"\" src=\"images/nm_1.jpg\"/>";
	// html+= "<span style=\"color:#206ab5\">洒店实景图</span>";
	html += "</div></div></div>";

	html += "<div style=\"float:left; margin-left:20px;\">";
	html += "<div style=\"color:Black;font-size:14px; \"><strong><span style=\"border-bottom:solid 1px Black;\">" + NutritionClubName + "</span></strong></div>";
	html += "<div>" + Store_Desc + "</div>";
//	html += "<div style=\"color:#5e5e5e\"><strong>店长物语</strong></div>";
//	html += "<div>" + Store_Prom + "</div>";
	html += "<div style=\"color:#206ab5;width: 300px;  margin-top:10px;\">";
    html += "<div style=\"width: 300px;\"><div style=\"float:left; padding-top:4px;\"><img alt=\"\" src=\"images/nm_2.jpg\"/></div><div style=\"float:left;\">&nbsp;&nbsp;地址;" + NutritionClubAddress + "</div></div>";
	html += "<div style=\"width: 300px;\"><div style=\"float:left; padding-top:4px;\"><img alt=\"\" src=\"images/nm_3.jpg\"/></div><div style=\"float:left;\">&nbsp;&nbsp;" + NcPhone+"</div></div>";
	html += "</div>";
	html += "<div style=\"color:#206ab5; margin-top:10px;\">";
//	html += "<div ><span>用户评级:&nbsp;</span><span>" + GetJsImg(CustoRank) + "</span></div>";
//	html += "<div  style=\"margin-top:5px;\"><span>评语热度:&nbsp;</span><span>" + GetLine(ClickRank, 10) + "</span></div>";
	html += "</div>";
	html += "</div>";
	html += "</div>";
   // html+="<div><img alt=\"\" src=\"images/map03_12-14.jpg\"></div>";
	return html;
}
function UrlEncode(str)
{ 
    var ret=""; 
    var strSpecial="!\"#$%&()*+,/:;<=>?[]^`{|}~%"; var tt="";
    for(var i=0;i<str.length;i++)
    { 
        var chr = str.charAt(i); 
        var c=str2asc(chr); 
        tt += chr+":"+c+"n"; 
        if(parseInt("0x"+c) > 0x7f)
        { 
            ret+="%"+c.slice(0,2)+"%"+c.slice(-2); 
        }
        else
        { 
            if(chr==" ") 
                ret+="+"; 
            else if(strSpecial.indexOf(chr)!=-1) 
                ret+="%"+c.toString(16); 
            else 
                ret+=chr; 
        } 
    } 
    return ret; 
} 

function UrlDecode(str){ 
    var ret=""; 
    for(var i=0;i<str.length;i++)
    { 
        var chr = str.charAt(i); 
        if(chr == "+")
        { 
            ret+=" "; 
        }
        else if(chr=="%")
        { 
            var asc = str.substring(i+1,i+3); 
            if(parseInt("0x"+asc)>0x7f)
            { 
                ret+=asc2str(parseInt("0x"+asc+str.substring(i+4,i+6))); 
                i+=5; 
            }
            else
            { 
                ret+=asc2str(parseInt("0x"+asc)); 
                i+=2; 
            } 
        }
        else
        { 
            ret+= chr; 
        } 
    } 
    return ret; 
} 
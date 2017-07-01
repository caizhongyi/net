//Design By :lenlong Copyright ©DoNetBbs.com 东网官方网站
var ProvinceArray = new Array;
ProvinceArray[1] = "北京";
ProvinceArray[2] = "上海市";
ProvinceArray[3] = "天津";
ProvinceArray[4] = "广西";
ProvinceArray[5] = "广东";
ProvinceArray[6] = "山西";
ProvinceArray[7] = "河北";
ProvinceArray[8] = "黑龙江";
ProvinceArray[9] = "辽宁";
ProvinceArray[10] = "吉林";
ProvinceArray[11] = "江苏";
ProvinceArray[12] = "安徽";
ProvinceArray[13] = "山东";
ProvinceArray[14] = "浙江";
ProvinceArray[15] = "江西";
ProvinceArray[16] = "福建";
ProvinceArray[17] = "湖南";
ProvinceArray[18] = "湖北";
ProvinceArray[19] = "河南";
ProvinceArray[20] = "内蒙古";
ProvinceArray[21] = "海南";
ProvinceArray[22] = "重庆";
ProvinceArray[23] = "贵州";
ProvinceArray[24] = "四川";
ProvinceArray[25] = "云南";
ProvinceArray[26] = "陕西";
ProvinceArray[27] = "甘肃";
ProvinceArray[28] = "宁夏";
ProvinceArray[29] = "青海";
ProvinceArray[30] = "新疆";
ProvinceArray[31] = "西藏";
ProvinceArray[32] = "香港";
ProvinceArray[33] = "台湾";
ProvinceArray[34] = "澳门";
ProvinceArray[35] = "其他地区";
tCitys = new Array; 
tCitys[1] = new Array;
tCitys[1][0] = "崇文区";
tCitys[1][1] = "宣武区";
tCitys[1][2] = "东城区";
tCitys[1][3] = "西城区";
tCitys[1][4] = "海淀区";
tCitys[1][5] = "朝阳区";
tCitys[1][6] = "丰台区";
tCitys[1][7] = "石景山区";
tCitys[1][8] = "其他地区";

tCitys[2] = new Array;
tCitys[2][0] = "市区";
tCitys[2][1] = "郊区";
tCitys[3] = new Array;
tCitys[3][0] = "市区";
tCitys[3][1] = "郊区";
tCitys[3][2] = "山区";
tCitys[4] = new Array;
tCitys[4][0] = "南宁";
tCitys[4][1] = "桂林";
tCitys[4][2] = "梧州";
tCitys[4][3] = "柳州";
tCitys[4][4] = "玉林";
tCitys[4][5] = "百色";
tCitys[4][6] = "北海";
tCitys[4][7] = "其他";
tCitys[5] = new Array;
tCitys[5][0] = "广州";
tCitys[5][1] = "深圳";
tCitys[5][2] = "珠海";
tCitys[5][3] = "潮州";
tCitys[5][4] = "东莞";
tCitys[5][5] = "湛江";
tCitys[5][6] = "肇庆";
tCitys[5][7] = "佛山";
tCitys[5][8] = "中山";
tCitys[5][9] = "江门";
tCitys[5][10] = "韶关";
tCitys[5][11] = "英德";
tCitys[5][12] = "梅州";
tCitys[5][13] = "汕头";
tCitys[5][14] = "惠州";
tCitys[5][15] = "河源";
tCitys[5][16] = "茂名";
tCitys[5][17] = "顺德";
tCitys[5][18] = "其他";
tCitys[6] = new Array;
tCitys[6][0] = "太原";
tCitys[6][1] = "大同";
tCitys[6][2] = "晋城";
tCitys[6][3] = "运城";
tCitys[6][4] = "临汾";
tCitys[6][5] = "长治";
tCitys[6][6] = "析州";
tCitys[6][7] = "阳泉";
tCitys[6][8] = "候马";
tCitys[6][9] = "宁武";
tCitys[6][10] = "朔州";
tCitys[6][11] = "其他";
tCitys[7] = new Array;
tCitys[7][0] = "石家庄";
tCitys[7][1] = "承德";
tCitys[7][2] = "唐山";
tCitys[7][3] = "秦皇岛";
tCitys[7][4] = "沧州";
tCitys[7][5] = "张家口";
tCitys[7][6] = "保定";
tCitys[7][7] = "衡水";
tCitys[7][8] = "邢台";
tCitys[7][9] = "邯郸";
tCitys[7][10] = "廊坊";
tCitys[7][11] = "其他";
tCitys[8] = new Array;
tCitys[8][0] = "哈尔滨";
tCitys[8][1] = "齐齐哈尔";
tCitys[8][2] = "大庆市";
tCitys[8][3] = "佳木斯";
tCitys[8][4] = "牡丹江";
tCitys[8][5] = "伊春";
tCitys[8][6] = "绥化";
tCitys[8][7] = "黑河";
tCitys[8][8] = "鸡西";
tCitys[8][9] = "大兴安岭";
tCitys[8][10] = "鹤岗市";
tCitys[8][11] = "其他";
tCitys[9] = new Array;
tCitys[9][0] = "沈阳";
tCitys[9][1] = "大连";
tCitys[9][2] = "抚顺";
tCitys[9][3] = "鞍山";
tCitys[9][4] = "锦州";
tCitys[9][5] = "营口";
tCitys[9][6] = "本溪";
tCitys[9][7] = "丹东";
tCitys[9][8] = "辽阳";
tCitys[9][9] = "铁岭";
tCitys[9][10] = "其他";
tCitys[10] = new Array;
tCitys[10][0] = "长春";
tCitys[10][1] = "吉林";
tCitys[10][2] = "通化";
tCitys[10][3] = "四平";
tCitys[10][4] = "延吉";
tCitys[10][5] = "其他";
tCitys[11] = new Array;
tCitys[11][0] = "南京";
tCitys[11][1] = "无锡";
tCitys[11][2] = "苏州";
tCitys[11][3] = "徐州";
tCitys[11][4] = "常州";
tCitys[11][5] = "连云港";
tCitys[11][6] = "南通";
tCitys[11][7] = "淮阴";
tCitys[11][8] = "镇江";
tCitys[11][9] = "扬州";
tCitys[11][10] = "泰州";
tCitys[11][11] = "盐城";
tCitys[11][12] = "常熟";
tCitys[11][13] = "沭阳";
tCitys[11][14] = "其他";
tCitys[12] = new Array;
tCitys[12][0] = "合肥";
tCitys[12][1] = "蚌埠";
tCitys[12][2] = "芜湖";
tCitys[12][3] = "马鞍山";
tCitys[12][4] = "黄山";
tCitys[12][5] = "淮南";
tCitys[12][6] = "淮北";
tCitys[12][7] = "淮化";
tCitys[12][8] = "阜阳";
tCitys[12][9] = "安庆";
tCitys[12][10] = "六安";
tCitys[12][11] = "其他";
tCitys[13] = new Array;
tCitys[13][0] = "济南";
tCitys[13][1] = "青岛";
tCitys[13][2] = "烟台";
tCitys[13][3] = "淄博";
tCitys[13][4] = "东营";
tCitys[13][5] = "潍坊";
tCitys[13][6] = "德州";
tCitys[13][7] = "泰安";
tCitys[13][8] = "济宁";
tCitys[13][9] = "荷泽";
tCitys[13][10] = "临沂";
tCitys[13][11] = "威海";
tCitys[13][12] = "日照";
tCitys[13][13] = "莱芜";
tCitys[13][14] = "滨州";
tCitys[13][15] = "聊城";
tCitys[13][16] = "其他";
tCitys[14] = new Array;
tCitys[14][0] = "杭州";
tCitys[14][1] = "宁波";
tCitys[14][2] = "温州";
tCitys[14][3] = "嘉兴";
tCitys[14][4] = "金华";
tCitys[14][5] = "湖州";
tCitys[14][6] = "丽水";
tCitys[14][7] = "衢州";
tCitys[14][8] = "台州";
tCitys[14][9] = "绍兴";
tCitys[14][10] = "临海";
tCitys[14][11] = "舟山";
tCitys[14][12] = "其他";
tCitys[15] = new Array;
tCitys[15][0] = "南昌";
tCitys[15][1] = "九江";
tCitys[15][2] = "赣州";
tCitys[15][3] = "上饶";
tCitys[15][4] = "鹰潭";
tCitys[15][5] = "景德镇";
tCitys[15][6] = "井冈山";
tCitys[15][7] = "宜春";
tCitys[15][8] = "抚州";
tCitys[15][9] = "吉安";
tCitys[15][10] = "其他";
tCitys[16] = new Array;
tCitys[16][0] = "福州";
tCitys[16][1] = "厦门";
tCitys[16][2] = "泉州";
tCitys[16][3] = "龙岩";
tCitys[16][4] = "莆田";
tCitys[16][5] = "漳州";
tCitys[16][6] = "南平";
tCitys[16][7] = "福安";
tCitys[16][8] = "三明";
tCitys[16][9] = "永安";
tCitys[16][10] = "其他";
tCitys[17] = new Array;
tCitys[17][0] = "长沙";
tCitys[17][1] = "株洲";
tCitys[17][2] = "湘潭";
tCitys[17][3] = "衡阳";
tCitys[17][4] = "岳阳";
tCitys[17][5] = "益阳";
tCitys[17][6] = "常德";
tCitys[17][7] = "张家界";
tCitys[17][8] = "邵阳";
tCitys[17][9] = "怀化";
tCitys[17][10] = "郴州";
tCitys[17][11] = "其他";
tCitys[18] = new Array;
tCitys[18][0] = "武汉";
tCitys[18][1] = "宜昌";
tCitys[18][2] = "十堰";
tCitys[18][3] = "襄樊";
tCitys[18][4] = "黄石";
tCitys[18][5] = "荆门";
tCitys[18][6] = "鄂州";
tCitys[18][7] = "荆洲";
tCitys[18][8] = "孝感";
tCitys[18][9] = "黄岗";
tCitys[18][10] = "其他";
tCitys[19] = new Array;
tCitys[19][0] = "郑州";
tCitys[19][1] = "洛阳";
tCitys[19][2] = "开封";
tCitys[19][3] = "安阳";
tCitys[19][4] = "许昌";
tCitys[19][5] = "信阳";
tCitys[19][6] = "周口";
tCitys[19][7] = "商丘";
tCitys[19][8] = "平顶山";
tCitys[19][9] = "焦作";
tCitys[19][10] = "鹤壁";
tCitys[19][11] = "新乡";
tCitys[19][12] = "濮阳";
tCitys[19][13] = "漯河";
tCitys[19][14] = "三门峡";
tCitys[19][15] = "驻马店";
tCitys[19][16] = "南阳";
tCitys[19][17] = "其他";
tCitys[20] = new Array;
tCitys[20][0] = "呼和浩特";
tCitys[20][1] = "包头";
tCitys[20][2] = "赤峰";
tCitys[20][3] = "临河";
tCitys[20][4] = "乌海";
tCitys[20][5] = "其他";
tCitys[21] = new Array;
tCitys[21][0] = "海口";
tCitys[21][1] = "三亚";
tCitys[21][2] = "其他";
tCitys[22] = new Array;
tCitys[22][0] = "重庆";
tCitys[23] = new Array;
tCitys[23][0] = "贵阳";
tCitys[23][1] = "六盘水";
tCitys[23][2] = "玉屏";
tCitys[23][3] = "凯里";
tCitys[23][4] = "遵义";
tCitys[23][5] = "铜仁";
tCitys[23][6] = "安顺";
tCitys[23][7] = "其他";
tCitys[24] = new Array;
tCitys[24][0] = "成都";
tCitys[24][1] = "绵阳";
tCitys[24][2] = "乐山";
tCitys[24][3] = "攀枝花";
tCitys[24][4] = "自贡";
tCitys[24][5] = "宜宾";
tCitys[24][6] = "南充";
tCitys[24][7] = "内江";
tCitys[24][8] = "泸州";
tCitys[24][9] = "涪陵";
tCitys[24][10] = "德阳";
tCitys[24][11] = "其他";
tCitys[25] = new Array;
tCitys[25][0] = "昆明";
tCitys[25][1] = "大理";
tCitys[25][2] = "楚雄";
tCitys[25][3] = "玉溪";
tCitys[25][4] = "丽江";
tCitys[25][5] = "曲靖";
tCitys[25][6] = "个旧";
tCitys[25][7] = "其他";
tCitys[26] = new Array;
tCitys[26][0] = "西安";
tCitys[26][1] = "咸阳";
tCitys[26][2] = "渭南";
tCitys[26][3] = "汉中";
tCitys[26][4] = "宝鸡";
tCitys[26][5] = "铜川";
tCitys[26][6] = "延安";
tCitys[26][7] = "其他";
tCitys[27] = new Array;
tCitys[27][0] = "兰州";
tCitys[27][1] = "酒泉";
tCitys[27][2] = "天水";
tCitys[27][3] = "西峰";
tCitys[27][4] = "其他";
tCitys[28] = new Array;
tCitys[28][0] = "银川";
tCitys[28][1] = "石嘴山";
tCitys[28][2] = "固源";
tCitys[28][3] = "吴忠";
tCitys[28][4] = "其他";
tCitys[29] = new Array;
tCitys[29][0] = "西宁";
tCitys[29][1] = "玉树";
tCitys[29][2] = "海东";
tCitys[29][3] = "其他";
tCitys[30] = new Array;
tCitys[30][0] = "乌鲁木齐";
tCitys[30][1] = "克拉玛依";
tCitys[30][2] = "石河子";
tCitys[30][3] = "库尔勒";
tCitys[30][4] = "吐鲁番";
tCitys[30][5] = "哈密";
tCitys[30][6] = "喀什";
tCitys[30][7] = "伊宁";
tCitys[30][8] = "其他";
tCitys[31] = new Array;
tCitys[31][0] = "拉萨";
tCitys[31][1] = "其他";
tCitys[32] = new Array;
tCitys[32][0] = "市区";
tCitys[32][0] = "郊区";
tCitys[33] = new Array;
tCitys[33][0] = "台北";
tCitys[33][1] = "基隆";
tCitys[33][2] = "台南";
tCitys[33][3] = "台中";
tCitys[33][4] = "其他";
tCitys[34] = new Array;
tCitys[34][0] = "市区";
tCitys[34][1] = "郊区";
tCitys[35] = new Array;
tCitys[35][0] = "其他";

function JavaScriptWebSiteProvinceOptionMenu(prov)
{
        var i;
        provincebox = eval("document.all."+prov);
        for(i = 1; i < ProvinceArray.length; i++)
        {
          provincebox.options[i-1] = new Option(ProvinceArray[i],ProvinceArray[i]);
	}
	provincebox.length = i-1;
	
}



function JavaScriptWebSiteSelectCity(prov,city,comfrom)
{  
//alert()
//alert(prov)
	provincebox = eval("document.all."+prov);
	selcity = parseInt(provincebox.selectedIndex)+1;

	tCity = tCitys[selcity];
    citybox = eval("document.all."+city);
	//alert()
	//return;
	//if(tCity != null)
		//{     //citybox = document.Form1.city;
                       //if (tCity.length>1){
                          //citybox.options[0] = new Option("选择","选择");
                          for(i = 0; i < tCity.length; i++)
		  	  {
			     str = tCity[i];
                             citybox.options[i] = new Option(str, str);
			  }
			  citybox.length = i;
			 
                        //}
                        //else
                        //{
                          //str = tCity[0];
                          //citybox.options[0] = new Option(str,str);
                          //citybox.length=1;
                          //citybox.options[0].selected;
                        //}
                       
	               

		//}
        //else{
          //if (citybox != null){
           //citybox.options[0] = new Option("选择","选择");
	  //citybox.length = 1;}
        // }
		 if (comfrom!="")
		 {
			 WebSiteBindComeFrom(prov,city,comfrom);
			 return;
			 }
}
function WebSiteBindComeFrom(prov,city,comfrom)
{
	provincebox = eval("document.all."+prov);
    citybox = eval("document.all."+city);
	
	for (var i=0;i<provincebox.length;i++)
	{
		if (comfrom.replace(provincebox[i].value,"")!=comfrom)
		{
			provincebox.options[i].selected=true;
			
			//alert(eval("document.all."+cityname).value)
		tCity = tCitys[i+1];
		  for(i = 0; i < tCity.length; i++)
		  	  {
			     str = tCity[i];
                             citybox.options[i] = new Option(str, str);
			  }
			  citybox.length = i;
		for (var i=0;i<citybox.length;i++)
	{
		if (comfrom.replace(citybox[i].value,"")!=comfrom)
		{
			citybox.options[i].selected=true;
			
			return;
			}
		}
		//
			}
		}
	//alert(comfrom)
	
	}//


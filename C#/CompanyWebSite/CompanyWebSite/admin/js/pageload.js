window.onload=function()
{
  
}

function ResponseFilterIndex()
{
     if(T_AdminAjaxMethods.ResponseFilterIndex().value)
     {
        alert("发布成功!");
     }
     else
     {
       alert("发布失败!");
     }
}

function ChangeNavItem(pageName,data)
{
    var content=document.getElementById("right-content");
    var s= T_AdminAjaxMethods.ChangeNavItem(pageName).value;
    content.innerHTML=s;
    
    
     var b_data=data==null || data==""?"":data;
     switch (pageName)
     {
            case "news.html": b_data =T_AdminAjaxMethods.GetCurrentNewsInfo(cur+1).value;CreateNews(b_data); break;
            case "user.html": b_data =T_AdminAjaxMethods.GetCurrentNewsInfo(cur+1).value;CreateNews(b_data); break;
            case "type.html": b_data =T_AdminAjaxMethods.GetCurrentNewsInfo(cur+1).value;CreateNews(b_data); break;
            case "roles.html":b_data =T_AdminAjaxMethods.GetCurrentNewsInfo(cur+1).value;CreateNews(b_data); break;
            case "news_update.html": CreateNewsUpdate(data);   break;
            case "user_update.html": CreateNewsUpdate(data);   break;
            case "type_update.html": CreateNewsUpdate(data);   break;
            case "roles_update.html":CreateNewsUpdate(data);   break;
     }
    
      
     
   
}
   function update(index, dataRow)
   { 
       ChangeNavItem(pageName,data)
    	var sBasePath = document.location.href.substring(0,document.location.href.lastIndexOf('_samples')) ;

	    var oFCKeditor = new FCKeditor( 'FCKeditor1' ) ;
	    oFCKeditor.BasePath	= sBasePath ;
	    oFCKeditor.Value =
	    oFCKeditor.ReplaceTextarea() ;
       alert(dataRow[0]); 
   }
   function del(index, dataRow)
   { alert(index); }
   function getdata(index)
{
   //alert("cur:"+nav.currentPage+",totalPage:,"+nav.totalPage+",totalCount:"+nav.totalCount+",pageSize:"+nav.pageSize);
}

function CreateNews(b_data)
{
     var gv;
     var nav;
     var cur=0;
      var totalCount=0;
      b_data=eval("("+b_data+")");
       totalCount=b_data.TotalCount;
       var data={
        head:[
        {text:"id",width:100},
        {text:"标题",width:100},
        {text:"内容",width:100},
        {text:"类型",width:100},
        {text:"创建时间",width:100},
        {text:"来源",width:100},
        {text:"作者",width:100},
        {text:"是否显示",width:100}
        ],
        body:[]
       }
        for(var i=0;i<b_data.Table.length;i++)
        {
           data.body.push([
           b_data.Table[i].n_id,
           b_data.Table[i].n_title,
           b_data.Table[i].n_content,
           b_data.Table[i].n_typeId,
           b_data.Table[i].n_createDate,
           b_data.Table[i].n_source,
           b_data.Table[i].n_author,
           b_data.Table[i].n_isShow
           ]);
        }
        
        gv = new czyjs.UI.Controls.GridView(
       {
           id: "grid1",
           width: "500px;",
           height: "400px",
           randerTo: "grid",
           className: "czy-grid",
           upDataEvent:update,
           deleteEvent:del,
           data:data					     //  格式为:[['data','data1'],['data','data1'],['data','data1']]

       }
       );
        
	    nav=new czyjs.UI.AjaxPager({
	 	randerTo        :'pager',//
		//beforeNumCount:2,//
		//behindNumCount:2,//
		pageSize        :15,//
		totalCount      :totalCount,//
        navLabelVis     :false,
		fun             :getdata,
		className       :"AjaxPager-Nav",
        id              :"aj"
	    } );
}

function CreateNewsUpdate(data)
{
    
}





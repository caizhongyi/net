if (!window.wpfhome_3Dturn)
	wpfhome_3Dturn = {};
//<!--from slcenter.cn Silverlight开源中心-->
wpfhome_3Dturn.Page = function() 
{
}
var Infos=new Array(
{"img":"images/1.jpg","link":"http://www.wpfhome.com","bigpic":"images/1.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/2.jpg","link":"http://www.wpfhome.com","bigpic":"images/2.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/3.jpg","link":"http://www.wpfhome.com","bigpic":"images/3.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/4.jpg","link":"http://www.wpfhome.com","bigpic":"images/4.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/5.jpg","link":"http://www.wpfhome.com","bigpic":"images/5.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/6.jpg","link":"http://www.wpfhome.com","bigpic":"images/6.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/7.jpg","link":"http://www.wpfhome.com","bigpic":"images/7.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/1.jpg","link":"http://www.wpfhome.com","bigpic":"images/1.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/2.jpg","link":"http://www.wpfhome.com","bigpic":"images/2.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/3.jpg","link":"http://www.wpfhome.com","bigpic":"images/3.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/4.jpg","link":"http://www.wpfhome.com","bigpic":"images/4.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/5.jpg","link":"http://www.wpfhome.com","bigpic":"images/5.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/6.jpg","link":"http://www.wpfhome.com","bigpic":"images/6.jpg","width":500,"height":500,"target":"_blank"},
{"img":"images/7.jpg","link":"http://www.wpfhome.com","bigpic":"images/7.jpg","width":500,"height":500,"target":"_blank"}
);
	var img_width=190,img_height=190,bezier=20,sl_width=900,sl_height=600;
	var boundLeft=50,boundRight=50,boundBottom=100,boundTop=50;
	var a_width=sl_width-img_width-(boundLeft+boundRight),//a_height=10;
	a_height=sl_height-img_height-(boundBottom+boundTop);//减法是边
	var OX=(sl_width-img_width-boundRight-boundLeft)/2,OY=(sl_height-img_height-boundBottom-boundTop)/2;	//半径
	var nMaxSpeed=0.02;
	var nDX = 0;
wpfhome_3Dturn.Page.prototype =
{
	handleLoad: function(sl_c, cc, root) 
	{
		this.sl_c= sl_c;
		this.findName('bg_c').Source='bg.jpg';
		this.findName('bg_c')["Canvas.Top"]=0;
		this.findName('bg_c')["Canvas.Left"]=0;
		this.findName('bg_c').Width=sl_width;
		this.findName('bg_c').Height=sl_height;
		this.findName("showMoreBtn").addEventListener("MouseEnter",function(s,e){s.findName("showMore_btnOn").begin();});
		this.findName("showMoreBtn").addEventListener("MouseLeave",function(s,e){s.findName("showMore_mouseOut").begin();});
		this.findName("showMoreBtn").addEventListener("MouseLeftButtonDown",Silverlight.createDelegate(this,function(s,e){
			s.findName("showMore_down").begin();
			window.open(Infos[this.prevDownInfo.id].link,Infos[this.prevDownInfo.id].target);
		}));
		this.findName("ComeBackBtn").addEventListener("MouseEnter",function(s,e){s.findName("hiddenBtn_On").begin();});
		this.findName("ComeBackBtn").addEventListener("MouseLeave",function(s,e){s.findName("hiddenBtn_mouseOut").begin();});
		this.findName("ComeBackBtn").addEventListener("MouseLeftButtonDown",Silverlight.createDelegate(this,this.hiddenClick));
		var centerCxaml='<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="center_C" Canvas.Left="0" Canvas.Top="0">'+
							'<Canvas.Resources>';		
		var xaml="";
		for(var i=0;i<Infos.length;i++)
		{
			var obj=Infos[i];
			xaml+='<Canvas Cursor="Hand" RenderTransformOrigin="0.5,0.5" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="imageItem'+i+'" Width="'+img_width+'" Height="'+img_height+'">'+
			 '<Canvas.Clip>'+
					  '<PathGeometry>'+
				        '<PathGeometry.Figures>'+
				          '<PathFigure StartPoint="0,'+bezier+'">'+
				            '<PathFigure.Segments>'+
				              '<BezierSegment '+
				                'Point1="0,0" '+
				                'Point2="'+bezier+',0" '+
				                'Point3="'+bezier+',0" />'+
								'<LineSegment Point="'+(img_width-bezier)+',0" />'+
								'<BezierSegment '+
				                'Point1="'+(img_width-bezier)+',0" '+
				                'Point2="'+(img_width)+',0" '+
				                'Point3="'+(img_width)+','+bezier+'"/>'+
								'<LineSegment Point="'+(img_width)+','+(img_height-bezier)+'" />'+
								'<BezierSegment '+
				                'Point1="'+(img_width)+','+(img_height)+'" '+
				                'Point2="'+(img_width)+','+(img_height)+'" '+
				                'Point3="'+(img_width-bezier)+','+(img_height)+'"/>'+
								'<LineSegment Point="'+bezier+','+(img_height)+'" />'+
								'<BezierSegment '+
				                'Point1="0,'+(img_height)+'" '+
				                'Point2="0,'+(img_height)+'" '+
				                'Point3="0,'+(img_height-bezier)+'"/>'+
								'<LineSegment Point="0,'+bezier+'" />'+
				            '</PathFigure.Segments>'+
				          '</PathFigure>'+
				        '</PathGeometry.Figures>'+
				      '</PathGeometry>'+
					  '</Canvas.Clip>'+
					'<Canvas.Resources>'+
					   '<Storyboard x:Name="imageItem_hover'+i+'">'+
							'<DoubleAnimationUsingKeyFrames BeginTime="00:00:00" Storyboard.TargetName="imageItemShow'+i+'" Storyboard.TargetProperty="(UIElement.Opacity)">'+
							'<SplineDoubleKeyFrame KeyTime="00:00:00.1000000" Value="0.865"/>'+
							'<SplineDoubleKeyFrame KeyTime="00:00:00.7000000" Value="0"/>'+
						'</DoubleAnimationUsingKeyFrames>'+
					'</Storyboard>'+
					'<Storyboard x:Name="imateItem_out'+i+'">'+
						'<DoubleAnimationUsingKeyFrames BeginTime="00:00:00" Storyboard.TargetName="imageItemShow'+i+'" Storyboard.TargetProperty="(UIElement.Opacity)">'+
							'<SplineDoubleKeyFrame KeyTime="00:00:00.3000000" Value="0" KeySpline="0,1,1,1"/>'+
						'</DoubleAnimationUsingKeyFrames>'+
					'</Storyboard>'+
				'</Canvas.Resources>'+
						'<Canvas.RenderTransform>'+
						'<TransformGroup>'+
							'<ScaleTransform ScaleX="1" ScaleY="1" x:Name="imageItem'+i+'_scale"/>'+
							'<TranslateTransform X="0" Y="0"/>'+
						'</TransformGroup>'+
					'</Canvas.RenderTransform>'+
					 '<Image x:Name="image'+i+'" Source="'+obj.img+'" Stretch="Fill" Width="'+img_width+'" Height="'+img_height+'">'+
					'</Image>'+
					'<Rectangle Width="'+img_width+'" Height="'+img_height+'" Fill="#FFFFFFFF" Canvas.Left="0" Canvas.Top="0" Opacity="0" x:Name="imageItemShow'+i+'"/>'+
					 '</Canvas>';
			centerCxaml+='<Storyboard x:Name="ScaleShow'+i+'" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">'+
			'<DoubleAnimationUsingKeyFrames x:Name="ScaleShow_X'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)">'+
				'<SplineDoubleKeyFrame x:Name="ScaleShow_X_V'+i+'" KeyTime="00:00:00.8000000" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames x:Name="ScaleShow_Y'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)">'+
				'<SplineDoubleKeyFrame x:Name="ScaleShow_Y_V'+i+'" KeyTime="00:00:00.8000000" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames x:Name="TransShow_X'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)">'+
				'<SplineDoubleKeyFrame x:Name="TransShow_X_V'+i+'" KeyTime="00:00:00.6000000" KeySpline="0.608,0.029,1,1" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames x:Name="TransShow_Y'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)">'+
				'<SplineDoubleKeyFrame x:Name="TransShow_Y_V'+i+'" KeyTime="00:00:00.6000000" KeySpline="0.608,0.029,1,1" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames BeginTime="00:00:00" x:Name="OpacityShow'+i+'" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.Opacity)">'+
				'<SplineDoubleKeyFrame KeyTime="00:00:00.8000000" Value="1" x:Name="OpacityShow_V'+i+'"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
		'</Storyboard>'+
		
		'<Storyboard x:Name="ScaleHidden'+i+'" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">'+
			'<DoubleAnimationUsingKeyFrames x:Name="ScaleHidden_X'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)">'+
				'<SplineDoubleKeyFrame x:Name="ScaleHidden_X_V'+i+'" KeyTime="00:00:01.0000000" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames x:Name="ScaleHidden_Y'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)">'+
				'<SplineDoubleKeyFrame x:Name="ScaleHidden_Y_V'+i+'" KeyTime="00:00:01.0000000" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames x:Name="TransHidden_X'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)">'+
				'<SplineDoubleKeyFrame x:Name="TransHidden_X_V'+i+'" KeyTime="00:00:01.0000000" KeySpline="0.608,0.029,1,1" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames x:Name="TransHidden_Y'+i+'" BeginTime="00:00:00" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)">'+
				'<SplineDoubleKeyFrame x:Name="TransHidden_Y_V'+i+'" KeyTime="00:00:01.0000000" KeySpline="0.608,0.029,1,1" Value="0"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
			'<DoubleAnimationUsingKeyFrames BeginTime="00:00:00" x:Name="OpacityHidden'+i+'" Storyboard.TargetName="imageItem'+i+'" Storyboard.TargetProperty="(UIElement.Opacity)">'+
				'<SplineDoubleKeyFrame KeyTime="00:00:01.0000000" Value="1" x:Name="OpacityHidden_V'+i+'"/>'+
			'</DoubleAnimationUsingKeyFrames>'+
		'</Storyboard>';
		}
		centerCxaml+='</Canvas.Resources>'+xaml+
		'<Rectangle Width="392" Height="401" Fill="#FFFFFFFF" Opacity="0.58" Canvas.Left="-'+OX+'" Canvas.Top="'+(-OY)+'" Visibility="Collapsed" Canvas.ZIndex="99999" x:Name="top_window_bg">'+
		'</Rectangle>'+
		'</Canvas>';
		var centerC=sl_c.content.createFromXaml(centerCxaml);
		root.Children.Add(centerC);
		this.findName('top_window_bg').Width=sl_width;
		this.findName('top_window_bg').Height=sl_height;
		centerC.setValue("Canvas.Top",OY);		//动画中心
		centerC.setValue("Canvas.Left",OX);		//动画中心
		
		for(var i=0;i<Infos.length;i++)
		{
			var obj=Infos[i];
			obj.c=this.findName("imageItem"+i);
			obj.c.addEventListener("MouseEnter",Silverlight.createDelegate(this,function(s,e){
				this.mouseOver=true;
				if(!this.clicked){
					var id=s.name.substr(9,s.name.length-9);
					this.findName("imageItem_hover"+id).begin();
					this.findName("a_d").stop();
				}
			}));
			obj.c.addEventListener("MouseLeave",Silverlight.createDelegate(this,function(s,e){
				if(this.mouseOver&&!this.clicked){
					var id=s.name.substr(9,s.name.length-9);
					this.findName("imateItem_out"+id).begin();
					this.findName("a_d").begin();
				}
				this.mouseOver=false;
			}));
				this.findName("ScaleHidden"+i).addEventListener("Completed",Silverlight.createDelegate(this,function(s,e){
					var id=parseInt(s.name.substr(11,s.name.length-11));
					this.clicked=false;
					var obj=Infos[id];
					obj.c["Canvas.ZIndex"]=this.prevDownInfo.ZIndex;
					this.findName("image"+id).Source=this.prevDownInfo.img;
					this.prevDownInfo=null;
					if(!this.mouseOver)
						this.playAnimation();
				}));
			obj.c.addEventListener("MouseLeftButtonUp",Silverlight.createDelegate(this,function(s,e){
			if(!this.clicked){
				this.clicked=true;
				var id=parseInt(s.name.substr(9,s.name.length-9));
				var obj=Infos[id];
				var image=this.findName("image"+id),image_scale=this.findName('imageItem'+id+'_scale');
				var pic_btns=this.findName("pic_btns");
				this.prevDownInfo={"ZIndex":s["Canvas.ZIndex"],"Left":s["Canvas.Left"],"Top":s["Canvas.Top"],"img":image.Source,"w":s.Width,"h":s.Height,"sx":image_scale.ScaleX,"sy":image_scale.ScaleY,"id":id,"opacity":s.Opacity};
				image.Source=obj.bigpic;
				var sx=obj.width/image.width;
				var sy=obj.height/image.height;
				this.findName("ScaleShow_X_V"+id).Value=sx;
				this.findName("ScaleShow_Y_V"+id).Value=sy;
				var bestX=(sl_width/2)-(img_width/2)-OX;
				var bestY=(sl_height/2)-(img_height/2)-OY;
				this.findName("TransShow_X_V"+id).Value=bestX-s["Canvas.Left"];
				this.findName("TransShow_Y_V"+id).Value=bestY-s["Canvas.Top"];
				this.findName("OpacityShow_V"+id).Value=1;
				this.findName("ScaleShow"+id).begin();				
				s["Canvas.ZIndex"]=this.findName("top_window_bg")["Canvas.ZIndex"]+100;
				this.findName("top_window_bg").Visibility="Visible";
				pic_btns.Visibility="Visible";
				pic_btns["Canvas.ZIndex"]=s["Canvas.ZIndex"]+1;
				}else
				{
					
				}
			}));
		}
		
		this.amount=0.0;
		this.pace=0.01;
		this.duration=2;
		this.findName("a_d").addEventListener("Completed",Silverlight.createDelegate(this,this.playAnimation));
		this.playAnimation();
	},
	findName:function(n){
		return this.sl_c.content.findName(n);
	},
	hiddenClick:function(s,e){
		s.findName("hiddenBtn_down").begin();
			if(this.clicked){
				var id=this.prevDownInfo.id;
				var obj=Infos[id];
				var pic_btns=this.findName("pic_btns");
				this.findName("ScaleHidden_X_V"+id).Value=this.prevDownInfo.sx;
				this.findName("ScaleHidden_Y_V"+id).Value=this.prevDownInfo.sy;
				this.findName("TransHidden_X_V"+id).Value=0;
				this.findName("TransHidden_Y_V"+id).Value=0;
				this.findName("OpacityHidden_V"+id).Value=this.prevDownInfo.opacity;
				this.findName("top_window_bg").Visibility="Collapsed";
				pic_btns.Visibility="Collapsed";
				this.findName("ScaleHidden"+id).begin();
				}
		
	},
	playAnimation:function(){
	var nPer = (  240 - a_width) / a_height;
    nDX += nMaxSpeed*nPer;
	var h="";
		for(var i=0;i<Infos.length;i++)
		{
		    var objThis = this.findName("imageItem" + i);
	        var nFactor_x = (2 * Math.PI) * (i / Infos.length) + nDX;
	        var nFactor_c = (OX) * Math.cos (nFactor_x)+boundLeft;
	        var nFactor_s = (OY) * Math.sin (nFactor_x)+boundTop;
	        objThis.setValue("Canvas.Left", nFactor_c);
	        objThis.setValue("Canvas.Top", nFactor_s); 
	        var nCurZIndex = Math.floor(objThis.getValue("Canvas.Top"));
	        objThis.setValue("Canvas.ZIndex", nCurZIndex);
	        var nPerY = (objThis.getValue("Canvas.Top")-boundTop+OY)/ (a_height);
	        var nNewOpacity = 0.3 + (0.7*nPerY);
	        objThis.setValue("Opacity", nNewOpacity);
	        var nCurScale = 0.3 + ((1-0.3)*nPerY);
	        var oCurScale = objThis.findName('imageItem'+i+'_scale');
	        oCurScale.setValue("ScaleX", nCurScale);
	        oCurScale.setValue("ScaleY", nCurScale);
		}
		this.findName('a_d').begin();
	}
}
/*
 * czyjs.UI
 */
if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}

/*
 * PicShow类
 *  param.width 显示图片的完
 *	param.height 显示图片的高 
 *	param.count 图片的数量
 *	param.root  图片所在的目录
 *	param.picName  图列名 [pic] --列表示例为[pic0.jpg|pic1.jpg...]
 *	param.picType  图片后缀名称 [jpg|png..]
 *	param.contenterId 容器标答id
 *	param.second  间隔时间
 */
czyjs.UI.PicShow=Class.create();
czyjs.UI.PicShow.prototype = {

    initialize: function (param) {

        this.picArray = new Array();
        this.cur = 0;

        this.pic = {
            width: param.width,
            height: param.height,
            count: param.count,
            root: param.root,   //root/images
            picName: param.picName,
            picType: param.picType,  //jgp
            contenterId: param.randerTo,
            speed: param.second * 1000,
            id: param.id,
            className: param.className == null ? "czy-pic" : param.className
        }
        this.obj = document.getElementById(this.pic.contenterId) == null ? this.pic.contenterId : document.getElementById(this.pic.contenterId);

        this.pic.width = this.pic.width == null ? 500 : this.pic.width;
        this.pic.height = this.pic.height == null ? 300 : this.pic.height;
        this.pic.count = this.pic.count == null ? 0 : this.pic.count;
        this.pic.picType = this.pic.picType == null ? "jpg" : this.pic.picType;
        this.pic.speed = this.pic.speed == null ? 5000 : this.pic.speed;

        this.picElement = document.createElement("div");
        this.picElement.id = this.pic.id;
        this.picElement.className = this.pic.className;
        this.picElement.style.posWidth = this.pic.width;
        this.picElement.style.posHeight = this.pic.height;
        this.picElement.style.width = this.pic.width + "px";
        this.picElement.style.height = this.pic.height + "px";
        //if(this.picElement.parentNode!=null)

        //this.picElement.parentNode.style.position="relative";

        for (var i = 0; i < this.pic.count; i++) {
            this.imgObj = document.createElement("img");
            this.imgObj.src = this.pic.root + "/" + this.pic.picName + i + "." + this.pic.picType;
            this.imgObj.style.width = "100%";
            this.imgObj.style.height = "100%";
            this.picArray.push(this.imgObj);
            if (i == 0) {
                this.imgObj.style.display = "block";
            }
            else {
                this.imgObj.style.display = "none";
            }
            this.picElement.appendChild(this.imgObj);
        }

        //this.imgObj.style.filter="progid:DXImageTransform.Microsoft.RevealTrans(enabled=ture,transition=23)";
        //this.imgObj.style.filter="FILTER:revealTrans(Duration=4.0, Transition=23);"

        this.CreateNav(this.pic.count);
        this._changePic = BindAsEventListener(this, function () { this.ChangePic() });
        this._TimeChange = BindAsEventListener(this, function () { this.TimeChange() });
        this.obj.appendChild(this.picElement);

        addEventHandler(this.picElement, "click", this._changePic);


        if (this.pic.speed == null)
        { setTimeout(this._TimeChange, 1000); }
        else
        { setTimeout(this._TimeChange, this.pic.speed); }
    },
    /*
    * 创建导航
    */
    CreateNav: function (count) {


        this.navObj = document.createElement("div");
        this.navObj.id = this.pic.id + "_picNav";
        this.navObj.className = "pic-nav";

        for (var i = 0; i < count; i++) {
            var li = document.createElement("li");
            var a = document.createElement("a");
            a.innerHTML = i + 1;
            a.tid = i;
            a.href = "javascript:void(0);";
            a.style.textDecoration = "none";
            a.className = "pic-item";
            if (i == 0) {
                a.className = "pic-item-on";
            }
            // li.appendChild(a);
            //a.onclick=function(){ChangePic(i)};
            this.navObj.appendChild(a);
        }
        this.picElement.appendChild(this.navObj);


    },


    //隐藏图片
    ImgHidden: function () {

        //        if (this.picElement.filters != null) {
        //            if (czyjs.Util.CheckNavigator('ie6')) {

        //            }
        //            else {
        //                this.picElement.filters[0].Apply();
        //            }
        //            this.picArray[this.cur].style.display = "none";
        //        }
        //        else {
        var sb = new czyjs.Effect.StoryBoard(
       {
           id: this.picArray[this.cur],
           startOpacity: 100,
           endOpacity: 0,
           delayTime: 10,
           type: "opacity",
           speed: 20
       });
        sb.Start();
       // this.picArray[this.cur].style.display = "none";
        //        }

    },
    //显示图片
    ImgShow: function () {
        //        if (this.picElement.filters != null) {
        //            if (czyjs.Util.CheckNavigator('ie6')) {
        //                this.picArray[this.cur].style.display = "";
        //                this.picElement.filters[0].Play(duration = 2);
        //            }
        //            else {
        //                this.aphaValue1 = 0;
        //                this.picArray[this.cur].style.display = "block";
        //                this._OpacityChange = BindAsEventListener(this, function () { this._OpacityChange(this.picElement, aphaValue, 0) });
        //                this._OpacityChange;
        //            }
        //        }
        //   else {
       this.picArray[this.cur].style.display = "block";
        var sb = new czyjs.Effect.StoryBoard(
       {
           id: this.picArray[this.cur],
           startOpacity: 0,
           endOpacity: 100,
           delayTime: 10,
           type: "opacity",
           speed: 20
       });
        sb.Start();
     
        // }
    },
    //点击图片变换
    ChangePic: function () {

        var o = czyjs.Event.GetEventObj();
        if (o.parentNode.id == this.pic.id + "_picNav" && o.tagName == 'A') {
            this.navObj.childNodes[this.cur].className = "pic-item";

            this.ImgHidden();

            this.cur = o.tid;

            this.ImgShow();
            o.className = "pic-item-on";

        }

    },

    //自动变换图片
    TimeChange: function () {

        this.navObj.childNodes[this.cur].className = "pic-item";

        this.ImgHidden();

        if (this.cur == this.pic.count - 1) {
            this.cur = 0;
        }
        else {
            this.cur += 1;
        }

        this.ImgShow();

        this.navObj.childNodes[this.cur].className = "pic-item-on";
        if (this.pic.speed == null)
        { setTimeout(this._TimeChange, 1000); }
        else
        { setTimeout(this._TimeChange, this.pic.speed); }

    }
}
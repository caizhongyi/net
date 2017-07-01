if(typeof(czyjs.Effect)=="undefined")
{
    czyjs.Effect = {};
}

//************************
//锟斤拷锟斤拷锟斤拷锟斤拷锟狡讹拷效锟斤拷
//************************
//obj   锟斤拷锟斤拷
//initStr         锟斤拷始
//changeStr       锟戒化
//type 锟斤拷锟斤拷
//***************************************
czyjs.Effect.OnMouseMove=Class.create();
czyjs.Effect.OnMouseMove.prototype = {
    initialize: function (params) {
        this.obj = document.getElementById(params.id);                                  //为列表或单一元素
        this.type = params.prototype == null ? "backgroundColor" : params.prototype;    //变化的属性
        this.initStyle = params.initStyle;                                              //初化属性值
        this.changeStyle = params.changeStyle;                                          //变化的属性值
        this.tag = params.tag;                                                       //对像内为tagName为此名称的变化


//        switch (this.type) {
//            case "backgroundColor": this.obj.style.backgroundColor = this.initStyle; break;
//            case "color": this.obj.style.color = this.initStyle; break;
//            case "backgroundImage": this.obj.style.backgroundImage = this.initStyle; break;
//            case "src": this.obj.src = this.initStyle; break;
//        }

        addEventHandler(this.obj, "mouseover", BindAsEventListener(this, this.Change));
        addEventHandler(this.obj, "mouseout", BindAsEventListener(this, this.ReChange));

    },
    Change: function () {
        var o = czyjs.Event.GetEventObj();

        if (o.tag != null) {
            if (o.tag == this.tag) {
                switch (this.type) {
                    case "backgroundColor": o.style.backgroundColor = this.changeStyle; break;
                    case "color": o.style.color = this.changeStyle; break;
                    case "backgroundImage": o.style.backgroundImage = this.changeStyle; break;
                    case "src": o.src = this.changeStyle; break;
                }
            }
        }
    },
    ReChange: function () {
        var o = czyjs.Event.GetEventObj();

        if (o.tag != null) {
            if (o.tag == this.tag) {
                switch (this.type) {
                    case "backgroundColor": o.style.backgroundColor = this.initStyle; break;
                    case "color": o.style.color = this.initStyle; break;
                    case "backgroundImage": o.style.backgroundImage = this.initStyle; break;
                    case "src": o.src = this.initStyle; break;
                }
            }
        }
    }
}

//************************
//锟斤拷锟斤拷锟戒化
//************************
//obj   锟斤拷锟斤拷
//initStr         锟斤拷始
//changeStr       锟戒化
//type 锟斤拷锟斤拷
//***************************************
czyjs.Effect.OnClick=Class.create();
czyjs.Effect.OnClick.prototype = {
    initialize: function (params) {
        this.obj = document.getElementById(params.id);                                  //为列表或单一元素
        this.type = params.prototype == null ? "backgroundColor" : params.prototype;    //变化的属性
        this.initStyle = params.initStyle;                                              //初化属性值
        this.changeStyle = params.changeStyle;                                          //变化的属性值
        this.tag = params.tag;                                                       //对像内为tagName为此名称的变化
        this.obj.isClick = false;
//        switch (this.type) {
//            case "backgroundColor": this.obj.style.backgroundColor = this.initStyle; break;
//            case "color": this.obj.style.color = this.initStyle; break;
//            case "backgroundImage": this.obj.style.backgroundImage = this.initStyle; break;
//            case "src": this.obj.src = this.initStyle; break;
//        }
        addEventHandler(document, "click", BindAsEventListener(this, this.Change));

    },
    Change: function () {
        var o = czyjs.Event.GetEventObj(); 
        if (o.tag != null) {
      
            if (o.tag == this.tag) {
               
                switch (this.type) {
                    case "backgroundColor":
                        if (!o.isClick || o.isClick == null) {

                            o.style.backgroundColor = this.changeStyle;
                            o.isClick = true;
                        }
                        else {
                            o.style.backgroundColor = this.initStyle;
                            o.isClick = false;
                        }

                        break;
                    case "color":

                        if (!o.isClick || o.isClick == null) {
                            o.style.color = this.initStyle;
                            o.isClick = true;
                        }
                        else {
                            o.style.color = this.changeStyle;
                            o.isClick = false;
                        }
                        break;
                    case "backgroundImage":

                        if (o.isClick || o.isClick == null) {
                            o.backgroundImage = this.initStyle;
                            o.isClick = true;
                        }
                        else {
                            oj.backgroundImg = this.changeStyle;
                            o.isClick = false;
                        }
                        break;
                    case "src":

                        if (!o.isClick || o.isClick == null) {
                            o.src = this.initStyle;
                            o.isClick = true;
                        }
                        else {
                            o.src = this.changeStyle;
                            o.isClick = false;
                        }
                        break;
                }
            }
        }
    }
}

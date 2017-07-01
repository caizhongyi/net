if (typeof (czyjs.Effect) == "undefined") {
    czyjs.Effect = {};
};


czyjs.Effect.StoryBoard=Class.create();
czyjs.Effect.StoryBoard.prototype = {
    /*
    * id:对像id
    *startPoint {x:10,y:10}
    *endPoint {x:10,y:10}
    *delayTime 延迟时间(越小越快,默认为1000)
    *speed 越小越快(默认为10)
    */
    initialize: function (param) {

        this.obj = document.getElementById(param.id) == null ? param.id : document.getElementById(param.id);
        this.delayTime = param.delayTime;
        this.speed = param.speed;         //增长速度
        this.type = param.type;           //动画类型
        this.isStop = true;              //是否停止
        this.changingEvent = param.changingEvent == null ? function (e, x, y) { } : param.changingEvent;
        this.changedEvent = param.changedEvent == null ? function (e, x, y) { } : param.changedEvent;
        if (this.delayTime == null) {
            this.delayTime = 10;
        }
        this.isStopX = true;
        this.isStopY = true;
        this.typeConfig = {
            position: "position",            //位置变化
            opacity: "opacity",              //透明度变化
            size: "size",                    //大小变化
            zoom: "zoom",                    //放缩
            shiftPosition: "shiftPosition",  //变速运动
            action: "action"
        }

        switch (this.type) {
            case this.typeConfig.position: this.MoveInit(param); break;
            case this.typeConfig.opacity: this.OpacityInit(param); break;
            case this.typeConfig.size: this.SizeInit(param); break;
            case this.typeConfig.zoom: this.ZoomInit(param); break;
            case this.typeConfig.shiftPosition: this.ShiftPositionInit(param); break;
            case this.typeConfig.action: this.ActionInit(param); break;
        }


    },
    /*
    *开始播放
    */
    Start: function () {


        if (this.isStop) {
            this.isStop = false;
            this.isStopX = false;
            this.isStopY = false;
            switch (this.type) {
                case this.typeConfig.position: this.MoveStart(); break;
                case this.typeConfig.opacity: this.OpacityChange(); break;
                case this.typeConfig.size: this.SizeChange(); break;
                case this.typeConfig.zoom: this.ZoomChange(); break;
                case this.typeConfig.shiftPosition: this.ShiftPositionChange(); break;
                case this.typeConfig.action: setTimeout(this.ActionChange.bind(this), this.delayTime); break;
            }

        }



    },
    /*
    *停止播放
    */
    Stop: function () {

        this.isStop = true;

    },

    /*
    *回播 (该函数只对Action类型的运动有效)
    */
    Reverse: function () {

        this.isReverse = true;

    },
    /*
    *顺播 (该函数只对Action类型的运动有效)
    */
    Order: function () {

        this.isReverse = false;

    },
    /************************位置变化***************************/
    //prviate
    ShiftPositionInit: function (param) {
        this.startPoint = param.startPoint;
        this.endPoint = param.endPoint;
        this.obj.style.position = "absolute";
        this.speedUp = param.speedUp == null ? false : param.speedUp;   //True为加速,false为减速

        if (this.startPoint == null) {
            var x = this.obj.style.left == null ? 0 : this.obj.style.left.replace("px", "");
            var y = this.obj.style.top == null ? 0 : this.obj.style.top.replace("px", "");
            this.startPoint = {
                x: parseFloat(x),
                y: parseFloat(y)
            }

        }
        this.obj.style.left = this.startPoint.x + "px";
        this.obj.style.top = this.startPoint.y + "px";

        this.pixdiffX = this.endPoint.x - this.startPoint.x;
        this.pixdiffY = this.endPoint.y - this.startPoint.y;

        var percent = this.pixdiffY == 0 ? 0 : this.pixdiffX / this.pixdiffY;
        percent = percent < 0 ? -percent : percent;


        this.speed = this.speed == null ? 50 : this.speed;
        //this.incrementX =this.pixdiffX!=0 ?this.pixdiffX/this.speed:0;
        //this.incrementY=this.pixdiffY!=0?this.pixdiffY/this.speed:0;
        // this.speed=10;
        this.incrementX = this.pixdiffX < 0 ? -this.speed : this.speed;
        var tempY = percent == 0 ? this.incrementX : this.incrementX / percent;
        this.incrementY = this.pixdiffX != 0 ? tempY : this.speed;
        this.incrementY = this.pixdiffY < 0 ? -this.incrementY : this.incrementY;


    },
    //prviate
    //位置变化
    ShiftPositionChange: function () {

        if (this.isStop) {
            this.changedEvent(this.obj, this.incrementX, this.incrementY);
            return;
        }

        if (this.incrementX > 0) {
            if (this.startPoint.x <= this.endPoint.x) {
                if (this.startPoint.x + this.incrementX <= this.endPoint.x) {
                    this.startPoint.x += this.incrementX;
                    this.percentX = this.incrementX / (this.endPoint.x - this.startPoint.x);
                    this.percentX = this.percentX < 0 ? 0 : this.percentX;
                    if (this.speedUp) {
                        this.incrementX += this.incrementX * this.percentX;
                    }
                    else {
                        this.incrementX -= this.incrementX * this.percentX;
                    }
                }
                else {
                    this.incrementX = (this.endPoint.x - this.startPoint.x);
                    this.startPoint.x += (this.endPoint.x - this.startPoint.x);
                    this.percentX = (this.endPoint.x - this.startPoint.x) / (this.endPoint.x - this.startPoint.x);
                    this.percentX = this.percentX < 0 ? 0 : this.percentX;
                    if (this.speedUp) {
                        this.incrementX += (this.endPoint.x - this.startPoint.x) * this.percentX;
                    }
                    else {
                        this.incrementX -= (this.endPoint.x - this.startPoint.x) * this.percentX;
                    }

                }

                this.obj.style.left = this.startPoint.x + "px";

            }
            else {
                this.isStopX = true;
            }
        }
        else if (this.incrementX < 0) {
            if (this.startPoint.x >= this.endPoint.x) {
                if (this.startPoint.x + this.incrementX >= this.endPoint.x) {
                    this.startPoint.x += this.incrementX;
                    this.percentX = this.incrementX / (this.endPoint.x - this.startPoint.x);
                    this.percentX = this.percentX < 0 ? 0 : this.percentX;
                    if (this.speedUp) {
                        this.incrementX += this.incrementX * this.percentX;
                    }
                    else {
                        this.incrementX -= this.incrementX * this.percentX;
                    }
                }
                else {
                    this.incrementX = (this.endPoint.x - this.startPoint.x);
                    this.startPoint.x += (this.endPoint.x - this.startPoint.x);
                    this.percentX = (this.endPoint.x - this.startPoint.x) / (this.endPoint.x - this.startPoint.x);
                    this.percentX = this.percentX < 0 ? 0 : this.percentX;
                    if (this.speedUp) {
                        this.incrementX += (this.endPoint.x - this.startPoint.x) * this.percentX;
                    }
                    else {
                        this.incrementX -= (this.endPoint.x - this.startPoint.x) * this.percentX;
                    }

                }

                this.obj.style.left = this.startPoint.x + "px";
            }
            else {
                this.isStopX = true;
            }
        }
        else {
            this.isStopX = true;
        }

        if (this.incrementY > 0) {
            if (this.startPoint.y <= this.endPoint.y) {
                if (this.startPoint.y + this.incrementY <= this.endPoint.y) {
                    this.startPoint.y += this.incrementY;
                    this.percentY = this.incrementY / (this.endPoint.y - this.startPoint.y);
                    this.percentY = this.percentY < 0 ? 0 : this.percentY;
                    if (this.speedUp) {
                        this.incrementY += this.incrementY * this.percentY;
                    }
                    else {
                        this.incrementY -= this.incrementY * this.percentY;
                    }
                }
                else {
                    this.incrementY = (this.endPoint.y - this.startPoint.y);
                    this.startPoint.y += (this.endPoint.y - this.startPoint.y);
                    this.percentY = (this.endPoint.y - this.startPoint.y) / (this.endPoint.y - this.startPoint.y);
                    this.percentY = this.percentY < 0 ? 0 : this.percentY;
                    if (this.speedUp) {
                        this.incrementY += (this.endPoint.y - this.startPoint.y) * this.percentY;
                    }
                    else {
                        this.incrementY -= (this.endPoint.y - this.startPoint.y) * this.percentY;
                    }

                }

                this.obj.style.top = this.startPoint.y + "px";
            }
            else {
                this.isStopY = true;
            }
        }
        else if (this.incrementY < 0) {
            if (this.startPoint.y >= this.endPoint.y) {
                if (this.startPoint.y + this.incrementY >= this.endPoint.y) {
                    this.startPoint.y += this.incrementY;
                    this.percentY = this.incrementY / (this.endPoint.y - this.startPoint.y);
                    this.percentY = this.percentY < 0 ? 0 : this.percentY;
                    if (this.speedUp) {
                        this.incrementY += this.incrementY * this.percentY;
                    }
                    else {
                        this.incrementY -= this.incrementY * this.percentY;
                    }
                }
                else {
                    this.incrementY = (this.endPoint.y - this.startPoint.y);
                    this.startPoint.y += (this.endPoint.y - this.startPoint.y);
                    this.percentY = (this.endPoint.y - this.startPoint.y) / (this.endPoint.y - this.startPoint.y);
                    this.percentY = this.percentY < 0 ? 0 : this.percentY;
                    if (this.speedUp) {
                        this.incrementY += (this.endPoint.y - this.startPoint.y) * this.percentY;
                    }
                    else {
                        this.incrementY -= (this.endPoint.y - this.startPoint.y) * this.percentY;
                    }

                }

                this.obj.style.top = this.startPoint.y + "px";
            }
            else {
                this.isStopY = true;
            }
        }
        else {
            this.isStopY = true;
        }




        if (!(this.isStopX && this.isStopY)) {
            this.changingEvent(this.obj, this.incrementX, this.incrementY);
            setTimeout(this.ShiftPositionChange.bind(this), this.delayTime);
        }
        else {
            this.changedEvent(this.obj, this.incrementX, this.incrementY);
        }
    },

    /************************位置变化***************************/
    //prviate
    MoveInit: function (param) {
        this.startPoint = param.startPoint;
        this.endPoint = param.endPoint;
        this.obj.style.position = "absolute";
        if (this.startPoint == null) {
            var x = this.obj.style.left == null ? 0 : this.obj.style.left.replace("px", "");
            var y = this.obj.style.top == null ? 0 : this.obj.style.top.replace("px", "");
            this.startPoint = {
                x: parseFloat(x),
                y: parseFloat(y)
            }

        }
        this.obj.style.left = this.startPoint.x + "px";
        this.obj.style.top = this.startPoint.y + "px";

        this.pixdiffX = this.endPoint.x - this.startPoint.x;
        this.pixdiffY = this.endPoint.y - this.startPoint.y;

        var percent = this.pixdiffY == 0 ? 0 : this.pixdiffX / this.pixdiffY;
        percent = percent < 0 ? -percent : percent;
        this.speed = this.speed == null ? 50 : this.speed;

        this.incrementX = this.pixdiffX < 0 ? -this.speed : this.speed;
        var tempY = percent == 0 ? this.incrementX : this.incrementX / percent;
        this.incrementY = this.pixdiffX != 0 ? tempY : this.speed;
        this.incrementY = this.pixdiffY < 0 ? -this.incrementY : this.incrementY;

    },
    //prviate
    //位置变化
    MoveStart: function () {

        if (this.isStop) {
            this.changedEvent(this.obj, this.incrementX, this.incrementY);
            return;
        }

        if (this.incrementX > 0) {
            if (this.startPoint.x <= this.endPoint.x) {
                if (this.startPoint.x + this.incrementX <= this.endPoint.x) {
                    this.startPoint.x += this.incrementX;
                }
                else {
                    this.incrementX = this.endPoint.x - this.startPoint.x;
                    this.startPoint.x += this.endPoint.x - this.startPoint.x;

                }

                this.obj.style.left = this.startPoint.x + "px";

                //  setTimeout(this.MoveStart.bind(this), this.delayTime);
            }
            else {
                this.isStopX = true;
            }
        }
        else if (this.incrementX < 0) {
            if (this.startPoint.x >= this.endPoint.x) {
                if (this.startPoint.x + this.incrementX >= this.endPoint.x) {
                    this.startPoint.x += this.incrementX;
                }
                else {
                    this.incrementX = this.endPoint.x - this.startPoint.x;
                    this.startPoint.x += this.endPoint.x - this.startPoint.x;

                }

                this.obj.style.left = this.startPoint.x + "px";
                // setTimeout(this.MoveStart.bind(this), this.delayTime);

            }
            else {
                this.isStopX = true;
            }
        }
        else {
            this.isStopX = true;
        }

        if (this.incrementY > 0) {
            if (this.startPoint.y <= this.endPoint.y) {
                if (this.startPoint.y + this.incrementY <= this.endPoint.y) {
                    this.startPoint.y += this.incrementY;
                }
                else {
                    this.incrementY = this.endPoint.y - this.startPoint.y;
                    this.startPoint.y += this.endPoint.y - this.startPoint.y;

                }

                this.obj.style.top = this.startPoint.y + "px";

                //setTimeout(this.MoveStart.bind(this), this.delayTime);
            }
            else {
                this.isStopY = true;
            }
        }
        else if (this.incrementY < 0) {
            if (this.startPoint.y >= this.endPoint.y) {
                if (this.startPoint.y + this.incrementY >= this.endPoint.y) {
                    this.startPoint.y += this.incrementY;
                }
                else {
                    this.incrementY = this.endPoint.y - this.startPoint.y;
                    this.startPoint.y += this.endPoint.y - this.startPoint.y;

                }

                this.obj.style.top = this.startPoint.y + "px";
            }
            else {
                this.isStopY = true;
            }
        }
        else {
            this.isStopY = true;
        }

        if (!(this.isStopX && this.isStopY)) {
            this.changingEvent(this.obj, this.incrementX, this.incrementY);
            setTimeout(this.MoveStart.bind(this), this.delayTime);
        }
        else
        { this.changedEvent(this.obj, this.incrementX, this.incrementY); }
    },
    /************************透明度变化***************************/
    OpacityInit: function (param) {
        //         var objOpacity=null;
        //         if(document.all)
        //         {
        //            objOpacity=this.obj.style.filter.toLowerCase().indexOf("opacity") !=-1 ?this.obj.style.filter.replace("(opacity=").replace(")"):param.opacity;
        //         }
        //         else
        //         {
        //            objOpacity=this.obj.style.opacity ?this.obj.style.opacity:param.opacity;
        //            objOpacity=objOpacity*100;
        //         }
        //var objOpacity=document.all ? 
        //this.opacity=objOpacity==null?param .opacity:objOpacity;
        this.startOpacity = param.startOpacity == null ? 100 : param.startOpacity;  //初始透明度
        this.endOpacity = param.endOpacity == null ? 0 : param.endOpacity;  //初始透明度
        this.speed = param.speed == null ? 50 : param.speed;   //增长速度
        this.speed = this.speed < 0 ? -this.speed : this.speed;
        this.speed = this.startOpacity - this.endOpacity > 0 ? -this.speed : this.speed;

        this.obj.style.position = "absolute";
        this.obj.style.filter = "alpha(opacity=" + this.opacity + ")";
        this.obj.style.opacity = this.opacity / 100;

    },
    OpacityChange: function () {
        if (this.isStop) {
            this.changedEvent(this.obj, this.speed);
            return;
        }

        if (this.speed < 0) {
            if (this.startOpacity > this.endOpacity) {
                this.startOpacity += this.speed;
                this.obj.style.filter = "alpha(opacity=" + this.startOpacity + ")";
                this.obj.style.opacity = this.startOpacity / 100;
                this.changingEvent(this.obj, this.speed);
                setTimeout(this.OpacityChange.bind(this), this.delayTime);
            }
            else {
                this.changedEvent(this.obj, this.speed);
            }
        }


        if (this.speed > 0) {
            if (this.startOpacity < this.endOpacity) {
                this.startOpacity += this.speed;
                this.obj.style.filter = "alpha(opacity=" + this.startOpacity + ")";
                this.obj.style.opacity = this.startOpacity / 100;
                this.changingEvent(this.obj, this.speed);
                setTimeout(this.OpacityChange.bind(this), this.delayTime);
            }
            else {
                this.changedEvent(this.obj, this.speed);
            }
        }

    },
    /************************大小变化***************************/
    SizeInit: function (param) {
        var width = this.obj.style.width != null ? parseFloat(this.obj.style.width.replace("px", "")) : 0;
        var height = this.obj.style.height != null ? parseFloat(this.obj.style.height.replace("px", "")) : 0;
        this.startSize = param.startSize == null ? { w: width, h: height} : param.startSize;   //开始大小
        this.endSize = param.endSize;                                          //结束大小 
        this.speed = param.speed == null ? 50 : param.speed;                           //增长速度
        this.obj.style.position = "absolute";

        this.pixdiffW = this.endSize.w - this.startSize.w;
        this.pixdiffH = this.endSize.h - this.startSize.h;

        var percent = this.pixdiffH == 0 ? 0 : this.pixdiffW / this.pixdiffH;
        percent = percent < 0 ? -percent : percent;
        this.speed = this.speed == null ? 10 : this.speed;
        this.speed = this.speed < 0 ? -this.speed : this.speed;
        //this.incrementX =this.pixdiffX!=0 ?this.pixdiffX/this.speed:0;
        //this.incrementY=this.pixdiffY!=0?this.pixdiffY/this.speed:0;
        // this.speed=10;
        this.incrementW = this.pixdiffW < 0 ? -this.speed : this.speed;
        var tempH = this.incrementW;
        if (percent != 0) {
            tempH = (this.incrementW / percent) < 0 ? -this.incrementW / percent : this.incrementW / percent;
        }
        this.incrementH = this.pixdiffW != 0 ? tempH : this.speed;
        this.incrementH = this.pixdiffH < 0 ? -this.incrementH : this.incrementH;


        this.obj.style.width = this.startSize.w + "px";
        this.obj.style.height = this.startSize.h + "px";
    },
    SizeChange: function () {
        if (this.isStop) {
            this.changedEvent(this.obj, this.incrementW, this.incrementH);
            return;
        }

        if (this.incrementW > 0) {
            if (this.startSize.w <= this.endSize.w) {
                if (this.startSize.w + this.incrementW <= this.endSize.w) {
                    this.startSize.w += this.incrementW;
                }
                else {
                    this.incrementW = this.endSize.w - this.startSize.w;
                    this.startSize.w += this.endSize.w - this.startSize.w;

                }

                this.startSize.w = this.startSize.w < 0 ? 0 : this.startSize.w;
                this.obj.style.width = this.startSize.w + "px";
                // setTimeout(this.SizeChange.bind(this), this.delayTime);
            }
            else {
                this.isStopX = true;
            }
        }
        else if (this.incrementW < 0) {
            if (this.startSize.w >= this.endSize.w) {
                if (this.startSize.w + this.incrementW >= this.endSize.w) {
                    this.startSize.w += this.incrementW;
                }
                else {
                    this.incrementW = this.endSize.w - this.startSize.w;
                    this.startSize.w += this.endSize.w - this.startSize.w;

                }

                this.startSize.w = this.startSize.w < 0 ? 0 : this.startSize.w;
                this.obj.style.width = this.startSize.w + "px";
                // setTimeout(this.SizeChange.bind(this), this.delayTime);
            }
            else {
                this.isStopX = true;
            }
        }
        else {
            this.isStopX = true;
        }

        if (this.incrementH > 0) {
            if (this.startSize.h <= this.endSize.h) {
                if (this.startSize.h + this.incrementH <= this.endSize.h) {
                    this.startSize.h += this.incrementH;
                }
                else {
                    this.incrementH = this.endSize.h - this.startSize.h;
                    this.startSize.h += this.endSize.h - this.startSize.h;

                }

                this.startSize.h = this.startSize.h < 0 ? 0 : this.startSize.h;
                this.obj.style.height = this.startSize.h + "px";

                // setTimeout(this.SizeChange.bind(this), this.delayTime);
            }
            else {
                this.isStopY = true;
            }
        }
        else if (this.incrementH < 0) {
            if (this.startSize.h >= this.endSize.h) {
                if (this.startSize.h + this.incrementH >= this.endSize.h) {
                    this.startSize.h += this.incrementH;
                }
                else {
                    this.incrementH = this.endSize.h - this.startSize.h;
                    this.startSize.h += this.endSize.h - this.startSize.h;

                }

                this.startSize.h = this.startSize.h < 0 ? 0 : this.startSize.h;
                this.obj.style.height = this.startSize.h + "px";

                //setTimeout(this.SizeChange.bind(this), this.delayTime);
            }
            else {
                this.isStopY = true;
            }
        }
        else {
            this.isStopY = true;
        }


        if (!(this.isStopX && this.isStopY)) {
            this.changingEvent(this.obj, this.incrementW, this.incrementH);
            setTimeout(this.SizeChange.bind(this), this.delayTime);
        }
        else {
            this.changedEvent(this.obj, this.incrementW, this.incrementH);
        }
    },
    /************************放缩***************************/
    ZoomInit: function (param) {
        var width = this.obj.style.width != null ? parseFloat(this.obj.style.width.replace("px", "")) : 0;
        var height = this.obj.style.height != null ? parseFloat(this.obj.style.height.replace("px", "")) : 0;
        var left = this.obj.style.left != null ? parseFloat(this.obj.style.left.replace("px", "")) : 0;
        var top = this.obj.style.top != null ? parseFloat(this.obj.style.top.replace("px", "")) : 0;

        this.startSize = param.startSize == null ? { w: width, h: height} : param.startSize;   //开始大小
        this.position = param.position == null ? { x: left, y: top} : param.position;
        this.endSize = param.endSize;  //结束大小 

        this.speed = param.speed = null ? 50 : param.speed;                           //增长速度
        this.obj.style.position = "absolute";

        this.pixdiffW = this.endSize.w - this.startSize.w;
        this.pixdiffH = this.endSize.h - this.startSize.h;

        var percent = this.pixdiffH == 0 ? 0 : this.pixdiffW / this.pixdiffH;

        percent = percent < 0 ? -percent : percent;
        this.speed = this.speed < 0 ? -this.speed : this.speed;

        this.incrementW = this.pixdiffW < 0 ? -this.speed : this.speed;
        var tempH = this.incrementW;
        if (percent != 0) {
            tempH = (this.incrementW / percent) < 0 ? -this.incrementW / percent : this.incrementW / percent;
        }

        this.incrementH = this.pixdiffW != 0 ? tempH : this.speed;
        this.incrementH = this.pixdiffH < 0 ? -this.incrementH : this.incrementH;
        this.obj.style.width = this.startSize.w + "px";
        this.obj.style.left = this.position.x + "px";
        this.obj.style.height = this.startSize.h + "px";
        this.obj.style.top = this.position.y + "px";


    },
    ZoomChange: function () {
        if (this.isStop) {
            this.changedEvent(this.obj, this.incrementW, this.incrementH);
            return;
        }

        if (this.incrementW > 0) {
            if (this.startSize.w <= this.endSize.w) {
                if (this.startSize.w + this.incrementW <= this.endSize.w) {
                    this.startSize.w += this.incrementW;
                    this.position.x -= this.incrementW / 2;
                }
                else {
                    this.incrementW = this.endSize.w - this.startSize.w;
                    this.startSize.w += this.incrementW;
                    this.position.x -= this.incrementW / 2;

                }

                this.startSize.w = this.startSize.w < 0 ? 0 : this.startSize.w;
                this.obj.style.width = this.startSize.w + "px";
                this.obj.style.left = this.position.x + "px";
           
                //setTimeout(this.SizeChange.bind(this), this.delayTime);
            }
            else {
                this.isStopX = true;
            }
        }
        else if (this.incrementW < 0) {
            if (this.startSize.w >= this.endSize.w) {
                if (this.startSize.w + this.incrementW >= this.endSize.w) {
                    this.startSize.w += this.incrementW;
                    this.position.x -= this.incrementW / 2;
                }
                else {
                    this.incrementW = this.endSize.w - this.startSize.w;
                    this.startSize.w += this.incrementW;
                    this.position.x -= this.incrementW / 2;

                }

                this.startSize.w = this.startSize.w < 0 ? 0 : this.startSize.w;
                this.obj.style.width = this.startSize.w + "px";
                this.obj.style.left = this.position.x + "px";
                //  setTimeout(this.SizeChange.bind(this), this.delayTime);
            }
            else {
                this.isStopX = true;
            }
        }
        else {
            this.isStopX = true;
        }

        if (this.incrementH > 0) {

            if (this.startSize.h <= this.endSize.h) {
                if (this.startSize.h + this.incrementH <= this.endSize.h) {
                    this.startSize.h += this.incrementH;
                    this.position.y -= this.incrementH / 2;

                }
                else {
                    this.incrementH = (this.endSize.h - this.startSize.h);
                    this.startSize.h += this.incrementH;
                    this.position.y -= this.incrementH / 2;
                }

                this.startSize.h = this.startSize.h < 0 ? 0 : this.startSize.h;
                this.obj.style.height = this.startSize.h + "px";
                this.obj.style.top = this.position.y + "px";

                //  setTimeout(this.ZoomChange.bind(this), this.delayTime);
            }
            else {
                this.isStopY = true;
            }
        }
        else if (this.incrementH < 0) {
            if (this.startSize.h >= this.endSize.h) {
                if (this.startSize.h + this.incrementH >= this.endSize.h) {
                    this.startSize.h += this.incrementH;
                    this.position.y -= this.incrementH / 2;
                }
                else {
                    this.incrementH = (this.endSize.h - this.startSize.h);
                    this.startSize.h += this.incrementH;
                    this.position.y -= this.incrementH / 2;


                }

                this.startSize.h = this.startSize.h < 0 ? 0 : this.startSize.h;
                this.obj.style.height = this.startSize.h + "px";
                this.obj.style.top = this.position.y + "px";

                // setTimeout(this.ZoomChange.bind(this), this.delayTime);
            }
            else {
                this.isStopY = true;
            }
        }
        else {
            this.isStopY = true;
        }

        if (!(this.isStopX && this.isStopY)) {

            this.changingEvent(this.obj, this.incrementW, this.incrementH);
            setTimeout(this.ZoomChange.bind(this), this.delayTime);
        }
        else {
            this.changedEvent(this.obj, this.incrementW, this.incrementH);
        }
    },
    /************************动画侦播放***************************/
    ActionInit: function (param) {
        this.arrayImageUrl = param.arrayImageUrl;
        this.property = param.property == null ? "backgroundImage" : param.property; //类型为两种 src或backgroundImage
        this.isLoop = param.isLoop == null ? true : param.isLoop; //是否循环播放
        this.isReverse = param.isReverse ? false : param.isReverse; //是否倒播
        this.index = 0;
        if (this.property == "backgroundImage") {
            this.obj.style.backgroundImage = "url(" + this.arrayImageUrl[0] + ")";
        }
        if (this.property == "src") {
            this.obj.src = this.arrayImageUrl[0];
        }

        if (this.isReverse) {
            if (this.property == "backgroundImage") {
                this.obj.style.backgroundImage = "url(" + this.arrayImageUrl[this.arrayImageUrl.length - 1]; +")";
            }
            if (this.property == "src") {
                this.obj.src = this.arrayImageUrl[this.arrayImageUrl.length - 1];
            }
            this.index = this.arrayImageUrl.length - 1;

        }

    },
    ActionChange: function () {

        if (this.isStop) {
            this.changedEvent(this.obj);
            return;
        }
        if (this.index < this.arrayImageUrl.length) {

            if (this.isReverse) {
                this.index--;
            }
            else {
                this.index++;
            }

            if (this.isLoop) {
                if (this.isReverse) {
                    if (this.index < 0) {
                        this.index = this.arrayImageUrl.length - 1;
                    }

                }
                else {
                    if (this.index > this.arrayImageUrl.length - 1) {
                        this.index = 0;
                    }
                }

            }
            else {
                if (this.isReverse) {
                    if (this.index < 0) {
                        this.changedEvent(this.obj);
                    }

                }
                else {
                    if (this.index > this.arrayImageUrl.length - 1) {
                        this.changedEvent(this.obj);
                    }
                }
            }

            if (this.property == "backgroundImage") {
                this.obj.style.backgroundImage = "url(" + this.arrayImageUrl[this.index] + ")";
            }
            if (this.property == "src") {
                this.obj.src = this.arrayImageUrl[this.index];
            }

            if (this.isLoop) {
                this.changingEvent(this.obj);
                setTimeout(this.ActionChange.bind(this), this.delayTime);
            }



        }

    }
}
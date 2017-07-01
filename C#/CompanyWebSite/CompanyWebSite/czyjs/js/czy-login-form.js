
if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}


//************************
//弹出JS渐变窗口
//************************
//formContent   -内容
//width         -宽
//height        -高
//contenterId   -加载元素ID
//speed         -渐变速度
//isPicture     -是否为内容显示为图片
//***************************************
czyjs.UI.LoginForm=Class.create();
czyjs.UI.LoginForm.prototype = {
    initialize: function (param) {

        //属性
        this.userName = "";
        this.passWord = "";

        //样式列表
        this.styles = {
            login_form: "czy-login",
            login_text: "czy-login-textBox",
            login_text_on: "czy-login-textBoxOn",
            login_button: "czy-login-button"
            //login_form    :  "",
        }
        this.fonts = {
            user_pwd: "密码",
            user_text: "用户名",
            validate: "验证码",
            login_button: "登陆",
            reset_button: "重置"
        }

        this.CreateUI(param);

    },

    CreateUI: function (param) {

        this.width = param.width==null?400:param.width;                                         //窗口宽
        this.height = param.height==null?400: param.height;                                       //窗口高
        this.formContentStr = param.formContent==null?"":param.formContent;                          //窗口内HTML
        this.topHTML= param.topHTML==null?"":param.topHTML;  
	    this.loginEvent= param.loginEvent==null?function(user,pwd){}:param.loginEvent;                                //登陆事件
        this.validateEvent=param.validateEvent==null?function (checkCode){}:param.validateEvent;
        this.codeImgURL = param.codeImgURL == null ? "" : param.codeImgURL;
        this.checkCode = 0;
        this.speed = param.speed==null?50:param.speed;
        this.type=param.type==null?"zoom":param.type;
        this.className=param.className==null?"czy-easyform":param.className;
        //创建UI
        var loginUI = "<div id='czy-login-form' class='czy-login-form'>";
        loginUI += "<div class='czy-login-inner'>";
        loginUI += "<div id='czy-login-user'><span class='czy-login-label'>" + this.fonts.user_text + " : </span><span class='czy-login-txts'><input id='czy-user-text' type='text' class='" + this.styles.login_text + "' /><span></div>";
        loginUI += "<div id='czy-login-pwd'><span class='czy-login-label'>" + this.fonts.user_pwd + " : </span> <span class='czy-login-txts'><input id='czy-passWord-text' type='text' class='" + this.styles.login_text + "' /><span></div>";
        if (this.codeImgURL != "") {
            loginUI += "<div id='czy-login-validate' ><span class='czy-login-label'>" + this.fonts.validate + " : </span> <input id='czy-validate-text' type='text'  class='" + this.styles.login_text + "'/><img alt='' src='" + this.codeImgURL + "' style='margin-left:10px;' /></div>";
        }
        loginUI += "<div id='czy-login'><input id='czy-login-button' type='button' value='" + this.fonts.login_button + "'/><input  type='reset' value='" + this.fonts.reset_button + "'/></div>";
        loginUI += "</div>";
        loginUI += "</div>";

        this.formContentStr = loginUI;
        //创建窗口对像

        this.form =  new czyjs.UI.EasyWindowForm(
		{
                   	id:"easyform",
		            width:this.width,
		            height:this.height,
		            formContent:this.formContentStr,
		            speed: this.speed ,
		            formTopHTML: this.topHTML,
		            isPicture: false,
                    scroll:false,
                    className:this.className,
                     //type:'opacity'
                    type:this.type
		   }
		   );


        this.AddListeners();
    },
    /*
    * 添加事件
    */
    AddListeners: function () {
        this.loginForm = document.getElementById("czy-login-form");
        this.userText = document.getElementById("czy-user-text");
        this.passWordText = document.getElementById("czy-passWord-text");
        this.loginButton = document.getElementById("czy-login-button");


        this._TxtOnFocus = BindAsEventListener(this, this.TxtOnFocus);
        this._TxtOnBlur = BindAsEventListener(this, this.TxtOnBlur);
        //this._TxtOnFocus1=BindAsEventListener(this,function(){this.TxtOnFocus(this)});
        //this._TxtOnBlur1=BindAsEventListener(this,function(){this.TxtOnBlur(this)});

        addEventHandler(this.passWordText, "focus", this._TxtOnFocus);
        addEventHandler(this.passWordText, "blur", this._TxtOnBlur);
        addEventHandler(this.userText, "focus", this._TxtOnFocus);
        addEventHandler(this.userText, "blur", this._TxtOnBlur);
        if (document.getElementById("czy-validate-text") != null) {
            addEventHandler(document.getElementById("czy-validate-text"), "focus", this._TxtOnFocus);
            addEventHandler(document.getElementById("czy-validate-text"), "blur", this._TxtOnBlur);
        }
        this._Login = BindAsEventListener(this, this.Login);
        addEventHandler(this.loginButton, "click", this._Login);

    },
    /*
    * 登陆事件
    */
    Login: function () {
 
            this.loginEvent(this.userName ,this.passWord);
        
    },

    Validate: function () {
        this.validateEvent(this.checkCode);
    },
    /*
    * 鼠标焦点
    */
    TxtOnFocus: function () {
        var o = czyjs.Event.GetEventObj();
        o.className = this.styles.login_text_on;

    },
    /*
    * 鼠标离开焦点
    */
    TxtOnBlur: function () {
        var o = czyjs.Event.GetEventObj();
        o.className = this.styles.login_text;
        this.userName = this.userText.value;
        this.passWord = this.passWordText.value;
        if (document.getElementById("czy-validate-text") != null) {
            this.checkCode = document.getElementById("czy-validate-text").value;
        }
    },

    Close: function () {
        this.form.Close();
    },

    Show: function () {
        this.form.Show();
    }



}






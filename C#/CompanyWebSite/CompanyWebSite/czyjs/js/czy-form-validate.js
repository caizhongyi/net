if(typeof(czyjs.Validate)=="undefined")
{
    czyjs.Validate = {};
};
/*
 * Need 为True时 为必填项
 * 目前支持的所有验证
 * userName:用户名
 * pwd:密码
 * comParePwd:二次密码
 * email:Email
 * phone:固定电话
 * tel:手机
 * QQ:QQ号
 * address:地址
 */
czyjs.Validate.CheckTrueIco=hostName+"images/validateForm/check_48.png";
if (czyjs.Validate.messages == null) {
	czyjs.Validate.messages = {
		userNameEmpty: "请输入用户名!",
		userEixts: "用户名已存在!",
		userError_XX: "为防止被部分防火墙屏蔽，用户名中不能带有 xx!",
		userError: "只能由3-16位字母(a-z)、数字(0-9)或下划线(_)构成，并且必须以字母开头!",
		userLength: "用户名不能少于3位!",
		pwdEmpty: "请输入密码!",
		pwdError: "密码不能少于3位!",
		pwdCompare: "两次输入的密码不一致!",
		emailEmpty: "请输入Email!",
		emailError: "Email格式有误!",
		photoError: "固定电话格式有误！",
		telEmpty: "请输入手机号码！",
		telError: "手机号码格式有误！",
		QQError: "QQ号码格式有误！",
		QQEmpty: "请输入QQ号码!",
		photoEmpty: "请输入电话号码!",
		addressEmpty: "请输入您的地址"
	};
}

czyjs.Validate.FormValidateHelper = Class.create();
czyjs.Validate.FormValidateHelper.prototype = {
	initialize: function(param){
	
		this.validate = new czyjs.Validate.ValidateHelper();
		this.picHtml = "<img alt='' src='" + czyjs.Validate.CheckTrueIco + "' width='25' height='25'>";
		this.config = {
			userNameObj: document.getElementById(param.userName),
			pwdObj: document.getElementById(param.pwd),
			comParePwdObj: document.getElementById(param.comParePwd),
			emailObj: document.getElementById(param.email),
			phoneObj: document.getElementById(param.phone),
			telObj: document.getElementById(param.tel),
			qqObj: document.getElementById(param.QQ),
			addressObj:document.getElementById(param.address)
		};
		this.msgContents = {
			userNameObj: document.getElementById(param.userName + "Msg"),
			pwdObj: document.getElementById(param.pwd + "Msg"),
			comParePwdObj: document.getElementById(param.comParePwd + "Msg"),
			emailObj: document.getElementById(param.email + "Msg"),
			phoneObj: document.getElementById(param.phone + "Msg"),
			telObj: document.getElementById(param.tel + "Msg"),
			qqObj: document.getElementById(param.QQ + "Msg"),
			addressObj:document.getElementById(param.address+ "Msg")
		}
		
		//////////事件绑定////////////
		this._SubMit = BindAsEventListener(this, function(){
			return this.SubMitData("subMit");
		});
		
		this._UserCheck = BindAsEventListener(this, function(){
			this.UserCheck("blur")
		});
		this._PwdCheck = BindAsEventListener(this, function(){
			this.PwdCheck("blur")
		});
		this._PwdCompare = BindAsEventListener(this, function(){
			this.PwdCompare("blur")
		});
		this._EmaliCheck = BindAsEventListener(this, function(){
			this.EmaliCheck("blur")
		});
		this._PhoneCheck = BindAsEventListener(this, function(){
			this.PhoneCheck("blur")
		});
		this._TelCheck = BindAsEventListener(this, function(){
			this.TelCheck("blur")
		});
		this._QQCheck = BindAsEventListener(this, function(){
			this.QQCheck("blur")
		});
			this._AddressCheck = BindAsEventListener(this, function(){
			this.AddressCheck("blur")
		});
		
		this.formObj = document.getElementById("form1");
		if (this.formObj != null) {
			addEventHandler(this.formObj, "submit", this._SubMit);
		}
		if (this.config.userNameObj != null) {
			addEventHandler(this.config.userNameObj, "blur", this._UserCheck);
		}
		if (this.config.pwdObj != null) {
			addEventHandler(this.config.pwdObj, "blur", this._PwdCheck);
		}
		if (this.config.comParePwdObj != null) {
			addEventHandler(this.config.comParePwdObj, "blur", this._PwdCompare);
		}
		if (this.config.emailObj != null) {
			addEventHandler(this.config.emailObj, "blur", this._EmaliCheck);
		}
		if (this.config.phoneObj != null) {
			addEventHandler(this.config.phoneObj, "blur", this._PhoneCheck);
		}
		if (this.config.telObj != null) {
			addEventHandler(this.config.telObj, "blur", this._TelCheck);
		}
		if (this.config.qqObj != null) {
			addEventHandler(this.config.qqObj, "blur", this._QQCheck);
		}
		if (this.config.addressObj != null) {
			addEventHandler(this.config.addressObj, "blur", this._QQCheck);
		}
		///////////////////////////////////////
	
	},
	////////////验证函数/////////////////
	UserCheck: function(Event){
		
		if (this.config.userNameObj != null) {
			if (this.config.userNameObj.Need != null && "TRUE" == this.config.userNameObj.Need.toUpperCase()) {
				if (this.config.userNameObj.value == "") {
					if (Event == "blur") {
						this.msgContents.userNameObj.innerHTML = czyjs.Validate.messages.userNameEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.userNameEmpty);
							this.config.userNameObj.focus();
						}
					return false;
				}
				
			}
		 if (this.CheckUser(false, Event)) {
						if (this.CheckUser(false, Event)) {
							this.msgContents.userNameObj.innerHTML = this.picHtml;
							return true;
						}
						else {
							return false;
						}
					}
			
		
		}
		return true;
	},
	//密码
	PwdCheck: function(Event){
		//密码
		if (this.config.pwdObj != null) {
			if (this.config.pwdObj.Need!=null && "TRUE" == this.config.pwdObj.Need.toUpperCase()) {
				if (this.config.pwdObj.value == "") {
					if (Event == "blur") {
						this.msgContents.pwdObj.innerHTML = czyjs.Validate.messages.pwdEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.pwdEmpty);
							this.config.pwdObj.focus();
						}
					return false;
				}
				
			}
			if(true)
			{
			    if(this.config.pwdObj.value.length<4)
				{
						if (Event == "blur") {
						this.msgContents.pwdObj.innerHTML = czyjs.Validate.messages.pwdError;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.pwdError);
							this.config.pwdObj.focus();
						}
						return false;
				}
				else
				{this.msgContents.pwdObj.innerHTML = this.picHtml;
	            return true;}
				
			}
		}
		return true;
	},
    //密码AG
	PwdCompare: function(Event){
		//再次密码
		if (this.config.comParePwdObj != null) {
			if (this.config.comParePwdObj.Need!=null && "TRUE" == this.config.comParePwdObj.Need.toUpperCase()) {
				if (this.config.comParePwdObj.value == "") {
					if (Event == "blur") {
						this.msgContents.comParePwdObj.innerHTML = czyjs.Validate.messages.pwdEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.pwdEmpty);
							this.config.comParePwdObj.focus();
						}
					return false;
				}

			}
			
			if (this.config.pwdObj.value != this.config.comParePwdObj.value) {
						if (Event == "blur") {
							this.msgContents.comParePwdObj.innerHTML = czyjs.Validate.messages.pwdCompare;
						}
						else 
							if (Event == "subMit") {
								alert(czyjs.Validate.messages.pwdCompare);
								this.config.pwdObj.focus();
							}
						return false;
					}
					else {
						this.msgContents.comParePwdObj.innerHTML = this.picHtml;
						return true;
					}
		}
		return true;
	},
	//邮件
	EmaliCheck: function(Event){
		//Email
		if (this.config.emailObj != null) {
			if (this.config.emailObj.Need !=null && "TRUE" == this.config.emailObj.Need.toUpperCase()) {
				if (this.config.emailObj.value == "") {
					if (Event == "blur") {
						this.msgContents.emailObj.innerHTML = czyjs.Validate.messages.emailEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.emailEmpty);
							this.config.emailObj.focus();
						}
					
					return false;
				}
			} 
			if (!this.validate.isMail(this.config.emailObj.value)) {
					if (Event == "blur") {
						this.msgContents.emailObj.innerHTML = czyjs.Validate.messages.emailError;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.emailError);
							this.config.emailObj.focus();
						}
					return false;
				}
				else {
					this.msgContents.emailObj.innerHTML = this.picHtml;
					return true;
				}	
		}
		return true;
	},
	//电话
	PhoneCheck: function(Event){
		//电话
		if (this.config.phoneObj != null) {
			if (this.config.phoneObj.Need !=null && "TRUE" == this.config.phoneObj.Need.toUpperCase()) {
				if (this.config.phoneObj.value == "") {
					if (Event == "blur") {
						this.msgContents.phoneObj.innerHTML = czyjs.Validate.messages.photoEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.photoEmpty);
							this.config.phoneObj.focus();
						}
					
					return false;
				}
			}
				if (!this.validate.isPhone(this.config.phoneObj.value)) {
					if (Event == "blur") {
						this.msgContents.phoneObj.innerHTML = czyjs.Validate.messages.photoError;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.photoError);
							this.config.phoneObj.focus();
						}
					return false;
				}
				else {
					this.msgContents.phoneObj.innerHTML = this.picHtml;
						return true;
				}
		}
		return true;
	},
	
	//手机
	TelCheck: function(Event){
		//手机
		if (this.config.telObj != null) {
			if (this.config.telObj.Need!=null && "TRUE" == this.config.telObj.Need.toUpperCase()) {
				if (this.config.telObj.value == "") {
					if (Event == "blur") {
						this.msgContents.telObj.innerHTML = czyjs.Validate.messages.telEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.telEmpty);
							this.config.telObj.focus();
						}
					return false;
				}
			}
		 
				if (!this.validate.isTel(this.config.telObj.value)) {
					if (Event == "blur") {
						this.msgContents.telObj.innerHTML = czyjs.Validate.messages.telError;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.telError);
							this.config.telObj.focus();
						}
					return false;
				}
				else {
					this.msgContents.telObj.innerHTML = this.picHtml;
						return true;
				}
		}
		return true;
	},
	//QQ
	QQCheck: function(Event){
		if (this.config.qqObj != null) {
			if ( this.config.qqObj.Need !=null && "TRUE" == this.config.qqObj.Need.toUpperCase()) {
				if (this.config.qqObj.value == "") {
					if (Event == "blur") {
						this.msgContents.qqObj.innerHTML = czyjs.Validate.messages.QQEmpty;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.QQEmpty);
							this.config.qqObj.focus();
						}
					return false;
				}
			}
		
				if (!this.validate.isNumber(this.config.qqObj.value)) {
					if (Event == "blur") {
						this.msgContents.qqObj.innerHTML = czyjs.Validate.messages.QQError;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.QQError);
							this.config.qqObj.focus();
						}
					return false;
				}
				else {
					this.msgContents.qqObj.innerHTML = this.picHtml;
					return true;
				}
		}
		return true;
	},
	//街道地址
	AddressCheck:function()
	{
			if (this.config.addressObj != null) {
				if (this.config.addressObj.Need != null && "TRUE" == this.config.addressObj.Need.toUpperCase()) {
					if (this.config.addressObj.value == "") {
						if (Event == "blur") {
							this.msgContents.addressObj.innerHTML = czyjs.Validate.messages.addressEmpty;
						}
						else 
							if (Event == "subMit") {
								alert(czyjs.Validate.messages.addressEmpty);
								this.config.addressObj.focus();
							}
						return false;
					}
				}
				return true;
			}
			return true;
	},
	
	/////////////////////////////////
	
	//提交
	SubMitData: function(Event){
		if (!this.UserCheck(Event)) {
			return false;
		}
		else 
			if (!this.PwdCheck(Event)) {
				return false;
			}
			else 
				if (!this.PwdCompare(Event)) {
					return false;
				}
				else 
					if (!this.EmaliCheck(Event)) {
						return false;
					}
					else 
						if (!this.PhoneCheck(Event)) {
							return false;
						}
						else 
							if (!this.TelCheck(Event)) {
								return false;
							}
							else 
								if (!this.QQCheck(Event)) {
									return false;
								}
								else {
									return true;
								}
	},
	//用户验证
	CheckUser: function(Exits, Event){
		var pattern = /^[a-zA-Z][a-zA-Z0-9_]{1,14}[a-zA-Z0-9]$/i;
		if (this.config.userNameObj.value.indexOf("xx", 0) != -1) { // 不能包含xx
			if (Event == "blur") {
				this.msgContents.userNameObj.innerHTML = czyjs.Validate.messages.userError_XX;
				return false;
			}
			else 
				if (Event == "subMit") {
					alert(czyjs.Validate.messages.userError_XX);
					this.config.userNameObj.focus();
					return false;
				}
			
		}
		else 
			if (!pattern.test(this.config.userNameObj.value)) {
			
				if (Event == "blur") {
					this.msgContents.userNameObj.innerHTML = czyjs.Validate.messages.userError;
					return false;
				}
				else 
					if (Event == "subMit") {
						alert(czyjs.Validate.messages.userError);
						this.config.userNameObj.focus();
						
						return false;
					}
				
			}
			else 
				if (this.config.userNameObj.value.length < 3) { // 判断长度
					if (Event == "blur") {
						this.msgContents.userNameObj.innerHTML = czyjs.Validate.messages.userLength;
						return false;
					}
					else 
						if (Event == "subMit") {
							alert(czyjs.Validate.messages.userLength);
							this.config.userNameObj.focus();
							return false;
						}
				}
				else 
					if (Exits) {
						if (Event == "blur") {
							this.msgContents.innerHTML = czyjs.Validate.messages.userEixts;
							return false;
						}
						else 
							if (Event == "subMit") {
								alert(czyjs.Validate.messages.userEixts);
								this.config.userNameObj.focus();
								return false;
							}
					}
					else {
						return true;
					}
	}
}
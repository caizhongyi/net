if (typeof (czyjs.File) == "undefined") {
    czyjs.File = {};
}

czyjs.File.uploadswf = hostName + "js/yui/uploader.swf";

czyjs.File.YahooUpLoad = Class.create();
czyjs.File.YahooUpLoad.prototype = {

    initialize: function (param) {


        this.css = {
            progress: "czy-progress",
            uploadList: "czy-uploadList",
            uploadListItem: "czy-uploadListItem"
        };

        this.config = {
            maxLimit: param.maxLimit,
            contenterId: param.contenterId,
            formats: param.format,            //上图片过滤:示例[{name:"Image",filter:"*.jpg;*.png;*.gif" },{name:"video",filter:"*.avi;*.mov;*.mpg" }]
            listenters: param.listenters,
            url: param.url
        };

        this.CreateUI();
        this.YahooLoad(this.config.maxLimit, this.config.url);
    },
    CreateUI: function () {

        this.contenter = document.getElementById(this.config.contenterId);

        var html = "";

        html += "<div id=\"uploaderContainer\" class=\"czy-yahoo-upload\">";
	html += "<div class='czy-upload-header-left'></div>";
        html += "<div class='czy-upload-header-right'></div>";
        html += "<div class='czy-upload-header-center'></div>";

	html += "<div class='czy-upload-left'></div>";
        html += "<div class='czy-upload-right'></div>";
        html += "<div class='czy-upload-center'>";

        html += "<div id=\"uploaderOverlay\" style=\"position:absolute; z-index:2\"></div>";
        html += "<div id=\"selectFilesLink\" style=\"z-index:1\"><a id=\"selectLink\" href=\"#\">选择文件</a></div>";

        html += "<div id=\"uploadFilesLink\"><a id=\"uploadLink\"  href=\"#\">上传</a></div>";

        html += "<div  class=\"czy-upload-data\">";
        html += "<ul class='czy-data-header'>";
        html += "<li>文件名</li>";
        html += "<li>|</li>";
        html += "<li>文件大小</li>";
        html += "<li>|</li>";
        html += "<li>进度</li>";
        html += "<li>|</li>";
        html += "<li>上传状态</li>";
        html += "</ul>";
        html += "<ul id=\"czy-upload-list\" class='czy-upload-list'></ul>";
        html += "</div>";

        html += "</div>";
        html += "<div class='czy-upload-footer-left'></div>";
        html += "<div class='czy-upload-footer-right'></div>";
        html += "<div class='czy-upload-footer-center'></div>";

        html += "</div>";

        this.contenter.innerHTML = html;

        this.AddListeners();
    },
    /*
    * 添加事件
    */
    AddListeners: function () {

        this.uploadLink = document.getElementById("uploadLink");
        this._UpLoad = BindAsEventListener(this, this.upload);
        addEventHandler(this.uploadLink, "click", this._UpLoad);

    },
    /*YahooLoad*/
    YahooLoad: function (limit, url) {
        YAHOO.util.Event.onDOMReady(function () {
            var uiLayer = YAHOO.util.Dom.getRegion('selectLink'); //选择按扭
            var overlay = YAHOO.util.Dom.get('uploaderOverlay');
            YAHOO.util.Dom.setStyle(overlay, 'width', uiLayer.right - uiLayer.left + "px");
            YAHOO.util.Dom.setStyle(overlay, 'height', uiLayer.bottom - uiLayer.top + "px");
        });

        //swf上传控件路径
        YAHOO.widget.Uploader.SWFURL = czyjs.File.uploadswf;

        // Instantiate the uploader and write it to its placeholder div.
        this.uploader = new YAHOO.widget.Uploader("uploaderOverlay");

        // Add event listeners to various events on the uploader.
        // Methods on the uploader should only be called once the 
        // contentReady event has fired.
        this._handleContentReady = BindAsEventListener(this, this.handleContentReady);
		this._onFileSelect = BindAsEventListener(this, this.onFileSelect);
		this._onUploadStart = BindAsEventListener(this, this.onUploadStart);
		this._onUploadProgress = BindAsEventListener(this, this.onUploadProgress);
		this._onUploadCancel = BindAsEventListener(this, this.onUploadCancel);
		this._onUploadComplete = BindAsEventListener(this, this.onUploadComplete);
		this._onUploadResponse = BindAsEventListener(this, this.onUploadResponse);
		this._onUploadError = BindAsEventListener(this, this.onUploadError);
		this._handleRollOver = BindAsEventListener(this, this.handleRollOver);
		this._handleRollOut = BindAsEventListener(this, this.handleRollOut);
	    this._handleClick = BindAsEventListener(this, this.handleClick);

		
        this.uploader.addListener('contentReady', this._handleContentReady);
        this.uploader.addListener('fileSelect', this._onFileSelect)
        this.uploader.addListener('uploadStart', this._onUploadStart);
        this.uploader.addListener('uploadProgress', this._onUploadProgress);
        this.uploader.addListener('uploadCancel', this._onUploadCancel);
        this.uploader.addListener('uploadComplete', this._onUploadComplete);
        this.uploader.addListener('uploadCompleteData', this._onUploadResponse);
        this.uploader.addListener('uploadError', this._onUploadError);
        this.uploader.addListener('rollOver', this._handleRollOver);
        this.uploader.addListener('rollOut', this._handleRollOut);
        this.uploader.addListener('click', this._handleClick);

        // Variable for holding the filelist.




    },
    //鼠标放入在选择按扭上
    handleRollOver: function () {
        //            YAHOO.util.Dom.setStyle(YAHOO.util.Dom.get('selectLink'), 'color', "#FFFFFF");
        //            YAHOO.util.Dom.setStyle(YAHOO.util.Dom.get('selectLink'), 'background-color', "#000000");
    },

    //鼠标移开选择按扭上
    handleRollOut: function () {
        //            YAHOO.util.Dom.setStyle(YAHOO.util.Dom.get('selectLink'), 'color', "#0000CC");
        //            YAHOO.util.Dom.setStyle(YAHOO.util.Dom.get('selectLink'), 'background-color', "#FFFFFF");
    },

    //点击选择按扭
    handleClick: function () {
    },

    // When contentReady event is fired, you can call methods on the uploader.
    handleContentReady: function () {
        // Allows the uploader to send log messages to trace, as well as to YAHOO.log
        this.uploader.setAllowLogging(true);

        // Allows multiple file selection in "Browse" dialog.
        this.uploader.setAllowMultipleFiles(true);

        // New set of file filters.
        var ff = new Array(
        //            { description: this.format.name, extensions: "*.jpg;*.png;*.gif" },
        //		    { description: "Videos", extensions: "*.avi;*.mov;*.mpg" }
             );
        //图片格式过滤
        for (var i = 0; i < this.format.length; i++) {
            var f = { description: this.format[i].name, extensions: this.format[i].filter };
            ff.push(f);
        }
        // Apply new set of file filters to the uploader.
        this.uploader.setFileFilters(ff);
    },

    // Actually uploads the files. In this case,
    // uploadAll() is used for automated queueing and upload 
    // of all files on the list.
    // You can manage the queue on your own and use "upload" instead,
    // if you need to modify the properties of the request for each
    // individual file.
    upload: function () {
        if (this.fileList != null) {
            this.uploader.setSimUploadLimit(this.config.maxLimit);
            this.uploader.uploadAll(this.config.url, "POST", null, "Filedata");
        }
    },

    // Fired when the user selects files in the "Browse" dialog
    // and clicks "Ok".
    //选择文件事件
    onFileSelect: function (event) {
        if ('fileList' in event && event.fileList != null) {

            this.fileList = event.fileList;
            this.CreateItem(this.fileList);
        }
		
    },
    //创建table
    CreateItem: function (entries) {
		
        var rowCounter = 0;
        this.fileIdHash = {};
        this.dataArr = [];
        for (var i in entries) {
            var entry = entries[i];
            entry["progress"] = "<div  class='czy-progress' style='height:5px;width:100px;background-color:#CCC;'></div>"; //进度条
            this.dataArr.unshift(entry);
        }
        //获取ID

        for (var j = 0; j < this.dataArr.length; j++) {
            this.fileIdHash[this.dataArr[j].id] = j;
        }
        //加载数据

        var l = "";

        for (var k = 0; k < this.dataArr.length; k++) {

            l += this.getItem(k, this.dataArr[k]["name"], this.dataArr[k]["size"], this.getProgBar(0), '等待上传');

        }
      
        document.getElementById("czy-upload-list").innerHTML = l;

    },
    getItem: function (index, fileName, size, prog, state) {

        var item = "<li index='" + index + "'>";
        item += "<ul class='czy-upload-listItem'>";
        item += "<li>" + fileName + "</li>";
        item += "<li>&nbsp;</li>";
        item += "<li>" + size + "KB</li>";
        item += "<li>&nbsp;</li>";
        item += "<li>" + prog + "</li>";
        item += "<li>&nbsp;</li>";
        item += "<li>" + state + "</li>";
        item += "</ul>";
        item += "</li>";
        return item;
    },

    getProgBar: function (prog) {

        var progbar = "<div class='czy-progress' style='height:5px;width:100px;background-color:#CCC;'><div style='height:5px;background-color:#F00;width:" + prog + "px;'></div></div>";
        return progbar;
    },


    // Do something on each file's upload start.
    //开始上传
    onUploadStart: function (event) {

    },

    // Do something on each file's upload progress event.
    //上传进程事件
    onUploadProgress: function (event) {
         
		var rowNum = this.fileIdHash[event["id"]];
        
	    var prog = Math.round(100 * (event["bytesLoaded"] / event["bytesTotal"]));
   
		// progbar = "<div class='" + this.css.progress + "' style='height:5px;width:100px;background-color:#CCC;'><div style='height:5px;background-color:#F00;width:" + prog + "px;'></div></div>";
        
		this.list=document.getElementById("czy-upload-list"); 
		//进度条
		this.list.childNodes[rowNum].childNodes[0].childNodes[4].innerHTML = this.getProgBar(prog);
        //上传状态
	    this.list.childNodes[rowNum].childNodes[0].childNodes[6].innerHTML = "上传中...";
        //singleSelectDataTable.updateRow(rowNum, { name: dataArr[rowNum]["name"], size: dataArr[rowNum]["size"], progress: progbar });
         
	},

    // Do something when each file's upload is complete.
    //完成上传事件
    onUploadComplete: function (event) {
        var rowNum = this.fileIdHash[event["id"]];
        var prog = Math.round(100 * (event["bytesLoaded"] / event["bytesTotal"]));
        //progbar = "<div style='height:5px;width:100px;background-color:#CCC;'><div style='height:5px;background-color:#F00;width:100px;'></div></div>";
        this.list=document.getElementById("czy-upload-list"); 
		//进度条
	    this.list.childNodes[rowNum].childNodes[0].childNodes[4].innerHTML = this.getProgBar(prog);
		//上传状态
        this.list.childNodes[rowNum].childNodes[0].childNodes[6].innerHTML = "上传完毕";

        //singleSelectDataTable.updateRow(rowNum, { name: dataArr[rowNum]["name"], size: dataArr[rowNum]["size"], progress: progbar });
    },

    // Do something if a file upload throws an error.
    // (When uploadAll() is used, the Uploader will
    // attempt to continue uploading.
    //上传错误事件
    onUploadError: function (event) {

    },

    // Do something if an upload is cancelled.
    //停止上传事件
    onUploadCancel: function (event) {

    },

    // Do something when data is received back from the server.
    //上传完回调事件
    onUploadResponse: function (event) {

    }

}
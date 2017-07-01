if (!window.Webabcd)
    window.Webabcd = {};

Webabcd.Piano = function() 
{
}

Webabcd.Piano.prototype =
{
    handleLoad: function(control, userContext, rootElement) 
    {
        // plugin�������
        this.control = control;
        
        // ��CanvasԪ��
        this.rootElement = rootElement;
        
        // object.addEventListener(eventName, functionReference)Ϊ�������һ���¼����������¼����ƣ��������ƣ�
        // Silverlight.createDelegate��������һ�����á�this���������µ��ض�������ί�У��䱻������runtime
        // Silverlight.createDelegate = function(instance, method) 
        // {
        //   return function() 
        //   {
        //      return method.apply(instance, arguments);
        //    }
        // }
        this.rootElement.addEventListener("GotFocus", Silverlight.createDelegate(this, this.resetPiano));
        this.rootElement.addEventListener("KeyDown", Silverlight.createDelegate(this, this.handleKeyDown));
        this.rootElement.addEventListener("KeyUp", Silverlight.createDelegate(this, this.handleKeyUp));

        // ���������Canvas
        // object.findName(name)
        this.pathCanvas = rootElement.findName("pathCanvas");   

        // ��ʼ¼�ư�ť
        this.startRecord = rootElement.findName("startRecord");   
        this.startRecord.addEventListener("MouseLeftButtonDown", Silverlight.createDelegate(this, this.handleStartRecord));

        // ֹͣ¼�ư�ť
        this.stopRecord = rootElement.findName("stopRecord");   
        this.stopRecord.addEventListener("MouseLeftButtonDown", Silverlight.createDelegate(this, this.handleStopRecord));
        
        // Ĭ��Ϊѡ��ֹͣ¼�ư�ť
        // plugin.content.findName(objectName)
        document.getElementById('SilverlightControl').content.findName("stopRecord").Stroke = "LightBlue";
        document.getElementById('SilverlightControl').content.findName("stopRecord").StrokeThickness = 6;
        
        // ���Ű�ť
        this.play = rootElement.findName("play");   
        this.play.addEventListener("MouseLeftButtonDown", Silverlight.createDelegate(this, this.handlePlay));

        // object.children��Ӧ<Canvas>һ��������Ԫ��</Canvas>
        // object.getItem(index)��ȡ������ָ���Ķ���
        // object.count�����ڵĳ�Ա����
        for(var i = 0; i<this.pathCanvas.children.count; ++i)
        {
            // Ϊ����������������ص��¼�������
            this.pathCanvas.children.getItem(i).addEventListener("MouseLeftButtonDown", Silverlight.createDelegate(this, this.handleMouseDown));
            this.pathCanvas.children.getItem(i).addEventListener("MouseLeftButtonUp", Silverlight.createDelegate(this, this.handleMouseUp));
        }
        
        // �Ƿ�����¼��
        this._enableRecord = false;
        // ��һ�ΰ�����ʱ��
        this._prevTime = new Date().getTime();
    },
    
    // ������ʼ¼�ư�ť
    handleStartRecord: function(sender, eventArgs) 
    {
        // ���ÿ�ʼ¼�ư�ť��ʽ
        sender.Stroke = 'LightBlue';
        sender.StrokeThickness = 6;
        
        // ����ֹͣ¼�ư�ť��ʽ
        document.getElementById('SilverlightControl').content.findName("stopRecord").Stroke = "Black";
        document.getElementById('SilverlightControl').content.findName("stopRecord").StrokeThickness = 3;
        
        // ����¼��
        this._enableRecord = true;
        // ��¼ʱ��
        this._prevTime = new Date().getTime();
    },
    
    // ����ֹͣ¼�ư�ť
    handleStopRecord: function(sender, eventArgs) 
    {
        // ����ֹͣ¼�ư�ť��ʽ
        sender.Stroke = 'LightBlue';
        sender.StrokeThickness = 6;

        // ���ÿ�ʼ¼�ư�ť��ʽ
        document.getElementById('SilverlightControl').content.findName("startRecord").Stroke = "Black";
        document.getElementById('SilverlightControl').content.findName("startRecord").StrokeThickness = 3;

        // ����������ƺ���������
        document.getElementById(txtInput).value = '';
        document.getElementById(txtName).value = '';
        
        // ֹͣ¼��
        this._enableRecord = false;
    },
    
    // �������Ű�ť
    handlePlay: function(sender, eventArgs) 
    {
        // ���������Զ�����
        autoPlay(this, 0);
    },
    
    // ��¼����
    handleRecord: function(currentPianoKeyID)
    {
        if (this._enableRecord)
        {
            // ��������
            document.getElementById(txtInput).value += new Date().getTime() - this._prevTime + ',';
            document.getElementById(txtInput).value += currentPianoKeyID + ';';

            // ����ʱ��
            this._prevTime = new Date().getTime();
        }
    },
    
    handleMouseDown: function(sender, eventArgs) 
    {
        // object.captureMouse()Ϊ����������겶׽������뿪����Ҳ�ɴ�������¼�����MouseLeftButtonUp��
        sender.captureMouse();
        
        var currentPianoKeyID = sender.name.substr(0, sender.name.indexOf("Path"));
        var currentImage = sender.findName("img" + currentPianoKeyID);
        currentImage.opacity = 1;
        
        var currentMediaElement = sender.findName(currentPianoKeyID);
        currentMediaElement.stop();
        currentMediaElement.play();
        
        this.handleRecord(currentPianoKeyID);
    },
    
    handleMouseUp: function(sender, eventArgs) 
    {
        // object.releaseMouseCapture()�ͷŶ������겶׽
        sender.releaseMouseCapture();
        
        var currentPianoKeyID = sender.name.substr(0, sender.name.indexOf("Path"));
        var currentImage = sender.findName("img" + currentPianoKeyID);
        currentImage.opacity = 0;
    },

    pressKey: function (currentPianoKeyID)
    {
        var currentImage = this.control.content.findName("img" + currentPianoKeyID);
        currentImage.opacity = 1;
        
        var currentMediaElement = this.control.content.findName(currentPianoKeyID);
        // object.play()���ţ�object.pause()��ͣ��object.stop()ֹͣ
        currentMediaElement.stop();
        currentMediaElement.play();
    },

    depressKey: function (currentPianoKeyID)
    {
        var currentImage = this.control.content.findName("img" + currentPianoKeyID);
        currentImage.opacity = 0;
    },

    handleKeyDown: function (sender, eventArgs)
    {
        switch (eventArgs.Key)
        {
            // eventArgs.Key - ��ȡ����¼���صļ��̰���
            case 55: this.pressKey("C"); this.handleRecord("C"); break;
            case 48: this.pressKey("C2"); this.handleRecord("C2"); break;
            case 53: this.pressKey("D");  this.handleRecord("D"); break;
            case 33: this.pressKey("D2"); this.handleRecord("D2"); break;
            case 32: this.pressKey("E");  this.handleRecord("E"); break;
            case 51: this.pressKey("F");  this.handleRecord("F"); break;
            case 36: this.pressKey("F2"); this.handleRecord("F2"); break;
            case 31: this.pressKey("G");  this.handleRecord("G"); break;
            case 37: this.pressKey("G2"); this.handleRecord("G2"); break;
            case 43: this.pressKey("A");  this.handleRecord("A"); break;
            case 39: this.pressKey("A2"); this.handleRecord("A2"); break;
            case 42: this.pressKey("B");  this.handleRecord("B"); break;
        }
    },

    handleKeyUp: function (sender, eventArgs)
    {
        switch (eventArgs.Key)
        {
            // eventArgs.Key - ��ȡ����¼���صļ��̰���
            case 55: this.depressKey("C");  break;
            case 48: this.depressKey("C2"); break;
            case 53: this.depressKey("D");  break;
            case 33: this.depressKey("D2"); break;
            case 32: this.depressKey("E");  break;
            case 51: this.depressKey("F");  break;
            case 36: this.depressKey("F2"); break;
            case 31: this.depressKey("G");  break;
            case 37: this.depressKey("G2"); break;
            case 43: this.depressKey("A");  break;
            case 39: this.depressKey("A2"); break;
            case 42: this.depressKey("B");  break;
        }
    },
    
    resetPiano: function(sender, eventArgs) 
    {
        this.depressKey("C");
        this.depressKey("C2");
        this.depressKey("D");
        this.depressKey("D2");
        this.depressKey("E");
        this.depressKey("F");
        this.depressKey("F2");
        this.depressKey("G");
        this.depressKey("G2");
        this.depressKey("A");
        this.depressKey("A2");
        this.depressKey("B");
    }
}


var _obj; // Webabcd.Piano����
var _index; // ��������
function autoPlay(obj, index)
{
    _obj = obj;
    _index = index;
    
    execAutoPlay();
}

function execAutoPlay()
{
    _obj.resetPiano();

    var str = document.getElementById(txtInput).value; // ����
    var ary = str.split(';'); // ���ʱ�䣬��������
    
    if (_index != 0 && typeof(ary[_index-1]) != 'undefined' && ary[_index-1] != '')
    {
        // ���µ�ǰ������������һ�������İ���
        _obj.pressKey(ary[_index-1].split(',')[1]); 
    }
     
    var currentIndex = _index;   
    _index++;
    
    if (typeof(ary[currentIndex]) != 'undefined')
    {
        setTimeout("_obj.resetPiano();", 100)
        // ������ǰ���������ļ��ʱ�������execAutoPlay()
        setTimeout("execAutoPlay();", parseInt(ary[currentIndex].split(',')[0], 10)); 
    }
}

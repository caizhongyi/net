if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI.Controls = {};
};

if(typeof(czyjs.UI.Controls)=="undefined")
{
    czyjs.UI.Controls = {};
};

czyjs.UI.Controls.Register = function () {
    var controls = document.getElementsByTagName("object"); 
    for (var i = 0; i < controls.length; i++) {
        if (controls[i].param != null) {

            var type = controls[i].type;
            //var propertys=eval("("+controls[i].class+")");
            //var id=controls[i].id;
            //var name=controls[i].name;
            //var picURL=propertys.picURL;
            
            switch (type) {
                case "text": new czyjs.UI.Controls.Text(controls[i]); break;
                case "button": new czyjs.UI.Controls.Button(controls[i]); break;
                case "select": new czyjs.UI.Controls.Select(controls[i]); break;
                default: break;

            }
        }
    }
}

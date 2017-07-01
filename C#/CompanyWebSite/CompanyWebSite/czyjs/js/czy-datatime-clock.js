if (typeof (czyjs.Time) == "undefined") {
    czyjs.Time = {};
}
czyjs.Time.TimeClock = Class.create();
czyjs.Time.TimeClock.prototype = {
    initialize: function (param) {

        this.obj = document.getElementById(param.id);
        this.SetDateTime();
    },

    SetDateTime: function () {
        this.d = new Date();
        var year = this.d.getFullYear();
        var month = this.d.getMonth()+1;
        var day = this.d.getDate();
        var h = this.d.getHours();
        var m = this.d.getMinutes();
        var s = this.d.getSeconds();
        this.obj.innerHTML = year + "年" + month + "月" + day + "日 " + h + ":" + m + ":" + s;
        setTimeout(this.SetDateTime.bind(this), 1000);
    }
}
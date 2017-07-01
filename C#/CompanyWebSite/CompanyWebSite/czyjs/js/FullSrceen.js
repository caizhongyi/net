
    function addbookmark() {
        alert("IE only")
    }
    var mode = 0
    var old = new Array();
    function fullme(e) {
        if (mode == 0) {
            if (typeof document.all != 'undefined') {
                if (top.document.body.offsetWidth == screen.availWidth) {
                    alert("信息提示：你的浏览器已被锁定视窗最大化，无法启用全屏模式！");
                    e.returnValue = false;
                    return false;
                }
                top.moveBy(e.clientX - e.screenX, e.clientY - e.screenY);
                top.resizeBy(screen.availWidth - top.document.body.offsetWidth,
screen.availHeight - top.document.body.offsetHeight);
            } else {
                window.top.moveTo(0, 0);
                window.top.resizeTo(screen.availWidth, screen.availHeight);
                old[0] = window.toolbar.visible;
                old[1] = window.statusbar.visible;
                old[2] = window.menubar.visible;
                window.toolbar.visible = false;
                window.statusbar.visible = false;
                window.menubar.visible = false;
            }
            mode = 1;
        } else {
            if (typeof document.all != 'undefined') {
                top.moveTo(0, 0);
                top.resizeTo(screen.availWidth, screen.availHeight);
            } else {
                window.toolbar.visible = old[0];
                window.statusbar.visible = old[1];
                window.menubar.visible = old[2];
            }
            mode = 0;
        }
        return true;
    }

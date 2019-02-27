var cardReader = new function () {
    // IC卡读取时触发
    this.onIcCardRead = function (callback) {
        var time = new Date();
        var code = "";

        document.onkeypress = function (e) {
            e = e || event;
            var dateT = new Date();
            if (dateT - time > 50) {
                code = "";
            }
            time = dateT;

            if (e.which != 13) {
                code = code + String.fromCharCode(e.which);
            }
            else if (code && callback) {
                callback(fixCode(code));
                e.preventDefault();
                code = "";
            }
        };
    }

    function fixCode(code) {
        while (code && code.length < 16) {
            code = code + '0';
        }
        return code;
    }
};

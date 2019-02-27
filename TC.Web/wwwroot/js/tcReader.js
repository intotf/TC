var tcReader = new function () {
    var address = 'ws://127.0.0.1:4005';
    var ws = new jsonWebSocket(address);
    var versionCallBack;

    // 连接成功
    ws.onopen = function (e) {
        var idReader = 1;
        var fpReader = 2;
        var cameraReader = 4;

        ws.invokeApi("Subscribe", [idReader | fpReader | cameraReader]);
        ws.invokeApi('GetVersion', [], function (ver) {
            versionCallBack && versionCallBack(ver);
        });
    };

    // 未安装本机服务时触发
    this.onNoService = function (callback) {
        ws.onclose = function (e) {
            if (callback) callback();
        }
    }

    // 服务初始完成时触发
    this.onServiceOk = function (callBack) {
        versionCallBack = callBack;
    };

    // 摄像头读取到图像时
    this.onCameraImage = function (callBack) {
        ws.bindApi("OnReadCameraImage", callBack);
    }

    // 身份证读取时触发
    this.onIdCardRead = function (callBack) {
        ws.bindApi("OnReadIdCard", callBack);
    }

    // 指纹读取时触发
    this.onFingerPrint = function (callBack) {
        ws.bindApi("OnReadFingerPrint", callBack);
    } 
};

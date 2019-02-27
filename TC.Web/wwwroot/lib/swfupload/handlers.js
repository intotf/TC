var uploadSWF = "../Content/swfupload/swfupload.swf";
var buttonImag = "../Content/swfupload/button.gif";

/*初始化大文件上传控件*/
function initUploader(url, sizelimit, ext, btnName) {
    var swfu = new SWFUpload({
        // 基本设置
        upload_url: url,
        post_params: { X_Requested_With: "true" },
        // 上传文件设置
        file_size_limit: "" + sizelimit + " MB",
        file_types: "" + ext + "",
        file_types_description: "支持的格式",
        file_upload_limit: 0,    // 0表示不限制选择文件的数量

        // 定义事件				
        file_queued_handler: fileQueued,
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_start_handler: uploadStart,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,
        // 按钮设置
        // Button settings
        button_image_url: buttonImag,
        button_placeholder_id: "spanButtonPlaceholderSingle",
        button_width: 64,
        button_height: 31,
        // button_text: '<span class="button">' + btnName + '</span>',
        button_text_style: '.button {FONT-FAMILY: Arial, Helvetica, sans-serif, "宋体";font-size: 12pt;display:inline-block;width:100%;text-align:left;} ',
        button_text_top_padding: 0,
        button_text_left_padding: 0,

        // Flash设置
        flash_url: uploadSWF,

        // 是否开启调试，true是，false否
        debug: false
    });
    return swfu;
}


function fileQueued(file) {
    try {
        if (file.name != "") {
            $("#uploadfilename").html(file.name);
            var $targetInput = $("input[autofillfilename='1']");
            if ($targetInput.length > 0) {//将文件上传的名称自动填充文件名的textbox中
                if ($targetInput.val().length == 0) {
                    var ext = "." + file.name.replace(/.+\./, "");
                    var mnames = file.name.split("\\");
                    $targetInput.val(mnames[mnames.length - 1].replace(ext, ""));
                }
            }
        }
    } catch (e) {
    }
}

function fileQueueError(file, errorCode, message) {
    try {
        this.cancelUpload(false);
        switch (errorCode) {
            case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                alert("你上传的文件太多.\n" + (message === 0 ? "你已经达到了上传限制." : "你还可以选择 " + (message > 1 ? message + " 文件." : "1个文件.")));
                return;
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                alert("文件大小不能超过" + this.settings.file_size_limit);
                return;
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                alert("您选择的文件是空的。请选择另一个文件.");
                return;
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                alert("你选择的文件是不允许的文件类型.");
                return;
            default:
                //alert("上传时发生了一个错误。请稍后再试.");
                return;
        }
    } catch (e) {
    }
}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
    if (numFilesSelected > 1) {
        $("#filename").val('');
        alert('一次只能选择一个文件上传.');
        return;
    }

    this.startUpload();
}

function uploadStart(file) {
    return true;
}

function uploadProgress(file, bytesLoaded, bytesTotal) {
    try {
        var p = Math.ceil((bytesLoaded / bytesTotal) * 100);
        $("#percent").css("width", p + "%").html(p + '%');
        if ($.isFunction(uploadProgress)) {
            uploadProgress();
        }
    } catch (ex) {
    }
}

function uploadSuccess(file, serverData) {
    onUploadSuccess(serverData);
}

function uploadComplete(file) {
    try {
        //如果文件队列中还有文件，则清空
        if (this.getStats().files_queued > 0) {
            this.cancelUpload(false);
        }
    } catch (e) {
    }
}

function uploadError(file, errorCode, message) {
    try {
        if (errorCode === SWFUpload.UPLOAD_ERROR.FILE_CANCELLED) {
            return;
        }
        //发生错误，信息重置
        $("#percent").css("width", "0%");

        var msg = "";
        // Handle this error separately because we don't want to create a FileProgress element for it.
        switch (errorCode) {
            case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
                msg = "配置错误，上传失败.";
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                msg = "你可能只能上传1个文件.";
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                return;
            default:
                msg = "发生了一个错误，请稍后再试.";
                break;
        }

        if (msg != "") {
            alert(msg);
        }

    } catch (ex) {
    }
}
$.__post = $.post;
$.post = function (url, data, done, dataType) {
    $.ajax({
        url: url,
        data: data,
        type: 'post',
        dataType: dataType,
        success: function (html) {
            if (done) { done(html) }
        }, error: function (xhr, status, error) {
            if (xhr.status == 401) {
                top.location.href = xhr.responseText;
            } else {
                if (toastr) toastr.error(xhr.responseText);
                if (layer) layer.closeAll();
            }
        }
    });
};

// 判断各种浏览器，找到正确的方法
function launchFullscreen(element) {
    if (element.requestFullscreen) {
        element.requestFullscreen();
    } else if (element.mozRequestFullScreen) {
        element.mozRequestFullScreen();
    } else if (element.webkitRequestFullscreen) {
        element.webkitRequestFullscreen();
    } else if (element.msRequestFullscreen) {
        element.msRequestFullscreen();
    }
}

// 全选
function dochkAll() {
    if ($("#chkAll").length > 0 && $("input[name='chk']").length > 0) {
        $("input[name='chk']").prop("checked", $("#chkAll").prop("checked"));
    }
}



$(function () {
    $("[required]").parent("div").prev('label').append("<font color='red'>*</font>");
    if ($(".table .tdNoData").length > 0) {
        $(".tdNoData").attr("colspan", $(".table thead>tr>th").length).css({ "text-align": "center" });
    }

    if (toastr) {
        toastr.options.positionClass = "toast-top-right";
    }


    history.__go = history.go;
    history.__back = history.back;

    history.back = function (arg) {
        if (document.referrer && new Uri(document.referrer).path() != location.pathname) {
            location.href = document.referrer;
        } else {
            history.__back(arg);
        }
    }

    history.go = function (arg) {
        if (document.referrer && new Uri(document.referrer).path() != location.pathname) {
            location.href = document.referrer;
        } else {
            history.__go(arg);
        }
    }
});
(function ($) {
    $.fn.serializeJson = function () {
        var obj = {};
        var form = $(this);
        $(this.serializeArray()).each(function () {
            var value = this.value;
            var type = form.find("input[name='" + this.name + "']").attr("data-type");
            if (type == "bool") {
                value = eval(value || 'false');
            } else if (type == "number") {
                value = Number(value);
            }
            obj[this.name] = value;
        });
        return obj;
    };
})(jQuery);

function requestTest(contentType) {
    $('#dataResponse').val('');
    $('#requestBtn').attr("disabled", "disabled");

    var method = $('#httpMethod');
    var bodyData = null;
    if (contentType) {
        if (contentType.indexOf('www') > 0) {
            bodyData = $.param($('#dataForm').serializeJson());
        } else {
            bodyData = JSON.stringify($('#dataForm').serializeJson(), null, 4);
        }
    }

    var option = {
        type: method.html(),
        url: method.next().val(),
        data: bodyData,
        contentType: contentType,
        beforeSend: function (xhr) {
            if (method.html() != "GET") {
                $('#requestHeader').val('Content-Type: ' + contentType);
            }
            $('#dataRequest').val(bodyData);
        },
        success: function (result, status, xhr) {
            var json = JSON.stringify(result, null, 4);
            $('#dataResponse').val(json);
            $('#requestBtn').removeAttr("disabled");
        },
        error: function (xhr, status, error) {
            $('#dataResponse').val(error);
            $('#requestBtn').removeAttr("disabled");
        }
    };
    $.ajax(option);
}


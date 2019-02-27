(function ($) {
    var defaultOption = {
        // 距离底部多少像素时触发
        offset: 200,
        // 触发之前(实际像素)
        before: function (offset) { },
        // 触发之后(html的jq对象)
        after: function (jq) { },
        // 每页记录大小
        pageSize: 10,
        // 请求的远程地址
        url: '',
        // 请求参数
        queryParam: {}
    };

    var option = {};

    $.fn.scrollPage = function (op) {
        var self = $(this);
        var ajaxing = false;
        var totalCount = -1;

        if ($.isPlainObject(op)) {
            option = $.extend({}, defaultOption, op);
        }
        else {
            var method = (op || '').toLowerCase();
            if (method == 'option') {
                return option;
            } else if (method == 'reload') {
                $(window).scrollTop(0);
                self = self.empty();
                ajax();
                return self;
            }
            return false;
        }

        function init() {
            $(window).scroll(function () {
                var bottomOffset = $(document).height() - $(window).scrollTop() - $(window).height();
                if (bottomOffset > option.offset) {
                    return;
                }

                if (self.children().length >= totalCount) {
                    return;
                }

                if (ajaxing == true) {
                    return;
                }

                if ($.isFunction(option.before)) {
                    option.before(bottomOffset);
                }

                ajax();
            });

            ajax();
            return self;
        }

        function ajax() {
            ajaxing = true;
            var pageIndex = Math.ceil(self.children().length / option.pageSize);
            var urlParam = 'pageIndex=' + pageIndex + '&pageSize=' + option.pageSize;

            var and = option.url.indexOf("?") < 0 ? '?' : '&';
            var queryUrl = option.url + and + urlParam;
            var query = $.isFunction(option.queryParam) ? option.queryParam() : option.queryParam;

            $.post(queryUrl, query, function (data) {
                totalCount = data.totalCount;
                var jq = $(data.html).appendTo(self);
                ajaxing = false;
                if ($.isFunction(option.after)) {
                    option.after(jq)
                }
            });
        }
        return init();
    }
})(jQuery);
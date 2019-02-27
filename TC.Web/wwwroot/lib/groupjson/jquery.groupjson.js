(function ($) {
    // 通过group属性进行分组序列化得到json字符串，数字或逻辑型加number属性表示不做字符串处理    
    $.fn.groupJson = function () {
        var result = [];
        ($(this).first().attr("group") == undefined ? $(this).find("[group]") : $(this)).each(function () {
            var array = [];
            $(this).find("input[name][name!=''],select[name][name!=''],textarea[name][name!='']").each(function () {
                var key = $.trim($(this).attr("name"));
                var value = $.trim($(this).val());
                if ($(this).attr("number") != undefined && (Number(value) != NaN || /^true$|^false$/i.test(value))) {
                    array.push('"' + key + '":' + value);
                } else {
                    if ($(this).attr("type")=="checkbox") {
                        if ($(this).prop("checked")) {
                            array.push('"' + key + '":"' + value + '"');
                        }else
                            array.push('"' + key + '":"false"');//复选框没有选中时给false
                                
                    }else
                        array.push('"' + key + '":"' + value + '"');
                }
            });
            result.push("{" + array.toString() + "}");
        });
        return "[" + result.toString() + "]";
    }
})(jQuery);
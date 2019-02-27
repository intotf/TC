$(function () {
    $("input").keydown(function (e) {
        if (e.keyCode == 13) {
            login($("[type='button']"));
        }
    });
    $("[type=text]:first").focus();
})

function login(dom) {
    $(dom).attr("disabled", "disabled");
    toastr.options.positionClass = "toast-top-right";
    var indexload = layer.load(2, { shade: [0.1, '#fff'] });//0.1透明度的白色背景
    $.post("/home/login", $('form').serialize(), function (data) {
        $(dom).removeAttr("disabled");
        layer.close(indexload);
        if (data.state) {
            toastr.success("登录成功");
            location.replace(data.value);
        } else {
            $("img:last").click();
            toastr.error(data.value)
        }
    })
}

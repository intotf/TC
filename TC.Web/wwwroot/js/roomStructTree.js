; (function (win) {
    var setting = {
        view: {
            selectedMulti: false
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            onClick: function (event, treeId, treeNode) {
                var node = $("#" + treeNode.tId + "_a");
                var offset = node.offset();
                var menu = $(".tree-menu");

                offset.left = offset.left + node.outerWidth();
                menu.offset(offset).children("ul:first").attr("nav-id", treeNode.id);
                console.log(treeNode.pId);

                if (treeNode.isRoom) {
                    menu.addClass("open").find("li.create").hide();
                } else {
                    menu.addClass("open").find("li").show();
                    if (treeNode.pId == null) {
                        menu.find("li.create").siblings().hide();
                    }
                }
            }
        }
    };

    function delRoomStruct(id) {
        $.post('/RoomStruct/Remove', { id: id, force: false }, function (data) {
            if (data.state) {
                toastr.success(data.value);
                location.reload();
            } else {
                toastr.error(data.value);
            }
        });
    }

    $(function () {
        $.post("/RoomStruct/GetRoomStructTree", { IsRoom: true }, function (data) {
            $.fn.zTree.init($("#treeDemo"), setting, data);
        });
        $(".tree-menu").hover(function () { }, function () {
            $(this).removeClass("open");
        })
    })

    win.del = function (dom) {
        var id = $(dom).parents("ul:first").attr("nav-id");
        layer.confirm('确定要删除该结构及其子结构吗？', function (index) {
            $.post('/RoomStruct/CheckRemove', { id: id }, function (cd) {
                if (cd.state) {
                    delRoomStruct(id);
                } else {
                    layer.prompt({ title: "该结构下有数据,如需删除请输入\"确定\" ", formType: 3 }, function (text, index) {
                        if (text == "确定") {
                            delRoomStruct(id);
                        } else {
                            layer.msg("输入错误不予删除");
                            setTimeout(function () { layer.closeAll() }, 1000);
                        }
                    });
                }
            });

        });
    }

    win.create = function (dom) {
        var id = $(dom).parents("ul:first").attr("nav-id");
        layer.open({
            type: 2,
            title: '新增子结构',
            shadeClose: true,
            shade: 0.8,
            area: ['600px', '300px'],
            content: "/roomstruct/create?ParentId=" + id
        });
    }

    win.edit = function (dom) {
        var id = $(dom).parents("ul:first").attr("nav-id");
        layer.open({
            type: 2,
            title: '编辑结构',
            shadeClose: true,
            shade: 0.8,
            area: ['600px', '320px'],
            content: "/roomstruct/edit?id=" + id
        });
    }
})(window);
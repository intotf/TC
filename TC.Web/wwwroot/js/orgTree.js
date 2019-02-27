var setting = {
    view: {
        selectedMulti: false,
        removeHoverDom: function (treeId, treeNode) {
            $(".tree-menu").removeClass("open");
        }
    },
    edit: {
        enable: false
    },
    callback: {
        onClick: function (event, treeId, treeNode) {
            var node = $("#" + treeNode.tId + "_a");
            var offset = node.offset();
            var menu = $(".tree-menu");

            offset.left = offset.left + node.outerWidth();
            menu.offset(offset).children("ul:first").attr("org-id", treeNode.id);

            if (treeNode.createOnly) {
                menu.addClass("open").find("li.create").show().siblings().hide();
            } else {
                if (treeNode.isAdminNode) {
                    menu.addClass("open").find("li.community").hide().siblings().show();
                } else {
                    menu.addClass("open").find("li").show();
                }
            }
        }
    },
    data: {
        simpleData: {
            enable: true
        }
    }
};

function loadOrgData() {
    var keyword = $("#keyword").val();
    $.post("/Organize/getOrganizeTree", { keyword: keyword }, function (data) {
        $.fn.zTree.init($("#treeDemo"), setting, data);
    });
}

function doSearch() {
    loadOrgData();
}

$(function () {
    loadOrgData();
    $(".tree-menu").hover(function () { }, function () {
        $(this).removeClass("open");
    })
})

function create(dom) {
    var id = $(dom).parents("ul:first").attr("org-id");
    location.href = '/organize/create?pid=' + id;
}


function edit(dom) {
    var id = $(dom).parents("ul:first").attr("org-id");
    location.href = '/organize/edit/' + id;
}

function permission(dom) {
    var id = $(dom).parents("ul:first").attr("org-id");
    location.href = '/organize/permission/' + id;
}

function community(dom) {
    var id = $(dom).parents("ul:first").attr("org-id");
    location.href = '/organize/community/' + id;
}


function removeOrg(dom) {
    var id = $(dom).parents("ul:first").attr("org-id");
    layer.confirm('确定要删除此结构吗？', function (index) {
        $.post("/organize/Remove/", { id: id }, function (data) {
            if (data.state) {
                location.reload();
            } else {
                layer.alert(data.value);
            }
        })
        layer.close(index);
    })
}


var MenuManager = {
    init: function () {
        $.get("/Admin/Menu/GetSiteMap?url=" + $("#hidUrl").val() + "&title=" + $("#hidTitle").val(), function (data) {
            if (data != null) {
                $("#siteMap").html(data);
            }else {
                $("#siteMap").html("");
            }
        });
    },
    EditMenu:function (event) {
        var menuId = $("#" + event.id).attr("menuId");
        window.location.href = "/Admin/MenuManager/Edit/"+menuId;
    },
    DeleteMenu: function (event) {
        var menuId = $("#" + event.id).attr("menuId");
        if (confirm("Are you sure? Delete a parent menu will delete every child of it!") == true) {
            $.get("/Admin/MenuManager/Delete?menuId=" + menuId, function (data) {
                if(data==true) {
                    window.location.href = "/Admin/MenuManager/";
                }
            });
        }
    }
};
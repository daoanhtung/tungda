var Menu = {
    init: function () {
        $.get("/Admin/Menu/GetMenu?url=" + $("#hidUrl").val(), function (data) {
            if (data != null) {
                $("#queldoreiNav").html(data);
            }else {
                $("#queldoreiNav").html("");
            }
        });
    }
};
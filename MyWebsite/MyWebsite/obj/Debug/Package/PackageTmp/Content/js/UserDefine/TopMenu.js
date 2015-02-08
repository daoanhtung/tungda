var TopMenu = {
    init: function () {
        $.get("/TopMenu/GetMenu", function (data) {
            if (data != null) {
                $("#queldoreiNav").html(data);
            }else {
                $("#queldoreiNav").html("");
            }
        });
        $.get("/TopMenu/GetCart", function (data) {
            if (data != null && data != "") {
                $("#NoItemInCart").hide();
                $("#HaveItemInCart").show();
                $("#cart-sidebar").html(data);
            } else {
                $("#cart-sidebar").html("");
                $("#NoItemInCart").show();
                $("#HaveItemInCart").hide();
            }
        });
        $.get("/TopMenu/GetCompare", function (data) {
            if (data != null && data != "") {
                $("#compare-items-top").html(data);
            }else {
                $("#compare-items-top").html("");
            }
        });
    }
};
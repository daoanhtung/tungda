var Common = {
    init: function () {
        $('.required-entry').each(function () {
            if ($(this).val() == "") {
                $(this).addClass("validation-failed");
            } else {
                $(this).removeClass("validation-failed");
            }
        });
    },
    ValidateNumber: function (i) {
        if (i.value.length > 0) {
            i.value = i.value.replace(/[^\d ]+/g, '');
        }
    }
};
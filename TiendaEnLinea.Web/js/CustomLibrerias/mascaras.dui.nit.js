$(function() {
    $(".nit").inputmask({
        mask : "9999-999999-999-9",
        placeholder : " ",
        clearIncomplete : true
    });

    $(".dui").inputmask({
        mask : "99999999-9",
        placeholder : " ",
        clearIncomplete : true
    });

    $(".nit.optional").inputmask({
        mask: "[9999-999999-999-9]",
        placeholder: " ",
        clearIncomplete: true
    });

    $(".dui.optional").inputmask({
        mask: "[99999999-9]",
        placeholder: " ",
        clearIncomplete: true
    });
});
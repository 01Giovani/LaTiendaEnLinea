$(document).ready(function () {
    showHideTipoPago();

    $(document).on("change", ".cbx-forma-pago", function (ev) {
        var tipoPago = $(this).val();
        if (tipoPago == "Enlinea") {
            var urlPagoEnLinea = $("#hidden_urlPagoEnLinea").val();
            $("#frm_ProcesarPago").attr("action", urlPagoEnLinea)
        }
        else {
            var urlPagoEnLinea = $("#hidden_urlPagoContraEntrega").val();
            $("#frm_ProcesarPago").attr("action", urlPagoEnLinea)
        }
        showHideTipoPago();
    });
});

function showHideTipoPago() {
    if ($("#formaPagoTarjeta").is(":checked")) {
        $(".container-form-tarjeta").show();
        $(".container-form-efectivo").hide();
    } else {
        $(".container-form-tarjeta").hide();
        $(".container-form-efectivo").show();
    }
}
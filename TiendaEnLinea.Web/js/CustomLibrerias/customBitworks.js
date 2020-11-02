function inicializarHerramientas() {
    $('.footable').footable();

    if (utilidadesBitworks.getQueryString("saved") == "1") {
        swal({
            title: "Confirmación",
            text: "Registro Guardado Exitosamente!",
            type: "success"
        });
    }

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });


    if ($('.input-group.date').length > 0)
        $('.input-group.date').datepicker({
            format: 'dd/mm/yyyy',
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            constrainInput: false
        });

    if ($('.clockpicker').length > 0) $('.clockpicker').clockpicker();

    $('.input-group.date input').attr('readonly', true);
    $('.input-group.clockpicker input').attr('readonly', true);

    $(".canbereadonly").attr("disabled", true);

}




$(document).ready(function () {
    inicializarHerramientas();

    $(document).on("click", ".modal-pop-up", function (ev) {
        ev.preventDefault();
        var urlPregunta = $(this).attr('href');
        var size = $(this).data("size");
        var title = $(this).data("title");

        if (size != null) {

            $("#modal-dialog").removeClass("modal-lg").removeClass("modal-md").removeClass("modal-sm").addClass("modal-" + size);
        }

        var modal = "#modal-forms";
        var divReload = "#modal-content";
        $('#modal-title').text(title);

        $(divReload).empty();

        Bitworks.Ajax.callGetJsonWithGet(urlPregunta, function (respuesta) {
            //$(divReload).empty();
            $(divReload).html(respuesta.Mensaje);
            //inisializo todos las herramientas de nuevo
            inicializarHerramientas();

            //Configuro los validadores de .net si es que existen
            if ($.validator) {
                $.validator.unobtrusive.parse("form");
            }
            //configuro la onda para levantar las ventanas
            utilidadesBitworks.ConfigVal(true);
            //$(modal).modal('show');

            $(modal).modal({
                backdrop: 'static',
                keyboard: false
            });
        });
        if ($.validator) {
            $.validator.unobtrusive.parse("form");
        }
        //configuro la onda para levantar las ventanas
        utilidadesBitworks.ConfigVal(true);
    });


    $(document).on("click", ".eliminar-registro", function (ev) {
        ev.preventDefault();
        var href = $(this).attr('href');
        var $this = $(this);

        var title = $(this).data("title");
        var subtitle = $(this).data("title");

        if (title == null) {
            title = "¿Estás seguro que quieres eliminar este registro?";
        }
        if (subtitle == null) {
            subtitle = "";
        }

        swal({
            title: title,
            text: subtitle,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Aceptar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        },
        function () {
            Bitworks.Ventanas.mostrarLoading();

            $.ajax({
                type: "POST",
                url: href,
                success: function (data) {
                    if (data.Estado === 0) {
                        $this.parent().parent().fadeOut(function () {
                            $(this).remove();
                        });
                        swal("Eliminado", "Registro eliminado con éxito", "success");
                    } else {
                        swal("No eliminado", data.Mensaje, "error");
                    }

                }
            });
        });
    });

});



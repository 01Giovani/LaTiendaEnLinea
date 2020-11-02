
//Metodo para inicializar todos los pluggins o Heramientas que se utilizan en el sitio. 
//se debe agregar la inicilizacion dentro del metodo.
function inicializarHerramientas() {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        //"positionClass": "toast-bottom-center",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    if (utilidadesBitworks.getQueryString("saved") == "1") {
        swal({
            title: "Confirmación",
            text: "Registro Guardado Exitosamente!",
            type: "success"
        });
    }

    //clase para desabilitar controles
    $(".canbereadonly").attr("disabled", true);

    //Configuracion para los validadores de .net si es que existen
    if ($.validator) {
        $.validator.unobtrusive.parse("form");
    }
    //configuracion para para levantar las ventanas de errores
    utilidadesBitworks.ConfigVal(true);


    /*

    //$('.i-checks').iCheck({
    //    checkboxClass: 'icheckbox_square-green',
    //    radioClass: 'iradio_square-green',
    //});

    //para la input date
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




    //para los input de tiempo en horas o minutos
    if ($('.clockpicker').length > 0) $('.clockpicker').clockpicker();
    $('.input-group.date input').attr('readonly', true);
    $('.input-group.clockpicker input').attr('readonly', true);

    */

}

function MostrarLoading(mensaje) {
    if (mensaje === undefined) {
        mensaje = "Cargando...";
    }
    Swal.fire({
        allowOutsideClick: false,
        title: mensaje,
        onBeforeOpen: function () {
            Swal.showLoading();
        }
    });
}

function CerrarLoading(callback) {
    if (callback != null && typeof(callback) == "function") {
        sweetAlert.close(null, function () { callback(); });
    } else {
        sweetAlert.close();
    }
    //$("#ventana_loading").hide().remove();
}




$(document).ready(function() {

    //se inicializa las herramientas o pluggins a utilizar.
    inicializarHerramientas();

    //evento para levantar modal de manera dinamica
    $(document).on("click", ".modal-pop-up", function (ev) {
        ev.preventDefault();
        Bitworks.Ventanas.mostrarLoading();
        var url = $(this).attr('href');
        var size = $(this).data("size");
        var title = $(this).data("title");

        if (size !== null) {
            $("#modal-dialog").removeClass("modal-lg").removeClass("modal-md").removeClass("modal-sm").addClass("modal-" + size);
        }

        var modal = "#modal-forms";
        var divReload = "#modal-content";
        $('#modal-title').text(title);

        $(divReload).empty();
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                //se completa el div html para el modal
                $(divReload).html(data.html);

                //inisializo todos las herramientas de nuevo
                inicializarHerramientas();
                
                //se levanta el modal
                $(modal).modal({
                    backdrop: 'static',
                    keyboard: false
                });

                //se cierra el loading 
                Bitworks.Ventanas.ocultarLoading();
            },
            error: function (response, text, errorThrown) {
                //Bitworks.Ventanas.ocultarLoading();
                swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            }
        });
        
    });

    //evento para eliminar registros de manera dinamica
    $(document).on("click", ".eliminar-registro", function (ev) {
        ev.preventDefault();
        var href = $(this).attr('href');
        var $this = $(this);

        var title = $(this).data("title");
        var subtitle = $(this).data("title");

        if (title === null) {
            title = "¿Estás seguro que quieres eliminar este registro?";
        }
        if (subtitle === null) {
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

//mascaras para inputs de dui y nit
//$(function () {
//    $(".nit").inputmask({
//        mask: "9999-999999-999-9",
//        placeholder: " ",
//        clearIncomplete: true
//    });

//    $(".dui").inputmask({
//        mask: "99999999-9",
//        placeholder: " ",
//        clearIncomplete: true
//    });

//    $(".nit.optional").inputmask({
//        mask: "[9999-999999-999-9]",
//        placeholder: " ",
//        clearIncomplete: true
//    });

//    $(".dui.optional").inputmask({
//        mask: "[99999999-9]",
//        placeholder: " ",
//        clearIncomplete: true
//    });
//});

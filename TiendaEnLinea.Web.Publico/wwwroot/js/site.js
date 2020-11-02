
function ShowStatusCodeError(res) {
    switch (res.status) {
        case 401:
            swal("Error", "Probablemente su sesión se ha vencido o no tiene permiso para ejecutar esta acción.", "error");
            break;
        default:
            swal("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
            break;
    }
}

function ShowErrorListArray(errors) {
    var errorHtml = "<ul>";
    for (var i = 0, len = errors.length; i < len; i++) {
        var error = "<li>" + errors[i] + "</li>";
        errorHtml = errorHtml + error;
    }
    errorHtml = errorHtml + "</ul>";

    swal({
        title: "<h3>Revisa la siguiente información</h3>",
        html: errorHtml
    });
}

$(function () {
    $(document).on('click', '.showLoading', function (ev) {
        MostrarLoading();
    });

    $(document).on("submit", "#frm_RegistrarUsuario", function(ev) {
        MostrarLoading("Creando tu usuario, por favor espera...");
    });

    /*Inicializa los mensajes de alerta*/
    var error = $("#txtMensajeError").val()
    if (error != "" && error != undefined) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            html: error,
        });
    }
    var mensaje = $("#txtMensajeExito").val();
    if (mensaje != "" && mensaje != undefined) { 
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            html: mensaje
        });
    }

    var warning = $("#txtWarningMessage").val();
    if (warning != "" && warning != undefined) {
        Swal.fire({
            icon: 'warning',
            html: warning
        });
    }

    //Este es para el control up&Down de los productos en toda la pagina de navagacion.
    $(document).on("click", ".add-box .input .ctrl", function (e) {
        e.preventDefault();
        var theCtrl = $(this);
        var theInput = theCtrl.siblings("input").first();
        var theValue = parseInt(theInput.val());
        var maxValue = parseInt(theInput.data('maxcantidad'));
        var minValue = parseInt(theInput.data("mincantidad"));
        var newValue = theValue;

        var disable = parseInt(theInput.data("disable"));

        var provoqueEvent = false;

        if (theCtrl.hasClass("up") && theValue < maxValue) {
            newValue = parseInt(theValue + 1);
            provoqueEvent = true;
        } else if (theCtrl.hasClass("down") && theValue > minValue) {
            newValue = parseInt(theValue - 1);
            provoqueEvent = true;
        }

        if (isNaN(newValue) || newValue < minValue) {
            newValue = minValue;
        } else if (newValue > maxValue) {
            newValue = maxValue;
        }
        theInput.val(newValue);

        if (provoqueEvent) {
            if (disable === 1 || disable === "1") {
                theCtrl.attr("disabled", true);
            }
            theInput.trigger("change");
        }
    });

    $(document).on('submit', '#frm-busqueda', function (e) {
        var keyWord = $("#txt-palabra-clave").val();
        if (keyWord.length < 2)
            e.preventDefault();
    });


    //$('.datepicker-age').datepicker({
    //    format: 'dd/mm/yyyy',ssssb
    //    startView: 2
    //});

    $(document).on("submit", "#form-micuenta", function (ev) {
        ev.stopPropagation();
        MostrarLoading("Guardando cambios...");
    });
});


function GetParametersFromUrl(targetUrl) {
    var vars = [], hash;
    var hashes = targetUrl.slice(targetUrl.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}





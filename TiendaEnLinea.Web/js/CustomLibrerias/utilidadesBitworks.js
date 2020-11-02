
//Trasforma validation summary en Jquery UI
/// <reference path="Json.js" />
/// <reference path="jquery-1.5.1.js" />

$(document).ready(function () {
    utilidadesBitworks.ConfigVal();

    $("textarea[data-val-length-max], input[data-val-length-max]").each(function () {
        var $this = $(this);
        var data = $this.data();
        $this.attr("maxlength", data.valLengthMax);
    });
});
//fin vaidation sumary como jquery UI

var utilidadesBitworks = function () {

    var InicializarConfigVal = function (soloEvento) {
        // alert(document.body.clientWidth);
        if (!soloEvento) {
            $(".validation-summary-valid").attr("title", "Revise los siguientes campos");
            $(".validation-summary-errors:first").each(function () {
                $(this).hide();
                //Abrir la validacion con la nuevas alertas del nuevo diseño
                swal({
                    title: "Revise los siguientes campos",
                    text: $(this).html(),
                    html: true

                });
            });
        }

        $("form").submit(function () {
            if (!$(this).valid()) {
                var f = this;
                var $formError = $(this).children(".validation-summary-errors");
                $formError.hide();

                swal({
                    title: "Revise los siguientes campos",
                    text: $formError.html(),
                    html: true

                });

            }
        });

    }
    var getAjaxJson = function (url, data, sucess, failure) {

        if (!data || data == "") {
            data = {};
        }

        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                sucess(data);
                //alert(data.Estado);
            },
            error: function (response, text, errorThrown) {
                if (errorThrown == "Internal Server Error") {
                    try {
                        //este es un error interno del servidor. Problemas en el codigo!
                        //error de programacion... O errores custom como falta de permisos etc....
                        var objetoRespuesta = JSON.parse(response.responseText);
                        failure(objetoRespuesta);
                    }
                    catch (err) {
                        //este error es cuando no encuentra el web service generalmente cuando el web servidor ha fallado
                        // $error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                        failure(null);
                    }
                } else {
                    //este error es cuando hay un problema del lado del cliente generalmente cuando no hay internet
                    //$error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                    failure(null);
                }
            }
        });
    },
    getAjaxPost = function (url, data, sucess, failure) {

        if (!data || data == "") {
            data = {};
        }

        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            data: data,
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                sucess(data);
                //alert(data.Estado);
            },
            error: function (response, text, errorThrown) {
                if (errorThrown == "Internal Server Error") {
                    try {
                        //este es un error interno del servidor. Problemas en el codigo!
                        //error de programacion... O errores custom como falta de permisos etc....
                        var objetoRespuesta = JSON.parse(response.responseText);
                        failure(objetoRespuesta);
                    }
                    catch (err) {
                        //este error es cuando no encuentra el web service generalmente cuando el web servidor ha fallado
                        // $error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                        failure(null);
                    }
                } else {
                    //este error es cuando hay un problema del lado del cliente generalmente cuando no hay internet
                    //$error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                    failure(null);
                }
            }
        });
    },
     getAjaxJsonWithGet = function (url, sucess, failure) {
         $.ajax({
             type: "GET",
             url: url,
             dataType: "json",
             cache: false,
             success: function (data, textStatus, jqXHR) {
                 sucess(data);
             },
             error: function (response, text, errorThrown) {
                 if (errorThrown == "Internal Server Error") {
                     try {
                         //este es un error interno del servidor. Problemas en el codigo!
                         //error de programacion... O errores custom como falta de permisos etc....
                         var objetoRespuesta = JSON.parse(response.responseText);
                         failure(objetoRespuesta);
                     }
                     catch (err) {
                         //este error es cuando no encuentra el web service generalmente cuando el web servidor ha fallado
                         // $error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                         failure(null);
                     }
                 } else {
                     //este error es cuando hay un problema del lado del cliente generalmente cuando no hay internet
                     //$error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                     failure(null);
                 }
             }
         });
     },
    getAjaxHtml = function (url, sucess, failure) {
        $.ajax({
            type: "GET",
            url: url,
            dataType: "HTML",
            cache: false,
            success: function (data, textStatus, jqXHR) {
                sucess(data);
            },
            error: function (response, text, errorThrown) {
                if (errorThrown == "Internal Server Error") {
                    try {
                        //este es un error interno del servidor. Problemas en el codigo!
                        //error de programacion... O errores custom como falta de permisos etc....
                        var objetoRespuesta = JSON.parse(response.responseText);
                        failure(objetoRespuesta);
                    }
                    catch (err) {
                        //este error es cuando no encuentra el web service generalmente cuando el web servidor ha fallado
                        // $error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                        failure(null);
                    }
                } else {
                    //este error es cuando hay un problema del lado del cliente generalmente cuando no hay internet
                    //$error.MostrarError("<b>Ha ocurrido un error en la comunicación con el servidor.</b><br/>Generalmente este error se debe a problemas de red. Recargue la página e intente de nuevo. Si no se puede restablecer la comunicación en 10 segundos sus conversaciones se asignaran a otro operador.");
                    failure(null);
                }
            }
        });
    },
    //Funcion que me dice si el objeto es un arreglo o no.
    isArray = function (arg) {

        if (typeof arg == 'object') {
            var criteria = arg.constructor.toString().match(/array/i);
            return (criteria != null);
        }
        return false;
    },
    isIframe = function () {
        return (window.location != window.parent.location) ? true : false;
    },
    //funcion que cortar los nombres largos y le agrega ...
    cortarStringLargos = function (texto, topCaracteres) {
        if (texto.length > topCaracteres) {
            texto = texto.substring(0, (topCaracteres - 3));
            texto += "...";
        }
        return texto;
    },
    getParameterByName = function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    },
    isInteger = function (str) {
        var n = ~ ~Number(str);
        return String(n) === str;
    }
    return {
        getAjaxJson: getAjaxJson, getAjaxHTML: getAjaxHtml, isArray: isArray, isIframe: isIframe, cortarStringLargos: cortarStringLargos, getQueryString: getParameterByName, ConfigVal: InicializarConfigVal,
        getAjaxJsonWithGet: getAjaxJsonWithGet, PostAjaxFormValues: getAjaxPost, isInteger: isInteger
    };
}();


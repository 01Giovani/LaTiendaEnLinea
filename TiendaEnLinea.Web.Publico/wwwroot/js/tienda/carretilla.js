$(document).ready(function () {

    $(document).on("click", "#btn_GuardarBeneficiario", function (ev)
    {
        ev.preventDefault();

        var url = $(this).attr("href");
        var idproductotienda = $("#txh_IdProductoTienda").val();
        var urlCarretilla = $("#url-agregarcarretilla").val(); 
        var cantidad = 1;
        var min = 1;
        var idinputcant = "";

        var nombreBeneficiario = $("#txtNombreBeneficiario").val();

        if (!nombreBeneficiario)
        {
            Swal.fire("Campo Requerido", "Por favor, ingrese el nombre del beneficiario de la tarjeta VIP", "warning");
            return;
        }

        MostrarLoading();

        $.ajax({
            type: "POST",
            url: url,
            data: { NombreBeneficiario: nombreBeneficiario},
            success: function (jResp)
            {
                switch (jResp.result)
                {
                    case 1:
                        Carretilla.addEditProducto(idproductotienda, cantidad, urlCarretilla, true, idinputcant, min);
                        $.fancybox.close();
                        $("#txtNombreBeneficiario").val("");
                        break;
                    default:
                        Swal.fire("Ops!", jResp.message, "error");
                        break;
                }
            },
            error: function (error)
            {
                switch (error.status) {
                    case 401:
                        Swal.fire("Error", "Su sesión ha caducado, porfavor recargue la página y vuelva a iniciar sesión.", "warning");
                        break;
                    default:
                        Swal.fire("Error", "Se ha producido un error durante la operación. Por favor contacte a su servicio de soporte técnico", "error");
                        break;
                }
            }

        });

    });

    //metodo para agregar o editar detalle de la carretilla
    $(document).on("click", ".add-carretilla-btw", function (ev) {
        ev.preventDefault();
        var urlAgregar = $("#url-agregarcarretilla").val();
        var idproductotienda = $(this).data('idproductotienda');
        var idinputcant = $(this).data('idinputcant');
        var idProductoAdmin = $(this).data('idproducto');

        var max = $(this).data("maxcantidad") * 1;
        var min = $(this).data("mincantidad") * 1;
        var multiplo = $(this).data("multiplo") * 1;
        var cantidad = $(idinputcant).val() * 1;


        if (isNumber(cantidad) === false) {
            Swal.fire("Error", "La cantidad debe ser un número.", "error");
            $(idinputcant).val(min);
            return;
        }

        //hago las validaciones del producto.
        //la cantidad es menor o mayor a la configurada.
        if (cantidad < min) {
            Swal.fire("Error", "La cantidad mínima de compra de este producto es de " + min , "error");
            $(idinputcant).val(min);
            return;
        }
        if (cantidad > max) {
            Swal.fire("Error", "La cantidad máxima de compra de este producto es de " + max, "error");
            $(idinputcant).val(min);
            return;
        }

        if (IsMultiplo(cantidad, multiplo) === false) {
            Swal.fire("Error", "La cantidad debe ser un múltiplo de " + multiplo, "error");
            //Swal.fire("Error", "La cantidad debe ser divisible entre 1", "error");
            $(idinputcant).val(min);
            return;
        }

        Carretilla.addEditProducto(idproductotienda, idProductoAdmin, cantidad, urlAgregar, false, idinputcant, min);
    });

    //Metodo para editar un detalle de carretilla cuando cambia la cantidad el onchange de un input
    $(document).on("change", ".edit-carretilla-btw", function (ev) {
        ev.preventDefault();

        var urlModificar = $("#url-editarcarretilla").val();
        var idproductotienda = $(this).data('idproductotienda');
        var idinputcant = $(this).data('idinputcant');
        var idProductoAdmin = $(this).data('idproducto');

        var max = $(this).data("maxcantidad") * 1;
        var min = $(this).data("mincantidad") * 1;
        var multiplo = $(this).data("multiplo") * 1;
        var cantidad = $(idinputcant).val() * 1;

        if (isNumber(cantidad) === false) {
            Swal.fire("Error", "La cantidad debe ser un número.", "error");
            $(idinputcant).val(min);
            return;
        }

        //hago las validaciones del producto.
        //la cantidad es menor o mayor a la configurada.
        if (cantidad < min) {
            Swal.fire("Error", "La cantidad mínima de compra de este producto es de " + min, "error");
            $(idinputcant).val(min);
            return;
        }
        if (cantidad > max) {
            Swal.fire("Error", "La cantidad máxima de compra de este producto es de " + max, "error");
            $(idinputcant).val(min);
            return;
        }

        if (IsMultiplo(cantidad, multiplo) === false) {
            Swal.fire("Error", "La cantidad debe ser un múltiplo de " + multiplo, "error");
            //Swal.fire("Error", "La cantidad debe ser divisible entre 1", "error");
            $(idinputcant).val(min);
            return;
        }

        Carretilla.editProductoCarretilla(idproductotienda, idProductoAdmin, cantidad, urlModificar, "#contenedor_listadoProductos_DetalleCarretilla");
    });

    //Metodo para eliminar un detalle de carretilla
    $(document).on("click", ".delete-detalle-btw", function (ev) {
        ev.preventDefault();
        var urlAgregar = $("#url-eliminardetalle").val();
        var idDetalle = $(this).data('codigodetalle');
        Swal.fire({
            title: "",
            text: "¿Está seguro de que desea eliminar este producto de su carretilla?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si",
            cancelButtonText: "Cancelar",
        }).then(function (result) {
            if (result.value) {
                Carretilla.deleteProducto(idDetalle, urlAgregar, "#contenedor_listadoProductos_DetalleCarretilla");
            }
        });
    });

    //metodo para limpiar la carretilla, elimina todos los detalles
    $(document).on("click", "#delete-carretilla-btw", function (ev) {
        ev.preventDefault();
        var urlEliminar = $(this).data("url");

        Swal.fire({
            title: "",
            text: "¿Está seguro de que desea eliminar todos los productos de su carretilla?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false,
            closeOnCancel: true
        }).then(function (result) {
            if (result.value) {
                Carretilla.deleteCarretilla(urlEliminar, "#renderDetalles");
            }
        });

    });

    //---------------ACCION PARA PASAR AL SIGUIENTE PASO DEL RESUMEN ----------------------------//

    $(document).on("click", "#btn-Accion-Continuar", function (e) {
        e.preventDefault();
        //valido si el pedido tiene algún producto que requiere receta.
        if ((/true/i).test($("#txh_productoReqReceta").val())) {
            //valido si se ha subido algún archivo de receta.
            if (!(/true/i).test($("#txh_anyImageUploaded").val())) {
                Swal.fire("Productos requieren receta.", "En su carretilla tiene algunos productos que requieren receta. Por favor suba foto de la receta para continuar.", "warning");
                return;
            }
        }

        //valido que haya seleccionado un tipo de pago del pedido, esto para mostrarle los descuento correctos.
        if ($("#ban-seleccionpago").val() === "" || $("#ban-seleccionpago").val() === undefined) {
            //$("#dll-tipo-pago").focus();
            //Swal.fire("", "Por favor selecciona un tipo de pago, para poder visualizar los descuentos según tu forma de pago.", "warning");
            Swal.fire({
                title: "",
                text: "Por favor selecciona un tipo de pago, para poder visualizar los descuentos según tu forma de pago.",
                type: 'warning',
                confirmButtonText: "Aceptar",
                closeOnConfirm: true,
                focusConfirm: false,
            }).then(function (result) {
                if (result.value) {
                    $("#dll-tipo-pago").focus();
                }
            });

            return;
        }
        window.location.href = $(this).attr("href");
    });

    $(document).on("change", "#dll-tipo-pago", function (e) {
        MostrarLoading("Calculando descuentos…");
        $("#frm-tipo-pago").submit();        
    });


      //-------------------------------------------- ----------------------------//
});

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function IsMultiplo(numero, multiplo) {
    var remainder = (numero % multiplo) / 100;
    if (remainder === 0) {
        // numero is a multiple of multiplo
        return true;
    }
    return false;
}

var Carretilla = (
    function () {

        function Carretilla() {
        }

        Carretilla.addEditProducto = function (idproducto, idProductoAdmin, cantidad, url, editarCantidad, idinput, cantMin) {
            var prod = new Producto(idproducto, idProductoAdmin, cantidad, editarCantidad);
            loadingTienda("Cargando...");

            //Bitworks.Ventanas.mostrarLoading("");
            $.ajax({
                type: "POST",
                url: url,
                data: prod,
                success: function (data) {
                    if (data.existoso === true) {

                       CerrarLoading();
                        //agregar aqui alguna funcionalidad extra cuando se agregado al producto
                        //aqui solo muestro el mensaje que viene en la respuesta
                        toastr.success("Producto Agregado Correctamente.", "", { timeOut: 3000, showEasing: "swing" });

                        //Debo resetear el input con la cantidad minima.
                        $(idinput).val(cantMin);
                        Carretilla.ActualizarIconoCarretilla();

                        //Debo actualiza total de caretilla
                        //$("#cont-icon-carrito").html(data.html);

                    } else {
                        //muestro el error si no se puedo agregar en la carretilla segun lo que viene
                        //en la respuesta
                        Swal.fire("Error", data.mensaje, "error");
                        console.log(data.requestError);
                    }
                },
                error: function (response, text, errorThrown) {
                    //Bitworks.Ventanas.ocultarLoading();
                    Swal.fire("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
                }
            });
        };

        Carretilla.editProductoCarretilla = function (idproducto, idProductoAdmin, cantidad, url, idcontent) {
            var prod = new Producto(idproducto, idProductoAdmin, cantidad, true);
            loadingTienda();
            $.ajax({
                type: "POST",
                url: url,
                data: prod,
                success: function (data) {
                    if (data.existoso === true) {
                        //actualiza total de caretilla
                        $(idcontent).html(data.html);

                        var ahorro = $("#lbl-d-ahorro").text();
                        var pago = $("#lbl-d-pago").text();
                        $("#lbl-t-ahorro").text(ahorro);
                        $("#lbl-t-pago").text(pago);

                        CerrarLoading();
                        //agregar aqui alguna funcionalidad extra cuando se agregado al producto
                        //aqui solo muestro el mensaje que viene en la respuesta
                        toastr.success("Cantidad editada correctamente.", "", { timeOut: 3000, showEasing: "swing" });
                        //resteon el input con la cantidad minima.                                             

                        Carretilla.ActualizarIconoCarretilla();

                        //window.location.reload();
                    } else {
                        //muestro el error si no se puedo agregar en la carretilla segun lo que viene
                        //en la respuesta
                        Swal.fire("Error", data.mensaje, "error");
                    }
                },
                error: function (response, text, errorThrown) {
                    //Bitworks.Ventanas.ocultarLoading();
                    Swal.fire("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
                }
            });
        };

        Carretilla.ActualizarIconoCarretilla = function (abrir) {
            var url = $("#url-actualizar-icono-carretilla").val();
            $.ajax({
                type: "GET",
                url: url,
                success: function (data) {
                    if (data.existoso === true) {
                        //actualiza total de caretilla
                        $("#cont-icon-carrito").html(data.html);
                    }
                }
            });
        };

        Carretilla.deleteProducto = function (idDetalle, url, idcontent) {
            loadingTienda();
            $.ajax({
                type: "POST",
                url: url,
                data: { idDetalle: idDetalle },
                success: function (data) {
                    if (data.existoso === true) {
                        CerrarLoading();
                        //agregar aqui alguna funcionalidad extra cuando se agregado al producto
                        //aqui solo muestro el mensaje que viene en la respuesta
                        //Swal.fire("", "El producto se ha eliminado correctamente.", "success");
                        toastr.success(data.mensaje, "", { timeOut: 3000, showEasing: "swing" });
                        $(idcontent).html(data.html);
                        var ahorro = $("#lbl-d-ahorro").text();
                        var pago = $("#lbl-d-pago").text();
                        $("#lbl-t-ahorro").text(ahorro);
                        $("#lbl-t-pago").text(pago);

                        Carretilla.ActualizarIconoCarretilla();
                        //$(".total-carrito").text(data.Otros);
                    } else {
                        //muestro el error si no se puedo agregar en la carretilla segun lo que viene
                        //en la respuesta
                        Swal.fire("Error", data.mensaje, "error");
                    }
                },
                error: function (response, text, errorThrown) {
                    Swal.fire("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
                }
            });
        };

        Carretilla.deleteProductoResumen = function (url) {
            loadingTienda();
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.estado === true) {
                        CerrarLoading();
                        //agregar aqui alguna funcionalidad extra cuando se agregado al producto
                        //aqui solo muestro el mensaje que viene en la respuesta
                        toastr.success("Producto eliminado correctamente.", "", { timeOut: 3000, showEasing: "swing" });
                        Carretilla.ActualizarIconoCarretilla(true);
                    } else {
                        //muestro el error si no se puedo agregar en la carretilla segun lo que viene
                        //en la respuesta
                        Swal.fire("Error", data.mensaje, "error");
                    }
                },
                error: function (response, text, errorThrown) {
                    Swal.fire("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
                }
            });
        };

        Carretilla.deleteCarretilla = function (url, idcontent) {
            loadingTienda();
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.existoso === true) {
                        CerrarLoading();
                        //agregar aqui alguna funcionalidad extra cuando se agregado al producto
                        //aqui solo muestro el mensaje que viene en la respuesta
                        //Swal.fire("", "El producto se ha eliminado correctamente.", "success");
                        toastr.success(data.mensaje, "", { timeOut: 3000, showEasing: "swing" });
                        $(idcontent).html(data.html);
                        Carretilla.ActualizarIconoCarretilla();
                        //$(".total-carrito").text(data.Otros);
                    } else {
                        //muestro el error si no se puedo agregar en la carretilla segun lo que viene
                        //en la respuesta
                        Swal.fire("Error", data.mensaje, "error");
                    }
                },
                error: function (response, text, errorThrown) {
                    Swal.fire("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
                }
            });
        };

        Carretilla.editarFavorito = function (url, idcontent, idProducto, element) {
            loadingTienda();
            $.ajax({
                type: "POST",
                url: url,
                data: { idProducto: idProducto },
                success: function (data) {
                    if (data.Exito === true) {
                        CerrarLoading();
                        toastr.success(data.Mensaje, "", { timeOut: 3000, showEasing: "swing" });
                        $(idcontent).html(data.Html);
                    } else {

                        CerrarLoading();
                        toastr["error"](data.Mensaje);
                        $(element).removeClass("active");
                    }
                },
                error: function (response, text, errorThrown) {
                    Swal.fire("Error", "Lo sentimos, la acción no pudo ser procesada.", "error");
                }
            });
        };

        return Carretilla;

    }());

function loadingTienda(title) {
    MostrarLoading(title);
}

function CerrarloadingTienda() {
    return CerrarLoading();
}

var Producto = (function () {
    function Producto(idproducto, idProductoAdmin, cantidad, editarCantidad) {
        this.IdProducto = idproducto;
        this.IdProductoAdmin = idProductoAdmin;
        this.Cantidad = cantidad;
        this.EditarCantidad = editarCantidad;
    }
    return Producto;
}());

//agregar funcion para resetear un input de agregar producto.

//---------- Subir archivo - Receta ---------------

/*
 Dropzone.autoDiscover = false;

function actualizarContenedorImagenes(html) {
    if (html === '') {
        var url = $("#urlListadorecetas").val();
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                $("#ContenedorImagenes").html(data.htmlListaRecetas);
            }
        });
    } else {
        $("#ContenedorImagenes").html(html);
    }
   CerrarLoading();
}

var dropzoneGaleria;

function InicializarDropZone() {
    //div para mostrar las imagenes de preview
    var htmlPreview = $("div#previewDropZoneReceta").html();

    if (htmlPreview !== undefined) {

        //inicializo el dropzone en una variable.
        dropzoneGaleria = $("form#upload-widget").dropzone({
            paramName: "file",
            uploadMultiple: false,
            previewTemplate: htmlPreview,
            parallelUploads: 20,
            acceptedFiles: "image/jpeg,image/pjpeg,image/png",
            autoProcessQueue: false,
            maxFiles: 100,
            maxFilesize: 20,
            dictDefaultMessage: "Haga click aquí para subir un archivo o arrastrelo y sueltelo.",
            init: function () {
                //aqui agregar los handlers de los diferentes eventos
                var myDropZonse = this;

                //evento cuando se esta mandando un archivo al servidor
                myDropZonse.on("sending", function (file, xhr, formdata) {
                });

                myDropZonse.on("addedfile", function (file, xhr, formdata) {
                    $("#btn-cancel-upload-files").show();
                });

                //evento cuando ya se ha subido todos los archivos.
                myDropZonse.on("queuecomplete", function () {
                    $("#btn-cancel-upload-files").hide();
                    myDropZonse.removeAllFiles();
                    actualizarContenedorImagenes('');
                    $("#btn-upload-files").removeAttr('disabled', 'disabled');
                   CerrarLoading();
                    toastr.success('Los archivos se agregaron exitosamente');
                });

                //Evento que se ejecuta con cada imagen subida con exito
                myDropZonse.on("success", function (file, respServer) {
                    actualizarContenedorImagenes(respServer.htmlListaRecetas);
                    $("#btn-cancel-upload-files").hide();
                });

                //evento cuando se generar un error en el dropzone
                myDropZonse.on("error", function (file, errormessage, xhr) {
                    toastr.error('Los archivos a subir no puede ser mayor a 20mb', "Archivos Excede limite de tama&ntilde;o");
                    actualizarContenedorImagenes("");
                    $("#btn-cancel-upload-files").hide();
                });

            }
        })[0].dropzone;
    }


}*/
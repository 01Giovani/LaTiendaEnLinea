$(document).ready(function ()
{
    $(document).on("click", "#btn-cargar-mas-productos", function (event){
        event.preventDefault();
        MostrarLoading('Cargando...');

        var url = $(this).attr('href');
        var topTotalProductos = $(this).data("topbusqueda");
        $.ajax({
            url: url,
            type: 'GET',
            success: function (resultado) {
                if (resultado.htmlProductos != undefined) {
                    var containerProductos = $('#contenedor-listado-productos');
                    containerProductos.append(resultado.htmlProductos);
                }

                var containerBotonMas = $('#contenedor-boton-mas');

                if (topTotalProductos > parseInt($("#txt_TotalResultadoProductos").val())) {
                    $(containerBotonMas).remove();
                }
                else {
                    if (resultado.htmlBoton != undefined) {
                        containerBotonMas.html(resultado.htmlBoton);
                    }
                }
                sweetAlert.close();
            },
            error: function (error) {
                Swal.fire('error', error.statusText, "error");
            }
        });
    });
});
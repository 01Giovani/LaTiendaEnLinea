$(document).ready(function () {
  

});

$(document).on("click", ".tabTipoEntrega", function (ev)
{
    ev.stopPropagation();
    var tipoEntrega = $(this).data("tipoentrega");
    $("#hidden_TipoEntrega").val(tipoEntrega);
});

$(document).on("change", "#ddDireccion_Beneficiario", function (ev) {
    var codigoBeneficiario = $(this).val();

    var OcultarTablaBeneficiarios = $("#tabla-TodosLosBeneficiarios").data("ocultartabla");
    if ((/true/i).test(OcultarTablaBeneficiarios)) {
        if (codigoBeneficiario != "")
            $("#tabla-TodosLosBeneficiarios").addClass("displayNone");
        else 
            $("#tabla-TodosLosBeneficiarios").removeClass("displayNone");
    }

    //Muestro el detalle resumido del beneficiario
    $('.beneficiario_resumen').removeClass("beneficiarioSeleccionado");
    $('.beneficiario_resumen[data-idbeneficiario="' + codigoBeneficiario + '"]').addClass("beneficiarioSeleccionado");
    //Oculto el beneficiario seleccionado de la lista de todos los beneficiarios 
    $('.trListadoTodasDirecciones').removeClass("beneficiarioSeleccionado");
    $('.trListadoTodasDirecciones[data-idbeneficiario="' + codigoBeneficiario + '"]').addClass("beneficiarioSeleccionado");
});

$(document).on("change", "#ddSucursal_Entrega", function (ev) {
    var codigoSucursal = $(this).val();
    //Muestro el detalle resumido de la sucursal
    $('.sucursal_resumen').removeClass("sucursalSeleccionada");
    $('.sucursal_resumen[data-idsucursal="' + codigoSucursal + '"]').addClass("sucursalSeleccionada");
});

$(document).on("click", ".btn-add-edit-beneficiario", function (e) {
    e.preventDefault();
    MostrarLoading();
    var urlAddEditBeneficiario = $(this).data("url");

    $.ajax({
    type: "post",
    url: urlAddEditBeneficiario,
    dataType: "json",
    success: function (res) {
        if (res.success === false) {
        swal.fire("Ops!", "Ocurrio un error, por favor intenta recargar la pagina", "error");
        return;
        }
        $("#render-beneficiario").html(res.html);
        RecalcularMunicipios();
        //Configuro los validadores de .net si es que existen
        if ($.validator) {
        $.validator.unobtrusive.parse("form");
        }
        //configuro la onda para levantar las ventanas
        utilidadesBitworks.ConfigVal(true);
        CerrarLoading();
        $("#render-beneficiario").modal({backdrop: 'static', keyboard: false});
          
    },
    error: function (err) {
        swal.fire("Ops!", "Ocurrio un error, por favor intenta recargar la pagina", "error");
    }
    });
});

$(document).on("submit", "#frm_GuardarBeneficiario",
    function (e) {
      e.preventDefault();
      MostrarLoading();
      $("#btn-form-beneficiario").prop("disabled", true);
      var form = $(this);

      $.ajax({
        type: "post",
        url: form.attr("action"),
        dataType: "json",
        data: form.serialize(),
        success: function (res) {
          if (res.success) {
              $('#render-beneficiario').modal('toggle');
              MostrarLoading("Guardando...");
              location.reload();
            return;
            }

            $("#btn-form-beneficiario").prop("disabled", false);

          if (res.errorMessage != null) {
              swal.fire("Ops!", res.errorMessage, "error");
              return;
          }
          var errors = [];
          $.each(res.validationErrors,
              function (i, error) {
                errors.push(error);
              });
          showErrorListFromArray(errors);
        },
        error: function (err) {
            swal.fire("Ops!", "Ocurrio un error, por favor intenta recargar la pagina", "error");
            $("#btn-form-beneficiario").prop("disabled", false);
        }
      });

    });

$(document).on("change", "#dll-departamentos", function () {
      var iddep = $(this).val();
      if (iddep == "") {
        $("option[data-iddepartamento]").hide();
        $('#dll-municipios option:first').prop('selected', true);
      } else {
        $("option[data-iddepartamento]").hide();
        //$('#dll-departamentos option:first').prop('selected', true);
        $('#dll-municipios option:first').prop('selected', true);
        $("option[data-iddepartamento=" + iddep + "]").show();
      }
    });


$(document).on("submit", "#frmPagar", function (e) {
    MostrarLoading("Agregando Datos de Entrega.");
});

$(document).on("click", ".eliminarBeneficiario", function (ev) {
    ev.preventDefault();
    ev.stopPropagation();

    var urlAddEditBeneficiario = $(this).data("url");
    var idBeneficiario = $(this).data("idbeneficiario");
    swal.fire({
        title: "&#191;Esta seguro que desea eliminar esta informaci&#243;n de entrega?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonClass: "btn-primary",
        confirmButtonText: "Eliminar",
        cancelButtonText: "Cancelar"
    }).then(function (result) {
        if (result.value) {
            MostrarLoading("Eliminando registro...");
            $("#hidden_IdBeneficiario").val(idBeneficiario);
            $("#frm_EliminarBeneficiario").submit();
        }
    });
});

$(document).on("click", "#btn-asignar-beneficiario", function (ev) {
    ev.stopPropagation();

    var isFormValid = true;
    var message;

    if ($("#hidden_TipoEntrega").val() == "Domicilio")
    {
        if ($("#ddDireccion_Beneficiario").val() == "")
        {
            isFormValid = false;
            message = "Por favor agregue o seleccione una direcci&#243;n de entrega.";
        }
    }
    else
    {
        if ($("#ddSucursal_Entrega").val() == "") {
            isFormValid = false;
            message = "Por favor seleccione una sucursal de entrega.";
        }
    }

    if(isFormValid)
    {
        MostrarLoading("Guardando informaci&#243;n de entrega.");
        $("#frm_DefinirDatosEntrega").submit();
    }
    else {
        Swal.fire({
            icon: 'warning',
            title: message
        });
    }
});

function IsNullOrEmpty(value) {

  if (value === "")
    return true;
  else if (value === null)
    return true;
  else if (value === undefined)
    return true;
  return false;

};



function showErrorListFromArray(errors) {
  var errorHtml = "<ul>";
  for (var i = 0, len = errors.length; i < len; i++) {
    var error = "<li>" + errors[i] + "</li>";
    errorHtml = errorHtml + error;}
  errorHtml = errorHtml + "</ul>";
  Swal.fire({
      title: '<h3>Revisa la siguiente informaci&oacute;n</h3>',
      html: errorHtml
  });
}

function RecalcularMunicipios() {
    var departamento = $("#dll-departamentos").val();
    if (departamento != "") {
        $("option[data-iddepartamento=" + departamento + "]").show();
    }
}

$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesCotiz();
    creaEventosRegistroCotiz();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesCotiz() {
    $("#frmRegistroCotizModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIDUsuario: {
                required: true
            },
            pNumeroCotizacion: {
                required: true
            },
            pNombreCliente: {
                required: true
            },
            pTelefonoCliente: {
                required: true
            },
            pCorreoCliente: {
                required: true
            },
            pHoraCot: {
                required: true
            },
            pCosto: {
                required: true
            }, 
        }
    });
}

function abriModal() {
    $("#btnAbrirModal").on("click", function () {
        //$("#exampleModal").modal('show');

        showMessageRegistrarse("Hola");
    });

    $("#btnCerrarModal").on("click", function () {
        $("#exampleModal").modal('hide');
    });
}

function creaEventosRegistroCotiz() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroCotizModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosCotizM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosCotizM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Cotizacion/NuevaCotizacionModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdUsuario: $("#pIdUsuario").val(),
        pNumeroCotizacion: $("#pNumeroCotizacion").val(),
        pNombreCliente: $("#pNombreCliente").val(),
        pTelefonoCliente: $("#pTelefonoCliente").val(),
        pCorreoCliente: $("#pCorreoCliente").val(),
        pHoraCot: $("#pHoraCot").val(),
        pCosto: $("#pCosto").val()

    };

    var funcion = cargaMensajeModalCotiz;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalCotiz(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalCotizacion").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}
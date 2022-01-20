
$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesCita();
    creaEventosRegistroCita();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesCita() {
    $("#frmRegistroCitaModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIdUsuario: {
                required: true
            },
            pIdMarca: {
                required: true
            },
            pNombreCliente: {
                required: true
            },
            pNumeroCliente: {
                required: true
            },
            pPlacaMoto: {
                required: true
            },
            pFechaCita: {
                required: true
            },
            pHoraCita: {
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

function creaEventosRegistroCita() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroCitaModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosCitaM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosCitaM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Citas/NuevaCitaModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdUsuario: $("#pIdUsuario").val(),
        pIdMarca: $("#pIdMarca").val(),
        pNombreCliente: $("#pNombreCliente").val(),
        pNumeroCliente: $("#pNumeroCliente").val(),
        pPlacaMoto: $("#pPlacaMoto").val(),
        pFechaCita: $("#pFechaCita").val(),
        pHoraCita: $("#pHoraCita").val()

    };

    var funcion = cargaMensajeModalCita;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalCita(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalCitas").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}

$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesSalida();
    creaEventosRegistroSalida();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesSalida() {
    $("#frmRegistroSalidaModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIDUsuario: {
                required: true
            },
            pHoraSalida: {
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

function creaEventosRegistroSalida() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroSalidaModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosSalidaM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosSalidaM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Salidas/NuevaSalidaModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIDUsuario: $("#pIDUsuario").val(),
        pHoraSalida: $("#pHoraSalida").val()
        
    };

    var funcion = cargaMensajeModalSal;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalSal(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalSalidas").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}
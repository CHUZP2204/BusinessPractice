$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesTipoServicio();
    creaEventosRegistroTServicio();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesTipoServicio() {
    $("#frmRegistroTServicioM").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pNombreTipoSERV: {
                required: true
            },
            pDescripcionTSERV: {
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

function creaEventosRegistroTServicio() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroTServicioM");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosTServicioM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosTServicioM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/TipoServicios/NuevoTipoSERVModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pNombreTipoSERV: $("#pNombreTipoSERV").val(),
        pDescripcionTSERV: $("#pDescripcionTSERV").val()
    };

    var funcion = cargaMensajeModalTServ;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalTServ(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalTServicios").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}
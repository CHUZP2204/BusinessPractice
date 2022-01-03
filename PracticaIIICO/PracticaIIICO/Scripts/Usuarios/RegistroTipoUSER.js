$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesTUSER();
    creaEventosRegistroTUSER();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesTUSER() {
    $("#frmRegistroTUSERM").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pNombreTUsuario: {
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

function creaEventosRegistroTUSER() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroTUSERM");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosTUSERM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosTUSERM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/TipoUsuarios/NuevoTipoUSERModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pNombreTUsuario: $("#pNombreTUsuario").val()
    };

    var funcion = cargaMensajeModalTUSER;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalTUSER(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalTUSER").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}
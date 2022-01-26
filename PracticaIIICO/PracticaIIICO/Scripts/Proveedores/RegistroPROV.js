$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesPROV();
    creaEventosRegistroProv();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesPROV() {
    $("#frmRegistroPROV").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pNombrePROV: {
                required: true
            },
            pCorreoPROV: {
                required: true
            },
            pDireccionPROV: {
                required: true
            },
            pTelefonoPROV: {
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

function creaEventosRegistroProv() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroPROV");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosProvM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosProvM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Proveedores/RegistrarPROVModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pNombrePROV: $("#pNombrePROV").val(),
        pCorreoPROV: $("#pCorreoPROV").val(),
        pDireccionPROV: $("#pDireccionPROV").val(),
        pTelefonoPROV: $("#pTelefonoPROV").val()
    };

    var funcion = cargaMensajeModalProv;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalProv(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalProveedores").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}

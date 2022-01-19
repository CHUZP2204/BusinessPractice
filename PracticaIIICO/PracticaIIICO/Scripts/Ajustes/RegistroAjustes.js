
$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesAjuste();
    creaEventosRegistroAjuste();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesAjuste() {
    $("#frmRegistroAjusteModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIdProducto: {
                required: true
            },
            pIdUsuario: {
                required: true
            },
            pTipoAjuste: {
                required: true
            },
            pCantAjustar: {
                required: true
            },
            pDescripAjuste: {
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

function creaEventosRegistroAjuste() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroAjusteModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosAjusteM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosAjusteM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Ajustes/NuevoAjusteModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdProducto: $("#pIdProducto").val(),
        pIdUsuario: $("#pIdUsuario").val(),
        pTipoAjuste: $("#pTipoAjuste").val(),
        pCantAjustar: $("#pCantAjustar").val(),
        pDescripAjuste: $("#pDescripAjuste").val()

    };

    var funcion = cargaMensajeModalAjuste;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalAjuste(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalAjustes").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}
$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesServicio();
    creaEventosRegistroServicio();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesServicio() {
    $("#frmRegistroServicioM").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIdTipoServicio: {
                required: true
            },
            pNombreServicio: {
                required: true
            },
            pPrecioServicio: {
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

function creaEventosRegistroServicio() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroServicioM");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosServicioM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosServicioM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Servicios/NuevoServicioModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdTipoServicio: $("#pIdTipoServicio").val(),
        pNombreServicio: $("#pNombreServicio").val(),
        pPrecioServicio: $("#pPrecioServicio").val()
    };

    var funcion = cargaMensajeModalServ;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalServ(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalServicios").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}


$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesEntrada();
    creaEventosRegistroEntrada();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesEntrada() {
    $("#frmRegistroEntradaModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIdProveedor: {
                required: true
            },
            pIdUsuario: {
                required: true
            },
            pNumFactura: {
                required: true
            },
            FechaP: {
                required: true
            },
            pMontoBruto: {
                required: true
            },
            pDescuento: {
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

function creaEventosRegistroEntrada() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroEntradaModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosEntradaM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosEntradaM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Entradas/NuevaEntradaModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdProveedor: $("#pIdProveedor").val(),
        pIdUsuario: $("#pIdUsuario").val(),
        pNumFactura: $("#pNumFactura").val(),
        pFecha: $("#FechaP").val()
    };

    var funcion = cargaMensajeModalEn;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalEn(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalEntradas").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}


$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesDetEntr();
    creaEventosRegistroDetEntr();
    creaEventosEliminaDetEntr();
    
    eliminarDetalleEntr();

});

function eliminarDetalleEntr(numero) {
    //var valor = document.getElementById("detalleCot_" + numero).value;
    document.getElementById("pEliminaIdDetalleEntr").value = numero;
}

///frmRegistraUsuario
function creaValidacionesDetEntr() {
    $("#frmRegistroDetEntModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIdEntrada: {
                required: true
            },
            pIdProducto: {
                required: true
            },
            pCantidad: {
                required: true
            },
            pPrecioCant: {
                required: true
            },
        }
    });
}


//Registar Detalle Cotizacion
function creaEventosRegistroDetEntr() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroDetEntModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosDetEntrM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}

//Eliminar Detalle Cotizacion

function creaEventosEliminaDetEntr() {
    $("#EliminarDetEntr").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmBorrarDetEntr");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            EliminaDatosDetEntrM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}

//Elminar Detalle Cotizacion

function EliminaDatosDetEntrM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/DetalleEntradas/eliminaDetalleEntr';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idDetEntr: $("#pEliminaIdDetalleEntr").val()
    };

    var funcion = cargaMsjEliminaDetEntr;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

//Llamar modal Al Eliminar
function cargaMsjEliminaDetEntr(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;


    if (estadoFuncion === 1) {

        $("#ModalEliminarDetEntr").modal('hide');
        //showMessageRegistrarse(resultadoFuncion);

        ///Pausa Para Redirigir De Pagina
    }
    showMessageRegistrarse(resultadoFuncion);

}

///Agregar Detalle Cotizacion
function RegistraDatosDetEntrM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/DetalleEntradas/nuevoDetEntrModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdEntrada: $("#pIdEntrada").val(),
        pIdProducto: $("#pIdProducto").val(),
        pCantidad: $("#pCantidad").val(),
        pPrecioCant: $("#pPrecioCant").val()
    };

    var funcion = cargaMensajeDetalleEntr;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}


//Llamar Modal Al Registrar
function cargaMensajeDetalleEntr(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;


    if (estadoFuncion === 1) {

        $("#ModalAgregaDetEnt").modal('hide');
        //showMessageRegistrarse(resultadoFuncion);

        ///Pausa Para Redirigir De Pagina
    }
    showMessageRegistrarse(resultadoFuncion);

}
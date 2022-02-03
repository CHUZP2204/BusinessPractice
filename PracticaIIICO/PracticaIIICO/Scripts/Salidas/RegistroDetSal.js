
$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesDetSal();
    creaEventosRegistroDetSal();
    creaEventosEliminaDetSal();

    eliminarDetalleSal();

});

function eliminarDetalleSal(numero) {
    //var valor = document.getElementById("detalleCot_" + numero).value;
    document.getElementById("pEliminaIdDetalleSal").value = numero;
}

///frmRegistraUsuario
function creaValidacionesDetSal() {
    $("#frmRegistroDetSalModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIDSalida: {
                required: true
            },
            pIDProducto: {
                required: true
            },
            pCantSalidaPROD: {
                required: true
            },
        }
    });
}


//Registar Detalle Cotizacion
function creaEventosRegistroDetSal() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroDetSalModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosDetSalM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}

//Eliminar Detalle Cotizacion

function creaEventosEliminaDetSal() {
    $("#EliminarDetEntr").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmBorrarDetSal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            EliminaDatosDetSalM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}

//Elminar Detalle Cotizacion

function EliminaDatosDetSalM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/DetalleSalidas/eliminaDetalleSal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idDetSal: $("#pEliminaIdDetalleSal").val()
    };

    var funcion = cargaMsjEliminaDetSal;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

//Llamar modal Al Eliminar
function cargaMsjEliminaDetSal(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;


    if (estadoFuncion === 1) {

        $("#ModalEliminarDetSal").modal('hide');
        //showMessageRegistrarse(resultadoFuncion);

        ///Pausa Para Redirigir De Pagina
    }
    showMessageRegistrarse(resultadoFuncion);

}

///Agregar Detalle Cotizacion
function RegistraDatosDetSalM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/DetalleSalidas/nuevoDetSalModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIDSalida: $("#pIDSalida").val(),
        pIDProducto: $("#pIDProducto").val(),
        pCantSalidaPROD: $("#pCantSalidaPROD").val()
    };

    var funcion = cargaMensajeDetalleSal;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}


//Llamar Modal Al Registrar
function cargaMensajeDetalleSal(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;


    if (estadoFuncion === 1) {

        $("#ModalAgregaDetSal").modal('hide');
        //showMessageRegistrarse(resultadoFuncion);

        ///Pausa Para Redirigir De Pagina
    }
    showMessageRegistrarse(resultadoFuncion);

}
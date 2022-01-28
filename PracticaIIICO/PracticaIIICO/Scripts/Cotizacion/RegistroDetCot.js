
$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesDetCotiz();
    creaEventosRegistroDetCotiz();
    creaEventosEliminaDetCotiz()
    abriModal();
    eliminarDetalle();

});

function eliminarDetalle(numero) {
    //var valor = document.getElementById("detalleCot_" + numero).value;
    document.getElementById("pEliminaIdDetalleCot").value = numero;
}

///frmRegistraUsuario
function creaValidacionesDetCotiz() {
    $("#frmRegistroDetCotModal").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            pIdCotizacion: {
                required: true
            },
            pIdProducto: {
                required: true
            },
            pIdServicio: {
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

function abriModal() {
    $("#btnAbrirModal").on("click", function () {
        //$("#exampleModal").modal('show');

        showMessageRegistrarse("Hola");
    });

    $("#btnCerrarModal").on("click", function () {
        $("#exampleModal").modal('hide');
    });
}

//Registar Detalle Cotizacion
function creaEventosRegistroDetCotiz() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroDetCotModal");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosDetCotizM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}

//Eliminar Detalle Cotizacion

function creaEventosEliminaDetCotiz() {
    $("#EliminarDetCot").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmBorrarDetCot");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            EliminaDatosDetCotizM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}

//Elminar Detalle Cotizacion

function EliminaDatosDetCotizM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/DetalleCotizacion/eliminaDetalleCot';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idDetCot: $("#pEliminaIdDetalleCot").val()
    };

    var funcion = cargaMsjEliminaCot;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

//Llamar modal Al Eliminar
function cargaMsjEliminaCot(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;


    if (estadoFuncion === 1) {

        $("#ModalEliminarDetCot").modal('hide');
        //showMessageRegistrarse(resultadoFuncion);

        ///Pausa Para Redirigir De Pagina
    }
    showMessageRegistrarse(resultadoFuncion);

}

///Agregar Detalle Cotizacion
function RegistraDatosDetCotizM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/DetalleCotizacion/NuevaDetCotModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pIdCotizacion: $("#pIdCotizacion").val(),
        pIdProducto: $("#pIdProducto").val(),
        pIdServicio: $("#pIdServicio").val(),
        pCantidad: $("#pCantidad").val(),
        pPrecioCant: $("#pPrecioCant").val()
    };

    var funcion = cargaMensajeDetalleC;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}


//Llamar Modal Al Registrar
function cargaMensajeDetalleC(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;


    if (estadoFuncion === 1) {
        
        $("#ModalAgregaDetCot").modal('hide');
        //showMessageRegistrarse(resultadoFuncion);
        
        ///Pausa Para Redirigir De Pagina
    }
    showMessageRegistrarse(resultadoFuncion);
    
}
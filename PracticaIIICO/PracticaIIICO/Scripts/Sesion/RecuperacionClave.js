
$(function () {
    //EstableceMensajesJqueryValidate();
    //creaValidaciones();
    creaEventosRecuperacion();
    //abriModal();

});

function creaEventosRecuperacion() {
    $("#btnRecuperar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroInicio");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RecuperarDatosCLiente();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}


function RecuperarDatosCLiente() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Sesion/RecuperarClave';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pNombreUsuario: $("#NombreUsuario").val(),
        pCorreo: $("#CorreoUsuario").val()
    };

    var funcion = cargaMensaje1;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensaje1(data) {
    var resultadoFuncion = data.result;
    //$("#exampleModalCenteredScrollable").dialog("close");

    $("#RecuperacionClave").modal('hide');

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRecuperacion(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}

function showMessageRecuperacion(msg) {

    const mensaje = document.querySelector("#messageRecuperacion");

    var html =
        "<div class='modal fade' id='exampleModal1' tabindex='- 1' aria-labelledby='exampleModalLabel' aria-hidden='true'>" +
        "<div class='modal-dialog'>" +
        "  <div class='modal-content'>" +
        "<div class='modal-header'>" +
        "<h5 class='modal-title' id='exampleModalLabel'>Ventana De Confirmacion!</h5>" +
        "<button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>" +
        "</div>" +
        "<div class='modal-body'>" +
        "<p>" + msg + "</p>" +
        "</div>" +
        "<div class='modal-footer'>" +
        "<button type ='button' class='btn btn-secondary' id ='btnCerrarRec'>Salir</button>" +        
        "  </div>" +
        "</div>" +
        "</div >" +
        "</div >";

    mensaje.innerHTML = html;

    //$("#ModalChuz").modal('hide');
    $("#exampleModal1").modal('show');

    $("#btnCerrarRec").on("click", function () {
        $("#exampleModal1").modal('hide');
        $("#RecuperacionClave").modal('hide');
    });

    /*$("#myModalRegistrar").modal('show');
    //no tiene funcion
    /* $(function () {
         $("#btnShow").click(function () {
             showModal();
         });
     });*/
}



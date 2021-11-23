$(function () {
    EstableceMensajesJqueryValidate();
    creaValidaciones();
    creaEventosRegistrar();
    abriModal();

});


///frmRegistraUsuario
function creaValidaciones() {
    $("#frmRegistroInicio").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            Nombre_U: {
                required: true
            },
            Apellido1_U: {
                required: true
            },
            Apellido2_U: {
                required: true
            },
            Correo_U: {
                required: true,
                email: true
            },
            contrasenaVal1: {
                required: true,
                minlength: 5,
                maxlength: 8
            },
            contrasenaVal: {
                required: true,
                equalTo: "#contrasenaVal1",
                minlength: 5,
                maxlength: 8
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

function creaEventosRegistrar() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroInicio");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosCLiente();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosCLiente() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Sesion/RegistrarUsuario';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pNombreC: $("#Nombre_U").val(),
        pApellido1: $("#Apellido1_U").val(),
        pApellido2: $("#Apellido2_U").val(),
        pCorreo: $("#Correo_U").val(),
        pContrasenia: $("#contrasenaVal1").val()
    };

    var funcion = cargaMensaje;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensaje(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalChuz").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}

//Modal Pequeño 
/*
function showMessageRegistrarse(msg) {

    const mensaje = document.querySelector("#messageRegistrarse");

    var html = "<div class='modal' data-backdrop='static' id='myModalRegistrar' tabindex='-1' role='dialog'> " +
        "      <div class='modal-dialog  modal-dialog-centered' >" +
        "        <div class='modal-content'>" +
        "            <div class='modal-header'>" +
        "                <h5 class='modal-title'>Validación</h5>" +
        "           <button type='button' class='close' data-dismiss='modal' aria-label='Close'>" +
        "               <span aria-hidden='true'>&times;</span>" +
        "            </button>" +
        "        </div>" +
        "        <div class='modal-body'>" +
        "            <p>" + msg + "</p>" +
        "        </div>" +
        "        <div class='modal-footer'>" +
        "            <button type='button' class='btn btn-primary' data-dismiss='modal'>Ok</button>" +
        "        </div>" +
        "    </div>" +
        "</div >" +
        "</div >";

    mensaje.innerHTML = html;


    $("#exampleModal").modal('show');

    $("#myModalRegistrar").modal('show');
    //no tiene funcion
    /* $(function () {
         $("#btnShow").click(function () {
             showModal();
         });
     });*/
//}

function showMessageRegistrarse(msg) {

    const mensaje = document.querySelector("#messageRegistrarse");

    var html =
        "<div class='modal fade' id='exampleModal' tabindex='- 1' aria-labelledby='exampleModalLabel' aria-hidden='true'>" +
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
        "<button type ='button' class='btn btn-secondary' id = 'btnCerrarModal'>Salir</button>" +
        "  </div>" +
        "</div>" +
        "</div >" +
        "</div >";

    mensaje.innerHTML = html;

    //$("#ModalChuz").modal('hide');
    $("#exampleModal").modal('show');

    $("#btnCerrarModal").on("click", function () {
        $("#exampleModal").modal('hide');
        $("#ModalChuz").modal('hide');
    });

    /*$("#myModalRegistrar").modal('show');
    //no tiene funcion
    /* $(function () {
         $("#btnShow").click(function () {
             showModal();
         });
     });*/
}

//////Validaciones

function EstableceMensajesJqueryValidate() {
    $.extend($.validator.messages, {
        maxlength: $.validator.format("Favor ingrese {0} o menos caracteres"),
        minlength: $.validator.format("Favor ingrese al menos {0} caracteres"),
        required: $.validator.format("Valor Requerido"),
        url: "Debe ingresar una dirección web válida",
        rangelength: $.validator.format("Favor ingrese un valor entre {0} y {1} caracteres de longitud"),
        range: $.validator.format("Favor ingrese un valor entre {0} y {1}"),
        max: $.validator.format("Favor ingrese un valor menor o igual a: {0}"),
        min: $.validator.format("Favor ingrese un valor mayor o igual a: {0}"),
        number: "Favor ingrese un número válido",
        digits: "Favor ingrese solo números",
        email: "Favor ingrese una dirección de correo electrónico válida",
        accept: $.validator.format("Favor seleccione un formato válido {0}"),
        extension: $.validator.format("Favor seleccione un formato válido {0}"),
        require_from_group: $.validator.format("Ingrese al menos uno de estos valores"),
        equalTo: $.validator.format("Los contraseñas no coinciden")

    });
}
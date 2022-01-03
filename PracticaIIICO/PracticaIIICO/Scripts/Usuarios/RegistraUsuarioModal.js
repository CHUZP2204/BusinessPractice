﻿$(function () {
    EstableceMensajesJqueryValidate();
    creaValidacionesUser();
    creaEventosRegistrarUser();
    abriModal();

});


///frmRegistraUsuario
function creaValidacionesUser() {
    $("#frmRegistroUsuarioM").validate({
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
            Cedula_U: {
                required: true

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

function creaEventosRegistrarUser() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        //$("#exampleModalCenteredScrollabl").modal('hide');
        ///el resultado del selector
        var formulario = $("#frmRegistroUsuarioM");
        ///Ejecutar El MEtodo De Validacion
        formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        if (formulario.valid()) {
            RegistraDatosUsuarioM();
        }

        // RegistraDatosCLiente();

        /*if (formulario.valid()) {
            RegistraDatosCLiente();
        }*/
    });
}



function RegistraDatosUsuarioM() {
    ///dirección a donde se enviarán los datos
    var urlMetodo = '/Usuarios/RegistrarUsuarioModal';
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        pNombreC: $("#Nombre_U").val(),
        pApellido1: $("#Apellido1_U").val(),
        pApellido2: $("#Apellido2_U").val(),
        pCorreo: $("#Correo_U").val(),
        pContrasenia: $("#contrasenaVal1").val(),
        pCedula: $("#Cedula_U").val()
    };

    var funcion = cargaMensajeModalU;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensajeModalU(data) {
    var resultadoFuncion = data.resultado;
    //$("#exampleModalCenteredScrollable").dialog("close");
    var estadoFuncion = data.estado;

    if (estadoFuncion === 1) {
        $("#ModalUsuarios").modal('hide');
    }

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}

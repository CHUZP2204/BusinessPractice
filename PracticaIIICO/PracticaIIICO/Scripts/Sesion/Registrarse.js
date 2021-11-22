$(function () {
    
    creaEventosRegistrar();
});

function creaEventosRegistrar() {
    $("#registrar").on("click", function () {
        ///Asignar a la variable formulario
        ///el resultado del selector
        var formulario = $("#frmRegistraUsuario");
        ///Ejecutar El MEtodo De Validacion
       //formulario.validate();
        ///Si El Formulario Es Valido
        ///Ejecutar la funcion invocaMetodosPost
        RegistraDatosCLiente();
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
        pContrasenia: $("#Clave_U").val()
    };

    var funcion = cargaMensaje;

    ///Llamar al Metodo Que Se Comunica con el servidor
    ejecutaAjax(urlMetodo, parametros, funcion)
}

function cargaMensaje(data) {
    var resultadoFuncion = data;
    //$("#exampleModalCenteredScrollable").dialog("close");
    $("#exampleModalCenteredScrollabl").modal("hide");
    

    //showMessageRegistrarse(resultadoFuncion);
    showMessageRegistrarse(resultadoFuncion);
    ///Pausa Para Redirigir De Pagina

}

//Modal Pequeño
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

    $("#myModalRegistrar").modal('show');
    //no tiene funcion
    /* $(function () {
         $("#btnShow").click(function () {
             showModal();
         });
     });*/
}
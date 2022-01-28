
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

    const mensaje = document.querySelector("#mensajeModalRegistro");

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
        $("#ModalProductos").modal('hide');
        
    });

    /*$("#myModalRegistrar").modal('show');
    //no tiene funcion
    /* $(function () {
         $("#btnShow").click(function () {
             showModal();
         });
     });*/
}
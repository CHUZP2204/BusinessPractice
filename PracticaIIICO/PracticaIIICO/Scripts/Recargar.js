//Cuando la página esté cargada
$(document).ready(function () {
    //Creamos el evento click del botón
    $("#refrescar").click(function () {
        //Actualizamos la página
        location.reload();
    });
});
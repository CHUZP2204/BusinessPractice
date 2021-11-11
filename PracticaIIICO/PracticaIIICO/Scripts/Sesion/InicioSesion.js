jQuery(function ($) {


    $("#btnIniciar").click(function () {
        llamarMensaje();
    });


});

function llamarMensaje() {
    $('#alertRoja').fadeIn(1000);
    $("#alertRoja").css("display", "flex");
    setTimeout(function () {
        $('#alertRoja').fadeOut(1000);
    }, 5000);
}


jQuery(function ($){
    

    $("#btnCreditCart").click(function () {
        bloquearSidebar();
    });

    $("#btnDesbloqueo").click(function () {
        desbloquearSidebar();
    });
});


/// funcion que obtiene los registros
// del metodo del controlador
// RetornaEmpresas()
function bloquearSidebar() {
    /////construir la dirección del método del servidor
    var urlMetodo = '/Home/RetornaOpcionSidebarBloqueo';
    var parametros = {};

    $.ajax({
        url: urlMetodo,
        dataType: 'json', //formato en los que se envían los datos
        type: 'post',//tipo de método(post o get)
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            alert(data.resultado);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}


//Desbloquear Side Bar

function desbloquearSidebar() {
    /////construir la dirección del método del servidor
    var urlMetodo = '/Home/RetornaOpcionSidebarDesbloqueo';
    var parametros = {};

    $.ajax({
        url: urlMetodo,
        dataType: 'json', //formato en los que se envían los datos
        type: 'post',//tipo de método(post o get)
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            alert(data.resultado);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

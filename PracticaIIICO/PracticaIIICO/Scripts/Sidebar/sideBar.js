jQuery(function ($) {

    $(".sidebar-dropdown > a").click(function () {
        $(".sidebar-submenu").slideUp(200);
        if (
            $(this)
                .parent()
                .hasClass("active")
        ) {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .parent()
                .removeClass("active");
        } else {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .next(".sidebar-submenu")
                .slideDown(200);
            $(this)
                .parent()
                .addClass("active");
        }
    });

    /* Preguntar Si Muestra Sidebar o Lo Ouclta en un Mismo boton  V1
    $("#show-sidebar2").click(function () {

        valor = $(".page-wrapper").hasClass("toggled");
        if (valor === true) {
            $(".page-wrapper").removeClass("toggled");
        }
        else if (valor === false) {
            $(".page-wrapper").addClass("toggled");
        }
    });

*/
    /* Preguntar Si Muestra Sidebar o Lo Ouclta en un Mismo boton  */
    $("#show-sidebar2").click(function () {
        var ventana_ancho = $(window).width();
        //alert(ventana_ancho);
        body = $('html,body');
        valor = $(".page-wrapper").hasClass("toggled");

        if (valor === true) {
            $(".page-wrapper").removeClass("toggled");
            if (ventana_ancho < 768) {
                body.removeClass('block-scroll');
            }
        }
        else if (valor === false) {
            $(".page-wrapper").addClass("toggled");
            if (ventana_ancho < 768) {
                body.addClass('block-scroll');
            }
        }
    });

    /*Al Dar click que el overlay desaparezca y la sidebar tambien */
    $(document).on("click", ".overlay", function () {
        $(".page-wrapper").removeClass("toggled"); /* Quita El Side bar*/
        $(".overlay").fadeOut();/*Quita el display */
        body = $('html,body');
        body.removeClass('block-scroll');
        //$('html,body').css('overflow', 'scroll');
        /*$(".overlay").attr("class","") quita la clase el metodo .attr()*/
        //$("html").attr("style", "")
    });
/*Mantener Sidebar Abierto al hacer click en productos*/
    $('#btnProducts').on('click', function () {
        window.location.href = '/Products/ListProducts';
        
    });
    
   
    

    /*Evebtos para cerrar side bar por boton*/
    $("#close-sidebar").click(function () {
        $(".page-wrapper").removeClass("toggled");
    });
    $("#show-sidebar").click(function () {
        $(".page-wrapper").addClass("toggled");
    });

});
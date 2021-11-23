function myFunction() {
    var input, filter, table, tr, td, td1, i, txtValue, textoObtenido, elementoEncontrado = 0, filaMostrar = 0;/*Variables*/

    input = document.getElementById("myInputUSER");/*Etiqueta Donde Se Ingresa el texto*/
    filter = input.value.toUpperCase(); /**/
    table = document.getElementById("myTableUSER");/*Obtner Tabla*/
    tr = table.getElementsByTagName("tr");

    for (i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td");/*Obtiene las columnas del tr*/
        elementoEncontrado = 0;
        //Recorrer el tr
        for (j = 0; j < td.length - 1; j++) {
            //Si se encuentra un dato en cualquiera de las columnas de la fila que estemos
            //Se debe asignar un 1 para ya no entrar en esa fila
            if (elementoEncontrado == 0) {
                if (td[j]) {

                    txtValue = td[j].textContent || td[j].innerText; /*Compara el texto Obtenido Con el existente del primer td encontrado*/
                    textoObtenido = txtValue.toUpperCase();

                    //Comparamos el valor Encontrado con el dato que se ingreso en el filtro
                    if (textoObtenido.indexOf(filter) > -1) {
                        elementoEncontrado = 1;
                        filaMostrar = filaMostrar + 1;
                    }

                }

            }
        }
        if (elementoEncontrado == 1) {

            tr[i].style.display = "";
        }
        else {

            tr[i].style.display = "none";
        }
    }

    alertVerde = document.getElementById("alertVerde");
    mensajeAlertVerde = document.getElementById("mensajeAlertVerde");

    alertRoja = document.getElementById("alertRoja");
    mensajeAlertRja = document.getElementById("mensajeAlertaRoja");

    if (filaMostrar == 0) {
        mensajeAlertRja.innerHTML = "No Se Encontro Ningun Dato ";
        $('#alertRoja').fadeIn(1000);
        $("#alertRoja").css("display", "flex");
        setTimeout(function () {
            $('#alertRoja').fadeOut(1000);
        }, 5000);
    } else {
        mensajeAlertVerde.innerHTML = 'Se Encontraron ' + filaMostrar + ' Objetos';
        $('#alertVerde').fadeIn(1000);
        $("#alertVerde").css("display", "flex");
        setTimeout(function () {
            $('#alertVerde').fadeOut(1000);
        }, 5000);
    }
}
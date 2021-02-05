$(document).ready(function () {
    PNotify.prototype.options.styling = "jqueryui";
}
);

function MostrarMensaje(Mensaje, Tipo) {
    //debugger;
        new PNotify({
            title: "Inventario PJEH informa",
            text: Mensaje,
            type: Tipo,
            addclass: 'CustomPnotify'
        });
}

var Msj = "";
var Tpo = "";
var myVar = "";

function MostrarMensajeInterval(Mensaje, Tipo) {
    //debugger;
    Msj = Mensaje;
    Tpo = Tipo;
    myVar = setInterval(IntervaloTerminado, 1000);
}


function confirma() {

    swal({
        title: "¿Seguro que quieres hacer esto?",
        showCancelButton: true,
        ////confirmButtonColor: 'darkgray',
        ////cancelButtonColor: 'darkgray',
        confirmButtonText: 'Si',
        cancelButtonText: "No"
    }).then(function (result) {
   
        if (result) {
      alert('Oh ok. Chicken, I see.');

        }
        });
}

function confirm(event) {

    swal({
        title: "¿Desea eliminar el registro?",
        showCancelButton: true,
        confirmButtonColor: '#3000d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: "No"
    }).then(function (result) {

        if (result.value == true) {


            __doPostBack('AccionEliminarEdificio', '');
         
        } else {
            
            __doPostBack('AccionVacio', '');
        }
          
        
    });
}

function confirm1(event) {

    swal({
        title: "¿Desea eliminar el registro?",
        showCancelButton: true,
        confirmButtonColor: '#3000d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: "No"
    }).then(function (result) {

        if (result.value == true) {


            __doPostBack('AccionEliminarr1', '');

        } else {

            __doPostBack('AccionVacio', '');
        }


    });
}

function confirm2(event) {

    swal({
        title: "¿Desea eliminar el registro?",
        showCancelButton: true,
        confirmButtonColor: '#3030c6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: "No"
    }).then(function (result) {

        if (result.value == true) {


            __doPostBack('AccionEliminarr2', '');

        } else {

            __doPostBack('AccionVacio', '');
        }


    });
}



function IntervaloTerminado(Mensaje, Tipo) {
    new PNotify({
        title: "Inventario PJEH informa",
        text: Msj,
        type: Tpo,
        addclass: 'CustomPnotify'
    });
    clearInterval(myVar);
}


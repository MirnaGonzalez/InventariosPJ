
//function ValidacionLetras(e) {
//    var charCode = (e.which) ? e.which : event.keycode
//    if (charCode > 31 && (charCode < 45 || charCode > 57)) 
//    {
//        return true;
//    }
        
    
//    return false;
//}


//function soloNumeros(e) {
//    var key = window.Event ? e.which : e.keyCode
//    return (key >= 48 && key <= 57)
//}

function ValidacionLetras(e) {
    var charCode = (e.which) ? e.which : event.keycode
    if (charCode > 31 && (charCode < 45 || charCode > 57)) {
        return true;
    }


    return false;
}


function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function numerosDecimales(e, TxtValorUma) {
    var key = window.Event ? e.which : e.keyCode
    if (key == 8) return true;

    //0-9 a partir del punto decimal 
    if (TxtValorUma.value != "") {
        if ((TxtValorUma.value.indexOf(".")) > 0) {
            //valida los digítos en la parte decimal
            if (key >= 48 && key <= 57) {
                if (TxtValorUma.value == "") return true
                RegExp = /[0-9]{1,3}[\.][0-9]{2}$/
                return !(RegExp.test(TxtValorUma.value))
            }
        }
    }
    // 0-9
    if (key >= 48 && key <= 57) {
        if (TxtValorUma.value == "") return true
        RegExp = /[0-9]{3}/
        return !(RegExp.test(TxtValorUma.value))
    }
    // .
    if (key == 46) {
        if (TxtValorUma.value == "") return false
        RegExp = /^[0-9]+$/
        return RegExp.test(TxtValorUma.value)
    }
    return false
}

function numerosDecimalesIva(e, TxtIVA) {
    var key = window.Event ? e.which : e.keyCode
    if (key == 9) return true;

    //0-9 a partir del punto decimal 
    if (TxtIVA.value != "") {
        if ((TxtIVA.value.indexOf(".")) > 0) {
            //valida los digítos en la parte decimal
            if (key > 47 && key < 58) {
                if (TxtIVA.value == "") return true
                regexp = /[0-9]{1,3}[\.][0-9]{2}$/
                return !(regexp.test(TxtIVA.value))
            }
        }
    }
    // 0-9
    if (key > 47 && key < 58) {
        if (TxtIVA.value == "") return true
        regexp = /[0-9]{3}/
        return !(regexp.test(TxtIVA.value))
    }
    // .
    if (key == 46) {
        if (TxtIVA.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(TxtIVA.value)
    }
    return false
}


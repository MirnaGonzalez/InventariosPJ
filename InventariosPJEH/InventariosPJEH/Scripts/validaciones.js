function numbersonly(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 && unicode != 44 && unicode != 46) 
    {
        return true
    }
    if (unicode < 48 || unicode > 57) //Si no es un número
    {
       return false
    } 
}

function Precio(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode < 48 || unicode > 57) 
    {
        return false
    }     
}

//Para validar el Radio de Registro Individual
function showContent() {
    element = document.getElementById("bodyInfoAdquisicion");
    element2 = document.getElementById("InfoGeneral2");
    element3 = document.getElementById("Detalle");
    element4 = document.getElementById("Ubicacion");
    element5 = document.getElementById("Botones");
    check = document.getElementById("RegistroIndividual");
    if (check.checked) {
        element.style.display = 'block';
        element2.style.display = 'block';
        element3.style.display = 'block';
        element4.style.display = 'block';
        element5.style.display = 'block';
    }
    else {
        element.style.display = 'none';
        element2.style.display = 'none';
        element3.style.display = 'none';
        element4.style.display = 'none';
        element5.style.display = 'none';
    }
}

//Para validar el Radio de Registro por Lote
function showContent1() {
    element = document.getElementById("bodyInfoAdquisicion");
    element4 = document.getElementById("InfoGeneral2");
    element5 = document.getElementById("Detalle");
    element6 = document.getElementById("Ubicacion");
    element7 = document.getElementById("Botones");
    element8 = document.getElementById("CantidadBienes");
    check = document.getElementById("RegistroPorLote");
    if (check.checked) {
        element.style.display = 'block';
        element4.style.display = 'block';
        element5.style.display = 'block';
        element6.style.display = 'block';
        element7.style.display = 'block';
        element8.style.display = 'block';
    }
    else {
        element.style.display = 'none';
        element4.style.display = 'none';
        element5.style.display = 'none';
        element6.style.display = 'none';
        element7.style.display = 'none';
        element8.style.display = 'none';
    }
}

function SubTotal() {
    var pu = document.getElementById('TextBoxPrecioUnitario');
    var iva = document.getElementById('labelIVA');
    var res = (parseFloat(pu) + (parseFloat(pu) * parseFloat(iva)));
    document.getElementById("labelSubTotal").value = res;
}

function iva() {
    element = document.getElementById("labelIVA");
    check = document.getElementById("checkboxIVA");
    if (check.checked) {
        element.style.display = 'block';
    }
    else {
        element.style.display = 'none';
    }
}

function numbersonly(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 && unicode != 44 && unicode != 46) {
        return true
    }
    if (unicode < 48 || unicode > 57) //Si no es un número
    {
        return false
    }
}

function Precio(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode < 48 || unicode > 57) {
        return false
    }
}

function ValidarDecimales(e, TextBox) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 9) return true

    // 0-9 a partir del .decimal  
    if (TextBox.value != "") {
        if ((TextBox.value.indexOf(".")) > 0) {
            //si tiene un punto valida dos digitos en la parte decimal
            if (key > 47 && key < 58) {
                if (TextBox.value == "") return true
                regexp = /[0-9]{1,9}[\.][0-9]{6}$/
                //regexp = /[0-9]{2}$/
                return !(regexp.test(TextBox.value))
            }
        }
    }                                                        
    // 0-9 
    if (key > 47 && key < 58) {
        if (TextBox.value == "") return true
        regexp = /[0-9]{9}/
        return !(regexp.test(TextBox.value))
    }
    // .
    if (key == 46) {
        if (TextBox.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(TextBox.value)
    }
    // other key
    return false

}

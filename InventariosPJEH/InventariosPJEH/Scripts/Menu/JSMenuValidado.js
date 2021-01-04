$(document).ready(function () {
    //$('#idmenu').mnmenu({ responsiveMenuEnabled: false });

    var Perfil = document.querySelector("#HFPerfil").value;
    var resultadoWs = document.querySelector("#HFJSONMenu").value;;

    Perfil = "Admin";

    if (Perfil == "Admin") {

        var resultadoWs = '{"Menus": [' +
            '	            {' +
            '	                "Nombre": "Catálogos",' +
            '	                "SubMenus": [' +
            '			            {' +
 //           '			            "Nombre": "Unidades Administrativas",' +
 //           '			                "URL": ""' +
 //           '			                "URL": "frmCatUniAdmin.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Personal",' +
            '			                "URL": "frmCatPersonal.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Edificios/Niveles/Secciones",' +
            '			                "URL": "frmCatEdNiSec.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Proveedores",' +
            '			                "URL": "frmCatProveedores.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "CONAC",' +
            '			                "URL": "frmCatCONAC.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Partidas/Suclase",' +
            '			                "URL": "frmCatSubClasesPartidas.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Marcas/Subclase",' +
            '			                "URL": "frmCatMarca.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Tipo Adquisiciones",' +
            '			                "URL": "frmCatTipoAdquisicion.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Propiedades",' +
            '			                "URL": "frmCatPropiedades.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "INCP",' +
            '			                "URL": "frmCatINCP.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "TipoDocumento",' +
            '			                "URL": "frmCatTipoDocumento.aspx"' +
            '			            }' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Bienes",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			            "Nombre": "Registro Bien",' +
            '			                "URL": "frmRegistroBien.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Modificación Bien",' +
            '			                "URL": "frmModificacionDeUnBien.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Modificación Factura",' +
            '			                "URL": "frmModificarFactura.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Baja Definitiva/Temporal",' +
            '			                "URL": "frmBajaBienAdmin.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Consulta Bienes",' +
            '			                "URL": "frmConsultaBienes.aspx"' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Impresión de Etiquetas",' +
            '			                "URL": ""' +
            '			            }' +            
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Resguardos",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Registrar Resguardo",' +
            '			                "URL": "frmAsignarResguardo.aspx"' +
            '			            }' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Depreciaciones",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Reporte Trimestral",' +
            '			                "URL": "frmCalcularDepreciacion.aspx"' +
            '			            },' +
            '			            {' +
            '			                "Nombre": "Reporte en libros",' +
            '			                "URL": "frmReporteCONAC.aspx"' +
            '			            },' +
            '			            {' +
            '			                "Nombre": "Conac",' +
            '			                "URL": ""' +
            '			            },' +
            '                       {' +
            '			                "Nombre": "Reporte Inegi",' +
            '			                "URL": ""' +
            '			            },' +
            '                       {' +
            '			                "Nombre": "Patrimonio Bienes Muebles",' +
            '			                "URL": ""' +
            '			            }' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Consultas",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Histórico Bien",' +
            '			                "URL": "frmHistoricoBien.aspx"' +
            '			            },' +
            '                       {' +
            '			                "Nombre": "Histórico Persona",' +
            '			                "URL": "frmHistoricoPersonal.aspx"' +
            '			            }' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Mantenimiento",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Recibir Bien",' +
            '			                "URL": "frmRecibirBien.aspx"' +
            '			            },' +
            '                       {' +
            '			                "Nombre": "Reparar Bien",' +
            '			                "URL": "frmReparaBien.aspx"' +
            '			            }' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Reemplazos",' +
            '	                "SubMenus": [' +
            '                       {' +
            '			                "Nombre": "Generación y Consulta de Actas",' +
            '			                "URL": "frmActaAdministrativa.aspx"' +
            '			            }' +
            '		            ]' +
            '	            }' +
            '            ]' +
            '        }';
    }

    if (Perfil == "Gestion") {

        var resultadoWs = '{"Menus": [' +
            '                       {' +
            '	                       "Nombre": "Bienes",' +
            '	                       "SubMenus": [' +
            '			            {' +
            '			            "Nombre": "Consulta Bienes",' +
            '			                "URL": ""' +
            '			            },' +
            '			            {' +
            '			            "Nombre": "Impresión de Etiquetas",' +
            '			                "URL": ""' +
            '			            }' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Resguardos",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Registrar Resguardo",' +
            '			                "URL": "frmAsignarResguardo.aspx"' +
            '			            },' +
            '		            ]' +
            '	            },' +
            '	            {' +
            '	                "Nombre": "Consultas",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Histórico Bien",' +
            '			                "URL": "frmHistoricoBien.aspx"' +
            '			            },' +
            '                       {' +
            '			                "Nombre": "Histórico Persona",' +
            '			                "URL": "frmHistoricoPersonal.aspx"' +
            '			            }' +
            '		            ]' +
            '	            }' +
            '            ]' +
            '        }';
    }

    if (Perfil == "Soporte") {

        var resultadoWs = '{"Menus": [' +
            '	            {' +
            '	                "Nombre": "Mantenimiento",' +
            '	                "SubMenus": [' +
            '			            {' +
            '			                "Nombre": "Recibir Bien",' +
            '			                "URL": "frmRecibirBien.aspx"' +
            '			            },' +
            '                       {' +
            '			                "Nombre": "Reparar Bien",' +
            '			                "URL": "frmReparaBien.aspx"' +
            '			            }' +
            '		            ]' +
            '	            }' +
            '            ]' +
            '        }';
    }



    if (resultadoWs != "") {
        obj = JSON.parse(resultadoWs);

        var $ContenedorMenus = $('#ContenedorMenus');
        var color = "bluemenu";
        var $temp = CrearMenu(color);
        $ContenedorMenus.append($temp);
        $temp.mnmenu({ menuClassName: color.toString() });
    }
    else
        new PNotify({ title: 'Inventario PJEH INFORMA', type: 'error', text: 'No se pudieron crear los menús consulte con su administrador.' });

});

function CrearMenu(id) {
    var $temp = $(["<ul id='", id, "'></ul>"].join(""));
    //ADD Submenus
    for (var i = 0; i < obj.Menus.length; i++) {
        var label = obj.Menus[i].Nombre;
        var $firstLevel = $(["<li>", label, "</li>"].join(""));
        $temp.append($firstLevel);
        SubMenus(i, $firstLevel);
    }
    return $temp;
}

function SubMenus(Nivel, $component) {
    var $newContainer = null;
    for (var i = 0; i < obj.Menus[Nivel].SubMenus.length; i++) {
        if ($newContainer === null) {
            $newContainer = $("<ul></ul>");
            $component.append($newContainer);
        }
        var newLabel = obj.Menus[Nivel].SubMenus[i].Nombre;
        var Url = obj.Menus[Nivel].SubMenus[i].URL;
        var $newLevel = $(["<li><a href='", Url, "' >", newLabel, "</a></li>"].join(""));
        $newContainer.append($newLevel);
    }
}

//Create and init additional menus
var $demoContainer = $('#demoContainer');
$demoContainer.append($("<h3>Regular menus</h3>"));
$demoContainer.append($("<p>Menus with different colors and styling.</p>"));
//Create lists
$(["bluemenu", "grayround", "squarewhite", "blue2menu"]).each(function () {
    var color = this;
    var $temp = generateMenu(color);
    $demoContainer.append($temp);
    $temp.mnmenu({ menuClassName: color.toString() });
});
$demoContainer.append($(["<h3>Right aligned menu</h3>"].join("")));
$demoContainer.append($("<p>This menu is the reversed version of the top menu."
    + " Menus will be displayed from bottom to top and from right to left.</p>"));
//Init right to left menu with custom level settings
var rightMenuLevelSettings = {};
//Defaults
rightMenuLevelSettings[0] = new MNLevelSettings();
rightMenuLevelSettings[0].parentAttachmentPosition = "SW";
rightMenuLevelSettings[0].attachmentPosition = "SE";
//First level
rightMenuLevelSettings[1] = new MNLevelSettings();
rightMenuLevelSettings[1].parentAttachmentPosition = "NE";
rightMenuLevelSettings[1].attachmentPosition = "SE";
var $rightbtmenu = generateMenu("rightbtmenu");
$demoContainer.append($rightbtmenu);
$rightbtmenu.mnmenu({ menuClassName: "rightbtmenu", levelSettings: rightMenuLevelSettings });

/////////////////////////////////////////////////////
function generateMenu(id) {
    var $temp = $(["<ul id='", id, "'></ul>"].join(""));
    //ADD Submenus
    for (var i = 1; i < 6; i++) {
        var label = ["Level-", i].join("");
        var $firstLevel = $(["<li>", label, "</li>"].join(""));
        $temp.append($firstLevel);
        addLevels(1, 5, $firstLevel, label);
    }
    return $temp;
}
function addLevels(current, max, $component, label) {
    var $newContainer = null;
    for (var i = 1; i < Math.floor(Math.random() * 6) + 1; i++) {
        if ($newContainer === null) {
            $newContainer = $("<ul></ul>");
            $component.append($newContainer);
        }
        var newLabel = [label, "-", i].join("");
        var $newLevel = $(["<li>", newLabel, "</li>"].join(""));
        $newContainer.append($newLevel);
        if (current < max) {
            addLevels(current + 1, max, $newLevel, newLabel);
        }
    }
}
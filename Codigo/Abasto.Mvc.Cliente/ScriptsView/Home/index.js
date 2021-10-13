var RutaAPI = "http://localhost:59617/Api/Digimon";
var MetodoFormulario = "POST";
function Iniciar() {
    //$(`#datos`).hide();
    $(`#nombre`).dxTextBox({
        placeholder: `Nombre`,
    }).dxValidator({
        validationRules: [{
            message: "Nombre es requerido",
            type: "custom",
            reevaluate: true,
            validationCallback: ((e) => {
                let sw = true;
                if (!$("#validacion").dxSwitch(`option`, `value`)) {
                    if (!e.value) sw = false;
                }
                return sw;
            }),
        }]   
    });

    $(`#temporada`).dxTextBox({
        placeholder: `Temporada`,
    }).dxValidator({
        validationRules: [{
            message: "Temporada es requiredo",
            type: "custom",
            reevaluate: true,
            validationCallback: ((e) => {
                let sw = true;
                if (!$("#validacion").dxSwitch(`option`, `value`)) {
                    if (!e.value) sw = false;
                }
                return sw;
            }),
        }]
    });

    $(`#companero`).dxTextBox({
        placeholder: `Compañero`,
    }).dxValidator({
        validationRules: [{
            message: "Compañero es requiredo",
            type: "custom",
            reevaluate: true,
            validationCallback: ((e) => {
                let sw = true;
                if (!$("#validacion").dxSwitch(`option`, `value`)) {
                    if (!e.value) sw = false;
                }
                return sw;
            }),
        }]
    });

    $(`#url`).dxTextBox({
        placeholder: `URL`,
    }).dxValidator({
        validationRules: [{
            message: "URL es requiredo",
            type: "custom",
            reevaluate: true,
            validationCallback: ((e) => {
                let sw = true;
                if (!$("#validacion").dxSwitch(`option`, `value`)) {
                    if (!e.value) sw = false;
                }
                return sw;
            }),
        }]
    });

    $("#guardar").dxButton({
        //icon: "check",
        stylingMode: "contained",
        type: "success",
        text: "Guardar",
        onClick: function (e) {
            guardar();
        }
    });

    $("#nuevo").dxButton({
        stylingMode: "contained",
        //icon: "new",
        type: "success",
        text: "Nuevo",
        onClick: function (e) {
            LimpiarFomrulario();
        }
    });

    $("#validacion").dxSwitch({
        value: false,
        switchedOffText: `NO`,
        switchedOnText: `SI`,        
    }).dxSwitch(`instance`);

    dxDataGrid();
};
function ValidarDatosGuardar (tab) {
    let sw = false;
    try {
        let valGroup = $(`${tab}`).dxValidationGroup({}).dxValidationGroup("instance");
        let validation = DevExpress.validationEngine.validateGroup(valGroup);
        if (!validation.isValid) {
            Notify("Por favor verifique los campos obligatorios.", "error");            
        }
        sw = validation.isValid;
    }
    catch (e) {
        console.error(`No se Reconoce la Validacion de ${tab}`, e);
    }
    return sw;
};

function Notify (mensaje, tipo, position, tiempo) {
    ////"info", "warning", "error" or "success".
    tipo = tipo ? tipo : `success`;
    //right,left,center,top,bottom
    position = position ? position : `center`;
    tiempo = tiempo ? tiempo : 3000;

    $(document).ready(function () {
        let width = 500;
        if (window.matchMedia('(max-width: 991px)').matches &&
            window.matchMedia('(max-height: 991px)').matches) {
            width = 300;
        }
        DevExpress.ui.notify({
            message: mensaje,
            position: position,
            width: width,
            shading: false,
        }, tipo, tiempo);
    });

};

async function MetodoAjax (metodo, url, data = {}) {
    let obj = await $.ajax({
        method: metodo,
        url: `${url}`,
        data: data,
    });
    return obj;
};
function ObjetoFomrulario(obj) {
    $(`#nombre`).dxTextBox(`option`, `value`, obj.nombre);
    $(`#companero`).dxTextBox(`option`, `value`, obj.companero);
    $(`#temporada`).dxTextBox(`option`, `value`, obj.temporada);
    $(`#url`).dxTextBox(`option`, `value`, obj.url);
    MetodoFormulario = "PUT";
}
function LimpiarFomrulario() {
    MetodoFormulario = "POST";
    let group = $(`#datos`).dxValidationGroup({}).dxValidationGroup("instance");
    group.reset();
}

function FormularioObjeto() {
    let obj = {};
    obj.nombre = $(`#nombre`).dxTextBox(`option`, `value`);
    obj.companero = $(`#companero`).dxTextBox(`option`, `value`);
    obj.temporada = $(`#temporada`).dxTextBox(`option`, `value`);
    obj.url = $(`#url`).dxTextBox(`option`, `value`);
    return obj;
}

async function guardar() {
    if (!ValidarDatosGuardar(`#datos`)) return;
    try {
        let obj = FormularioObjeto();
        let result = await MetodoAjax(MetodoFormulario, `${RutaAPI}`, obj);
        MetodoFormulario = "PUT";
        Notify(result.mensaje);
        dxDataGrid();
    }
    catch (e) {
        if (e.responseJSON) {
            Notify(e.responseJSON.mensaje, `error`);
        }
        else Notify("Error", `error`);
    }
    
}

function SetDataSourceDataGrid(codigo, url, filtro, dataSource = []) {
    return new DevExpress.data.CustomStore({
        key: codigo,
        load: ((e) => {
            let d = $.Deferred();
            
                $.ajax({
                    url: url,
                    method: `GET`,
                    data: filtro,
                    dataType: `json`,
                    success: ((result) => {
                        d.resolve(result);
                    }),
                    error: (() => {
                        d.reject(`Error al Cargar Datos`);
                    }),
                });          
            return d.promise();
        }),
    });
};
function dxDataGrid() {
    $(`#dxDataGrid`).dxDataGrid({
        dataSource: SetDataSourceDataGrid(`nombre`, `${RutaAPI}/Search`),
        //remoteOperations: true,
        //keyExpr: "conId",
        showBorders: true,
        showColumnLines: true,
        showRowLines: true,
        ////filas Responsive de Acuerdo a la Columna
        //wordWrapEnabled: true,

        rowAlternationEnabled: true,
        hoverStateEnabled: true,
        columnAutoWidth: true,
        loadPanel: {
            text: "Cargando...",
        },
        paging: {
            enabled: true,
            pageIndex: 0,
            pageSize: 10,
        },
        pager: {
            infoText: "Página {0} de {1} ({2} registros)",
            visible: "auto",
            allowedPageSizes: [10, 20, 50, 100, 200, 500],
            showInfo: true,
            showPageSizeSelector: true,
            showNavigationButtons: true,
        },
        headerFilter: {
            allowSearch: true,
            visible: true,
        },
        filterRow: {
            showOperationChooser: false,
            visible: true,
        },
        selectedRowKeys: [],
        columns: [
            { dataField: "nombre", caption: "Nombre", dataType: "string", sortOrder: "asc" },
            { dataField: "temporada", caption: "Temporada", dataType: "string" },
            { dataField: "companero", caption: "Compañero", dataType: "string" },
            {
                dataField: "url", caption: "imagen", dataType: "string",
                cellTemplate: function (container, options) {
                    $("<div>")
                        .append($("<img>", { "src": options.value,"width":"100px","heigth":"100px"}))
                        .appendTo(container);
                }
            },
        ],
        selection: {
            mode: "single"
        }, onSelectionChanged: ((item) => {
        }),
        onRowClick: ((e) => {
            ObjetoFomrulario(e.data);
        }),
    }).dxDataGrid("instance");
}
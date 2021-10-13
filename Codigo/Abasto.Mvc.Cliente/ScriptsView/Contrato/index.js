Page.RutaAPI = General.UrlApiNegocio() + "/Api/Palpacion";
Page.IDNumerico = true;

Page.Init = (() => {
    //alert("Entró al init");

    $("#objNombre").html("Contrato");
    Page.InitBusqueda();
    //Page.InitDatos();
    //Page.ObjId = $("#ddlDatosCodigo").dxTextBox("instance");
    //Page.ObjDescripcion = $("#ddlDatosNombre").dxSelectBox("instance");
    //Page.RevisarVariables();
    //PAGE COMMON------------------------------------------------------------------
    ////////////////////////////

    Page.InitBotones();
    //Page.AutoSelect('palId');
});
/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Busqueda   --------------------------------------------------------****/
/*****************************************************************************************************************************************/

Page.InitBusqueda = (() => {
    //CONTROLES BUSQUEDA---------------------------------------------------------------

    Page.ddlBusqueda = $("#busqueda").dxValidationGroup({}).dxValidationGroup(`instance`);

    let tabIndex = 1;

    Page.ddlBusFecha = $("#ddlBusFecha").dxDateBox({
        tabIndex: tabIndex++,
        placeholder: 'Fecha',
        type: "date",
        displayFormat: "dd/MM/yyyy",
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxDateBox("instance");

    Page.ddlBusRepresentante = $("#ddlBusRepresentante").dxTextBox({
        tabIndex: tabIndex++,
        placeholder: 'Representante',
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

    Page.ddlBusProveedor = $("#ddlBusProveedor").dxTextBox({
        tabIndex: tabIndex++,
        placeholder: 'Proveedor',
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

    Page.ddlBusDocumento = $("#ddlBusDocumento").dxTextBox({
        tabIndex: tabIndex++,
        placeholder: 'Documento',
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

    Page.ddlBusDomicilio = $("#ddlBusDomicilio").dxTextBox({
        tabIndex: tabIndex++,
        placeholder: 'Domicilio',
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

    Page.ddlBusDireccion = $("#ddlBusDireccion").dxTextBox({
        tabIndex: tabIndex++,
        showClearButton: true,
        placeholder: 'ddlBusDireccion',
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

    Page.ddlBusCiudad = $("#ddlBusCiudad").dxTextBox({
        tabIndex: tabIndex++,
        placeholder: 'Ciudad',
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

    Page.ddlBusImporte = $("#ddlBusImporte").dxNumberBox({
        tabIndex: tabIndex++,
        min: 0,
        step: 0,
        mode: "number",
        placeholder: 'Importe',
        showClearButton: true,
        onEnterKey: (e => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxNumberBox("instance");

    Page.ddlBusCuenta = $("#ddlBusCuenta").dxTextBox({
        tabIndex: tabIndex++,
        placeholder: 'Cuenta',
        showClearButton: true,
        onEnterKey: ((e) => { Page.Buscar(); }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxTextBox("instance");

});

Page.LimpiarControlesBusqueda = (() => {
    Page.ddlBusqueda.reset();
});

Page.CargarGridBusqueda = (() => {

    let ClickKey;
    Page.grdBusqueda = $("#grdBusqueda").dxDataGrid({
        dataSource: Page.SetDataSourceDataGrid({
            key: `palId`, url: `${Page.RutaAPI}/GetContrato`, data: function () {
                return {
                    //id: Page.ddlBusId.option("value"),                    
                };
            }
        }),
        remoteOperations: true,
        showBorders: true,
        showColumnLines: true,
        showRowLines: true,
        ////filas Responsive de Acuerdo a la Columna
        //wordWrapEnabled: true,
        rowAlternationEnabled: true,
        hoverStateEnabled: true,
        columnAutoWidth: true,
        paging: {
            enabled: true,
            pageIndex: 0,
            pageSize: 10,
        },
        pager: {
            infoText: "Página {0} de {1} ({2} registros)",
            visible: "auto",
            allowedPageSizes: [10, 20, 30],
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
        columns: [{ dataField: "palId", caption: "ID", dataType: `number`, editorOptions: { placeholder: `ID`, showClearButton: true }, headerFilter: { allowSearch: true }, },
        { dataField: "palFecha", caption: "Fecha", dataType: 'date', format: 'dd/MM/yyyy', editorOptions: { format: `dd/MM/yyyy`, placeholder: `Fecha`, showClearButton: true }, headerFilter: { allowSearch: true }, },
        { dataField: "camNombre", caption: "Campo", dataType: `string`, editorOptions: { placeholder: `Campo`, showClearButton: true }, headerFilter: { allowSearch: true }, },
        //{ dataField: "empNombre", caption: "Empresa", dataType: `string`, editorOptions: { placeholder: `Empresa`, showClearButton: true }, headerFilter: { allowSearch: true }, },
        { dataField: "palEstado", caption: "Estado", dataType: `string`, editorOptions: { placeholder: `Estado`, showClearButton: true }, headerFilter: { allowSearch: true } },
        ],
        selection: {
            mode: "single"
        },
        noDataText: "Sin Registros",
        onSelectionChanged: ((item) => {
            Page.grdBusqueda_onSelectionChanged(item);
            if (item.selectedRowKeys.length > 0) {
                Page.LimpiarTodoGrid();
            }
        }),
        onRowClick: (e => {
            if (ClickKey == e.key) {
                Page.TabCambiarPage("#tabDatos");
                ClickKey = null;
            }
            else {
                ClickKey = e.key;
            }
        }),
    }).dxDataGrid("instance");
});

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Fin Busqueda ------------------------------------------------------****/
/*****************************************************************************************************************************************/
Page.LimpiarTodoGrid = (() => {
    if (Page.grdDetalle) {
        Page.grdDetalle.dispose();
    }
    if (Page.grdDetalleExcelError) {
        Page.grdDetalleExcelError.dispose();
    }
    if (Page.grdLog) {
        Page.grdLog.dispose();
    }
});

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Datos -------------------------------------------------------------****/
/*****************************************************************************************************************************************/
Page.InitDatos = (() => {
    //CONTROLES DATOS--------------------------------------------------------------------
    Page.ddlDatos = $("#datos").dxValidationGroup({}).dxValidationGroup(`instance`);
    let tabIndex = 1;
    Page.ddlDatosId = $("#ddlDatosId").dxTextBox({
        tabIndex: tabIndex++,
        value: 0,
        placeholder: 'Codigo',
        readOnly: true,
        mode: "search",
    }).dxTextBox("instance");

    Page.ddlDatosEmpresa = $("#ddlDatosEmpresa").dxSelectBox({
        tabIndex: tabIndex++,
        placeholder: 'Seleccione una Empresa',
        valueExpr: 'empId',
        displayExpr: 'empNombre',
        noDataText: "Sin Registros",
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
        dataSource: Page.SetDataSourceSelectBox(`empId`, `${General.UrlApiNegocio()}/Api/Empresa/GetList`),
        onValueChanged: ((e) => {
            if (e.value && Page.EmpresaAnterior != e.value) {
                Page.EmpresaAnterior = e.value;
                let empId = e.value;
                let campo = Page.SetDataSourceSelectBox(`camId`, `${General.UrlApiNegocio()}/Api/Campo/GetListFecha`, { empresa: empId });
                Page.ddlDatosCampo.option(`dataSource`, campo);

                let evaluacion = Page.SetDataSourceSelectBox(`evaId`, `${General.UrlApiNegocio()}/Api/Evaluacion/GetList`, { empresa: empId });
                Page.ddlDetalleEvaluacion.option(`dataSource`, evaluacion);

                let tropa = Page.SetDataSourceSelectBox(`troId`, `${General.UrlApiNegocio()}/Api/Tropa/GetList`, { empresa: empId });
                Page.ddlDetalleTropaDestino.option(`dataSource`, tropa);
            }
        }),
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Empresa es requerido'
        }]
    }).dxSelectBox("instance");

    Page.ddlDatosCampo = $("#ddlDatosCampo").dxSelectBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceSelectBox(`camId`),
        placeholder: 'Seleccion un Campo',
        valueExpr: 'camId',
        displayExpr: 'camNombre',
        noDataText: "Seleccione una Empresa",
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
        onSelectionChanged: ((e) => {
            let item = e.selectedItem;
            if (item && [null, undefined, `C`].includes(Page.data.estado)) {
                if (item.fecha) {
                    let min = new Date(item.fecha.min).getTime() + Page.MiliSegundoEnDia;
                    Page.ddlDatosFecha.option(`min`, min);
                    Page.ddlDatosFecha.option(`max`, new Date(item.fecha.max).getTime());
                }
                else {
                    General.Notify(`El Campo no tiene Inventario Inicial Aprobado`, 'warning');
                }
            }
            else {
                Page.ddlDatosFecha.option(`min`, null);
                Page.ddlDatosFecha.option(`max`, null);
            }
        })
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Campo es requerido'
        }]
    }).dxSelectBox("instance");

    Page.ddlDatosFecha = $("#ddlDatosFecha").dxDateBox({
        tabIndex: tabIndex++,
        placeholder: 'Fecha',
        type: "date",
        displayFormat: "dd/MM/yyyy",
        dateOutOfRangeMessage: "Fecha fuera de rango",
        showClearButton: true,
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Fecha es requerido'
        }]
    }).dxDateBox("instance");

    Page.ddlDatosComentario = $("#ddlDatosComentario").dxTextArea({
        tabIndex: tabIndex++,
        maxLength: 150,
        placeholder: 'Comentario',
        showClearButton: true,
        height: 100,
    }).dxTextArea("instance");

    Page.ddlDatosFechaCreacion = $("#ddlDatosFechaCreacion").dxDateBox({
        tabIndex: tabIndex++,
        placeholder: 'Fecha',
        type: "datetime",
        displayFormat: "dd/MM/yyyy hh:mm a",
        value: new Date(),
        readOnly: true,
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Fecha es requerido'
        }]
    }).dxDateBox("instance");

    Page.ddlDatosEstado = $("#ddlDatosEstado").dxSelectBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceSelectBox(`value`, null, {}, [
            { nombre: "Creado", value: "C" },
            { nombre: "Enviado", value: "E" },
            { nombre: "Aprobado", value: "A" },
            { nombre: "Anulado", value: "X" },
        ]),
        value: "C",
        placeholder: 'Estado',
        valueExpr: 'value',
        displayExpr: 'nombre',
        searchEnabled: true,
        readOnly: true,
    }).dxSelectBox("instance");

    $("#ddlDatosComentarioRechazadoMostrar").hide();
    Page.ddlDatosComentarioRechazado = $("#ddlDatosComentarioRechazado").dxTextArea({
        tabIndex: tabIndex++,
        //maxLength: 150,
        placeholder: 'Comentario Rechazado',
        height: 100,
        readOnly: true,
    }).dxTextArea("instance");

    //POPUP CONFIRMACION --------------------------------------------------------

    $("#divModalConfirmarTxn").dxValidationGroup();

    Page.ddlDatosComentarioRechazar = $("#ddlDatosComentarioRechazar").dxTextArea({
        tabIndex: tabIndex++,
        maxLength: 150,
        placeholder: 'Comentario...',
        showClearButton: true,
        height: 100,
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'El comentario es requerido',
        }]
    }).dxTextArea("instance");

});

Page.FormToObj = (() => {
    Page.data.palId = Page.ObjId.option('value');
    Page.data.palEstado = Page.data.estado;
    //CONTROLES DATOS--------------------------------------------------------------------       
    Page.data.empId = Page.ddlDatosEmpresa.option("value");
    Page.data.camId = Page.ddlDatosCampo.option("value");
    Page.data.palFechaCreacion = new Date().toJSON();
    Page.data.palFecha = Page.FormatDateString(Page.ddlDatosFecha.option("value"));

    if ([`R`, `X`].includes(Page.data.estado)) Page.data.palComentario = Page.ddlDatosComentarioRechazar.option("value");
    else Page.data.palComentario = Page.ddlDatosComentario.option("value");
});

Page.ObjToForm = (() => {
    Page.CargarPredeterminado = false;
    $("#ddlDatosComentarioRechazadoMostrar").hide();

    Page.data.codigo = Page.data.palId;
    Page.data.estado = Page.data.palEstado;
    //CONTROLES DATOS--------------------------------------------------------------------
    Page.ObjId.option('value', Page.data.palId);
    Page.ddlDatosId.option("value", Page.data.palId);
    Page.ddlDatosEmpresa.option("value", Page.data.empId);
    Page.ddlDatosCampo.option("value", Page.data.camId);
    Page.ddlDatosFechaCreacion.option("value", Page.data.palFechaCreacion);
    Page.ddlDatosFecha.option("value", Page.data.palFecha);
    Page.ddlDatosEstado.option("value", Page.data.palEstado);
    Page.ddlDatosComentario.option("value", Page.data.palComentario);

    if ([`C`, `X`].includes(Page.data.estado) && Page.data.logComentario) {
        Page.ddlDatosComentarioRechazado.option(`value`, Page.data.logComentario);
        let estado = Page.data.estado == `C` ? `Rechazado` : `Anulado`;
        $("#ddlDatosComentarioEstado").text(`${estado}`);
        $("#ddlDatosComentarioRechazadoMostrar").show();
    }
    Page.SetDataReadOnly();
    Page.SetDetalleReadOnly();
    Page.CargarPredeterminado = true;
});

Page.LimpiarControlesDatos = (() => {
    Page.CargarPredeterminado = false;
    $("#ddlDatosComentarioRechazadoMostrar").hide();
    Page.ControlesDatosResetValidator();
    Page.ddlDatosId.option("value", "0");
    Page.ddlDatosEstado.option("value", "C");
    Page.ddlDatosFechaCreacion.option(`value`, new Date());
    Page.ddlDatosComentario.reset();
    Page.ddlDatosComentarioRechazado.reset();
    Page.ddlDatosEmpresa.focus();
    Page.SetDataReadOnly();
    Page.LimpiarTodoGrid();
    Page.CargarPredeterminado = true;
});

Page.ControlesDatosResetValidator = (() => {
    let data = [
        { nombre: "ddlDatosEmpresa" },
        { nombre: "ddlDatosCampo" },
        { nombre: "ddlDatosFecha" },
        //{ nombre: "ddlDatosComentario" },
        //{ nombre: "ddlDatosComentarioRechazar" },
    ];
    data.forEach(e => {
        Page.TryCatchResetValidator(`#${e.nombre}`);
    });
});

Page.SetDataReadOnly = (() => {
    let readOnly = false;
    if (!["C", undefined].includes(Page.data.estado)) {
        readOnly = true;
    }
    Page.ddlDatosEmpresa.option("readOnly", readOnly);
    Page.ddlDatosCampo.option("readOnly", readOnly);
    Page.ddlDatosFecha.option("readOnly", readOnly);
    Page.ddlDatosComentario.option("readOnly", readOnly);
});

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Fin Datos  --------------------------------------------------------****/
/*****************************************************************************************************************************************/

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Detalle------------------------------------------------------------****/
/*****************************************************************************************************************************************/
Page.TabDetalle = (() => {
    privilegios.MostrarBotones("D");
    $("#divDetalleData").hide();
    $("#lblExcelError").hide();
    if (Page.data.codigo) {
        $("#divResultadoDetalle").show();
        setTimeout(function () {
            Page.CargarDetalleGrid();
        }, 250);
        Page.CargarSetIniciarDetalle();
    }
    else {
        $("#divResultadoDetalle").hide();
        $(`#ddlDetalleDocumentoMostrar`).hide();
    }
});

Page.initDetalleDatos = (() => {
    Page.ddlDetalle = $("#detalle").dxValidationGroup({}).dxValidationGroup(`instance`);

    let tabIndex = 1;

    Page.ddlDetalleMultiple = $("#ddlDetalleMultiple").dxSwitch({
        tabIndex: tabIndex++,
        value: false,
        switchedOffText: `NO`,
        switchedOnText: `SI`,
        onValueChanged: (e => {
            let id = Page.FormatStringNumber($("#hidDetId").val());
            if (e.value && id) {
                Page.grdDetalle.option(`selection.mode`, `multiple`);
                //$(`#ddlDetalleAnimalMostrar`).hide();            
                Page.ddlDetalleAnimal.option("readOnly", true);
            }
            else {
                Page.grdDetalle.option(`selection.mode`, `single`);
                if (id) {
                    Page.DetalleToForm(id);
                    Page.grdDetalle.option(`selectedRowKeys`, [id]);
                } else {
                    //$(`#ddlDetalleAnimalMostrar`).show();
                    Page.TryCatchResetValidator(`#ddlDetalleAnimal`);
                }
                if (Page.data.estado == "C") Page.ddlDetalleAnimal.option("readOnly", false);
                if (Page.ddlDetalleAnimalDataGrid) Page.ddlDetalleAnimalDataGrid.option("selection.mode", e.value ? "multiple" : "single");
            }
        }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxSwitch(`instance`);

    //$(`#ddlDetalleAnimalMostrar`).hide();

    Page.ddlDetalleAnimal = $("#ddlDetalleAnimal").dxDropDownBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceDataGrid({
            key: `aniId`, url: `${Page.RutaAPI}Detalle/Animal/GetList`, data: function () {
                return {
                    empresa: Page.data.empId,
                    estado: Page.data.estado == "C" ? "A" : "*"
                };
            }
        }),
        placeholder: "Seleccionar los animales...",
        valueExpr: "aniId",
        //displayExpr: "aniCodigo",
        displayExpr: ((item) => {
            if (Page.ddlDetalleMultiple.option(`value`)) {
                return item && item.aniCodigo;
            } else {
                return item && `[${item.aniCodigo}, ${item.troNombre}, ${item.catNombre}, ${item.razNombre}]`;
            }
        }),
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
        value: [],
        contentTemplate: ((e) => {
            let value = e.value ? e.value.map((x) => { if (typeof x == "object") return x.aniId; else return x; }) : e.value,
                $dataGrid = $("<div>").dxDataGrid({
                    remoteOperations: true,
                    showBorders: true,
                    showColumnLines: true,
                    showRowLines: true,
                    rowAlternationEnabled: true,
                    hoverStateEnabled: true,
                    columnAutoWidth: true,
                    paging: {
                        enabled: true,
                        pageIndex: 0,
                        pageSize: 5,
                    },
                    pager: {
                        visible: "auto",
                        allowedPageSizes: [5, 10, 30, 50],
                        showInfo: true,
                        showPageSizeSelector: true,
                        showNavigationButtons: true,
                    },
                    headerFilter: { visible: true },
                    filterRow: {
                        showOperationChooser: false,
                        visible: true,
                    },
                    dataSource: e.component.getDataSource(),
                    columns: [
                        //{ dataField: "aniId", caption: "ID", editorOptions: { placeholder: `ID`, showClearButton: true }, headerFilter: { allowSearch: true }, },
                        { dataField: "aniCodigo", caption: "Codigo", dataType: `string`, editorOptions: { placeholder: `Codigo`, showClearButton: true }, headerFilter: { allowSearch: true }, },
                        { dataField: "troNombre", caption: "Tropa", dataType: `string`, editorOptions: { placeholder: `Tropa`, showClearButton: true }, headerFilter: { allowSearch: true }, },
                        { dataField: "catNombre", caption: "Categoria", dataType: `string`, editorOptions: { placeholder: `Categoria`, showClearButton: true }, headerFilter: { allowSearch: true }, },
                        { dataField: "razNombre", caption: "Raza", dataType: `string`, editorOptions: { placeholder: `Raza`, showClearButton: true }, headerFilter: { allowSearch: true }, },
                        //{ dataField: "aniSexo", caption: "Sexo", dataType: `string`, editorOptions: { placeholder: `Sexo Animal`, showClearButton: true }, headerFilter: { allowSearch: true } },
                    ],
                    //scrolling: { mode: "infinite" },
                    height: 300,
                    selection: { mode: Page.ddlDetalleMultiple.option(`value`) ? "multiple" : "single" },
                    selectedRowKeys: value,
                    onSelectionChanged: function (item) {
                        let data = item.selectedRowsData;
                        data = data.length > 0 ? data : null;
                        e.component.option("value", data);
                    },
                });

            let dataGrid = $dataGrid.dxDataGrid("instance");
            Page.ddlDetalleAnimalDataGrid = dataGrid;
            e.component.on("valueChanged", function (args) {
                if (args.value) {
                    let type = args.value[0];
                    if (typeof type != "object") dataGrid.selectRows(args.value, false);
                }
                else dataGrid.selectRows(args.value, false);

                if (!Page.ddlDetalleMultiple.option(`value`)) e.component.close();
            });
            return $dataGrid;
        }),
        onValueChanged: (e => {
            let texto = 0;
            if (e.value) {
                texto = e.value.length;
            }
            $(`#ddlDetalleAnimalSeleccionado`).text(`${texto}`);
        }),
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            message: 'Animal es requerido',
            reevaluate: true,
            validationCallback: (e => {
                let sw = false;
                if (![null, undefined].includes(e.value)) {
                    if (e.value.length > 0) {
                        sw = true;
                    }
                }
                return sw;
            }),
        }]
    }).dxDropDownBox("instance");

    Page.ddlDetallePrenez = $("#ddlDetallePrenez").dxSelectBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceSelectBox(`value`, null, {}, [
            { nombre: `Preñada`, value: `P` },
            { nombre: `Vacia`, value: `V` },
            { nombre: 'Ninguno', value: `N` },
        ]),
        placeholder: 'Diagnostico...',
        valueExpr: 'value',
        displayExpr: 'nombre',
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Diagnostico es requerido',
        }]
    }).dxSelectBox("instance");

    Page.ddlDetalleTamanoPrenez = $("#ddlDetalleTamanoPrenez").dxSelectBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceSelectBox(`value`, ``, {}, [
            //{ value: "N", nombre: "Ninguno" },
            { value: "G", nombre: "Grande" },
            { value: "M", nombre: "Mediana" },
            { value: "P", nombre: "Pequeña" },
        ]),
        placeholder: 'Tamaño de Preñez',
        valueExpr: 'value',
        displayExpr: 'nombre',
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Tamaño de Preñez es requerido',
        }]
    }).dxSelectBox("instance");

    Page.ddlDetalleEvaluacion = $("#ddlDetalleEvaluacion").dxSelectBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceSelectBox(`evaId`),
        placeholder: 'Evaluacion',
        valueExpr: 'evaId',
        displayExpr: 'evaNombre',
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
        noDataText: "Sin Registros",
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            message: 'Evaluacion es requerido',
            reevaluate: true,
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxSelectBox("instance");

    Page.ddlDetalleTropaDestino = $("#ddlDetalleTropaDestino").dxSelectBox({
        tabIndex: tabIndex++,
        dataSource: Page.SetDataSourceSelectBox(`troId`),
        placeholder: 'Tropa Destino',
        valueExpr: 'troId',
        displayExpr: 'troNombre',
        showClearButton: true,
        searchEnabled: true,
        mode: "search",
        noDataText: "Sin Registros",
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            validationCallback: ((e) => {
                return true;
            }),
        }]
    }).dxSelectBox("instance");

    Page.ddlDetalleDocumento = $("#ddlDetalleDocumento").dxFileUploader({
        tabIndex: tabIndex++,
        accept: '.xls,.xlsx',
        allowedFileExtensions: [".xls", ".xlsx"],
        selectButtonText: "Buscar Documento...",
        labelText: "o arrastre el documento aqui...",
        //maxFileSize: 1000000,
        multiple: false,
        uploadUrl: `/`,
        uploadMethod: `POST`,
        uploadedMessage: "Documento Cargado.",
        uploadFailedMessage: "Ocurrió un error al Cargar el Documento.",
        readyToUploadMessage: "Listo",
        //invalidFileExtensionMessage: "Tipo de archivo no permitido",
        uploadUrl: `${General.UrlApiNegocio()}/Api/File/PostFile`,
        onUploaded: ((e) => {
            $("#divDetalleData").hide();
            General.ShowPleaseWait('Procesando el Documento, por favor espere');
            //FileName = e.request.responseText.replace('"', '').replace('"', '');
            Page.DetalleValidarExcel(e.request.responseText.replace('"', '').replace('"', ''));
        }),
        //validationStatus: "valid",
    }).dxFileUploader("instance");

});

Page.CargarSetIniciarDetalle = (() => {
    Page.DetalleResetValidator();
});

Page.SetDetalleReadOnly = (() => {
    let readOnly = true;
    if (["C"].includes(Page.data.estado)) {
        readOnly = false;
        $(`#ddlDetalleMultipleMostrar`).show();
        $(`#ddlDetalleDocumentoMostrar`).show();

    } else {
        $(`#ddlDetalleMultipleMostrar`).hide();
        $(`#ddlDetalleDocumentoMostrar`).hide();
    }
    Page.ddlDetalleAnimal.option(`readOnly`, readOnly);
    Page.ddlDetallePrenez.option(`readOnly`, readOnly);
    Page.ddlDetalleTamanoPrenez.option(`readOnly`, readOnly);
    Page.ddlDetalleEvaluacion.option(`readOnly`, readOnly);
    Page.ddlDetalleTropaDestino.option(`readOnly`, readOnly);
    //Page.ddlDetalle.option(`readOnly`, readOnly);
});

Page.SetDetalleReadOnlyOpcionPrenez = (readOnly => {
    Page.ddlDetalleTamanoPrenez.option(`readOnly`, readOnly);
    Page.ddlDetalleEvaluacion.option(`readOnly`, readOnly);
    Page.ddlDetalleTropaDestino.option(`readOnly`, readOnly);
});

Page.DetalleLimpiar = (() => {
    Page.CargarPredeterminado = false;
    $("#hidDetId").val("");
    Page.DetalleResetValidator();
    //Page.ddlDetallePrenez.reset();
    Page.ddlDetalleTropaDestino.reset();
    Page.CargarPredeterminado = true;
});

Page.DetalleResetValidator = (() => {
    Page.ddlDetalle.reset();
});

Page.TabDetalleFocus = (() => {
    setTimeout(function () {
        //Page.ddlDetalleAnimal.focus();
        Page.ddlDetallePrenez.focus();
    }, 100);
});

Page.DetalleNuevo = (() => {
    Page.grdDetalle.clearSelection();
    Page.DetalleLimpiar();
    $("#divDetalleData").show(100);
    //$(`#ddlDetalleAnimalMostrar`).show();
    $("#btnEnviar").hide();
    if (Page.ddlDetalleAnimalDataGrid && Page.data.estado == "C") Page.ddlDetalleAnimalDataGrid.state(null);
    Page.TabDetalleFocus();
});

Page.DetalleCancelar = (() => {
    Page.DetalleLimpiar();
    $("#divDetalleData").hide(100);
    $("#btnEnviar").show();
    $("#btnCelGuardar").hide();
    $("#btnCelNuevoAbajo").show();
    Page.grdDetalle.option(`selection.mode`, `single`);
});

Page.DetalleToForm = ((codigo) => {

    Page.DetalleLimpiar();
    $.ajax({
        url: `${Page.RutaAPI}Detalle`,
        dataType: "json",
        method: "GET",
        data: { id: codigo },
        success: (result => {
            let obj = result.Result;
            Page.CargarPredeterminado = false;
            //$(`#ddlDetalleAnimalMostrar`).show();
            $("#hidDetId").val(obj.detId);
            $("#divDetalleData").show(100);
            $("#btnEnviar").hide();
            $("#btnCelGuardar").show();
            $("#btnCelNuevoAbajo").hide();
            Page.ScrollTopAnimate("#divDetalleData");
            //setTimeout(function () {
            Page.ddlDetalleAnimal.option(`value`, [obj.aniId]);
            Page.ddlDetalleEvaluacion.option(`value`, obj.evaId);
            Page.ddlDetalleTropaDestino.option(`value`, obj.troIdDestino);
            Page.ddlDetalleTamanoPrenez.option(`value`, obj.detTamanoPrenez);
            Page.ddlDetallePrenez.option(`value`, obj.detPrenez);

            Page.CargarPredeterminado = true;
            Page.TabDetalleFocus();
        }),
        error: ((e) => {
            General.HidePleaseWait();
            let error = e.responseJSON
            General.ShowAlert(error.Mensaje, (error.TipoMensaje == 3 ? "Error" : "Validacion"), error.TipoMensaje);
        }),
    });
});

Page.DetalleGuardar = (() => {
    if ($("#divDetalleData").is(":visible")) {
        if (!Page.ValidarDatosGuardar(`#detalle`)) {
            return;
        }

        if (!Page.data.codigo) {
            General.ShowAlert("Por favor, seleccione un Registro.");
            return;
        }

        let obj = {};
        obj.detId = $("#hidDetId").val();
        obj.palId = Page.data.codigo;
        obj.detPrenez = Page.ddlDetallePrenez.option(`value`);
        obj.detTamanoPrenez = Page.ddlDetalleTamanoPrenez.option(`value`);
        obj.evaId = Page.ddlDetalleEvaluacion.option(`value`);
        obj.troIdDestino = Page.ddlDetalleTropaDestino.option(`value`);
        obj.codigo = [];
        obj.multiple = Page.ddlDetalleMultiple.option(`value`);

        if (obj.multiple && obj.detId > 0) {
            obj.codigo = Page.grdDetalle.option(`selectedRowKeys`);
            if (!obj.codigo.length) {
                General.ShowAlert("Por favor seleccione un Registro en el Detalle Para Actualizar.");
                return;
            }
        }
        else {
            let key = Page.ddlDetalleAnimal.option('value');
            obj.codigo = key.map((x) => { if (typeof x == "object") return x.aniId; else return x; });
        }
        let verb = 'POST';
        if (obj.detId > 0) verb = 'PUT';

        General.ShowPleaseWait('Procesando, por favor espere');
        $.ajax({
            method: verb,
            url: `${Page.RutaAPI}Detalle`,
            data: obj,
            dataType: "json",
            success: (result => {
                General.HidePleaseWait();
                $("#divDetalleData").hide(100);
                Page.DetalleLimpiar();
                setTimeout(function () {
                    Page.CargarDetalleGrid();
                }, 250);
                General.ShowAlert(`${result.Result}`);
                $("#btnEnviar").show();
                $("#btnCelGuardar").hide();
                $("#btnCelNuevoAbajo").show();
            }),
            error: (e => {
                General.HidePleaseWait();
                let error = e.responseJSON
                General.ShowAlert(error.Mensaje, (error.TipoMensaje == 3 ? "Error" : "Validacion"), error.TipoMensaje);
            }),
        });
    }

});

Page.DetalleEliminar = (() => {
    let id = $("#hidDetId").val();
    if ($("#divDetalleData").is(":visible") && id > 0) {
        let obj = {};
        obj.palId = Page.data.codigo;
        obj.codigo = [id]
        if (Page.ddlDetalleMultiple.option(`value`)) {
            obj.codigo = Page.grdDetalle.option(`selectedRowKeys`);
            if (!(obj.codigo.length > 0)) {
                General.ShowAlert("Por favor seleccione un Registro en el Detalle Para Eliminar.");
                return;
            }
        }
        General.ShowPleaseWait('Procesando, por favor espere');

        $.ajax({
            method: 'DELETE',
            url: `${Page.RutaAPI}Detalle`,
            dataType: "json",
            data: obj,
            success: (result => {
                General.HidePleaseWait();
                Page.DetalleLimpiar();
                setTimeout(function () {
                    Page.CargarDetalleGrid();
                }, 250);
                $("#divDetalleData").hide(100);
                $("#btnEnviar").show();
                $("#btnCelGuardar").hide();
                $("#btnCelNuevoAbajo").show();
                General.ShowAlert(`${result.Result}`);
            }),
            error: (e => {
                General.HidePleaseWait();
                let error = e.responseJSON
                General.ShowAlert(error.Mensaje, (error.TipoMensaje == 3 ? "Error" : "Validacion"), error.TipoMensaje);
            }),
        });
    }
    else {
        General.ShowAlert("Por favor seleccione un Registro.");
    }
});
Page.CargarDetalleGrid = (() => {

    let ClickKey;
    Page.grdDetalle = $("#grdDetalle").dxDataGrid({
        dataSource: Page.SetDataSourceDataGrid({ key: `detId`, url: `${Page.RutaAPI}Detalle/GetBy`, data: { id: Page.data.codigo } }),
        remoteOperations: true,
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
            allowedPageSizes: [10, 20, 30, 50, 100],
            showInfo: true,
            showPageSizeSelector: true,
            showNavigationButtons: true,
        },
        headerFilter: { visible: true },
        filterRow: {
            showOperationChooser: false,
            visible: true,
        },
        selectedRowKeys: [],
        columns: [
            /*{ dataField: "detId", caption: "ID", dataType: `number`, editorOptions: { placeholder: `ID`, showClearButton: true }, headerFilter: { allowSearch: true }, },*/
            { dataField: "aniCodigo", caption: "Codigo Animal", dataType: `string`, editorOptions: { placeholder: `Codigo Animal`, showClearButton: true }, headerFilter: { allowSearch: true } },
            { dataField: "troNombreOrigen", caption: "Tropa Origen", dataType: `string`, editorOptions: { placeholder: `TropaOrigen`, showClearButton: true }, headerFilter: { allowSearch: true } },
            { dataField: "troNombreDestino", caption: "Tropa Destino", dataType: `string`, editorOptions: { placeholder: `TropaDestino`, showClearButton: true }, headerFilter: { allowSearch: true } },
            { dataField: "catNombre", caption: "Categoria", dataType: `string`, editorOptions: { placeholder: `Categoria`, showClearButton: true }, headerFilter: { allowSearch: true } },
            { dataField: "razNombre", caption: "Raza", dataType: `string`, editorOptions: { placeholder: `Raza`, showClearButton: true }, headerFilter: { allowSearch: true }, },
            { dataField: "detPrenez", caption: "Diagnostico", dataType: `string`, width: 80, editorOptions: { placeholder: `Diagnostico`, showClearButton: true }, headerFilter: { allowSearch: true } },
            { dataField: "detTamanoPrenez", caption: "Tamaño Preñez", dataType: `string`, width: 90, editorOptions: { placeholder: `Tamaño Preñez`, showClearButton: true }, headerFilter: { allowSearch: true } },
            { dataField: "evaNombre", caption: "Evaluacion", dataType: `string`, editorOptions: { placeholder: `Evaluacion`, showClearButton: true }, headerFilter: { allowSearch: true } },
        ],
        onExporting: (e => {
            e.component.beginUpdate();
            //e.component.columnOption('detId', 'visible', false);
            let columnaVisible = [
                { id: `razNombre` },
                { id: `aniSexo` },
            ];
            columnaVisible.forEach(x => {
                Page.ColumnaVisibleTryCatch(e, x.id, `visible`, false);
            });

            let columnaCaption = [
                { id: `detTamanoPrenez`, entrada: `TamanoPrenez`, salida: `` },
                //{ id: ``, entrada:``,salida:``},
            ];

            columnaCaption.forEach(x => {
                x.salida = Page.ColumnaVisibleTryCatch(e, x.id, `caption`, x.entrada, true);
            });

            var workbook = new ExcelJS.Workbook();
            var worksheet = workbook.addWorksheet('MiCampoPalpacion');
            DevExpress.excelExporter.exportDataGrid({
                component: e.component,
                worksheet: worksheet,
                autoFilterEnabled: true,
            }).then(function (cellRange) {
                // header
                //console.log(cellRange);
                let row = 2, height = 15, columa = 15, celda = 9;
                let headerRowNota = worksheet.getRow(row);
                headerRowNota.height = height;
                worksheet.mergeCells(row, celda, row, columa);

                headerRowNota.getCell(celda).value = `Nota`;
                headerRowNota.getCell(celda).font = { name: 'Calibri', size: 12, bold: true };
                headerRowNota.getCell(celda).alignment = { horizontal: 'center', vertical: "middle" };
                //headerRowNota.getCell(7).alignment = { horizontal: 'left', vertical: "middle" };

                row++;
                let headerRowNotaCodigo = worksheet.getRow(row);
                headerRowNotaCodigo.height = height;
                worksheet.mergeCells(row, celda, row, columa);

                headerRowNotaCodigo.getCell(celda).value = `Codigo Animal: Obligatorio.`;
                headerRowNotaCodigo.getCell(celda).font = { name: 'Calibri', size: 12, bold: false };
                headerRowNotaCodigo.getCell(celda).alignment = { horizontal: 'left', vertical: "middle" };

                row++;
                let headerRowNotaTropaDestino = worksheet.getRow(row);
                headerRowNotaTropaDestino.height = height;
                worksheet.mergeCells(row, celda, row, columa);

                headerRowNotaTropaDestino.getCell(celda).value = `Tropa Destino: Opcional (Dejar vacio si el animal no cambio de tropa)`;
                headerRowNotaTropaDestino.getCell(celda).font = { name: 'Calibri', size: 12, bold: false };
                headerRowNotaTropaDestino.getCell(celda).alignment = { horizontal: 'left', vertical: "middle" };

                row++;
                let headerRowNotaDiagnostico = worksheet.getRow(row);
                headerRowNotaDiagnostico.height = height;
                worksheet.mergeCells(row, celda, row, columa);

                headerRowNotaDiagnostico.getCell(celda).value = `Diagnostico: Obligatorio => P=Preñada, V=Vacia, N=Ninguno`;
                headerRowNotaDiagnostico.getCell(celda).font = { name: 'Calibri', size: 12, bold: false };
                headerRowNotaDiagnostico.getCell(celda).alignment = { horizontal: 'left', vertical: "middle" };

                row++;
                let headerRowNotaTamanoPrenez = worksheet.getRow(row);
                headerRowNotaTamanoPrenez.height = height;
                worksheet.mergeCells(row, celda, row, columa);

                headerRowNotaTamanoPrenez.getCell(celda).value = `Tamaño Preñez: Obligatorio => G=Grandre, M=Mediana, P=Pequeña`;
                headerRowNotaTamanoPrenez.getCell(celda).font = { name: 'Calibri', size: 12, bold: false };
                headerRowNotaTamanoPrenez.getCell(celda).alignment = { horizontal: 'left', vertical: "middle" };

                row++;
                let headerRowNotaEvaluacion = worksheet.getRow(row);
                headerRowNotaEvaluacion.height = height;
                worksheet.mergeCells(row, celda, row, columa);

                headerRowNotaEvaluacion.getCell(celda).value = `Evaluacion: Opcional=> De Acuerdo con lo configurado por el Usuario`;
                headerRowNotaEvaluacion.getCell(celda).font = { name: 'Calibri', size: 12, bold: false };
                headerRowNotaEvaluacion.getCell(celda).alignment = { horizontal: 'left', vertical: "middle" };
            }).then(function () {
                workbook.xlsx.writeBuffer().then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'MiCampoPalpacion.xlsx');
                });
            }).then(function () {

                columnaVisible.forEach(x => {
                    Page.ColumnaVisibleTryCatch(e, x.id, `visible`, true);
                });
                columnaCaption.forEach(x => {
                    Page.ColumnaVisibleTryCatch(e, x.id, `caption`, x.salida);
                });
                e.component.endUpdate();
            });
            e.cancel = true;
        }),
        selection: {
            mode: "single"
        },
        onRowClick: (e => {
            if (e.component.option(`selection.mode`) == `single`) {
                if (ClickKey == e.key && e.key) {
                    Page.DetalleToForm(e.key);
                    ClickKey = null;
                }
                else ClickKey = e.key;
            }
        }),
        onSelectionChanged: function (e) {
            if (e.component.option(`selection.mode`) == `multiple`) {
                let data = e.selectedRowsData;
                if (data) data = data.map((x) => {
                    return {
                        aniId: x.aniId,
                        aniCodigo: x.aniCodigo,
                        troNombre: x.troNombreOrigen,
                        razNombre: x.razNombre,
                        catNombre: x.catNombre,
                    };
                });
                Page.ddlDetalleAnimal.option("value", data);
            }
        },
        onContentReady: function (e) {
            if ($("#tabDetalleLi.active").length === 1) {
                Page.ScrollTopAnimate("#grdDetalle");
            }
        },
        summary: {
            totalItems: [
                {
                    name: "total",
                    summaryType: "none",
                    showInColumn: "aniCodigo",
                    customizeText: function (data) {
                        return `Cantidad: ${General.formatNumber(Page.grdDetalle.totalCount())}`;
                    }
                },
            ],
        },
    }).dxDataGrid("instance");
});

Page.ColumnaVisibleTryCatch = ((e, id, nombre, valor, devolver = false) => {
    try {
        let result = '';
        if (devolver) {
            result = e.component.columnOption(id, nombre);
        }
        e.component.columnOption(id, nombre, valor);
        if (devolver) {
            return result;
        }
    }
    catch {
        console.error(`Error al Colocar en ${id}, ${nombre}:${valor}`);
    }
});

Page.DetalleValidarExcel = ((file) => {
    if (Page.grdDetalleExcelError) {
        Page.grdDetalleExcelError.dispose();
    }
    let obj = {
        nombre: file,
        palId: Page.data.codigo,
    };
    $.ajax({
        method: "POST",
        url: `${Page.RutaAPI}Detalle/PostArchivo`,
        data: obj,
        success: (e => {
            General.HidePleaseWait();

            let mensaje = e.Mensaje;
            if (e.Result.length > 0) {
                $("#lblExcelError").show();
                Page.CargarDetalleGridExcelError(e.Result);
            } else {
                $("#lblExcelError").hide();
            }
            General.ShowAlert(mensaje);
            setTimeout(function () {
                Page.CargarDetalleGrid();
            }, 250);
        }),
        error: (e => {
            General.HidePleaseWait();
            let error = e.responseJSON
            General.ShowAlert(error.Mensaje, (error.TipoMensaje == 3 ? "Error" : "Aviso"), error.TipoMensaje);
        }),
    });
});

Page.CargarDetalleGridExcelError = ((data = []) => {

    Page.grdDetalleExcelError = $("#grdDetalleExcelError").dxDataGrid({
        dataSource: Page.SetDataSourceDataGrid({ key: `nro`, dataSource: data }),
        showBorders: true,
        showColumnLines: true,
        showRowLines: true,
        ////filas Responsive de Acuerdo a la Columna
        //wordWrapEnabled: true,
        rowAlternationEnabled: true,
        hoverStateEnabled: true,
        columnAutoWidth: true,
        export: {
            fileName: "Animales con Errores",
        },
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
            allowedPageSizes: [10, 20, 30, 50, 100],
            showInfo: true,
            showPageSizeSelector: true,
            showNavigationButtons: true,
        },
        headerFilter: { visible: true },
        filterRow: {
            showOperationChooser: false,
            visible: true,
        },
        selectedRowKeys: [],
        columns: [
            { dataField: "codigo", caption: "Cod. Animal", dataType: `string`, editorOptions: { placeholder: `Cod. Animal`, showClearButton: true }, headerFilter: { allowSearch: true }, },
            { dataField: "mensaje", caption: "Mensaje", dataType: `string`, editorOptions: { placeholder: `Mensaje`, showClearButton: true }, headerFilter: { allowSearch: true }, },
        ],
        selection: {
            mode: "single"
        },
    }).dxDataGrid("instance");

    //setTimeout(function () {
    //    Page.grdDetalleExcelError.exportToExcel();
    //}, 5000);
});

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Log        --------------------------------------------------------****/
/*****************************************************************************************************************************************/
Page.TabLog = (() => {
    privilegios.MostrarBotones("N");
    if (Page.data.codigo) {
        $("#divResultadoLog").show();
        setTimeout(function () {
            Page.LogCargarGridBusqueda();
        }, 250);
    }
    else {
        $("#divResultadoLog").hide();
    }
});
Page.LogCargarGridBusqueda = (() => {

    Page.grdLog = $("#grdLog").dxDataGrid({
        dataSource: Page.SetDataSourceDataGrid({ key: `logId`, url: `${Page.RutaAPI}Log/GetBy`, data: { id: Page.data.codigo } }),
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
            allowedPageSizes: [10, 20, 30],
            showInfo: true,
            showPageSizeSelector: true,
            showNavigationButtons: true,
        },
        headerFilter: { visible: true },
        filterRow: {
            showOperationChooser: false,
            visible: true,
        },
        selectedRowKeys: [],
        columns: [
            { dataField: "logFechaDoc", caption: "Fecha", dataType: 'date', format: 'dd/MM/yyyy hh:mm a', editorOptions: { placeholder: `Fecha`, showClearButton: true }, headerFilter: { allowSearch: true }, },
            { dataField: "usrId", caption: "Usuario", editorOptions: { placeholder: `Usuario`, showClearButton: true }, headerFilter: { allowSearch: true }, },
            {
                dataField: "logEstado", caption: "Estado", editorOptions: { placeholder: `Estado`, showClearButton: true }, headerFilter: { allowSearch: true },
                calculateCellValue: (data => {
                    let row = data.logEstado;
                    if (row == "C") {
                        return "Creado";
                    }
                    else if (row == "E") {
                        return "Enviado";
                    }
                    else if (row == "A") {
                        return "Aprobado";
                    }
                    else if (row == "X") {
                        return "Anulado";
                    }
                    else if (row == "R") {
                        return "Rechazado";
                    }
                }),
            },
            { dataField: "logComentario", caption: "Comentario", width: 450, editorOptions: { placeholder: `Comentario`, showClearButton: true }, headerFilter: { allowSearch: true }, },
        ],
        selection: {
            mode: "single"
        },
    }).dxDataGrid("instance");

});

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Fin Log  ----------------------------------------------------------****/
/*****************************************************************************************************************************************/

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Botones------------------------------------------------------------****/
/*****************************************************************************************************************************************/
Page.InitBotonesCRUD = (() => {
    $("#btnNuevo, #btnCelNuevo").click(function () {
        let active = Page.GetTabActive();
        if (active == "tabDatosLi") {
            Page.NuevoRegistro();
        }
        else if (active == "tabDetalleLi") {
            Page.grdDetalle.option(`selection.mode`, `single`);
            Page.DetalleNuevo();
        }

    });

    $("#btnGuardar, #btnCelGuardar").click(function () {
        let active = Page.GetTabActive();
        if (active == "tabDatosLi") {
            Page.GuardarRegistro();
        }
        else if (active == "tabDetalleLi") {
            Page.DetalleGuardar();
        }

    });

    $("#btnCancelar").click(function () {
        DevExpress.ui.dialog.confirm("¿Seguro que desea Cancelar?", "Cancelar").done(result => {
            if (result) {
                let active = Page.GetTabActive();
                if (active == "tabDatosLi") {
                    //Page.LimpiarControlesDatos();
                    Page.CancelarRegistro();
                }
                else if (active == "tabDetalleLi") {
                    Page.DetalleCancelar();
                }
            }
        });
    });

    $("#btnEliminar").click(function () {
        let active = Page.GetTabActive();

        if (active == "tabDatosLi") {
            DevExpress.ui.dialog.confirm("¿Seguro que desea eliminar?", "Eliminar").done(result => {
                if (result) {
                    Page.EliminarRegistro(false, true);
                }
            });
        }
        else if (active == "tabDetalleLi") {
            let titulo = "Eliminar";
            if (Page.ddlDetalleMultiple.option(`value`)) {
                titulo = "Eliminar Multiple";
            }
            DevExpress.ui.dialog.confirm("¿Seguro que desea eliminar?", titulo).done(result => {
                if (result) {
                    Page.DetalleEliminar();
                }
            });
        }
    });

    $("#btnEnviar").click(function () {
        DevExpress.ui.dialog.confirm("¿Seguro que desea Enviar?", "Enviar").done(result => {
            if (result) {
                Page.data.estado = 'E';
                Page.GuardarRegistro();
            }
        });
    });

    $("#btnAprobar").click(function () {
        DevExpress.ui.dialog.confirm("¿Seguro que desea Aprobar?", "Aprobar").done(result => {
            if (result) {
                //let active = Page.GetTabActive();
                Page.data.estado = 'A';
                Page.GuardarRegistro();
                //if (active == "tabDetalleLi") {
                //    Page.TabDetalle();
                //}
            }
        });
    });

    $("#btnRechazar, #btnAnular").attr("data-toggle", "modal");
    $("#btnRechazar, #btnAnular").attr("data-target", "#divModalConfirmarTxn");
    //$("#btnRechazarConfirmado").attr("data-dismiss", "modal");
    $("#btnRechazar").click(function () {
        Page.TryCatchResetValidator(`#ddlDatosComentarioRechazar`);
        $(`#divModalTitulo`).text(`Rechazar`);
        $(`#divModalEstado`).text(`Rechazar`);
    });
    $("#btnAnular").click(function () {
        Page.TryCatchResetValidator(`#ddlDatosComentarioRechazar`);
        $(`#divModalTitulo`).text(`Anular`);
        $(`#divModalEstado`).text(`Anular`);
    });
    $("#btnConfirmadoTxn").click(function () {
        if (!Page.ValidarDatosGuardar(`#divModalConfirmarTxn`, false)) {
            return;
        }
        let titulo = $(`#divModalTitulo`).text();
        DevExpress.ui.dialog.confirm(`¿Seguro que desea ${titulo}?`, titulo).done(result => {
            $("#divModalConfirmarTxn").modal('hide');
            if (result) {
                if (Page.data.estado == `E`) {
                    Page.data.estado = 'R';
                }
                else if ([`A`].includes(Page.data.estado)) {
                    Page.data.estado = 'X';
                }

                let active = Page.GetTabActive();
                if (active == "tabDetalleLi" && Page.data.estado == "R") {
                    //Page.TabDetalle();
                    $("#divDetalleData").hide();
                    Page.CargarSetIniciarDetalle();
                }
                Page.GuardarRegistro();
            }
        });
    });

    $("#btnExcel").click(function () {
        let active = Page.GetTabActive();
        if (active == "tabDetalleLi") {
            DevExpress.ui.dialog.confirm({ title: "Excel", showTitle: true, messageHtml: "Descargar Excel", buttons: [{ text: "Nuevo", onClick: function () { return true; } }, { text: "Detalle", onClick: function () { return false; } }] }).done(result => {
                General.Notify("Descargando Excel.....");
                if (result) {
                    let url = `${General.UrlApiNegocio()}/Archivos/MiCampoPalpacion.xlsx`;
                    Page.DescargarDocumento(url);
                }
                else {
                    Page.grdDetalle.exportToExcel()
                }
            });
        }
    });
    $(`#tabDetalle`).click(function () {
        Page.TabDetalle();
    });

    $(`#tabLog`).click(function () {
        Page.TabLog();
    });
});

Page.VerificarBotones = (tabActive => {

    let codigo = Page.data.codigo;
    if ([`tabDetalleLi`, `tabDatosLi`].includes(tabActive) && codigo) {
        if (tabActive == "tabDetalleLi") $("#btnExcel").show();
        Page.VerficarRolUsuario();
    }
});

Page.GuardarRegistroPageFail = (() => {
    Page.data.estado = Page.ddlDatosEstado.option(`value`);
});

/*****************************************************************************************************************************************/
/*****---------------------------------------------------- Seccion Fin Botones--------------------------------------------------------****/
/*****************************************************************************************************************************************/
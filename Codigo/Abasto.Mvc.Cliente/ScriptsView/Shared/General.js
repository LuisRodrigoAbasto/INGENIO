/**
 * Module for displaying "Waiting for..." dialog using Bootstrap
 * http://bootsnipp.com/snippets/featured/quotwaiting-forquot-modal-dialog
 * @author Eugene Maslovich <ehpc@em42.ru>
 */

var General = General || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return {
        //PLEASE WAIT BEGIN
        /** 
         * @param message Custom message
         * @param options Custom options:
         * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
         * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
         */
        IsShowingPleaseWait: false,

        ShowPleaseWait: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Procesando...';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: 'success',
                //progressType: 'error',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog
            $dialog.modal();
        },
        HidePleaseWait: function () {
            $dialog.modal('hide');
        },
        //PLEASE WAIT END
        //ALERT BEGIN
        ShowAlert: function (message, title, tipoMensaje) {
            if (message == null && title == null) {
                message = '';
                title = '';
            }
            var icono = "";
            switch (tipoMensaje) {
                case 2:
                    icono = "<span class='glyphicon glyphicon-info-sign' style='font-size:30px; color:#ffc400;'></span>";
                    break;
                case 3:
                    icono = "<span class='glyphicon glyphicon-exclamation-sign' style='font-size:30px; color:#d50000;'></span> ";
                    break;
                default:
                    icono = "<span class='glyphicon glyphicon-info-sign' style='font-size:30px; color:#0091ea;'></span>";
                    break;
            }
            var inicio = "<table style='max-width:700px'><tr><td style='vertical-align:text-top; padding:0px 10px 0px 0px;'>";
            var medio = "</td><td style='vertical-align:center'>";
            try {
                DevExpress.ui.dialog.alert(inicio + icono + medio + message + "</td></tr></table>", title);
            } catch (e) {
                alert(message, title);
            }

        },
        ShowConfirm: function (message, title) {
            if (message == null && title == null) {
                message = 'Esta seguro que desea proceder?';
                title = 'Confirmar';
            };
            return confirm(message, title);
        },
        ShowMenu: function () {
            var slideMenu = $('#divMenu');
            var slideBody = $('#divCuerpo');

            if (slideBody.hasClass("col-sm-12")) {
                slideBody.removeClass("col-sm-12");
                slideBody.addClass("col-sm-9");
                slideMenu.show();
            }
        },

        HideMenu: function () {
            var slideMenu = $('#divMenu');
            var slideBody = $('#divCuerpo');

            if (slideBody.hasClass("col-sm-9")) {
                slideMenu.hide();
                slideBody.removeClass("col-sm-9");
                slideBody.addClass("col-sm-12");
            }
        },
        //ALERT END
        //Notify
        Notify: ((mensaje, tipo, position, tiempo) => {
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

        }),
        //Notify END
        //HANDLE ERROR BEGIN
        HandleError: function (message, title, error) {
            General.ShowAlert(message, title);
            //General.LogError(error);
        },
        LogError: function (error) {
            var strError = "";
            if (typeof error === 'undefined') {
                error = 'Error indefinido';
            } else if (typeof error === 'string') {
                strError = error;
            } else if (error.responseJSON) {
                error.responseJSON.originalRequestOptions = {};
                error.responseJSON.originalRequestOptions.ruta = error.originalRequestOptions.url;
                error.responseJSON.originalRequestOptions.metodo = error.originalRequestOptions.method;
                strError = encodeURIComponent(JSON.stringify(error.responseJSON));
            } else if (error.responseText) {
                error.responseText.originalRequestOptions = {};
                error.responseText.originalRequestOptions.ruta = error.originalRequestOptions.url;
                error.responseText.originalRequestOptions.metodo = error.originalRequestOptions.method;
                strError = encodeURIComponent(JSON.stringify(error.responseText));
            } else {
                error = JSON.parse(error);
                error.originalRequestOptions = {};
                error.originalRequestOptions.ruta = error.originalRequestOptions.url;
                error.originalRequestOptions.metodo = error.originalRequestOptions.method;
                strError = encodeURIComponent(JSON.stringify(error));
            }


            //Manejo del error
            $.ajax({
                type: "POST",
                url: General.UrlApiNegocio() + "/Api/Helper/PostError?message=" + strError + "&user=" + General.Usuario(),
                success: function (result) {
                },
                error: function (result) {
                }
            });
        },
        //HANDLE ERROR
        //GLOBALIZE BEGIN
        Globalizar: async function () {

            await $.ajax({
                url: General.UrlBase() + '/Scripts/cldr/supplemental/likelySubtags.json',
                type: 'GET',
                success: function (data) {
                    Globalize.load(data);
                }
            });

            await $.ajax({
                url: General.UrlBase() + '/Scripts/cldr/dates/es/ca-generic.json',
                type: 'GET',
                success: function (data) {
                    Globalize.load(data);
                }
            });

            await $.ajax({
                url: General.UrlBase() + '/Scripts/cldr/dates/es/ca-gregorian.json',
                type: 'GET',
                success: function (data) {
                    Globalize.load(data);
                }
            });

            await $.ajax({
                url: General.UrlBase() + '/Scripts/cldr/dates/es/dateFields.json',
                type: 'GET',
                success: function (data) {
                    Globalize.load(data);
                }
            });

            await $.ajax({
                url: General.UrlBase() + '/Scripts/cldr/dates/es/timeZoneNames.json',
                type: 'GET',
                success: function (data) {
                    Globalize.load(data);
                }
            });

            await $.ajax({
                url: General.UrlBase() + '/Scripts/cldr/numbers/es/numbers.json',
                type: 'GET',
                success: function (data) {
                    Globalize.load(data);
                }
            });
            Globalize.locale("es");
        },

        //GLOBAL SESSION VARS
        Tokenkey: function (value) {
            General.SetDatos(value, "tokenkey");
            try {
                return General.GetDatosJson().tokenkey;
            } catch (e) {
                return "";
            }
        },
        Headers: function () {
            var headers = {};
            headers.Authorization = 'Bearer ' + General.Tokenkey();
            return headers;
        },

        SetAjaxDefaults: function () {
            $.ajaxPrefilter(function (options, originalOptions, jqXHR) {
                jqXHR.originalRequestOptions = originalOptions;
            });

            $.ajaxSetup({
                method: 'GET',
                headers: General.Headers(),
                error: function (jqXHR, textStatus, errorThrown) {

                    try {
                        if (jqXHR == null || jqXHR.responseJSON == null || jqXHR.responseJSON.Mensaje == null) {
                            if (navigator.onLine) {
                                // el navegador está conectado a la red                        
                                General.Notify("Lo sentimos no se ha podido procesar una validación o un error ha ocurrido.", "error");
                            } else {
                                // el navegador NO está conectado a la red
                                General.Notify("No hay Conexion a Internet.", "error");
                            }
                        }
                        else {
                            if (jqXHR.responseJSON.TipoMensaje) {
                                General.ShowAlert(jqXHR.responseJSON.Mensaje, "", jqXHR.responseJSON.TipoMensaje);
                            }
                            else {
                                General.ShowAlert(jqXHR.responseJSON.Mensaje);
                            }
                        }
                        General.HidePleaseWait();
                    } catch (e) {
                        General.Notify("Lo sentimos no se ha podido procesar una validación o un error ha ocurrido.", "error");
                        General.HidePleaseWait();
                    }
                }
            });
        },
        Usuario: function (value) {
            General.SetDatos(value, "usuario");
            if (General.GetDatos() != null) {
                return General.GetDatosJson().usuario;
            }
        },
        Email: function (value) {
            General.SetDatos(value, "email");
            return General.GetDatosJson().email;
        },
        FullName: function (value) {
            General.SetDatos(value, "fullName");
            return General.GetDatosJson().fullName.trim();
        },
        Application: function (value) {
            General.SetDatos(value, "application");
            return General.GetDatosJson().application;
        },
        Entorno: function (value) {
            General.SetDatos(value, "entorno");
            return General.GetDatosJson().entorno;
        },
        ApplicationName: function (value) {
            General.SetDatos(value, "applicationName");
            return General.GetDatosJson().applicationName;
        },
        UserRole: function (value) {
            General.SetDatos(value, "userRole");
            return General.GetDatosJson().userRole;
        },
        RoleNames: function (value) {
            General.SetDatos(value, "roleNames");
            return General.GetDatosJson().roleNames;
        },
        UrlApiSeguridad: function (value) {
            General.SetDatos(value, "urlApiSeguridad");
            return General.GetDatosJson().urlApiSeguridad;
        },
        UrlApiNegocio: function (value) {
            //General.SetDatos(value, "urlApiNegocio");
            //return General.GetDatosJson().urlApiNegocio;
            return "http://localhost:12383";
        },
        UrlBase: function (value) {
            //General.SetDatos(document.location.origin, "urlBase");
            //return document.location.origin;
            General.SetDatos(value, "urlBase");
            return General.GetDatosJson().urlBase;
            //return General.UrlBase();
        },
        ObjSelected: function (value) {
            General.SetDatos(value, "ObjSelected");
            return General.GetDatosJson().ObjSelected;
        },
        ModSelected: function (value) {
            General.SetDatos(value, "ModSelected");
            return General.GetDatosJson().ModSelected;
        },
        ObjSelectedActiveGroup: function (value) {
            General.SetDatos(value, "ObjSelectedActiveGroup");
            return General.GetDatosJson().ObjSelectedActiveGroup;
        },
        UserRoleTxn: function (value) {
            if (value) General.SetDatos(JSON.parse(value), "userRoleTxn");
            if (General.GetDatos() != null) {
                return General.GetDatosJson().userRoleTxn;
            }
        },

        //GLOBAL SESSION VARS END
        //SESSION TOKEN VALIDATION START
        ValidateToken: async function (urlBase) {
            urlBase = urlBase.replace(/\//g, '');
            if (urlBase.length > 0) urlBase = "/" + urlBase;
            if (General.ValidateDatos() == false) {
                General.ShowAlert("Por favor logeese antes de navegar");
                setTimeout(async function () {
                    window.location.href = urlBase + '/';
                }, 2500);
            }
            let result = await $.ajax({
                url: General.UrlApiNegocio() + "/Api/Helper/IsTokenValid",
                type: 'GET',
                headers: General.Headers()
            });
            if (!result) {
                General.ShowAlert("Por favor logeese antes de navegar");
                setTimeout(async function () {
                    window.location.href = urlBase + '/';
                }, 2500);
            }
        },
        //SESSION TOKEN VALIDATION END
        GetData: function (_url, _headers) {
            if (typeof _headers === 'undefined') {
                _headers = General.Headers();
            }
            var dsResult = undefined;
            $.ajax({
                url: _url,
                type: 'GET',
                headers: _headers,
                async: false,
                success: function (data) {
                    dsResult = data.Result;
                }
            });
            return dsResult;
        },
        //GLOBALIZE ERROR

        //[Nombre Objeto - Begin]
        GetNombreObjeto: function (_objId) {
            var NombreObjeto = "";
            $.ajax({
                type: 'GET',
                url: General.UrlApiSeguridad() + '/api/objeto?objId=' + _objId,
                async: false,
                success: function (data) {
                    NombreObjeto = data.Result.objNombre;
                }
            });
            return NombreObjeto;
        },
        UsuarioTieneRol: function (strRol) {
            if (!strRol) return false;
            if (this.UserRole().indexOf(strRol.toUpperCase()) >= 0) {
                return true
            }
            else if (this.UserRole().indexOf(strRol.toLowerCase()) >= 0) {
                return true
            }
            else if (this.UserRole().indexOf(strRol) >= 0) {
                return true
            }
            else { return false }
        },
        UsuarioTieneRolTxn: function (rol, key) {
            if (!rol) return false;
            let obj = General.UserRoleTxn();
            if (rol == "EC") return obj.encargado.includes(key);
            else if (rol == "GE") return obj.gerente.includes(key);
            else if (rol == "AD") return obj.administrador.includes(key);
            else if (rol == "DATOS") return obj.empresa.includes(key);
            else return false;
        },

        ToggleVisibility: function (item) {
            if (item.isVisible()) {
                item.hide();
            } else {
                item.show();
            }
        },

        Mensaje: function (modulo, llave) {
            if (typeof modulo === 'undefined' || typeof llave === 'undefined') {
                return '';
            }
            var mensaje = '';
            var mensajeria = localStorage.getItem(modulo + "mensajeria");
            if (typeof mensajeria === 'undefined' || mensajeria == null || mensajeria == 'undefined') {
                $.ajax({
                    type: "GET",
                    url: General.UrlApiNegocio() + "/Api/Mensajeria/GetMensajeByModulo?modulo=" + modulo,
                    async: false,
                    success: function (result) {
                        localStorage.setItem(modulo + "mensajeria", JSON.stringify(result.Result));
                        mensajeria = localStorage.getItem(modulo + "mensajeria");
                    },
                    error: function (result) {
                        return '';
                    }
                });
            }
            mensajeria = JSON.parse(mensajeria);
            mensaje = mensajeria[llave];
            if (typeof mensaje === 'undefined' || mensaje == null || mensaje == 'undefined') {
                return '';
            }
            else {
                return mensaje;
            }
        },
        ReadDate: function (date) {

            var date2 = new Date(date);

            date2.setHours(date2.getHours() + 12);
            return date2;

        },
        formatNumber: function (value) {
            if (!value) return value;

            value = parseFloat(value);
            value = Globalize.formatNumber(value);
            return value;
        },
        formatDate: function (date, formato = `yyyy-MM-dd`) {
            if ([null, undefined, ``].includes(date)) return ``;
            let fecha = new Date(date);
            if (fecha.toJSON()) {
                fecha = Globalize.formatDate(fecha, { raw: formato });
                return fecha;
            } else {
                return ``;
            }
        },
        EsFecha: function (date, formato = `dd/MM/yyyy`) {
            let fecha = new Date(date);
            if (fecha.toJSON()) {
                fecha = Globalize.formatDate(fecha, { raw: formato });
                return fecha;
            } else {
                return date;
            }
        },
        GetUrlParam: function (param) {
            var url = new URL(window.location.href);
            var result = url.searchParams.get(param);
            return result;
        },
        GetCookieName: function () {
            return 'DATOSMICAMPO';
        },
        SetDatos: function (value, prop) {
            if (value !== undefined) {
                var datos = Cookies.getJSON(General.GetCookieName());
                if (datos == null) {
                    datos = {};
                }
                datos[prop] = value;
                Cookies.set(General.GetCookieName(), datos);
            }
        },
        GetDatos: function (value, prop) {
            return Cookies.get(General.GetCookieName());
        },
        GetDatosJson: function (value, prop) {
            var datos = General.GetDatos();
            if (datos === undefined || datos === null) return null;
            return JSON.parse(datos);
        },
        ValidateDatos: function () {
            if (General.GetDatos() !== undefined) return true;
            return false;
        },
        Dominio: function (value) {
            General.SetDatos(value, "dominio");
            return General.GetDatosJson().dominio;
        },
        ShowToastWarning: function (textAlert, timeout) {
            if (!timeout) {
                timeout = 10000;
            }
            $("#alert-warning").html(textAlert);
            $("#alert-warning").show(250);
            setTimeout(
                function () {
                    $("#alert-warning").hide(250);
                }, timeout);
        },

        HideToastWarning: function (timeout) {
            $("#alert-warning").hide(250);
        },
    };
})(jQuery);

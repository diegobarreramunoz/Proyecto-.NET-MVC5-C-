var idioma = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún registro disponible en esta tabla",
    "sInfo": "_START_ al _END_ de _TOTAL_ registros",
    "sInfoEmpty": "sin registros a mostrar",
    "sInfoFiltered": "(de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": '<span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>',
    "sUrl": "",
    "sInfoThousands": ".",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }

};
Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;}
    
    
function ver_markdown(mark) {
    $("." + mark).markdown({
        autofocus: false,
        savable: false,
        hiddenButtons: "all",
        fullscreen: { enable: false },
        onShow: function (e) {
            e.showPreview();
            $(".md-header.btn-toolbar").hide();
            $(".md-preview").css("text-align", "left");
            $(".md-preview").css("vertical-align", "top");
            $(".md-preview").css("background", "none");
            $(".md-preview").css("border-top", "none");
            $(".md-editor").css("border", "none");
            $(".md-preview").css("border", "none");
            $(".md-preview").css("height", "50px");
            $(".md-preview").css("width", "100%");
            $(".md-preview").css("padding", "0px");
            $(".md-preview").css("overflow", "");
            $(".md-preview>h3").css("margin", "0px");
        }
    });
}


function InicializaMantenedor(grilla) {

    function pagefunction () {
        /* BASIC ;*/
        var responsiveHelper_datatable_col_reorder = undefined;
        var responsiveHelper_dt_basic = undefined;

        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        $('#' + grilla).dataTable({
            //      "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            //         "t" +
            //         "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
            oLanguage: idioma,
            autoWidth: true,
            paging: true,
            searching: true,
            "preDrawCallback": function () {
                // Initialize the responsive datatables helper once.
                if (!responsiveHelper_dt_basic) {
                    responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($('#' + grilla), breakpointDefinition);
                }
            }, "oTableTools": {
                "aButtons": [
                        "copy",
                            {
                            "sExtends": "csv",
                            "sFileName": "DescargaCSVTs",
                            "bFooter": false
                            },
                            {
                            "sExtends": "xls",
                            "sFileName": "DescargaExcelTs.xls",
                            "bFooter": false
                            },
                            {
                                "sExtends": "pdf",
                                "sTitle": "TS_PDF",
                                "sPdfMessage": "TS PDF Export",
                                "sPdfSize": "letter",
                                "sPdfOrientation": "landscape",// 
                                "bFooter": false, // se quita el footer del pdf.
                             },
                             {
                               "sExtends": "print",
                               "sMessage": "Generado por Timesheets <i>(Presione ESC para salir)</i>",
                               "bShowAll": false, // solo muestra lo que contiene la pagina, no todos los datos
                            

                             }
                 ],
                "sSwfPath": "/js/plugin/datatables/swf/copy_csv_xls_pdf.swf"

            }, dom: "<'dt-toolbar'<'col-xs-12 col-sm-6'lf><'table-responsive hidden-xs'C> Tr>",
            "rowCallback": function (nRow) {
                responsiveHelper_dt_basic.createExpandIcon(nRow);
            },
            "drawCallback": function (oSettings) {
                responsiveHelper_dt_basic.respond();
            }
        });

        /* END BASIC */


    };

    // load related plugins

    pagefunction();
}



//grilla Filtro

function InicializaGrillaFiltro(grilla) {
    var otable;
    function pagefunction() {
        /* BASIC ;*/
        var responsiveHelper_datatable_fixed_column = undefined;
        var responsiveHelper_datatable_col_reorder = undefined;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        otable = $('#' + grilla).DataTable({
            "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'lf><'table-responsive hidden-xs'C> Tr>" +
        "t" +
        "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
            "autoWidth": true,
            oLanguage: idioma,
            "oTableTools": {
                "aButtons": [
                        "copy",
                                   {
                                       "sExtends": "csv",
                                       "sFileName": "DescargaCSVTs",
                                       "bFooter": false
                                   },
                            {
                                "sExtends": "xls",
                                "sFileName": "DescargaExcelTs.xls",
                                "bFooter": false
                            },
                            {
                                "sExtends": "pdf",
                                "sTitle": "Reporte PDF Full",
                                "sPdfMessage": "Exportado desde Timesheets",
                                "sPdfSize": "letter",
                                "sPdfOrientation": "landscape",// 
                                "bFooter": false, // se quita el footer del pdf.
                            },
                             {
                                 "sExtends": "print",
                                 "sMessage": "Generado por Timesheets <i>(Presione ESC para salir)</i>",
                                 "bShowAll": false, // solo muestra lo que contiene la pagina, no todos los datos
                                 "bFooter": false,
                              
                             }
        ],
                "sSwfPath": "/js/plugin/datatables/swf/copy_csv_xls_pdf.swf"
            }, 
	    //dom: 't<"clear">lfrtip',
            "preDrawCallback": function () {
                // Initialize the responsive datatables helper once.
                if (!responsiveHelper_datatable_col_reorder) {
                    responsiveHelper_datatable_col_reorder = new ResponsiveDatatablesHelper($('#' + grilla), breakpointDefinition);
                }
            },
            "rowCallback": function (nRow) {
                responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
            },
            "drawCallback": function (oSettings) {
                responsiveHelper_datatable_col_reorder.respond();
            },
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': ['nosort']
            }],

            //combo en el final de la tabla para filtrar
            initComplete: function () {
                //this.api().columns().every(function () {
                this.api().columns('.visible').every(function () {
                    var column = this;
                    var select = $('<select class="btn btn-primary dropdown-toggle" data-toggle="dropdown" ><option value="" >Todos <li><span class="glyphicon glyphicon-ok"> </span></li></option></select>')
                        .appendTo($(column.footer()).empty())
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });

                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                });
            }
            // fin combo 



        });

        // Apply the filter
        $('#' + grilla + ' thead th input[type=text]').on('keyup change', function () {

            otable
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });

        /* END BASIC */


    };

    // load related plugins

    pagefunction();

    return otable;
}






function InicializaGrillaFiltroAjax(grilla, url, parametros, dataColumns, callBack) {
    var otable;
    callBack = typeof callBack !== 'undefined' ? callBack : function (ajax) { return ajax.data };
    function pagefunction() {
        /* BASIC ;*/
        var responsiveHelper_datatable_fixed_column = undefined;
        var responsiveHelper_datatable_col_reorder = undefined;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };


        otable = $('#' + grilla).DataTable({
            "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'lf><'table-responsive hidden-xs'C> Tr>" +
        "t" +
        "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
            "autoWidth": true,
            "ajax": {
                "url": url,
                "type": "POST",
                "data": parametros,
                "dataSrc": callBack
            },
            
            columns: dataColumns,
            oLanguage: idioma,
            "oTableTools": {
                "aButtons": [
                        "copy",
                       
                            {
                                "sExtends": "csv",
                                "sFileName": "DescargaCSVTs.csv",
                                "bFooter": false
                            },
                        "xls",
                           {
                               "sExtends": "pdf",
                               "sTitle": "Reporte Semanal",
                               "sPdfMessage": "Exportado desde TimeSheets",
                               "sPdfSize": "letter",
                               "sPdfOrientation": "landscape",
                               "bFooter": false, // se quita el footer del pdf.
                           },
                           {
                               "sExtends": "print",
                               "sMessage": "Generado por Timesheets <i>(Presione ESC para salir)</i>",
                               "bShowAll": false, // solo muestra lo que contiene la pagina, no todos los datos
                           }
                ],

                "sSwfPath": "/js/plugin/datatables/swf/copy_csv_xls_pdf.swf"
            },
            //dom: 't<"clear">lfrtip',
            "preDrawCallback": function () {
                // Initialize the responsive datatables helper once.
                if (!responsiveHelper_datatable_col_reorder) {
                    responsiveHelper_datatable_col_reorder = new ResponsiveDatatablesHelper($('#' + grilla), breakpointDefinition);
                }
            },
            "rowCallback": function (nRow) {
                responsiveHelper_datatable_col_reorder.createExpandIcon(nRow);
            },
            "drawCallback": function (oSettings) {
                responsiveHelper_datatable_col_reorder.respond();
            },
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': ['nosort']
            }],

            //combo en el final de la tabla para filtrar
            initComplete: function () {
                //this.api().columns().every(function () {
                this.api().columns('.visible').every(function () {
                    var column = this;
                    var select = $('<select class="btn btn-primary dropdown-toggle" data-toggle="dropdown" ><option value="" >Todos <li><span class="glyphicon glyphicon-ok"> </span></li></option></select>')
                        .appendTo($(column.footer()).empty())
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });

                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                });
            }
            // fin combo 



        });

        // Apply the filter
        $('#' + grilla + ' thead th input[type=text]').on('keyup change', function () {

            otable
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();

        });

        /* END BASIC */


    };

    // load related plugins

    pagefunction();

    return otable;
}

function morrisChartBar(element, data, ykeys, labels, barColors) {
    loadScript("js/plugin/raphael/raphael-min.js", function () {
        loadScript("js/plugin/morris-chart/morris-0.4.1.min.js", function () {
            $('#'+element).html('<div id="bar-example" class="" style="position: relative; min-height:600px;"></div>');
            Morris.Bar({
                element: 'bar-example',
                data: data,
                xkey: 'y',
                ykeys: ykeys,
                labels: labels,
                barRatio: 0.4,
                xLabelAngle: 65,
                xLabelMargin: 2,
                barColors: barColors,
                gridTextSize: 10,
            });
            $('#bar-example>svg').css('height', '800px');
        });
    });
    
}

function activaModal() {
    $('#remoteModal').on('show.bs.modal', function () {
        $('.modal .modal-body').css('overflow-y', 'auto');
        $('.modal .modal-body').css('height', $(window).height() * 0.7);
        $('.modal .modal-body').css('max-height', $(window).height() * 0.7);
    });
}

function setFormulario(form, rules, messages, contenedor) {
    if (contenedor == undefined || contenedor == null) {
        contenedor = "#remoteModal .modal-content";
    }
    var pagemodal = function () {
        var $orderForm = $("#" + form).validate({
            rules: rules,
            messages: messages,
            // Do not change code below
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        });

        $("button[action=save]").click(function () {
            //console.log("click save", document.getElementById(form).action);
            $("button[action=save]").prop('disabled', true);
            if ($('#' + form).valid()) {
                var myform = $('#' + form);
                var disabled = myform.find(':input:disabled').removeAttr('disabled');
                var serialized = myform.serialize();
                disabled.attr('disabled', 'disabled');
                $.ajax({
                    url: document.getElementById(form).action,
                    type: document.getElementById(form).method,
                    data: serialized,
                    success: function (result) { 
                        if (result.success) {
                            //console.log("form", "retorno exito");
                            mensajeExito("Formulario", "Registro procesado con exito");                             
                        } else {
                            if (result.error) {
                                //console.log("form", "retorno error");
                                mensajeError("Formulario", "Registro con errores<br>" + result.mensaje);
                            } else {
                                //console.log("form", "retorno html", contenedor, result);
                                $(contenedor).html(result);
                            }
                        }
                    },
                    complete: function(){
                        $("button[action=save]").prop('disabled', false);
                    }
                });
            } else {
                console.log("error formulario");
                $("button[action=save]").prop('disabled', false);
            }
        });
        $("._horas.msk").mask("09:09", { placeholder: "hh:mm" });
        $("._fecha.msk").mask("99-99-9999", { placeholder: "dd-mm-aaaa" });
        
    };
    
    loadScript("js/plugin/jquery-validate/jquery.validate.min.js", function () {
        loadScript("js/plugin/masked-input/jquery.mask.js", function () {
            loadScript("js/plugin/jquery-form/jquery-form.min.js", pagemodal);
        });
    });
    //proarray[0] = loadScriptPromise("js/plugin/jquery-validate/jquery.validate.min.js");
    //proarray[1] = loadScriptPromise("js/plugin/masked-input/jquery.maskedinput.min.js");
    //proarray[2] = loadScriptPromise("js/plugin/jquery-form/jquery-form.min.js");
    //Promise.all(proarray).then(function () { pagemodal });
    
}


function confirmaElimina(obj, url) {
    var msg = $(obj).attr("msg-confirma");
    if(msg == null || msg == ""){
        msg = "cambiar vigencia el registro";
    }
    $.SmartMessageBox({
        title: "Confirmación",
        content: "¿ Esta seguro de "+msg+" ?",
        buttons: '[No][Si]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Si") {
            $.ajax({
                url: url,
                type: 'post',
                success: function (result) {
                    if (result.success) {
                        mensajeExito("Confirmación", "Registro procesado con exito");                        
                    } else {
                        if (result.error) {
                            mensajeError("Confirmación", "Proceso cancelado...<br />" + result.mensaje); 
                        }
                    }
                }
            });
        }
    });
}

function confirmaAprobacion(url, obj2) {
    //var msg = $(obj).attr("msg-confirma");
    //if (msg == null || msg == "") {
    //    msg = "cambiar vigencia el registro";
    //}
    msg = "aprobar " + obj2.ids_string.length +" registros";
    $.SmartMessageBox({
        title: "Confirmación",
        content: "¿ Esta seguro de " + msg + " ?",
        buttons: '[No][Si]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Si") {
            $.ajax({
                url: url,
                type: 'post',
                data: obj2,
                success: function (result) {
                    if (result.success) {
                        mensajeExito("Confirmación", "Registro procesado con exito");
                    } else {
                        if (result.error) {
                            mensajeError("Confirmación", "Proceso cancelado...<br />" + result.mensaje);
                        }
                    }
                }
            });
        }
    });
}

function mensajeExito(titulo, mensaje) {
    $(".modal").modal("hide");
    $.smallBox({
        title: titulo,
        content: "<i class='fa fa-clock-o'></i> <i>" + mensaje + "...</i>",
        color: "#659265",
        iconSmall: "fa fa-check fa-2x fadeInRight animated",
        timeout: 4000
    });
    setTimeout(function () { checkURL(); }, 500);
}

function mensajeError(titulo, mensaje) {
    $.smallBox({
        title: titulo,
        content: "<i class='fa fa-clock-o'></i> <i>" + mensaje + "...</i>",
        color: "#C46A69",
        iconSmall: "fa fa-times fa-2x fadeInRight animated",
        timeout: 4000
    });
}

function confirmaCierre(obj, url) {
    var msg = $(obj).attr("msg-confirma");
    if (msg == null || msg == "") {
        msg = "cerrar sesión";
    }
    $.SmartMessageBox({
        title: "<i class='fa fa-sign-out txt-color-orangeDark'></i> Confirmación",
        content: "¿ Esta seguro de " + msg + " ?",
        buttons: '[No][Si]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Si") {
            location.href = url; 
        }
    });
}


function GrillaConsulta(grilla) {

    function pagefunction() {
        /* BASIC ;*/
        var responsiveHelper_dt_basic = undefined;

        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        $('#' + grilla).dataTable({
            "sDom":  "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",
            "oLanguage": idioma,
            "autoWidth": true,"bSort" : false,
            "preDrawCallback": function () {
                // Initialize the responsive datatables helper once.
                if (!responsiveHelper_dt_basic) {
                    responsiveHelper_dt_basic = new ResponsiveDatatablesHelper($('#' + grilla), breakpointDefinition);
                }
            },
            "rowCallback": function (nRow) {
                responsiveHelper_dt_basic.createExpandIcon(nRow);
            },
            "drawCallback": function (oSettings) {
                responsiveHelper_dt_basic.respond();
            }
        });

        /* END BASIC */


    };

    // load related plugins

    pagefunction();
}





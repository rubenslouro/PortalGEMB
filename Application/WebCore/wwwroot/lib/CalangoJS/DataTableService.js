export default class DataTableService {
    constructor() {
        
    }

    async loadAsync() {
        this.converteTabelas();
    }

    converteTabelas() {
        try {
            $("[data-table=true]").each( (i, obj)=> {

                let tab = $(obj);

                let totalPage = tab.attr("data-total-page");

                let ordenarColunas = tab.attr("data-ordenar-colunas");

                let filtro = tab.attr("data-filtro");

                let filtroTotal = tab.attr("data-filtro-total");

                let pagina = tab.attr("data-pagina");

                let filtroTotalAjax = tab.attr("data-filtro-total-ajax");

                let clearOnLoad = tab.attr("data-clear-onload");

                let selection = tab.attr("data-selection");

                let columnSorting = tab.attr("data-column-sorting");
                let columnSortingType = tab.attr("data-column-sorting-type");

                if (totalPage === null || totalPage === undefined) {
                    totalPage = 10;
                } else {
                    totalPage = parseInt(totalPage);
                }

                if (ordenarColunas !== null && ordenarColunas !== undefined) {
                    if (ordenarColunas === "false") {
                        ordenarColunas = false;
                    } else {
                        ordenarColunas = true;
                    }
                } else {
                    ordenarColunas = true;
                }

                if (filtro !== null && filtro !== undefined) {

                    if (filtro === "false") {
                        filtro = false;
                    } else {
                        filtro = true;
                    }
                } else {
                    filtro = true;
                }

                if (filtroTotal !== null && filtroTotal !== undefined) {
                    if (filtroTotal === "false") {
                        filtroTotal = false;
                    } else {
                        filtroTotal = true;
                    }
                } else {
                    filtroTotal = false;
                }

                if (filtroTotalAjax === null || filtroTotalAjax === undefined) {
                    filtroTotalAjax = "";
                }


                if (pagina !== null && pagina !== undefined) {
                    if (pagina === "false") {
                        pagina = false;
                    } else {
                        pagina = true;
                    }
                } else {
                    pagina = true;
                }

                let arr = [];
                //classe de coluna
                for (let ii = 0; ii < 255; ii++) {

                    let colClass = tab.attr("data-coluna-class-" + ii);
                    if (colClass !== undefined) {
                        arr.push({ targets: ii, className: colClass });
                    }

                    let colWidth = tab.attr("data-coluna-width-" + ii);
                    if (colWidth !== undefined) {
                        arr.push({ targets: ii, "width": colWidth });
                    }

                    let colPrioridade = tab.attr("data-coluna-prioridade-" + ii);
                    if (colPrioridade !== undefined) {
                        arr.push({ targets: ii, "responsivePriority": colPrioridade });
                    }

                    let colHide = tab.attr("data-coluna-hide-" + ii);
                    if (colHide !== undefined) {
                        arr.push({ targets: ii, "visible": false });
                    }
                }
                if (filtroTotal === true) {

                    if ($(tab).find('tfoot tr td').length === 0) {
                        alertMsgErro("para utlizar filtro total é necessário adicionar tfoot, tr e td");
                        return;
                    }
                    $(tab).find('tfoot tr td').each(function (colItem) {

                        //pega o id do td do tr
                        let idColFooter = "";
                        if (tab.attr("data-col-footer-filter-id-" + colItem) !== undefined) {
                            idColFooter = tab.attr("data-col-footer-filter-id-" + colItem);
                        }

                        if (tab.attr("data-col-footer-no-filter-" + colItem) === undefined) {

                            if (filtroTotalAjax !== "") {
                                $(this).html('<div class="input-group"><input type="text" id="' +
                                    idColFooter +
                                    '" class="form-control" placeholder="Busca" /><div class="input-group-btn"><button class="btn btn-primary" onclick="' +
                                    filtroTotalAjax +
                                    '" type="button"><span class="glyphicon glyphicon-search"></span></button></div></div>');
                                $("#" + idColFooter).keyup(function (e) {
                                    if (e.which === 13) {
                                        eval(filtroTotalAjax);
                                    }
                                });
                            } else {
                                $(this).html('<input type="text" class="form-control" placeholder="Busca" style="width:100%;" />');
                            }

                        } else {

                            $(this).html('<input type="text" class="form-control" placeholder="Desativado"  disabled />');

                        }

                    });
                }

                let domJsDtTable = '<"toolbar">frtip';

                if (filtro === false) {
                    domJsDtTable = '<"toolbar">rtip';
                }

                let dt = tab.DataTable({
                    responsive: true,
                    select: true,
                    "dom": domJsDtTable,
                    "bPaginate": pagina,
                    columnDefs: arr,
                    pageLength: totalPage,
                    searching: true,
                    "bSort": ordenarColunas,
                    "language": {
                        "sEmptyTable": "Nenhum registro encontrado",
                        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                        "sInfoPostFix": "",
                        "sInfoThousands": ".",
                        "sLengthMenu": "_MENU_ resultados por página",
                        "sLoadingRecords": "Carregando...",
                        "sProcessing": "Processando...",
                        "sZeroRecords": "Nenhum registro encontrado",
                        "sSearch": "Pesquisar",
                        "oPaginate": {
                            "sNext": "Próximo",
                            "sPrevious": "Anterior",
                            "sFirst": "Primeiro",
                            "sLast": "Último"
                        },
                        "oAria": {
                            "sSortAscending": ": Ordenar colunas de forma ascendente",
                            "sSortDescending": ": Ordenar colunas de forma descendente"
                        }
                    }
                });
                dt.on('draw', function () {
                    dt.columns.adjust().responsive.recalc();
                });

                if (filtroTotal === true && filtroTotalAjax === "") {

                    dt.columns().every(function () {
                        let that = this;
                        $("input", this.footer()).on("keyup change clear",
                            function () {
                                if (that.search() !== this.value) {
                                    that.search(this.value).draw();
                                }
                            }
                        );
                    });

                }

                if (clearOnLoad !== undefined) {
                    dt.rows().remove().draw();
                }


                if (selection !== null) {
                    if (tab.attr("id") !== undefined) {

                        $('#' + tab.attr("id") + ' tbody').on('click', 'tr',
                            function () {
                                $(this).toggleClass('selected');
                                $(this).toggleClass('linha-selecao-table');
                            });
                    }
                }
                if (columnSorting !== undefined && $.isNumeric(columnSorting)) {
                    if (columnSortingType !== "asc" && columnSortingType !== "desc") {
                        columnSortingType = "asc";
                    }

                    dt.order([[columnSorting, columnSortingType]]).draw();
                }

            });

            dt.columns.adjust().responsive.recalc().draw();

        } catch (e) {
            //
        }
    }
}
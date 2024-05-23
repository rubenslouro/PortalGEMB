import DataTableService from "./DataTableService.js";
import ServerOperationsService from "./ServerOperationsService.js";
import PopUpService from "./PopUpService.js";
import CpfCnpjService from "./CpfCnpjService.js";

//#region Métodos privados

const serverOperationsService = new ServerOperationsService();

//#endregion Métodos privados
export default class FormsService {
    constructor() {
        this.DataTableService = new DataTableService();
        this.Map = Array.prototype.map;
        this.PopUpService = new PopUpService();
        this.CpfCnpjService = new CpfCnpjService();
    }

    async loadAsync() {
        await this.PopUpService.loadAsync();
    }

    async initAsync() {
        await this.PopUpService.initAsync()
        this.efeitoCarregamentoPagina();
        this.showLoadingOnClickLink();
        await this.DataTableService.loadAsync();
        this.formSending();
        //this.animaCamposRequired();
        this.carregaMultiModal();
        this.startObjectsHide();
        this.bloquearBotaoVoltar();
        this.converteFormGroupAlert();
        this.configuracoesPadraoBootstrapModal();
        this.loadMentisMenu();
        this.loadMenuOperations();
        this.loadHideMenuOnClick();
        this.inicializaCleanValidatorJqueryValidate();

        //#region Antigo InputUtilityService
        this.converteInputsPortaTPC()
        this.converteMasksCnpj();
        this.convertInputsSoLetras();
        this.convertInputsSoNumeros();
        this.converteBalaoHelp();
        this.loadCustomValidatorsForJqueryValidator();
        this.tooltipClick();
        this.inputNoAutoComplete();
        this.nextFieldafterSelect();
        this.nextFieldMaxLenght();
        this.enterProximoCampo();
        this.setAllMaxLenght();
        this.converteInputsCalendariosComHora();
        this.converteInputsCalendarios();
        this.convertInputSoNumerosDecimal();
        this.convertInputsTextoPortugues();
        this.convertInputsSoLetrasNumeros();
        this.converteInputsMaskDataHora();
        this.converteInputsMaskCalendario();
        this.converteInputsMaskCep();
        this.converteInputsMaskCpf();
        this.converteInputsValorReal();
        this.converteInputsQuantidade();
        this.converteInputMaskTelefone();
        this.converterInputsVisualizadorSenha();
        await this.convertTextBoxBuscaAutomaticaAsync();
        this.converteTextBoxBuscaAutomaticaWithGroupBox();
        //#endregion Antigo InputUtilityService

        //#region Antigo SelectInputUtilityService
        this.iniciarSelect();
        //#endregion Antigo SelectInputUtilityService
    }

    //#region Antigo SelectInputUtilityService

    iniciarSelect() {

        $('select').each(function (i, obj) {

            var item = $(obj);

            if (item.attr("default-select") === "false") {
                //caso haja tratamento 
            } else {

                item.selectpicker({
                    liveSearch: 'true',
                    liveSearchNormalize: 'true',
                    liveSearchPlaceholder: 'Pesquisar',
                    showIcon: 'false',
                    showTick: 'true',
                    container: 'body',
                    noneResultsText: 'Sem resultados para pesquisa',
                    noneSelectedText: ""
                });
            }
        });
    }

    async carregaSelectParameterAsync(selectParameters) {
        try {
            var obj = $("#" + selectParameters.SelectId);
            this.showLoadFor(obj);
            var data = JSON.parse("{ " + selectParameters.ParametersJson + " }");

            var result = await serverOperationsService.callServerMethodAsync(selectParameters.Url, data);

            obj.find('option').remove();

            if (selectParameters.UsaPrimeiroVazio) {
                var textToEmptyObj = selectParameters.TextToEmpty == undefined ? "" : selectParameters.TextToEmpty;
                obj.append('<option value="">' + textToEmptyObj + '</option>');
            }

            this.Map.call(result, (item) => {
                if (item != null) {
                    obj.append('<option value=' + eval("item." + selectParameters.Value) + '>' + eval("item." + selectParameters.Text) + '</option>');
                }
            });
            this.disposeLoadFor(obj);

        } catch (e) {
            //
        }
    }

    async carregaSelectAsync(selectParameters) {
        try {

            if (selectParameters.UsaPrimeiroVazio == null) {
                selectParameters.UsaPrimeiroVazio = false;
            }

            var obj = $("#" + selectParameters.SelectId);

            this.showLoadFor(obj, "22px");

            obj.prop("disabled", true);

            obj.selectpicker('refresh');
            var dados = await serverOperationsService.callServerMethodAsync(selectParameters.Url, {});
            this.Map.call(dados, (item) => {
                if (item != null) {
                    obj.append('<option value=' + eval("item." + selectParameters.Value) + '>' + eval("item." + selectParameters.Text) + '</option>');
                }
            });
            this.disposeLoadFor(obj);
            obj.prop("disabled", false);
            obj.selectpicker('refresh');


        } catch (e) {
            //
        }
    }

    //#endregion Antigo SelectInputUtilityService

    //#region Antigo InputUtilityService

    async convertTextBoxBuscaAutomaticaAsync() {

        $("[autopesquisa]").each((i, obj) => {
            let txt = $(obj);
            txt.before(' <div class="dropdown pull-right escondido"></div>');
            txt.parent().find(".dropdown").append('<b class="control-label" >0</b> <i class="fa fa-caret-down control-label" data-toggle="dropdown"></i><ul class="dropdown-menu"></ul>');
            let bShowCount = txt.parent().find(".dropdown").find("b");
            let ulResultado = txt.parent().find(".dropdown").find(".dropdown-menu");

            bShowCount.hide();
            bShowCount.parent().hide();

            let codigoRefObj = txt.attr("codigoRef");

            let url = txt.attr("autopesquisa");

            let processa = async () => {
                try {
                    let dados = await serverOperationsService.callServerMethodAsync(url, { descricao: txt.val(), codigoRef: codigoRefObj });
                    ulResultado.html("");
                    bShowCount.text(dados.length);
                    if (dados.length === 0) {
                        bShowCount.hide();
                        bShowCount.parent().hide();
                    } else {
                        bShowCount.show();
                        bShowCount.parent().show();
                    }
                    this.Map.call(dados, (item) => {

                        let htmlTag = "<li><a href='" +
                            item.Url + "'><b>" +
                            item.Codigo + "</b> - " +
                            item.Descricao + "</a></li>";

                        ulResultado.append(htmlTag);
                        txt.valid();
                        this.converteFormGroupAlert();
                    });
                } catch (ex) {
                    this.PopUpService.alertMsgErro(ex.MessageError);
                }
            }

            txt.bind('input', async () => {
                await processa();
            });

            txt.change(async () => {
                await processa();
            });

            txt.focusout(async () => {
                await processa();
            });
        });


    }

    converteTextBoxBuscaAutomaticaWithGroupBox() {

        $("[autopesquisa-group-button]").each(async (i, obj) => {
            let txt = $(obj);

            let formGroup = txt.parent().parent();
            formGroup.removeClass("has-warning");
            if (txt.parent().parent().find("[data-dropdown-autopesquisa-especial]").length === 0) {
                txt.parent().before(' <div class="dropdown pull-right escondido" data-dropdown-autopesquisa-especial></div>');
                txt.parent().parent().find(".dropdown").append('<b class="control-label" >0</b> <i class="fa fa-caret-down control-label" data-toggle="dropdown"></i><ul class="dropdown-menu"></ul>');
            }

            let bShowCount = txt.parent().parent().find(".dropdown").find("b");
            let ulResultado = txt.parent().parent().find(".dropdown").find(".dropdown-menu");
            bShowCount.hide();
            bShowCount.parent().hide();
            let codigoRefObj = txt.attr("codigoRef");
            let url = txt.attr("autopesquisa-group-button");

            let processaInterno = async () => {

                let dados = await serverOperationsService.callServerMethodAsync(url, { descricao: txt.val(), codigoRef: codigoRefObj });

                ulResultado.html("");
                bShowCount.text(dados.length);
                if (dados.length === 0) {
                    bShowCount.hide();
                    bShowCount.parent().hide();
                    formGroup.removeClass("has-warning");
                } else {
                    bShowCount.show();
                    bShowCount.parent().show();
                    formGroup.addClass("has-warning");
                }

                this.Map.call(dados, (item) => {
                    let htmlTag = "<li><a href='" + item.Url + "'><b>" + item.Codigo + "</b> - " + item.Descricao + "</a></li>";
                    ulResultado.append(htmlTag);
                    if (txt.get(0).hasAttribute("no-jq-validate") === false) {
                        txt.valid();
                        this.converteFormGroupAlert();
                    }
                });
            }

            txt.bind('input',
                async () => {
                    await processaInterno();
                });

            txt.change(async () => {
                await processaInterno();
            });

            txt.focusout(async () => {
                await processaInterno();
            });
        });
    }

    async carregaPesquisaTextBoxAsync(txtPesquisa, url, bShowCount, ulResultado, codigoRefObj) {
        let txt = $("#" + txtPesquisa);
        $("#" + bShowCount).hide();
        $("#" + bShowCount).parent().hide();

        let acao = async () => {
            let dados = await serverOperationsService.callServerMethodAsync(url, { descricao: txt.val(), codigoRef: codigoRefObj });
            $("#" + ulResultado).html("");
            $("#" + bShowCount).text(dados.length);
            if (dados.length === 0) {
                $("#" + bShowCount).hide();
                $("#" + bShowCount).parent().hide();
            } else {
                $("#" + bShowCount).show();
                $("#" + bShowCount).parent().show();
            }
            this.Map.call(dados, (item) => {
                let htmlTag = "<li><a href='" + item.Url + "'><b>" + item.Codigo + "</b> - " + item.Descricao + "</a></li>";
                $("#" + ulResultado).append(htmlTag);
                txt.valid();
                this.converteFormGroupAlert();
            });
        };

        txt.bind('input', await acao());
        txt.change(await acao());
        txt.focusout(await acao());
    }

    converterInputsVisualizadorSenha() {

        $("[type='password']").each((i, obj) => {
            $(obj).before("<div class='input-group'></div>");
            $(obj).appendTo($(obj).parent().find(".input-group"));
            $(obj).parent().prepend("<span class='input-group-addon' title='Clique aqui para exibir a senha.' ><i class='fa fa-lock '></i></span>");
            $(obj).parent().find(".input-group-addon").click(() => {
                if ($(obj).attr("type") === "text") {
                    $(obj).attr("type", "password");
                    $(obj).parent().find("span").find("i").removeClass("fa-unlock");
                    $(obj).parent().find("span").find("i").addClass("fa-lock");
                } else {
                    $(obj).parent().find("span").find("i").removeClass("fa-lock");
                    $(obj).parent().find("span").find("i").addClass("fa-unlock");
                    $(obj).attr("type", "text");
                }
            });
        });
    }

    converteMasksCnpj() {
        try {
            $("[mask=cnpj]").mask("00.000.000/0000-00");
        } catch (e) {
            //
        }
    }

    converteMaskCnpj(jqueryObjectInputText) {
        try {
            jqueryObjectInputText.mask("00.000.000/0000-00");
        } catch (e) {
            //
        }
    }

    converteInputsPortaTPC() {
        try {
            $("[mask=PortaTCP]").mask("00000");
        } catch (e) {
            //
        }
    }

    inputSoLetrasNumeros(inputText) {
        $("#" + inputText).on("input", () => {
            let regexp = /[^a-zA-Z0-9á-úÁ-Ú ]/g;
            if (this.value.match(regexp)) {
                $(this).val(this.value.replace(regexp, ''));
            }
        });
    }

    convertInputsSoLetras() {
        $("[data-apenasletras]").each((i, obj) => {
            let txt = $(obj);
            txt.on("input", () => {
                let regexp = /[^a-zA-Zá-úÁ-Ú ]/g;
                if (this.value.match(regexp)) {
                    $(this).val(this.value.replace(regexp, ''));
                }
            });
        });
    }

    convertInputsSoNumeros() {
        $("[data-apenasnumeros]").each((i, obj) => {
            let txt = $(obj);
            txt.addClass("text-right");
            txt.on("input", () => {
                let regexp = /[^0-9]/g;
                if (this.value.match(regexp)) {
                    $(this).val(this.value.replace(regexp, ''));
                }
            });
        });
    }

    converteBalaoHelp() {
        $("[data-helper]").each((i, obj) => {
            let item = $(obj);
            let msg = item.attr("helper");
            item.after("<div class='input-group'></div>");
            item.appendTo($(obj).parent().find(".input-group"));
            item.parent().prepend('<span class="input-group-addon" data-toggle="popover" data-container="body" data-placement="bottom" data-content="' + msg + '"><i class="fa fa-question"></i></span>');


        });
    }

    loadCustomValidatorsForJqueryValidator() {
        //apenas dados mascarados da interface
        jQuery.validator.addMethod("validarCpfCnpj", (value) => {
            if (value === "") {
                return true;
            }
            if (value.length > 14) {
                if (value.substring(10, 11) !== "/") {
                    return false;
                }
                return this.CpfCnpjService.validarCnpj(value);
            } else {
                if (value.substring(10, 11) === "/") {
                    return false;
                }
                return this.CpfCnpjService.validarCpf(value);
            }
        }, "CPF/CNPJ inválido.");
    }

    converteInputsMaskCpf(valida) {
        try {

            if (valida === undefined || valida === null) {
                valida = true;
            }

            $("[mask=cpf]").mask("000.000.000-00");

            $("[mask=cpf]").rules('add', {
                validarCpfCnpj: valida
            });



        } catch (e) {
            //
        }
    }

    tooltipClick() {
        try {
            $("[data-toggle=popover]").popover();
            $("[data-toggle=popover]").attr("title", "Clique aqui para saber mais informações sobre o campo.");

        } catch (e) {
            //
        }
    }

    inputNoAutoComplete() {
        $("form").attr('autocomplete', 'false');
        $("input").attr('autocomplete', 'off');

    }

    nextFieldafterSelect() {
        try {
            $("select").each((i, obj) => {
                $(obj).change(() => {
                    let inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                });
            });
        } catch (e) {
            //
        }
    }

    nextFieldMaxLenght() {
        try {
            $("input").each((i, obj) => {
                $(obj).bind("input", () => {
                    let val1 = parseInt($(obj).val().length);
                    let val2 = parseInt($(obj).attr("maxlength"));
                    if (val1 === val2) {
                        let inputs = $(this).closest('form').find(':input:visible');
                        inputs.eq(inputs.index(this) + 1).focus();
                    }
                });
            });
        } catch (e) {
            //
        }
    }

    enterProximoCampo() {
        $('input').keydown((e) => {
            let key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key === 13) {
                e.preventDefault();
                let inputs = $(this).closest('form').find(':input:visible');
                inputs.eq(inputs.index(this) + 1).focus();
            }
        });
    }

    setAllMaxLenght() {
        try {
            $("[data-val-length-max]").each((i, obj) => {
                $(obj).attr("maxlength", $(obj).attr("data-val-length-max"));
            }, nextFieldMaxLenght());

        } catch (e) {
            //
        }
    }

    converteInputsCalendariosComHora() {
        $(() => {
            $("[calendario-data-hora]").datepicker({
                showOn: "",
                dateFormat: 'dd/mm/yy',
                dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                monthNames: [
                    'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro',
                    'Novembro', 'Dezembro'
                ],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                nextText: 'Próximo',
                prevText: 'Anterior',
                onSelect: () => {
                    $(this).focus();

                },
                beforeShow: () => {
                    setTimeout(() => {
                        $('.ui-datepicker').css('z-index', 99999999999999);
                    },
                        0);
                }
            });

            $("[calendario=true]").datepicker({
                dateFormat: 'dd/mm/yy',
                dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                nextText: 'Próximo',
                prevText: 'Anterior',
                beforeShow: () => {
                    setTimeout(() => {
                        $('.ui-datepicker').css('z-index', 99999999999999);
                    },
                        0);
                }
            });

            $("[calendario=false]").each((i, obj) => {
                $(obj).parent().append("<div class='input-group'></div>");
                $(obj).appendTo($(obj).parent().find(".input-group"));
                $(obj).parent().prepend("<span class='input-group-addon' title='Clique aqui para exibir o calendário.' ><i class='fa fa-calendar'></i></span>");
                $(obj).parent().find(".input-group-addon").click(() => {
                    $(obj).datepicker("show");
                });
            });

            try {
                $("[calendario]").change(() => {
                    $("[calendario]").valid();
                });
            } catch (e) {
                //
            }

        });
    }

    converteInputsCalendarios() {
        $(() => {
            let arrDate = [];

            $("[calendario=false]").each((i, objCal) => {

                if ($(objCal).attr("array-blocked-date") !== undefined) {
                    arrDate = $(objCal).attr("array-blocked-date").split(",");
                } else {
                    arrDate = [];
                }

                $(objCal).datepicker({
                    beforeShowDay: (date) => {
                        let string = jQuery.datepicker.formatDate('dd/mm/yy', date);
                        return [arrDate.indexOf(string) === -1];
                    },
                    showOn: "",
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: [
                        'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro',
                        'Novembro', 'Dezembro'
                    ],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior',
                    onSelect: () => {
                        try {
                            $(this).trigger("input");
                            $(this).focus();
                        } catch (e) {

                        }



                    },
                    beforeShow: () => {
                        setTimeout(() => {
                            $('.ui-datepicker').css('z-index', 99999999999999);
                        },
                            0);
                    }
                });
            });

            $("[calendario=true]").each((i, objCal) => {

                if ($(objCal).attr("array-blocked-date") !== undefined) {
                    arrDate = $(objCal).attr("array-blocked-date").split(",");
                } else {
                    arrDate = [];
                }

                $(objCal).datepicker({
                    onSelect: () => {
                        $(this).change();
                    },
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior',
                    beforeShow: () => {
                        setTimeout(() => {
                            $('.ui-datepicker').css('z-index', 99999999999999);
                        },
                            0);
                    }
                });
            });

            $("[calendario]").each((i, obj) => {

                if ($(obj).attr("calendario-min-date") !== undefined) {
                    let minDate = $(obj).attr("calendario-min-date");
                    $(obj).datepicker("option", "minDate", minDate);
                }

                if ($(obj).attr("calendario-max-date") !== undefined) {
                    let maxDate = $(obj).attr("calendario-max-date");
                    $(obj).datepicker("option", "maxDate", maxDate);
                }
            });

            $("[calendario=false]").each((i, obj) => {
                $(obj).parent().append("<div class='input-group'></div>");
                $(obj).appendTo($(obj).parent().find(".input-group"));
                $(obj).parent()
                    .prepend(
                        "<span class='input-group-addon' title='Clique aqui para exibir o calendário.' ><i class='fa fa-calendar'></i></span>");
                $(obj).parent().find(".input-group-addon").click(() => {
                    $(obj).datepicker("show");
                    if ($(obj).attr("calendario-focus") !== undefined) {
                        $([document.documentElement, document.body]).animate({
                            scrollTop: $(obj).offset().top
                        },
                            1000);
                    }
                });
            });


            try {
                $("[calendario]").each((i, o) => {

                    $(o).change(() => {
                        try {
                            $(o).valid();
                        } catch (e) {
                            //
                        }
                    });

                });
            } catch (e) {
                //
            }
            //try {
            //    $("[calendario]").change(() =>  {
            //        $("[calendario]").valid();
            //    });
            //} catch (e) {
            //    //
            //}

        });
    }

    bloquearInputCopiarColarRecortar(inputText) {
        try {
            $("#" + inputText).bind("cut copy paste", (e) => {
                e.preventDefault();
            });
        } catch (e) {
            //
        }
    }

    convertInputSoNumerosDecimal() {
        $("[data-apenas-numeros-virgula]").each((i, obj) => {

            let txt = $(obj);
            txt.on("input", () => {
                let regexp = /[^0-9,]/g;
                if (this.value.match(regexp)) {
                    $(this).val(this.value.replace(regexp, ''));
                }
            });
        });
    }

    convertInputsTextoPortugues() {
        $("[data-portugues]").each((i, obj) => {
            let txt = $(obj);
            txt.on("input", () => {
                let regexp = /[^a-zA-Z0-9á-úÁ-Ú?|:\.|,|@|/|!|-|\n| ]/g;
                if (this.value.match(regexp)) {
                    $(this).val(this.value.replace(regexp, ''));
                }
            });
        });
        $("[data-portugues]").each((i, item) => {

            let htmlToInject =
                "<em class='micro-text positivo pull-right' style='margin-right:5px;'>*Apenas caracteres ligua portuguesa e números</em>";

            if ($(item).attr("type") === "hidden") {
                return;
            }
            if (!$(item).parent().hasClass("input-group")) {
                $(item).after(htmlToInject);
            } else {
                $($(item).parent()).after(htmlToInject);

            }
        });

    }

    convertInputsSoLetrasNumeros() {
        $("[data-apenas-letras-numeros]").each((i, obj) => {
            let txt = $(obj);
            txt.on("input", () => {
                let regexp = /[^a-zA-Z0-9á-úÁ-Ú ]/g;
                if (this.value.match(regexp)) {
                    $(this).val(this.value.replace(regexp, ''));
                }
            });
        });

    }

    converteInputsMaskDataHora() {
        try {
            $("[mask=data-hora]").mask("00/00/0000 00:00");

        } catch (e) {
            //
        }
    }



    textBoxMudancaTextoForData(textBox, callAction) {
        try {

            $("body").on("'input change paste keyup blur focusout'", textBox, callAction);

        } catch (e) {
            //
        }
    }

    textBoxMudancaTexto(textBox, callAction) {
        try {

            $("body").on("input", "#" + textBox, callAction);

        } catch (e) {
            //
        }
    }

    converteInputsMaskCalendario() {
        try {
            $("[mask=data]").mask("00/00/0000");

        } catch (e) {
            //
        }
    }

    converteInputsMaskCep() {
        try {
            $("[mask=cep]").each((i, obj) => {
                $(obj).mask("00000-000");
            });

        } catch (e) {
            //
        }
    }

    converteInputsValorReal() {
        try {
            $("[mask='valor']").each((i, obj) => {

                if ($(obj).attr("masked-yet") === undefined || $(obj).attr("masked-yet") === null) {

                    $(obj).mask('#.##0,00', { reverse: true });

                    $(obj).attr("masked-yet", "true");
                    $(obj).addClass("text-right");
                    $(obj).attr("inputmode", "numeric");

                }

            });
        } catch (e) {
            //
        }
    }

    converteInputsQuantidade() {
        try {
            $("[mask=qtd]").mask("0000");
        } catch (e) {
            //
        }
    }

    converteInputMaskTelefone(inputElement = null) {
        try {

            if (inputElement !== null) {

                let item = $("#" + inputElement);
                item.unmask();
                if (item.val().length > 10 || item.val() === "") {

                    item.mask("(00) 00000-0000");
                } else {
                    item.mask("(00) 0000-0000");
                }

                item.focusout((event) => {
                    let target = (event.currentTarget) ? event.currentTarget : event.srcElement;
                    let phone = target.value.replace(/\D/g, '');
                    let element = $(target);
                    element.unmask();
                    if (phone.length > 10) {
                        element.mask("(00) 00000-0000");
                    } else {
                        element.mask("(00) 0000-0000");
                    }
                });
                item.focusin((event) => {
                    let target = (event.currentTarget) ? event.currentTarget : event.srcElement;
                    let element = $(target);
                    element.unmask();
                    element.unmask();
                    element.mask("(00) 00000-0000");
                });

            } else {

                $("[mask=telefone]").each((index, obj) => {

                    let item = $(obj);

                    if (item.val().length > 10 || item.val() === "") {

                        item.mask("(00) 00000-0000");
                    } else {
                        item.mask("(00) 0000-0000");
                    }

                    item.focusout((event) => {
                        let target = (event.currentTarget) ? event.currentTarget : event.srcElement;
                        let phone = target.value.replace(/\D/g, '');
                        let element = $(target);
                        element.unmask();
                        if (phone.length > 10) {
                            element.mask("(00) 00000-0000");
                        } else {
                            element.mask("(00) 0000-0000");
                        }
                    });

                    item.focusin((event) => {
                        let target = (event.currentTarget) ? event.currentTarget : event.srcElement;
                        let element = $(target);
                        element.unmask();
                        element.unmask();
                        element.mask("(00) 00000-0000");
                    });
                });
            }

        } catch (e) {
            //
        }
    }
    //#endregion Antigo InputUtilityService

    //#region FormsService

    efeitoCarregamentoPagina() {
        $("#no-wrapper").hide();

        $("#wrapper").fadeIn(500, () => {
            $("[data-table=true]").each((i, obj) => {
                var tab = $(obj).DataTable();
                tab.columns.adjust().responsive.recalc().draw();
            });
        });
    }

    alternaDisplayElementoDetalhe(elementoNome, btn, classIconDown, classIconUp) {

        var elemento = $("#" + elementoNome);
        var iElement = $(btn).find("i");
        var bElement = $(btn);
        bElement.css("outline", "none");
        if (classIconDown === undefined) {
            iElement.removeClass("fa fa-angle-down");
        } else {
            iElement.removeClass(classIconDown);
        }

        if (classIconUp === undefined) {
            iElement.removeClass("fa fa-angle-up");
        } else {
            iElement.removeClass(classIconUp);
        }

        elemento.removeClass("hide");

        if (elemento.css("display") === "none") {
            elemento.slideDown(500);

            bElement.hide(250,
                function () {
                    bElement.show(250);
                    if (classIconUp === undefined) {
                        iElement.addClass("fa fa-angle-up");
                    } else {
                        iElement.addClass(classIconUp);
                    }
                });
        } else {
            elemento.slideUp(500);

            bElement.hide(250,
                function () {
                    bElement.show(250);
                    if (classIconDown === undefined) {
                        iElement.addClass("fa fa-angle-down");
                    } else {
                        iElement.addClass(classIconDown);
                    }

                });
        }

    }

    inicializaCleanValidatorJqueryValidate() {
        $.fn.clearValidation = function () {
            try {

                var v = $(this).validate();

                $('[name]', this).each(function () {
                    v.successList.push(this);
                    v.showErrors();
                });
                v.resetForm();
                v.reset();
                $(this).find("div").removeClass("has-success");
            } catch (ex) {
            }
        };
    }

    loadHideMenuOnClick() {
        $("#page-wrapper").click(() => {
            $(".navbar-collapse").collapse("hide");
        });
        $("boby").click(() => {
            if (e.target !== this) return;
            $(".navbar-collapse").collapse("hide");
        });
        $(".areacliente").click(() => {
            $(".navbar-collapse").collapse("hide");
        });
    }

    loadMenuOperations() {
        /*odeio ver estas functions aqui dentro*/
        $(() => {
            $(window).bind("load resize", function () {
                var topOffset = 50;
                var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
                if (width < 768) {
                    $('div.navbar-collapse').addClass('collapse');
                    topOffset = 100; // 2-row-menu
                } else {
                    $('div.navbar-collapse').removeClass('collapse');
                }

                var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
                height = height - topOffset;
                if (height < 1) height = 1;
                if (height > topOffset) {
                    // $("#page-wrapper").css("min-height", (height) + "px");
                }
            });

            var url = window.location;
            var element = $('ul.nav a').filter(function () {//isso tem que ver pq é a selection automatica de menu;
                return this.href == url;
            }).addClass('active').parent().parent().addClass('in').parent();
            var element = $('ul.nav a').filter(function () {
                return this.href === url;
            }).addClass('active').parent();
            var s = 1;
            while (s === 1) {
                if (element.is('li')) {
                    element = element.parent().addClass('in').parent();
                } else {
                    break;
                }
            }
        });
    }

    loadMentisMenu() {
        $('#side-menu').metisMenu();
    }

    piscaTexto(obj, fontSz = "14px", fontMax = "25px") {
        obj.animate({ "font-size": fontMax },
            200,
            () => {
                obj.animate({ "font-size": fontSz },
                    200,
                    () => {
                        obj.animate({ "font-size": fontMax }, 200, () => {
                            obj.animate({ "font-size": fontSz }, 200);
                        });
                    });
            });
    }

    converteFormGroupAlert() {
        $(() => {

            var alertaCampo = $(".alerta-campo");

            alertaCampo.each((i, element) => {

                var objAlert = $(element);

                objAlert.parent().find('input[type="text"]').each((i, obj) => {
                    var textBox = $(obj);
                    textBox.focusout(() => {
                        try {
                            textBox.validate().valid();
                        } catch (e) {
                            this.PopUpService.alertMsg(e.message);
                        }

                    });
                });

                objAlert.bind("DOMSubtreeModified",
                    () => {
                        $(element).parent().removeClass("has-error");
                        $(element).parent().removeClass("has-success");
                        if ($(element).text() !== "") {
                            $(element).parent().addClass("has-error");
                        } else {
                            $(element).parent().addClass("has-success");
                        }

                    });

                $(element).parent().find(".dropdown").find("b").each((item, obj) => {
                    $(obj).bind("DOMSubtreeModified",
                        () => {

                            if ($(obj).text() !== "" && $(obj).text() !== "0") {
                                $(element).parent().addClass("has-warning");
                            } else {
                                $(element).parent().removeClass("has-warning");
                            }
                        });
                });
            });

        });
    }

    configuracoesPadraoBootstrapModal() {
        $(document).on('show.bs.modal', '.modal', () => {
            var zIndex = 1040 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);
            setTimeout(() => {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);
        });
    }

    bloquearBotaoVoltar() {
        let disableBackButton = () => {

            window.history.forward();
        }
        disableBackButton();
        window.onload = disableBackButton;
        window.onpageshow = (evt) => { if (evt.persisted) disableBackButton() }
        window.onunload = () => { void (0) }
    }

    startObjectsHide() {
        try {
            $("[start-hide=True],[start-hide=true]").hide();
        } catch (e) {
            //
        }
    }

    carregaMultiModal() {
        $('body').on('hidden.bs.modal', () => {
            if ($('.modal.in').length > 0) {
                $('body').addClass('modal-open');
            }
        });
    }

    animaCamposRequired() {

        $("[data-val-required]").each((i, item) => {

            if ($(item).attr("type") !== "checkbox") {

                if ($(item).attr("data-obrigatorio-loaded") !== "true") {
                    $(item).attr("data-obrigatorio-loaded", "true");

                    var htmlToInject =
                        "<em class='micro-text obrigatorio pull-right' style='margin-right:5px;'>*Obrigatório</em>";


                    var idAtual = $(item).attr("id");

                    if ($("body").find("[data-upload-imagem-base64='" + idAtual + "']").length) {
                        htmlToInject =
                            "<br/><em class='micro-text obrigatorio pull-left' style='margin-right:5px;'>*Obrigatório</em>";
                    }

                    if ($(item).attr("type") === "hidden" && $(item).attr("data-show-obrigatorio-na-tora") !== "true") {
                        //
                    } else {
                        if (!$(item).parent().hasClass("input-group")) {
                            $(item).after(htmlToInject);
                        } else {
                            $($(item).parent()).after(htmlToInject);

                        }
                    }

                }
            }

        });

    }

    formSending() {
        $("form").each((i, formItem) => {
            let form = $(formItem);
            form.submit(() => {

                let showAutoProcessesingPopUp = true;
                if (form.attr("data-show-auto-processing-popup") === "false")
                    showAutoProcessesingPopUp = false;

                jQuery.validator.setDefaults({
                    debug: true,
                    success: "valid"
                });

                form.validate();
                if (form.valid()) {
                    $("button, a").addClass("disabled");
                    $("button, a").attr("disabled", "true");
                    $("#wrapper").fadeOut(1000);
                    if ($("#no-wrapper").length > 0) {
                        $("#no-wrapper").show();
                        $("nav").hide();
                    } else {
                        if (showAutoProcessesingPopUp === true)
                            this.PopUpService.showLoading();//Colocado isso aqui devido a haver páginas que não podem exibir o loading para evitar muito retrabalho. Exemplo a página de instalação;
                    }

                } else {
                    let selector = $("[validator=true]");
                    this.piscaMsg(selector);
                }
            });
        });

    }

    formSendingAction(actionMethod) {
        $("form").submit(() => {
            jQuery.validator.setDefaults({
                debug: true,
                success: "valid"
            });
            let form = $("form");
            form.validate();
            if (form.valid()) {
                actionMethod();
            }
        });
    }

    slideToObject(objJquery, timerGo) {
        if (timerGo === undefined) {
            timerGo = 2000;
        }
        $([document.documentElement, document.body]).animate({
            scrollTop: objJquery.offset().top
        }, timerGo);
    }

    piscaMsg(selector) {
        selector.fadeTo('100',
            0.0,
            () => {
                selector.fadeTo("slow",
                    1.0,
                    () => {
                        selector.fadeTo('slow',
                            0.0,
                            () => {
                                selector.fadeTo("slow",
                                    1.0,
                                    () => {
                                        selector.fadeTo("slow",
                                            0.0,
                                            () => {
                                                selector.fadeTo("slow", 1.0);
                                            });
                                    });
                            });
                    });
            });
    }

    showLoadingOnClickLink() {
        $("a").each((i, item) => {
            let obj = $(item);
            obj.click(() => {
                if (obj.attr("href") != null && obj.attr("href") != "" && !obj.attr("href").includes("#")) {

                    $("#no-wrapper").show();
                    $("#wrapper").hide();
                    $("nav").hide();
                }
            });
        });
    }

    //#endregion FormsService

}
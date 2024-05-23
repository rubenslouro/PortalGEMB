import BasePageController from "../../../lib/CalangoJS/BasePageController.js";
import LayoutViewService from "../../../lib/CalangoJS/LayoutViewService.js";

export default class ConfiguracoesPageController extends BasePageController {
    constructor() {
        super();
        //#region Controles tela
        this.Controls = {
            HiddenCodTipoUsuario: $("#hiddenCodTipoUsuario"),
            DisplayAvisoTipoEmProducao: $("#displayAvisoTipoEmProducao"),
            TableUsuariosVinculados: $("#tableUsuariosVinculados"),
            DataTableUsuariosVinculados: null,
            BtnForcaAjuste: $("[data-btnForcaAjuste]"),
            AreaChkAplicarRegraRetroativa: $("#areaChkAplicarRegraRetroativa"),
            ChkAplicarRegraRetroativa: $("#chkAplicarRegraRetroativa"),
            WindowPermissaoDiferenciadaUsuario: new LayoutViewService("Administrativo/TipoUsuario/WindowPermissaoDiferenciadaUsuario", {}),
            DisplayAvisoAjusteAutomatico: $("#displayAvisoAjusteAutomatico"),
            Permissoes: {
                TxtPesquisaPermissaoAtiva: $("#txtPesquisaPermissaoAtiva"),
                RegrasAtribuidasArea: $("#regrasAtribuidasArea"),
                TxtPesquisaPermissaoInativa: $("#txtPesquisaPermissaoInativa"),
                RegrasNaoAtribuidasArea: $("#regrasNaoAtribuidasArea"),
                BtnPesquisarPermissaoInativa: $("#btnPesquisarPermissaoInativa"),
                BtnLimparPermissaoInativa: $("#btnLimparPermissaoInativa"),
                BtnAdicionarTodasPermissoes: $("#btnAdicionarTodasPermissoes"),
                BtnPesquisarPermissaoAtiva: $("#btnPesquisarPermissaoAtiva"),
                BtnLimparPermissaoAtiva: $("#btnLimparPermissaoAtiva"),
                BtnRemoverTodasPermissoes: $("#btnRemoverTodasPermissoes")
            }
        };
        //#endregion Controles tela
        //#region Urls de métodos do servidor
        this.ServerMethodsUrls = {
            ListarPermissoesAtivas: "Administrativo/TipoUsuario/ListarPermissoesAtivas",
            ListarPermissoesInativas: "Administrativo/TipoUsuario/ListarPermissoesInativas",
            AtribuirPermissao: "Administrativo/TipoUsuario/Atribuir",
            AtribuirTodasPermissao: "Administrativo/TipoUsuario/AtribuirTodas",
            RemoverTodasPermissao: "Administrativo/TipoUsuario/RemoverTodas",
            RemoverPermissao: "Administrativo/TipoUsuario/Remover",
            ListarUsuariosPorTipo: "Administrativo/TipoUsuario/ListarUsuariosPorTipo",
            ForcarTodosUsuariosPerfil: "Administrativo/TipoUsuario/ForcarTodosUsuariosPerfil"
        };
        //#endregion Urls de métodos do servidor
        //#region Propriedades Página
        this.CodTipoUsuario = this.Controls.HiddenCodTipoUsuario.val();
        this.TipoUsuarioDescricao = $("#displayTipoUsuarioDescricao").text();
        //#endregion Propriedades Página
    }

    async loadAsync() {
        try {
            await super.loadAsync();
            await this.Controls.WindowPermissaoDiferenciadaUsuario.loadAsync();
            this.Controls.DataTableUsuariosVinculados = this.Controls.TableUsuariosVinculados.DataTable();
            await this.carregaTelaAsync();
            this.registraEventosControles();
        }
        catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        }
    }

    async carregaTelaAsync() {
        await Promise.all([
            this.carregaUsuariosAssociadosAsync(),
            this.carregaPermissoesAtribuidasAsync(),
            this.carregaPermissoesNaoAtribuidasAsync()
        ]);
    }

    registraEventosControles() {

        this.Controls.Permissoes.BtnPesquisarPermissaoAtiva.click(async () => {
            try {
                await this.carregaPermissoesAtribuidasAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

        this.Controls.Permissoes.BtnPesquisarPermissaoInativa.click(async () => {
            try {
                await this.carregaPermissoesNaoAtribuidasAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

        this.Controls.Permissoes.BtnLimparPermissaoAtiva.click(async () => {
            try {
                await this.limparAtribuidasAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

        this.Controls.Permissoes.BtnLimparPermissaoInativa.click(async () => {
            try {
                await this.limparNaoAtribuidasAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

        this.Controls.Permissoes.BtnAdicionarTodasPermissoes.click(async () => {
            try {

                await this.atribuirTodasAsync();

            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

        this.Controls.Permissoes.BtnRemoverTodasPermissoes.click(async () => {
            try {

                await this.removerTodasAsync();

            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

        this.Controls.BtnForcaAjuste.click(async () => {
            try {

                await this.forcarAjustePerfilAsync();

            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

    }

    //#region Usuarios vinculados
    async carregaUsuariosAssociadosAsync() {

        this.Controls.BtnForcaAjuste.fadeOut();
        this.FormsService.PopUpService.showLoadFor(this.Controls.TableUsuariosVinculados);
        this.Controls.DataTableUsuariosVinculados.rows().remove().draw();
        let dados = await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.ListarUsuariosPorTipo, { codTipoUsuario: this.CodTipoUsuario });

        if (dados.length > 0) {
            this.Controls.DisplayAvisoTipoEmProducao.fadeIn();
            this.Controls.AreaChkAplicarRegraRetroativa.fadeIn();
        } else {
            this.Controls.DisplayAvisoTipoEmProducao.fadeOut();
            this.Controls.AreaChkAplicarRegraRetroativa.fadeOut();
        }

        let exibeRegraAviso = false;
        dados.map((item, index) => {

            //#region Controles
            let windowPermissaoDiferenciadaUsuario = this.Controls.WindowPermissaoDiferenciadaUsuario.Layout.clone();
            let modeloBotaoShowDiferencas = windowPermissaoDiferenciadaUsuario.find("[data-show-diff]");
            let listaPermissaoExedente = windowPermissaoDiferenciadaUsuario.find("[data-permissao-excedente]");
            let listaPermissaoExcedenteItem = windowPermissaoDiferenciadaUsuario.find("[data-permissao-excedente-item").clone();
            listaPermissaoExcedenteItem.removeClass("escondido");
            let listaPermissaoAusente = windowPermissaoDiferenciadaUsuario.find("[data-permissao-ausente]");
            let listaPermissaoAusenteItem = windowPermissaoDiferenciadaUsuario.find("[data-permissao-ausente-item]").clone();
            listaPermissaoAusenteItem.removeClass("escondido");
            let displayUsuarioDescricao = windowPermissaoDiferenciadaUsuario.find("[data-display-usuario-descricao]");
            displayUsuarioDescricao.text(this.TipoUsuarioDescricao);
            //#endregion Controles

            let btnShowDif = modeloBotaoShowDiferencas.clone();
            btnShowDif.clone();
            btnShowDif.removeClass("escondido");
            btnShowDif.attr("data-show-diff", index);

            let regraCustomizada;

            if (item.RegraCustomizada === true) {
                this.FormsService.Map.call(item.RegrasExedentes, (itemListaExedente) => {
                    let item = listaPermissaoExcedenteItem.clone();
                    item.text(itemListaExedente);
                    listaPermissaoExedente.append(item);
                });

                if (item.RegrasExedentes.length === 0) {
                    let item = listaPermissaoExcedenteItem.clone();
                    item.text("Sem ocorrências");
                    listaPermissaoExedente.append(item);
                }

                this.FormsService.Map.call(item.RegrasAusentes, (itemListaAusente) => {
                    let item = listaPermissaoAusenteItem.clone();
                    item.text(itemListaAusente);
                    listaPermissaoAusente.append(item);
                });
                if (item.RegrasAusentes.length === 0) {
                    let item = listaPermissaoAusenteItem.clone();
                    item.text("Sem ocorrências");
                    listaPermissaoAusente.append(item);
                }

                regraCustomizada = item.Usuario + "<br/><em class='micro-text negativo'>Permissão customizada<em>";

                this.Controls.BtnForcaAjuste.fadeIn();

            } else {
                regraCustomizada = item.Usuario;
            }

            this.Controls.DataTableUsuariosVinculados.row.add([
                item.Codigo,
                regraCustomizada, item.RegraCustomizada ? btnShowDif[0].outerHTML : ""
            ]).draw();

            if (item.RegraCustomizada === true) {
                exibeRegraAviso = true;
                this.Controls.TableUsuariosVinculados.find("[data-show-diff='" + index + "']").click(() => {
                    this.FormsService.PopUpService.alertMsg(windowPermissaoDiferenciadaUsuario[0].outerHTML,
                        true, "Permissões diferênciadas do usuário");
                });
            }

        });

        if (exibeRegraAviso === true)
            this.Controls.DisplayAvisoAjusteAutomatico.show();
        else
            this.Controls.DisplayAvisoAjusteAutomatico.hide();

        this.FormsService.PopUpService.disposeLoadFor(this.Controls.TableUsuariosVinculados);
    }

    async forcarAjustePerfilAsync() {

        await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.ForcarTodosUsuariosPerfil, { codTipoUsuario: this.CodTipoUsuario });
        await this.carregaTelaAsync();
        this.FormsService.PopUpService.alertMsg("Todos os usuários do tipo " + this.TipoUsuarioDescricao + " tiveram suas permissões ajustadas.");
    }

    //#endregion Usuarios vinculados

    //#region Permissoes
    async limparAtribuidasAsync() {
        this.Controls.Permissoes.TxtPesquisaPermissaoAtiva.val("");
        await this.carregaPermissoesAtribuidasAsync();
    }

    async limparNaoAtribuidasAsync() {
        this.Controls.Permissoes.TxtPesquisaPermissaoInativa.val("");
        await this.carregaPermissoesNaoAtribuidasAsync();
    }

    async carregaPermissoesNaoAtribuidasAsync() {
        try {
            let nomeRegra = this.Controls.Permissoes.TxtPesquisaPermissaoInativa.val();
            this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            this.Controls.Permissoes.RegrasNaoAtribuidasArea.html("");
            let dados = await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.ListarPermissoesInativas, { codTipoUsuario: this.CodTipoUsuario, nomeRegra: nomeRegra });

            this.FormsService.Map.call(dados, (item) => {
                if (item !== null) {
                    let btn = $("<button" +
                        " title='" + item.Detalhamento +
                        "' type='button' class='btn btn-block btn-success ajusta-texto-retcencias' >" +
                        item.RegraSistemaDescricao +
                        "</button>");
                    this.Controls.Permissoes.RegrasNaoAtribuidasArea.append(btn);
                    btn.click(async () => {
                        try {

                            await this.atribuirAsync(item.Codigo);
                        } catch (ex) {
                            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
                        }
                    });
                }
            });
        }
        finally {
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
        }

    }

    async carregaPermissoesAtribuidasAsync() {
        try {
            let nomeRegra = this.Controls.Permissoes.TxtPesquisaPermissaoAtiva.val();
            this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            this.Controls.Permissoes.RegrasAtribuidasArea.html("");
            let dados = await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.ListarPermissoesAtivas, { codTipoUsuario: this.CodTipoUsuario, nomeRegra: nomeRegra });

            this.FormsService.Map.call(dados, (item) => {
                if (item !== null) {
                    let btn = $("<button" + " title='" + item.Detalhamento + "' type='button' class='btn btn-block btn-danger ajusta-texto-retcencias'>" +
                        item.RegraSistemaDescricao + " </button>");
                    this.Controls.Permissoes.RegrasAtribuidasArea.append(btn);
                    btn.click(async () => {
                        try {
                            await this.removerAsync(item.Codigo);
                        } catch (ex) {
                            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
                        }
                    });
                }
            });
        } finally {
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        }
    }

    async atribuirAsync(codRegraSistema) {
        this.Controls.Permissoes.RegrasAtribuidasArea.find("button").addClass("disabled");
        this.Controls.Permissoes.RegrasNaoAtribuidasArea.find("button").addClass("disabled");

        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.TableUsuariosVinculados);

        let aplicarRegraRetroativa = this.Controls.ChkAplicarRegraRetroativa.is(":checked") ? false : true;
        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.AtribuirPermissao, { codTipoUsuario: this.CodTipoUsuario, CodRegraSistema: codRegraSistema, aplicaRegraRetroativa: aplicarRegraRetroativa });
        }
        finally {
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.TableUsuariosVinculados);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            await this.carregaTelaAsync();
        }

    }

    async atribuirTodasAsync() {
        this.Controls.Permissoes.RegrasAtribuidasArea.find("button").addClass("disabled");
        this.Controls.Permissoes.RegrasNaoAtribuidasArea.find("button").addClass("disabled");

        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.TableUsuariosVinculados);

        let aplicarRegraRetroativa = this.Controls.ChkAplicarRegraRetroativa.is(":checked") ? false : true;
        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.AtribuirTodasPermissao, { codTipoUsuario: this.CodTipoUsuario, aplicaRegraRetroativa: aplicarRegraRetroativa });
        } finally {
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.TableUsuariosVinculados);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            await this.carregaTelaAsync();
        }
    }

    async removerTodasAsync() {
        this.Controls.Permissoes.RegrasAtribuidasArea.find("button").addClass("disabled");
        this.Controls.Permissoes.RegrasNaoAtribuidasArea.find("button").addClass("disabled");

        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.TableUsuariosVinculados);

        let aplicarRegraRetroativa = this.Controls.ChkAplicarRegraRetroativa.is(":checked") ? false : true;
        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.RemoverTodasPermissao, { codTipoUsuario: this.CodTipoUsuario, aplicaRegraRetroativa: aplicarRegraRetroativa });
        } finally {
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.TableUsuariosVinculados);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            await this.carregaTelaAsync();
        }
    }

    async removerAsync(codRegraSistema) {
        this.Controls.Permissoes.RegrasAtribuidasArea.find("button").addClass("disabled");
        this.Controls.Permissoes.RegrasNaoAtribuidasArea.find("button").addClass("disabled");

        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.TableUsuariosVinculados);

        let aplicarRegraRetroativa = this.Controls.ChkAplicarRegraRetroativa.is(":checked") ? false : true;
        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.RemoverPermissao, { codTipoUsuario: this.CodTipoUsuario, CodRegraSistema: codRegraSistema, aplicaRegraRetroativa: aplicarRegraRetroativa });

        } finally {
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.TableUsuariosVinculados);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            await this.carregaTelaAsync();
        }
    }
    //#endregion Permissoes


}
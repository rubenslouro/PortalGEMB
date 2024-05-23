import BasePageController from "../../../lib/CalangoJS/BasePageController.js";
import LayoutViewService from "../../../lib/CalangoJS/LayoutViewService.js";

export default class configuracoesPageController extends BasePageController {
    constructor() {
        super();
        //#region Controles tela
        this.Controls = {
            BtnAfastar: $("#btnAfastar"),
            BtnAtivar: $("#btnAtivar"),
            BtnIniciarAfastamento: $("#btnIniciarAfastamento, #btnIniciarAfastamento2"),
            BtnIniciarAtivacao: $("#btnIniciarAtivacao, #btnIniciarAtivacao2"),
            HiddenCodUsuario: $("#hiddenCodUsuario"),
            HiddenNomeUsuario: $("#hiddenNomeUsuario"),
            HiddenUrlVisualizarUsuario: $("#hiddenUrlVisualizarUsuario"),
            WindowAvisoAfastamentoView: new LayoutViewService("Administrativo/Usuario/WindowAvisoAfastamento", {}),
            WindowAvisoAtivacaoView: new LayoutViewService("Administrativo/Usuario/WindowAvisoAtivacao", {}),           
            Permissoes: {
                AreaBotoesPermissoesInativas: $("#areaBotoesPermissoesInativas"),
                AreaBotoesPermissoesAtivas: $("#areaBotoesPermissoesAtivas"),
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
            AfastarUsuario: "Administrativo/Usuario/Afastar",
            AtivarUsuario: "Administrativo/Usuario/Ativar",
            VisualizarUsuario: this.Controls.HiddenUrlVisualizarUsuario.val(),
            ListarPermissoesAtivas: "Administrativo/Usuario/ListarPermissoesAtivas",
            ListarPermissoesInativas: "Administrativo/Usuario/ListarPermissoesInativas",
            AtribuirPermissao: "Administrativo/Usuario/Atribuir",
            AtribuirTodasPermissao: "Administrativo/Usuario/AtribuirTodas",
            RemoverTodasPermissao: "Administrativo/Usuario/RemoverTodas",
            RemoverPermissao: "Administrativo/Usuario/Remover"
        };
        //#endregion Urls de métodos do servidor
        //#region Propriedades Página
        this.CodUsuario = this.Controls.HiddenCodUsuario.val();
        this.NomeUsuario = this.Controls.HiddenNomeUsuario.val();
        //#endregion Propriedades Página
    }

    async loadAsync() {
        await super.loadAsync();
        await this.Controls.WindowAvisoAfastamentoView.loadAsync();
        await this.Controls.WindowAvisoAtivacaoView.loadAsync();
        this.registraEventosControles();
        await this.carregaTelaAsync();
    }

    carregaTelaAsync() {
        Promise.all([
             this.carregaPermissoesAtribuidasAsync(),
             this.carregaPermissoesNaoAtribuidasAsync()
        ]);
    }

    registraEventosControles() {
        this.Controls.BtnIniciarAfastamento.click(async () => await this.carregaPopUpAfastamento());
        this.Controls.BtnIniciarAtivacao.click(async () => await this.carregaPopUpAtivacao());

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

    }

    //#region Afastamento
    async carregaPopUpAfastamento() {

        let popUpWindow = this.Controls.WindowAvisoAfastamentoView.Layout.clone();
        let displayUsuario = popUpWindow.find("[data-usuario]");
        let btnAfastar = popUpWindow.find("[data-btn-afastar]");       
        this.FormsService.PopUpService.showModal(popUpWindow);
        btnAfastar.click(async () => {
            try {
                this.FormsService.PopUpService.closeModalWindow(popUpWindow);
                await this.afastarAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });
        displayUsuario.text(this.CodUsuario + "-" + this.NomeUsuario);
    }

    async carregaPopUpAtivacao() {
        let popUpWindow = this.Controls.WindowAvisoAtivacaoView.Layout.clone();
        let displayUsuario = popUpWindow.find("[data-usuario]");
        let btnAtivar = popUpWindow.find("[data-btn-ativar]");
        this.FormsService.PopUpService.showModal(popUpWindow);
        btnAtivar.click(async () => {
            try {
                this.FormsService.PopUpService.closeModalWindow(popUpWindow);
                await this.ativarAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });
        displayUsuario.text(this.CodUsuario + "-" + this.NomeUsuario);
    }

    async afastarAsync() {
        try {
            this.FormsService.PopUpService.showLoading();
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.AfastarUsuario, { codUsuario: this.CodUsuario });
            this.FormsService.PopUpService.hideLoading();
            this.FormsService.PopUpService.alertMsgRedirect("Usuário afastado com sucesso!", this.ServerMethodsUrls.VisualizarUsuario);
        } catch (ex) {
            this.FormsService.PopUpService.hideLoading();
            throw ex;
        }
    }

    async ativarAsync() {
        try {
            this.FormsService.PopUpService.showLoading();
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.AtivarUsuario, { codUsuario: this.CodUsuario });
            this.FormsService.PopUpService.hideLoading();
            this.FormsService.PopUpService.alertMsgRedirect("Usuário ativado com sucesso!", this.ServerMethodsUrls.VisualizarUsuario);
        } catch (ex) {
            this.FormsService.PopUpService.hideLoading();
            throw ex;
        }
    }
    //#endregion Afastamento

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
            var nomeRegra = this.Controls.Permissoes.TxtPesquisaPermissaoInativa.val();
            this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            this.Controls.Permissoes.RegrasNaoAtribuidasArea.html("");
            this.Controls.Permissoes.AreaBotoesPermissoesInativas.hide();
            var dados = await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.ListarPermissoesInativas, { codUsuario: this.CodUsuario, nomeRegra: nomeRegra });
            
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

        } finally {
            this.Controls.Permissoes.AreaBotoesPermissoesInativas.show();
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
        }

       
    }

    async carregaPermissoesAtribuidasAsync() {
        try {
            let nomeRegra = this.Controls.Permissoes.TxtPesquisaPermissaoAtiva.val();
            this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            this.Controls.Permissoes.RegrasAtribuidasArea.html("");
            this.Controls.Permissoes.AreaBotoesPermissoesAtivas.hide();
         

            let dados = await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.ListarPermissoesAtivas, { codUsuario: this.CodUsuario, nomeRegra: nomeRegra });          

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
            this.Controls.Permissoes.AreaBotoesPermissoesAtivas.show();
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        }        
    }

    async atribuirAsync(codRegraSistema) {
        this.Controls.Permissoes.AreaBotoesPermissoesInativas.hide();
        this.Controls.Permissoes.AreaBotoesPermissoesAtivas.hide();
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);     
        
        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.AtribuirPermissao, { codUsuario: this.CodUsuario, CodRegraSistema: codRegraSistema });
        } catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        }
        finally {
            this.Controls.Permissoes.AreaBotoesPermissoesInativas.show();
            this.Controls.Permissoes.AreaBotoesPermissoesAtivas.show();
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            await this.carregaTelaAsync();
        }

    }

    async atribuirTodasAsync() {
        this.Controls.Permissoes.AreaBotoesPermissoesInativas.hide();
        this.Controls.Permissoes.AreaBotoesPermissoesAtivas.hide();
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);

        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.AtribuirTodasPermissao, { codUsuario: this.CodUsuario });
        } catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        } finally {
            this.Controls.Permissoes.AreaBotoesPermissoesInativas.show();
            this.Controls.Permissoes.AreaBotoesPermissoesAtivas.show();
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            await this.carregaTelaAsync();
        }
    }

    async removerTodasAsync() {
        this.Controls.Permissoes.AreaBotoesPermissoesInativas.hide();
        this.Controls.Permissoes.AreaBotoesPermissoesAtivas.hide();
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);

        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.RemoverTodasPermissao, { codUsuario: this.CodUsuario });
        } catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        } finally {
            this.Controls.Permissoes.AreaBotoesPermissoesInativas.show();
            this.Controls.Permissoes.AreaBotoesPermissoesAtivas.show();
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            await this.carregaTelaAsync();
        }
    }

    async removerAsync(codRegraSistema) {
        this.Controls.Permissoes.AreaBotoesPermissoesInativas.hide();
        this.Controls.Permissoes.AreaBotoesPermissoesAtivas.hide();
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
        this.FormsService.PopUpService.showLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);

        try {
            await this.ServerOperationsService.callServerMethodAsync(this.ServerMethodsUrls.RemoverPermissao, { codUsuario: this.CodUsuario, CodRegraSistema: codRegraSistema });
        } catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        } finally {
            this.Controls.Permissoes.AreaBotoesPermissoesInativas.show();
            this.Controls.Permissoes.AreaBotoesPermissoesAtivas.show();
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasAtribuidasArea);
            this.FormsService.PopUpService.disposeLoadFor(this.Controls.Permissoes.RegrasNaoAtribuidasArea);
            await this.carregaTelaAsync();
        }
    }
    //#endregion Permissoes


}
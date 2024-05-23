import BasePageController from "../../../lib/CalangoJS/BasePageController.js";

export default class IndexPageController extends BasePageController {
    constructor() {
        super();
        //Cria o modelo
        this.Model = {
            Ativo: true,
            DadosLogin: {
                Usuario: "Rubens"
            }
        };
        //Atua como um validador e como um moderador além de observer
        this.ModelModerator = {
            DadosLogin: {
                Usuario: (oldVal, newVal) => {
                    //Impede que a propriedade tenha mais que 10 digitos
                    if (newVal.length > 10) {
                        alert("Caixa de texto com limite de 10 digitos");
                        return;
                    }
                    
                }
            }
        };
        //Carrega as Urls dos métodos do servidor
        this.ServerMethodsUrls = {
            DadosAtendimentoxSexo: "Administrativo/Principal/DadosGrafico_SexoxAtendimento"
        };
    }    

    async loadAsync() {
        try
        {
            super.loadAsync();

            //await this.carregarGraficoTotalxSexoAsync();
            await this.carregarGraficoTotalxSexoAsync();      

        }
        catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        }
    }

    //async carregarGraficoTotalxSexoAsync() {
    //    try {
    //        var pcodAssistido = this.Controls.txtCodAssistido.val();
    //        let assistido = await serverOperationsService.callServerMethodAsync(this.ServerMethodsUrls.DadosAtendimentoxSexo, { codAssistido: pcodAssistido });
    //        this.Controls.txtNomeAssistido.val(assistido.AssistidoNome);

    //    } catch (ex) {
    //        this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
    //    }
    //}

    async carregarGraficoTotalxSexoAsync() {

        //fetchGet(Controlador/nombremetodo, "tipo que devuelve metodo controlador", function data()
        fetchGet("Grafico/graficoInicial", "text", function (data) {
            document.getElementById("imgFoto").src = "data:image/png;base64," + data;
        })
    }

}
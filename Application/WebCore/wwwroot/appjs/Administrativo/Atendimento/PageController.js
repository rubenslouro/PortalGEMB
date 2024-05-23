import BasePageController from "../../../../lib/CalangoJS/BasePageController.js";
import LayoutViewService from "../../../../lib/CalangoJS/LayoutViewService.js";
import ServerOperationsService from "../../../../lib/CalangoJS/ServerOperationsService.js"
import FormsService from "../../../lib/CalangoJS/FormsService.js";

const serverOperationsService = new ServerOperationsService();

export default class CriarPageController extends BasePageController {
    constructor() {
        super();

        //#region Controles tela
        this.Controls = {
            txtCodAssistido: $("#txtCodAssistido"),
            txtNomeAssistido: $("#txtNomeAssistido")
        };
        //#endregion Controles tela

        //#region Urls de métodos do servidor
        this.ServerMethodsUrls = {
            RecuperarNomeAssistido: "Administrativo/Atendimento/Recuperar"
        };
        //#endregion Urls de métodos do servidor
    }

    async sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    async loadAsync() {
        await super.loadAsync();

        await this.registraEventos();      
    }

    async registraEventos() {

        this.Controls.txtCodAssistido.change(async() => {
            await this.carregarNomeAssistidoAsync();
        });
    }

    async carregarNomeAssistidoAsync() {
        try {
            var pcodAssistido = this.Controls.txtCodAssistido.val();
            let assistido = await serverOperationsService.callServerMethodAsync(this.ServerMethodsUrls.RecuperarNomeAssistido, { codAssistido: pcodAssistido });
            this.Controls.txtNomeAssistido.val(assistido.AssistidoNome);

        } catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        }
    }
}
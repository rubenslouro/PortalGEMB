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
            ddlTurma: $("#ddlTurma"),
            tbAlunos: $("#tbAlunos")
        };
        //#endregion Controles tela

        //#region Urls de métodos do servidor
        this.ServerMethodsUrls = {
            RecuperarListaAluno: "Administrativo/Presenca/CarregarListaAluno"
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

        this.Controls.ddlTurma.change(async() => {
            await this.carregarAlunos_TurmaAsync();
        });
    }

    async carregarAlunos_TurmaAsync() {
        try {
            var pcodTurma = this.Controls.ddlTurma.val();
            //alert("Cod Turma:" + pcodTurma)
            let listaAlunos = await serverOperationsService.callServerMethodAsync(this.ServerMethodsUrls.RecuperarListaAluno, { codTurma: pcodTurma });
            this.Controls.tbAlunos.val(listaAlunos.AssistidoNome);

        } catch (ex) {
            this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
        }
    }
}
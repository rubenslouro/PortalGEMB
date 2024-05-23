import BasePageController from "../../../lib/CalangoJS/BasePageController.js";

export default class IndexPageController extends BasePageController {
    constructor() {
        super();

        this.Controls = {
            BtnShowSenha: $("#btnShowSenha"),
            HidenSenha: $("#hidenSenha")
        };
        this.Senha = this.Controls.HidenSenha.val();
    }

    async loadAsync() {
        await super.loadAsync();
        this.registraEventos();
    }

    registraEventos() {

        this.Controls.BtnShowSenha.click(() => {
            this.exibeSenha();
        });

    }

    exibeSenha() {
        this.FormsService.PopUpService.alertMsg(this.Senha, true, 'Senha do servidor de SMTP');
    }
}
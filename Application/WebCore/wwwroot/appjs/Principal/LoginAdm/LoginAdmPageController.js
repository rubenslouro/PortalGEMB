import BasePageController from "../../../lib/CalangoJS/BasePageController.js";

export default class LoginAdmPageController extends BasePageController {
    constructor() {
        super();
        //#region Controles da página 
        this.Controls = {
            ValidationSum: $("#validationSumary"),
            PanelLogin: $("#panelLogin"),
            TxtEmail: $("#txtEmail"),
            CalangoImg: $("#calangoImg")
        };
        //#endregion Controles da página 
    }

    async loadAsync() {
        await super.loadAsync();
        this.validarExibicaoLogin();
        this.registraEventos();
    }


    registraEventos() {
        this.FormsService.formSendingAction(() => {
            this.FormsService.PopUpService.showLoadFor(this.Controls.PanelLogin);
        });

        setInterval(()=> this.efeitoPiscar() ,3000);
    }



    validarExibicaoLogin() {
        if (this.existeMensagemSumario()) {
            this.efeitoNegaLogin();
        } else {
            this.exibeLogin();
        }
    }

    existeMensagemSumario() {
        return this.Controls.ValidationSum.find("li").text() !== "";
    }

    efeitoPiscar() {
        this.Controls.CalangoImg.attr("src", this.Controls.CalangoImg.attr("src").replace("logo.png", "logo2.png"));
        setTimeout(() => {
            this.Controls.CalangoImg.attr("src", this.Controls.CalangoImg.attr("src").replace("logo2.png", "logo.png"));
            setTimeout(() => {
                this.Controls.CalangoImg.attr("src", this.Controls.CalangoImg.attr("src").replace("logo.png", "logo2.png"));
                setTimeout(() => {
                    this.Controls.CalangoImg.attr("src", this.Controls.CalangoImg.attr("src").replace("logo2.png", "logo.png"));
                },500);
            },1000);
        }, 500);
    }

    efeitoNegaLogin() {
        this.Controls.PanelLogin.fadeIn(1000, () => {
            this.Controls.PanelLogin.effect("shake", { times: 3 }, 500, () => {
                this.Controls.CalangoImg.animate({ right: "-40000px", top: "-5000" },
                    500,
                    () => {
                        this.Controls.CalangoImg.css("top", "-5000px");
                        this.Controls.CalangoImg.css("right", "-37px");
                        setTimeout(() => {
                            this.Controls.CalangoImg.animate({ top: "-32px", right: "-37px" }, 500);
                            },
                            2000);
                    });
            });

            
        });
    }

    exibeLogin() {
        this.Controls.PanelLogin.fadeIn(1500);
    }
}
/*
 * MENTEREI OS ALERTS YES NO COMENTADOS PARA UM DIA QUEM SABE SOB URGÊNCIA CRIAR
 * VERSÃO CRIADA DIA 09/02/2022*/
import LayoutViewService from "./LayoutViewService.js";
export default class PopUpService {
    constructor() {

        this.Controls = {
            DialogsContainer: $("#dialogsContainer")           
        };    

        this.Layouts = {
            AlertDialogErroRedirectView: new LayoutViewService("Alert/AlertDialogErroRedirectView", {}),
            AlertDialogErroView: new LayoutViewService("Alert/AlertDialogErroView", {}),
            AlertDialogOKRedirectView: new LayoutViewService("Alert/AlertDialogOKRedirectView", {}),
            AlertDialogOKView: new LayoutViewService("Alert/AlertDialogOKView", {}),
            AlertDialogSendingView: new LayoutViewService("Alert/AlertDialogSending", {}),
            DialogVisualizadorDeImagensView: new LayoutViewService("Alert/DialogVisualizadorDeImagensView", {})
        };

    }
    async loadAsync() {
        await Promise.all([
            this.Layouts.AlertDialogErroRedirectView.loadAsync(),
            this.Layouts.AlertDialogErroView.loadAsync(),
            this.Layouts.AlertDialogOKRedirectView.loadAsync(),
            this.Layouts.AlertDialogOKView.loadAsync(),
            this.Layouts.AlertDialogSendingView.loadAsync(),
            this.Layouts.DialogVisualizadorDeImagensView.loadAsync(),
        ]);        

        this.alertMsg();
        this.alertMsgRedirect();
        this.alertMsgErroRedirect();
        this.alertMsgErro();
        this.sumaryErrorDisplay();
    }

    async initAsync() {
        await Promise.all([
            this.Layouts.AlertDialogErroRedirectView.loadAsync(),
            this.Layouts.AlertDialogErroView.loadAsync(),
            this.Layouts.AlertDialogOKRedirectView.loadAsync(),
            this.Layouts.AlertDialogOKView.loadAsync(),
            this.Layouts.AlertDialogSendingView.loadAsync(),
            this.Layouts.DialogVisualizadorDeImagensView.loadAsync(),
        ]);
    }

    sumaryErrorDisplay() {
        let selector = $(".validation-summary-errors");
        if (selector.find("li").text() === "") {
            selector.hide();
        } else {
            selector.show();           
        }
    }

    alertMsgRedirect(mensagem, url, isHtmlContent = false) {
        if (typeof mensagem !== "string" && mensagem !== undefined && mensagem !== null)
            mensagem = mensagem.toString();

        if (mensagem === null || mensagem === undefined) {
            let alertDialogOKRedirectView = $("[data-alertDialogOKRedirect]").first();
            let alertDialogOKRedirectMensagemContent = alertDialogOKRedirectView.find("[data-alertDialogOKRedirectMensagemContent]").first();

            let selectorPopUpMsg = alertDialogOKRedirectMensagemContent.text().trim();
            if (selectorPopUpMsg.length !== 0) {
                alertDialogOKRedirectView.modal({ backdrop: 'static', keyboard: false });
                this.removeDialogAfterClose(alertDialogOKRedirectView);
            }
        } else {

            let alertDialogOKRedirectView = this.Layouts.AlertDialogOKRedirectView.Layout.clone();
            let alertDialogOKRedirectMensagemContent = alertDialogOKRedirectView.find("[data-alertDialogOKRedirectMensagemContent]").first();
            let alertDialogOKRedirectUrl = alertDialogOKRedirectView.find("[data-alertDialogOKRedirectUrl]").first();


            if (isHtmlContent) {
                alertDialogOKRedirectMensagemContent.html(mensagem);
            } else {
                alertDialogOKRedirectMensagemContent.text(mensagem);
            }
            let urlLocal = window.location.href;
            if (url !== undefined && url !== null) {
                alertDialogOKRedirectUrl.attr("href", url);
            } else {
                alertDialogOKRedirectUrl.attr("href", urlLocal.replace("#", ''));
            }

            alertDialogOKRedirectView.modal({ backdrop: 'static', keyboard: false });
            this.removeDialogAfterClose(alertDialogOKRedirectView);

        }
    }

    alertMsgErroRedirect(mensagem, url) {
        if (typeof mensagem !== "string" && mensagem !== undefined && mensagem !== null)
            mensagem = mensagem.toString();

        if (mensagem === null || mensagem === undefined) {

            let alertDialogErroRedirectView = $("[data-alertDialogErroRedirect]").first();
            let alertDialogErroRedirectMensagemContent = alertDialogErroRedirectView.find("[data-alertDialogErroRedirectMensagemContent]").first();

            let selectorPopUpMsg = alertDialogErroRedirectMensagemContent.text().trim();
            if (selectorPopUpMsg.length !== 0) {
                alertDialogErroRedirectView.modal({ backdrop: 'static', keyboard: false });
                this.removeDialogAfterClose(alertDialogErroRedirectView);
            }

        } else {

            let alertDialogErroRedirectView = this.Layouts.AlertDialogErroRedirectView.Layout.clone();
            let alertDialogErroRedirectMensagemContent = alertDialogErroRedirectView.find("[data-alertDialogErroRedirectMensagemContent]").first();
            let alertDialogErroRedirectUrl = alertDialogErroRedirectView.find("[data-alertDialogErroRedirectUrl]").first();


            alertDialogErroRedirectMensagemContent.text(mensagem);

            let urlLocal = window.location.href;
            if (url !== undefined) {
                alertDialogErroRedirectUrl.attr("href", url);
            } else {
                alertDialogErroRedirectUrl.attr("href", urlLocal);
            }
            alertDialogErroRedirectView.modal({ backdrop: 'static', keyboard: false });
            this.removeDialogAfterClose(alertDialogErroRedirectView);

        }
    }

    alertMsgErro(mensagem) {
        if (typeof mensagem !== "string" && mensagem !== undefined && mensagem !== null)
            mensagem = mensagem.toString();

        if (mensagem === null || mensagem === undefined) {

            let alertDialogErroView = $("[data-alertDialogErro]").first()
            let alertDialogErroMensagemContent = alertDialogErroView.find("[data-alertDialogErroMensagemContent]").first();

            if (alertDialogErroMensagemContent.text().trim().length !== 0) {
                alertDialogErroView.modal({ backdrop: 'static', keyboard: false });
                this.removeDialogAfterClose(alertDialogErroView);
            }
        } else {

            let alertDialogErroView = this.Layouts.AlertDialogErroView.Layout.clone();
            let alertDialogErroMensagemContent = alertDialogErroView.find("[data-alertDialogErroMensagemContent]").first();


            alertDialogErroMensagemContent.text(mensagem);
            alertDialogErroView.modal({ backdrop: 'static', keyboard: false });
            this.removeDialogAfterClose(alertDialogErroView);

        }
    }

    alertMsg(mensagem, isHtmlContent = false, titleMacedonic = "Aviso") {
       
        if (typeof mensagem !== "string" && mensagem !== undefined && mensagem !== null)
            mensagem = mensagem.toString();
       
        if (mensagem === null || mensagem === undefined) {
            let alertDialogOKView = $("[data-alertDialogOK]").first()
            let alertDialogOKMensagemContent = alertDialogOKView.find("[data-alertDialogOKMensagemContent]").first();
            let alertDialogOKTitle = alertDialogOKView.find("[data-alertDialogOKTitle]").first();
            alertDialogOKTitle.text(titleMacedonic);

            if (alertDialogOKMensagemContent.text().trim().length !== 0) {
                alertDialogOKView.modal({ backdrop: 'static', keyboard: false });
                this.removeDialogAfterClose(alertDialogOKView);
            }

        } else {
            let alertDialogOKView = this.Layouts.AlertDialogOKView.Layout.clone();
            let alertDialogOKMensagemContent = alertDialogOKView.find("[data-alertDialogOKMensagemContent]").first();
            let alertDialogOKTitle = alertDialogOKView.find("[data-alertDialogOKTitle]").first();
            alertDialogOKTitle.text(titleMacedonic);

            if (isHtmlContent) {
                alertDialogOKMensagemContent.html(mensagem);
            } else {
                alertDialogOKMensagemContent.text(mensagem);
            }

            alertDialogOKView.modal({ backdrop: 'static', keyboard: false });
            this.removeDialogAfterClose(alertDialogOKView);
        }
    }
  
    showModal(objectToModal, removeDomAfterClose = true) {
        let obj = $(objectToModal);
        obj.modal();
        if (removeDomAfterClose === true) {
            this.removeDialogAfterClose(obj);
        }
    }

    removeDialogAfterClose(dialog) {
        dialog.on("hidden.bs.modal", () => {
            dialog.remove();
        });
    }
      
    closeModalWindow(window) {
        if (window.length != 0) {           
            window.modal("toggle");
        }
    }

    hideLoading() {
        try {
            let alertDialogSendingView = $("[data-alertdialogsending]");
            if (alertDialogSendingView.length != 0) {
                this.closeModalWindow(alertDialogSendingView);                
            }
        }
        catch (ex) {
            alert(ex);
        }

    }

    showLoading(htmlContent = "", title = "Aguarde") {
        let alertDialogSendingView = this.Layouts.AlertDialogSendingView.Layout.clone();
        let alertDialogSendingTitleText = alertDialogSendingView.find("[data-alertDialogSendingTitleText]").first();
        let alertDialogSendingText = alertDialogSendingView.find("[data-alertDialogSendingText]").first();

        alertDialogSendingText.html(htmlContent);
        alertDialogSendingTitleText.text(title);
        alertDialogSendingView.modal({ backdrop: 'static', keyboard: false });
        this.removeDialogAfterClose(alertDialogSendingView);
    }  
    
    showLoadFor(obj) {
        let idObj = obj.attr("id");
        obj.before("<div data-load-for='" + idObj + "' class='loader'></div>");
        obj.hide();
    }

    disposeLoadFor(obj) {
        let idObj = obj.attr("id");
        let objDel = $("[data-load-for='" + idObj + "']");
        obj.show();
        objDel.remove();
    }
}
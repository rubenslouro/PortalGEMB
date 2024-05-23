import BasePageController from "../../../../lib/CalangoJS/BasePageController.js";
import LayoutViewService from "../../../../lib/CalangoJS/LayoutViewService.js";
export default class CriarPageController extends BasePageController {
    constructor() {
        super();

        //#region Urls de métodos do servidor
        this.ServerMethodsUrls = {
            LayoutCamera: "/appjs/Administrativo/Camera/WindowCamera.html"
        };
        //#endregion Urls de métodos do servidor

        //#region Controles tela
        this.Controls = {
            WindowCamera: new LayoutViewService(this.ServerMethodsUrls.LayoutCamera, {}, "GET"),
            BtnShot: $("#btnShot"),
            TxtImagem: $("#txtImagemAssistido"),
            BtnShowCam: $("#btnShowCamera"),
            ImgFotoCliente: $("#imgImagemAssistido"),
            TxtDtNascimento: $("#txtDtNascimento"),
            TxtIdade: $("#txtIdade"),
            DdlSexo: $("#ddlSexo"),
            DdlEstadoCivil: $("#ddlEstadoCivil"),
            DdlEscolaridade: $("#ddlEscolaridade"),
            DdlMoradia: $("#ddlMoradia"),
            DdlAtividadeRemunerada: $("#ddlAtividadeRemunerada"),
            DdlProblemaSaude: $("#ddlProblemaSaude"),
            DdlDeficienciaFisica: $("#ddlDeficienciaFisica"),
            DdlDeficienciaMental: $("#ddlDeficienciaMental"),
            DdlDependente: $("#ddlDependente"),
            TxtCodigoScore: $("#txtCodigoScore"),
            TxtNumeroScore: $("#txtNumeroScore"),
        };
        //#endregion Controles tela
    }

    async sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    async loadAsync() {
        await super.loadAsync();
        await this.Controls.WindowCamera.loadAsync();

        this.registraEventos();      
    }

    async carregaPopUpCamera() {
        const popUpWindow = this.Controls.WindowCamera.Layout.clone();
        this.FormsService.PopUpService.showModal(popUpWindow);

        setTimeout(() => {
            let video = document.querySelector("#videoWebCam");
            //As opções abaixo são necessárias para o funcionamento correto no iOS
            video.setAttribute('autoplay', '');
            video.setAttribute('muted', '');
            video.setAttribute('playsinline', '');

            if (navigator.mediaDevices.getUserMedia) {
                navigator.mediaDevices.getUserMedia({ audio: false, video: { facingMode: 'user' } })
                    .then(function (stream) {
                        popUpWindow.find("[btn-take-snapshot]").show();
                        //Definir o elemento vídeo a carregar o capturado pela webcam
                        video.srcObject = stream;
                    })
                    .catch(function (error) {
                        alert("Oooopps... Falhou :^(");
                    });
            }

            popUpWindow.find("[btn-take-snapshot]").click(() => {
               
                var video = document.querySelector("#videoWebCam");

                //Criando um canvas que vai guardar a imagem temporariamente
                var canvas = document.createElement('canvas');
                canvas.width = video.videoWidth;
                canvas.height = video.videoHeight;
                var ctx = canvas.getContext('2d');

                //Desenhando e convertendo as dimensões
                ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

                //Criando o JPG
                var dataURI = canvas.toDataURL('image/jpeg'); //O resultado é um BASE64 de uma imagem.
                this.Controls.TxtImagem.val(dataURI);
                this.Controls.TxtImagem.change();
                this.FormsService.PopUpService.closeModalWindow(popUpWindow);
            });

        }, 1000);

    }

    async registraEventos() {

        this.Controls.BtnShowCam.click(() => {
            this.carregaPopUpCamera();
        });

        this.Controls.TxtImagem.on("input change paste keyup blur focusout", () => {
            this.Controls.ImgFotoCliente.attr("src", this.Controls.TxtImagem.val());
        });

        if (this.Controls.TxtImagem.val() != "")
            this.Controls.TxtImagem.change();

        this.Controls.TxtDtNascimento.change(() => {
            this.Controls.TxtIdade.val(this.calcularIdade(this.Controls.TxtDtNascimento.val()));
            this.calcularScore();
        });

        this.Controls.DdlSexo.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlEscolaridade.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlEstadoCivil.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlMoradia.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlAtividadeRemunerada.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlProblemaSaude.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlDeficienciaFisica.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlDeficienciaMental.change(() => {
            this.calcularScore();
        });

        this.Controls.DdlDependente.change(() => {
            this.calcularScore();
        });
    }

    calcularIdade(dataNascimento) {
        // Obtém a data atual
        var hoje = new Date();

        // Divide a string da data de nascimento para obter dia, mês e ano
        var partesDataNascimento = dataNascimento.split('/');
        var diaNascimento = parseInt(partesDataNascimento[0], 10);
        var mesNascimento = parseInt(partesDataNascimento[1], 10) - 1; // Mês é baseado em zero
        var anoNascimento = parseInt(partesDataNascimento[2], 10);

        // Cria um objeto de data com a data de nascimento
        var dataNascimentoObj = new Date(anoNascimento, mesNascimento, diaNascimento);

        // Calcula a diferença em milissegundos entre as duas datas
        var diferencaEmMilissegundos = hoje - dataNascimentoObj;

        // Calcula a idade dividindo a diferença em milissegundos pelo número de milissegundos em um ano
        var idade = Math.floor(diferencaEmMilissegundos / (365.25 * 24 * 60 * 60 * 1000));

        return idade;
    }

    calcularScore() {
        var score = 0;
        var codScore = "";
        var numScore = 0;

        //Sexo
        if (this.Controls.DdlSexo.val() == 'F') { score += 5; } else { score += 3; }

        //Idade
        if (this.Controls.TxtIdade.val() < 19) { score += 4; }
        else if (this.Controls.TxtIdade.val() < 24) { score += 2; }
        else if (this.Controls.TxtIdade.val() < 36) { score += 1; }
        else if (this.Controls.TxtIdade.val() < 51) { score += 2; }
        else if (this.Controls.TxtIdade.val() < 66) { score += 4; }
        else { score += 6; }

        //Escolaridade
        if (this.Controls.DdlEscolaridade.val() == "A") { score += 14; }
        else if (this.Controls.DdlEscolaridade.val() == "F") { score += 10; }
        else if (this.Controls.DdlEscolaridade.val() == "M") { score += 6; }
        else if (this.Controls.DdlEscolaridade.val() == "S") { score += 2; }
        else { score += 0; }

        //Estado Civil
        if (this.Controls.DdlEstadoCivil.val() == "S") { score += 1; }
        else if (this.Controls.DdlEstadoCivil.val() == "C") { score += 2; }
        else if (this.Controls.DdlEstadoCivil.val() == "U") { score += 2; }
        else if (this.Controls.DdlEstadoCivil.val() == "D") { score += 3; }
        else if (this.Controls.DdlEstadoCivil.val() == "V") { score += 3; }
        else { score += 0; }

        //Moradia
        if (this.Controls.DdlMoradia.val() == "P") { score += 2; }
        else if (this.Controls.DdlMoradia.val() == "F") { score += 8; }
        else if (this.Controls.DdlMoradia.val() == "A") { score += 10; }
        else if (this.Controls.DdlMoradia.val() == "I") { score += 14; }
        else if (this.Controls.DdlMoradia.val() == "B") { score += 2; }
        else if (this.Controls.DdlMoradia.val() == "R") { score += 2; }
        else { score += 0; }

        //Atividade Remunerada
        if (this.Controls.DdlAtividadeRemunerada.val() == "A") { score += 2; }
        else if (this.Controls.DdlAtividadeRemunerada.val() == "E") { score += 6; }
        else if (this.Controls.DdlAtividadeRemunerada.val() == "P") { score += 4; }
        else if (this.Controls.DdlAtividadeRemunerada.val() == "B") { score += 8; }
        else if (this.Controls.DdlAtividadeRemunerada.val() == "S") { score += 10; }
        else { score += 0; }

        //Problema de Saúde
        if (this.Controls.DdlProblemaSaude.val() == 'S') { score += 15; } else { score += 3; }

        //Deficiencia Física
        if (this.Controls.DdlDeficienciaFisica.val() == 'S') { score += 10; } else { score += 2; }

        //Deficiencia Mental
        if (this.Controls.DdlDeficienciaMental.val() == 'S') { score += 20; } else { score += 4; }

        //Dependente
        if (this.Controls.DdlDependente.val() == "N") { score += 3; }
        else if (this.Controls.DdlDependente.val() == "1") { score += 6; }
        else if (this.Controls.DdlDependente.val() == "2") { score += 9; }
        else if (this.Controls.DdlDependente.val() == "3") { score += 12; }
        else if (this.Controls.DdlDependente.val() == "6") { score += 15; }
        else if (this.Controls.DdlDependente.val() == "D") { score += 18; }
        else { score += 0; }

        //Validar Código Score
        if (score < 19) { codScore = ""; numScore = 0; }
        else if (score < 40) { codScore = "A"; numScore = 1; }
        else if (score < 58) { codScore = "B"; numScore = 3; }
        else if (score < 75) { codScore = "C"; numScore = 4; }
        else if (score < 93) { codScore = "D"; numScore = 5; }
        else { codScore = "E"; numScore = 6; }

        //Validar Número do Score
        this.Controls.TxtCodigoScore.val(codScore)
        this.Controls.TxtNumeroScore.val(numScore)

        //return score += " = " + codScore;
    }
}
import BasePartialController from '../../../lib/CalangoJS/BasePartialController.js';
import QrCodeService from '../../../lib/CalangoJS/QrCodeService.js';
import DevicesService from '../../../lib/CalangoJS/DevicesService.js';

export default class ScriptsGenericosPartialController extends BasePartialController {
    constructor() {
        super();
        //#region Controles de tela
        this.Controls = {
            BtnShowQRCodePage: $("#btnShowQRCodePage"),
            AreaQrCodeUrlPage: $("#areaQrCodeUrlPage"),
            DataHoraRodapeAplicativo: $("[data-data-hora-rodape-aplicativo]"),
            BtnGlobalAddVisita: $("#btnGlobalAddVisita"),
            BtnGlobalListarPessoas: $("#btnGlobalListarPessoas"),
        };
        //#endregion Controles de tela        

        //#region Urls de métodos do servidor
        this.ServerMethodsUrls = {
            DataHora: "/Principal/DataHora"
        };
        //#endregion Urls de métodos do servidor
        this.QrCodeService = new QrCodeService(this.Controls.AreaQrCodeUrlPage.attr("id"), 128, 128);
        this.DevicesService = new DevicesService();
    }

    async loadAsync() {
        await super.loadAsync();
        if (this.Controls.AreaQrCodeUrlPage[0] !== undefined) {
            this.QrCodeService.gerarQrCode(window.location);
            this.exibeQrApenasPC();
        }

        await this.atualizaDataHoraAsync();
        await this.carregaEventosControlesAsync();
    }

    exibeQrApenasPC() {

        if (this.DevicesService.IsMobileAndTablet()) {
            this.Controls.BtnShowQRCodePage.hide();
        }
    }

    showQrCodePage() {
        
        let tela = "<div class='row'>" +
            "<div class='col-lg-8'>" +
            "<p>Este QR Code contém a URL da página que você está acessando." +
            "</p>" +
            "</div>" +
            "<div class='col-lg-4'>" +
            this.Controls.AreaQrCodeUrlPage.html() +
            "</div>" +
            "</div>";
        this.FormsService.PopUpService.alertMsg(tela, true, "QrCode da URL");
    }

    async atualizaDataHoraAsync() {
        let url = this.ServerMethodsUrls.DataHora;
        let result = await this.ServerOperationsService.callServerMethodAsync(url, {});
        this.Controls.DataHoraRodapeAplicativo.text(result.DataHora);
    }  

    //#region Eventos de controles

    async carregaEventosControlesAsync() {

        this.Controls.BtnShowQRCodePage.click(() => this.showQrCodePage());

        setInterval(async () => {
            await this.atualizaDataHoraAsync();
        }, 5000);


        this.Controls.BtnGlobalAddVisita.click(() => {
            window.location = "/Administrativo/Visita/Criar";
        });

        this.Controls.BtnGlobalListarPessoas.click(() => {
            window.location = "/Administrativo/Pessoa";
        });
    }

    //#endregion Eventos de controles
}
import QrCodeService from "../../../lib/CalangoJS/QrCodeService.js";
import Exception from "../../../lib/CalangoJS/Exception.js";
import BasePageController from "../../../lib/CalangoJS/BasePageController.js";

export default class QrReaderPageController extends BasePageController{
    constructor() {
        super();
       //#region Controles da página 
        this.Controls = {
            BtnCarregaCam: $("#btnCarregaCam"),
            CamPreview: $("#camPreview"),
            SelectCamera: $("#selectCamera")
        };        
        //#endregion Controles da página 

        this.QrReader = null;
        this.QrCodeService = new QrCodeService();
    }

    async loadAsync() {
        await super.loadAsync();
        await this.listarCamerasAsync();
        this.registrarEventosControles();
    }

    registrarEventosControles() {

        this.Controls.BtnCarregaCam.click(async () => {
            try {
                await this.carregaCameraAsync();
            } catch (ex) {
                this.FormsService.PopUpService.alertMsgErro(ex.MessageError);
            }
        });

    }

    async carregaCameraAsync() {

        let numCam = this.Controls.SelectCamera.children("option:selected").val();
     
        if (this.QrReader !== null) {
            this.QrReader.stop();
        }

        if (numCam === undefined) {
            throw new Exception("Selecione uma câmera antes de continuar.");
        }
        this.QrReader = await this.QrCodeService.carregaQrReaderAsync(this.Controls.CamPreview, numCam, "Processando ...");
    }

    async listarCamerasAsync() {
        await this.QrCodeService.listAllCamInSelectAsync(this.Controls.SelectCamera);
    }
}
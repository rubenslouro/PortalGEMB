import BasePageController from "../../../../lib/CalangoJS/BasePageController.js";
export default class VisualizarPageController extends BasePageController {
    constructor() {
        super();

        //#region Urls de métodos do servidor
        this.ServerMethodsUrls = {
            LayoutCamera: "Administrativo/Pessoa/WindowCamera"
        };
        //#endregion Urls de métodos do servidor

        //#region Controles tela
        this.Controls = {
            TxtImagem: $("#txtImagem"),
            TxtImagemDocumento: $("#txtImagemDocumento"),
            ImgFotoCliente: $("#imgFotoCliente"),
            ImgDocumento: $("#imgDocumento"),
            PTags: $("p")
        };
        //#endregion Controles tela
    }
    async loadAsync() {
        await super.loadAsync();
        this.carregaTela();
    }

    carregaTela() {
        this.preencheSemInformacoes();
    }

    preencheSemInformacoes() {
        this.Controls.PTags.each((i, p) => {
            const ptag = $(p);
            if (ptag.text() === "") {
                ptag.text("Sem informações");
            }
        });
    }
}
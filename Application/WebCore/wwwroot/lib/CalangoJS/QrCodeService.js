import Exception from './Exception.js';
export default class QrCodeService {
    constructor(qrCodeArea, widthQr, heightQr) {
        this.QrCodeArea = qrCodeArea;
        this.HeightQr = heightQr;
        this.WidthQr = widthQr;
        this.QrCode = null;
    }
    
    gerarQrCode(textToEncode) {

        if (textToEncode == null || textToEncode == "") 
            throw new Exception("Informe o texto para gerar o QrCode.");

        if (this.WidthQr == null)
            this.WidthQr = 256;

        if (this.HeightQr == null)
            this.HeightQr = 256;

     
        this.QrCode = new QRCode(this.QrCodeArea,
            {
                text: textToEncode,
                width: this.WidthQr,
                height: this.HeightQr,
                colorDark: "black",
                colorLight: "white",
                correctLevel: QRCode.CorrectLevel.H
            });

    }

    async carregaQrReaderAsync (videoTag, camIdNum, textProcessing) {
       
        try {

            let $video = videoTag,
                $window = $(window);
            $video.css({
                'height': 0,
                'width': 0,
                'object-fit': 'cover'
            });

            $video.before("<div style='border-style: solid;position:absolute;' id='divTargetQr'></div>");
            let qrTarget = $("#divTargetQr");
            let height = $window.height();

            $video.css({
                'height': height,
                'width': '100%'
            });
            qrTarget.css({
                'height': $video.width() / 2,
                'width': $video.width() / 2,
                /*'top': $video.height() / 2,
                'left': $video.width() / 4*/
                'position': 'absolute',
                'top': '0',
                'bottom': '0',
                'left': '0',
                'right': '0',
                'margin': 'auto',
                'border-width': '10px',
                'border-color': '#4542f5'

            });
           
            let scanner = new Instascan.Scanner(
                {
                    video: document.getElementById(videoTag.attr("id")),
                    mirror: false
                }
            );
            scanner.addListener('scan', (content) => {
                    if (content !== "") {
                        if (textProcessing !== undefined && textProcessing !== null && textProcessing === "") {
                            qrTarget.before(
                                '<div style="background-color:white; text-align:center;position: fixed; top:0;left:0;right:0;bottom:0;z-index:9999"><h1>' +
                                textProcessing +
                                '</h1></div>');
                        } else {
                            qrTarget.before(
                                '<div style="background-color:white; text-align:center;position: fixed; top:0;left:0;right:0;bottom:0;z-index:9999"><h1>' +
                                'Aguarde processando pedido' +
                                ' ...</h1></div>');
                        }

                        window.location.href = content;
                    }
                });
          
            let cams = await Instascan.Camera.getCameras();

            if (cams.length > 0)
                scanner.start(cams[camIdNum]);
            else
                throw new Exception("Não conseguimos localizar sua câmera. " +
                    "Várias coisas podem ter ocorrido, entre elas você pode não " +
                    "ter uma câmera, sua câmera pode estar desligada ou simplesmente " +
                    "não conseguimos permissão para acessa-la.");

            return scanner;

        } catch (e) {
            //alert(e);
        }
        return null;
    }

    async listAllCamInSelectAsync(selectObjHtml) {
        let select = selectObjHtml;
        let cams = await Instascan.Camera.getCameras();
        cams.map((cam, i ) => {
            select.append($('<option>',
                {
                    value: i,
                    text: 'Camera - ' + cam.name
                }));
        });
        select.selectpicker('refresh');

    }
}
export default class ImageService {
    constructor() {
        
    }

    async loadAsync() {
       
        this.inicializaUploadImagemBase64ByTag();
        this.inicializaVisualizadorDeImagensPadraoByTag();
        this.inicializaVisualizadorDeImagensPadrao();
    }

    inicializaVisualizacaoUploadImagem(idFileUploadId, idImg, idBtnAction) {
        try {
            $(document).ready(function () {

                $("#" + idBtnAction).click(function () {
                    $("#" + idFileUploadId).trigger("click");
                });

                $("#" + idFileUploadId).toggleClass("invisible");

                $("#" + idFileUploadId).change(function () {
                    readUrl(this, $("#" + idImg));
                });

                $("#" + idImg).on('load',
                    function () {
                        $("#txtFoto").val($("#imgFotocliente").attr("src"));
                    });

                function readUrl(input, imgElement) {
                    if (input.files && input.files[0]) {
                        var calc = input.files[0].size / 1024;
                        if (calc > 5500.0) {
                            $(input).val('');
                            imgElement.attr('src', "");
                            $("#alertDialogOKMensagemContent").html("<p>O arquivo que você selecionou excede os 5mb permitidos para envio de imagens.<br/>Reduza o tamanho do aquivo da imagem antes de enviar.</p>");

                            $(function () {
                                $("#alertDialogOK").modal();
                            });
                        }
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            imgElement.attr('src', e.target.result);//.width(75).height(100);
                            $("#" + idFileUploadId).val(e.target.result);
                        };
                        reader.readAsDataURL(input.files[0]);
                    }
                }
            });
        } catch (e) {
            //
        }
    }

    createBase64String(base64String) {
        return "data:image/png;base64," + base64String;
    }

    clearBase64String(base64StringComplete) {
        return base64StringComplete.replace(/^data:image\/(png|jpg|jpeg);base64,/, "");
    }

    inicializaVisualizadorDeImagensPadraoByTag() {
        try {
          
            $("[visualizador-imagem]").each( (i, obj) => {
                let visualizador = $(obj);
                visualizador.addClass("img-thumbnail");
                visualizador.attr("title", "Clique nesta imagem para visualiza-la em nosso visualizardor de imagens.");
                visualizador.click(() => {
                    $("#imgModal").attr("src", visualizador.attr("src"));
                    $("#visualizadorImagem").modal();
                });
            });

        } catch (e) {           
            //
        }

        try {
           
            $("[data-visualizador-imagem]").each((i, obj) =>{
                let visualizador = $(obj);
                visualizador.attr("title", "Clique nesta imagem para visualiza-la em nosso visualizardor de imagens.");
                visualizador.click(() => {                   
                    $("#imgModal").attr("src", visualizador.attr("src"));
                    $("#visualizadorImagem").modal();
                });

            });

        } catch (e) {
          
        }
    }

    inicializaVisualizadorDeImagensPadrao(imagemOrigem) {
        try {

            if (imagemOrigem === null) {
                $(document).ready(function () {
                    $("#img").attr("title", "Clique nesta imagem para visualiza-la em nosso visualizardor de imagens.");
                    $("#img").click(function () {
                        $("#imgModal").attr("src", $("#img").attr("src"));
                        $("#visualizadorImagem").modal();
                    });
                });
            } else {
                $("#" + imagemOrigem).attr("title", "Clique nesta imagem para visualiza-la em nosso visualizardor de imagens.");
                $("#" + imagemOrigem).click(function () {
                    $("#imgModal").attr("src", $("#" + imagemOrigem).attr("src"));
                    $("#visualizadorImagem").modal();
                });
            }

        } catch (e) {
            //
        }
    }

    inicializaUploadImagemBase64ByTag() {
        try {
            $(document).ready(function () {

                $("[data-upload-imagem-base64]").each(function (i, obj) {
                    var img = $(obj);
                    var widthRe = img.attr("data-width-resize");
                    var heightRe = img.attr("data-height-resize");
                    var resizeData = img.attr("data-resize");

                    var noRotateData = img.attr("data-no-rotate");

                    if (noRotateData === undefined || noRotateData === null) {
                        noRotateData = false;
                    }

                    if (widthRe === undefined || widthRe === null) {
                        widthRe = 150;
                    }
                    if (heightRe === undefined || heightRe === null) {
                        heightRe = 200;
                    }

                    if (resizeData === undefined || resizeData === null) {
                        resizeData = false;
                    }
                    else {
                        resizeData = true;
                    }

                    img.addClass("img-thumbnail");
                    imgProtected(img);
                    img.after("<p></p>");
                    var pTag = img.parent().find("p");
                    pTag.append(img);
                    img.after('<input  type = "file" accept = ".jpg,.png" class="form-control text-uppercase escondido"/>');


                    var fileUpload = img.parent().find("input[type='file']");

                    var nomeBtn = "";

                    if (img.attr("nome-botao") !== undefined) {
                        nomeBtn = img.attr("nome-botao");
                    }
                    if (noRotateData === false) {
                        fileUpload.parent().after(' <button class="btn btn-info btn-success" data-temp-btn-for-rotate-click  type = "button" title="Clique aqui para rotacionar a imagem"><i class="fa fa-rotate-right"></i></button> ');
                    }

                    fileUpload.parent().after('<button class="btn btn-info btn-success" data-temp-btn-for-click  type = "button" title="Clique aqui para adicionar a imagem"><i class="fa fa-camera"></i> ' + nomeBtn + '</button>');

                    var btn = fileUpload.parent().parent().find("[data-temp-btn-for-click]");
                    var btnRotate = fileUpload.parent().parent().find("[data-temp-btn-for-rotate-click]");

                    btn.click(function () {
                        fileUpload.trigger("click");
                    });

                    btnRotate.click(function () {
                        rotateImage(img.attr("id"));
                    });

                    fileUpload.toggleClass("escondido");

                    fileUpload.change(function () {
                        readUrl(this, img);
                    });

                    var txtId = $("#" + img.attr("data-upload-imagem-base64"));
                    if (txtId.val() !== "") {
                        img.attr("src", "data:image/jpeg;base64," + txtId.val());
                    }

                    if (img.attr("src") !== "") {
                        //transforma em base64
                        var url = img.attr("src");
                        var xhr = new XMLHttpRequest();
                        xhr.onload = function () {
                            var reader = new FileReader();
                            reader.onloadend = function () {
                                var res = reader.result;
                                txtId.val(res);
                                img.attr("src", res);
                            };
                            reader.readAsDataURL(xhr.response);
                        };
                        xhr.open('GET', url);
                        xhr.responseType = 'blob';
                        xhr.send();
                    }

                    txtId.addClass("escondido");
                    img.on('load', function () {
                        if (resizeData === true) {
                            if (img.attr("id") === null || img.attr("id") === undefined) {
                                alert("Não foi atribuído um Id para o objeto imagem responsável, pelo Upload. Todas as ações relativas ao Upload desta imagem estão suspensas");

                            } else {
                                resizeImageForUpload(img.attr("id"), widthRe, heightRe);
                            }
                        }
                        txtId.val(clearBase64String(img.attr("src")));
                        if (img.attr("call-back-sucesso-selecao") !== undefined) {
                            eval(img.attr("call-back-sucesso-selecao"));
                        }
                    });


                    function readUrl(input, imgElement) {
                        if (input.files && input.files[0]) {
                            var calc = input.files[0].size / 1024;
                            if (calc > 9000.0) {
                                $(input).val('');
                                imgElement.attr('src', "");
                                $("#alertDialogOKMensagemContent").html("<p>O arquivo que você selecionou excede os 9 mb permitidos para envio de imagens.<br/>Reduza o tamanho do aquivo da imagem antes de enviar.</p>");

                                $(function () {
                                    $("#alertDialogOK").modal();
                                });
                            }
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                var targetResultData = e.target.result;
                                imgElement.attr('src', targetResultData);

                                //fileUpload.val(targetResultData);
                            };
                            reader.readAsDataURL(input.files[0]);
                        }
                    }
                });
            });
        } catch (e) {
            //
        }
    }

    rotateImage(image) {
        var imgSelected = $("#" + image);
        var temResize = false;
        if (imgSelected.attr("data-resize") !== undefined && imgSelected.attr("data-resize") !== null) {
            temResize = true;
            $(imgSelected.removeAttr("data-resize"));
        }
        var offScreenCanvas = document.createElement('canvas');
        var offScreenCanvasCtx = offScreenCanvas.getContext('2d');
        var base64Image = $("#" + image).attr("src");
        var img = new Image();
        img.src = base64Image;
        offScreenCanvas.height = img.width;
        offScreenCanvas.width = img.height;
        offScreenCanvasCtx.rotate(90 * Math.PI / 180);
        offScreenCanvasCtx.translate(0, -offScreenCanvas.width);
        offScreenCanvasCtx.drawImage(img, 0, 0);
        $("#" + image).attr("src", offScreenCanvas.toDataURL("image/jpeg", 100));


        setTimeout(function () {
            if (temResize === true) {
                imgSelected.attr("data-resize", "true");
            }

        }, 1000);
    }

    resizeImageForUpload(imagem, largura, altura) {
        try {

            if ($("#" + imagem).attr("data-resize") === undefined || $("#" + imagem).attr("data-resize") === null) {
                return;
            }

            var image = document.getElementById(imagem);
            if (image === null || image === undefined) {
                return;
            }
            var img = new Image(largura, altura);
            img.src = image.src;
            var canvas = document.createElement("canvas");
            var context = canvas.getContext("2d");
            canvas.width = img.width;
            canvas.height = img.height;
            context.drawImage(img, 0, 0, canvas.width, canvas.height);
            var canvasResult = canvas.toDataURL();
            if ($("#" + imagem).attr("src") !== canvasResult) {
                $("#" + imagem).attr("src", canvasResult);
            }

        } catch (e) {
            alert(e);
        }
    }

    imageToBase64(img) {
        img = document.getElementById(img);
        var canvas = document.createElement("canvas");
        canvas.width = img.width;
        canvas.height = img.height;
        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0);
        var dataUrl = canvas.toDataURL("image/png");
        return dataUrl.replace(/^data:image\/(png|jpg);base64,/, "");
    }

    imgProtected(objJquery) {
        objJquery.each(function (i, obj) {
            try {
                var img = $(obj);
                var url = img.attr("src");
                var xhr = new XMLHttpRequest();
                xhr.onload = function () {
                    var reader = new FileReader();
                    reader.onloadend = function () {
                        var res = reader.result;
                        img.attr("src", res);
                    }
                    reader.readAsDataURL(xhr.response);
                };
                xhr.open('GET', url);
                xhr.responseType = 'blob';
                xhr.send();
            } catch (e) {
                //
            }
        });
    }

    allImgProtected() {
        $("img").each(function (i, obj) {
            try {
                var img = $(obj);
                var url = img.attr("src");
                var xhr = new XMLHttpRequest();
                xhr.onload = function () {
                    var reader = new FileReader();
                    reader.onloadend = function () {
                        var res = reader.result;
                        img.attr("src", res);
                    };
                    reader.readAsDataURL(xhr.response);
                };
                xhr.open('GET', url);
                xhr.responseType = 'blob';
                xhr.send();
            } catch (e) {
                //
            }
        });
    }
}
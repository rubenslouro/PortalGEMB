
import Exception from './Exception.js';
export default class CepService {
    constructor() {

    }

    inicializaPreenchimentoAutomaticoCep(cep, numero, rua, bairro, municipio, uf, popUpService) {
        try {
            function limpaFormularioCep() {
                // Limpa valores do formulário de cep.
                $("#" + rua).val("");

                $("#" + bairro).val("");

                $("#" + municipio).val("");

                $("#" + uf).selectpicker('val', "");
                //disposeLoadFor($("#" + municipio));
                //disposeLoadFor($("#" + rua));
                //disposeLoadFor($("#" + bairro));
                //disposeLoadFor($("#" + uf));
            }

            $("#" + cep).on("change paste keyup",
                function () {
                    if ($(this).val().length < 9) {
                        //limpaFormularioCep();
                        //disposeLoadFor($("#" + municipio));
                        //disposeLoadFor($("#" + rua));
                        //disposeLoadFor($("#" + bairro));
                        //disposeLoadFor($("#" + uf));
                    }

                });

            $("#" + cep).blur(function () {

                //Nova variável "cep" somente com dígitos.
                var cep = $(this).val().replace(/\D/g, '');


                //Verifica se campo cep possui valor informado.
                if (cep !== "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    //Valida o formato do CEP.
                    if (validacep.test(cep)) {

                        //Preenche os campos com "..." enquanto consulta webservice.
                        /*$("#" + rua).val("");
                        $("#" + bairro).val("");
                        $("#" + municipio).val("");
                        $("#" + uf).selectpicker('val', '');*/

                        //showLoadFor($("#" + municipio));
                        //showLoadFor($("#" + rua));
                        //showLoadFor($("#" + bairro));
                        //showLoadFor($("#" + uf));
                        //Consulta o webservice viacep.com.br/
                        $.getJSON("//viacep.com.br/ws/" + cep + "/json/?callback=?",
                            function (dados) {

                                if (!("erro" in dados)) {
                                    //Atualiza os campos com os valores da consulta.
                                    $("#" + rua).val(dados.logradouro);
                                    $("#" + bairro).val(dados.bairro);
                                    $("#" + municipio).val(dados.localidade);


                                    $("#" + rua).change();
                                    $("#" + bairro).change();
                                    $("#" + municipio).change();


                                    $("#" + uf + " option:contains(" + dados.uf + ")").attr('selected', 'selected');
                                    $("#" + uf).selectpicker('val', $("#" + uf + " option:contains(" + dados.uf + ")").val());
                                    $("#" + uf).change();

                                    //disposeLoadFor($("#" + municipio));
                                    //disposeLoadFor($("#" + rua));
                                    //disposeLoadFor($("#" + bairro));
                                    //disposeLoadFor($("#" + uf));

                                    $("#" + numero).focus();

                                    //feito para o jquery validation
                                    try {
                                        $("#" + rua).valid();
                                        $("#" + bairro).valid();
                                        $("#" + municipio).valid();
                                        $("#" + uf).valid();
                                    } catch (e) {
                                        //
                                    }


                                } //end if.
                                else {
                                    popUpService.alertMsg("CEP não encontrado!");
                                    //CEP pesquisado não foi encontrado.
                                    //limpaFormularioCep();
                                    //disposeLoadFor($("#" + municipio));
                                    //disposeLoadFor($("#" + rua));
                                    //disposeLoadFor($("#" + bairro));
                                    //disposeLoadFor($("#" + uf));
                                    //$("#alertDialogOKMensagemContent").text("O CEP informado não foi localizado em nossa base de CEP, você poderá continuar seu cadastro, porém se o CEP informado não existir nos correios sua carteirinha não poderá ser entregue.");
                                    //alertMsgErro("O CEP informado não foi localizado em nossa base de CEP. Você poderá continuar, porém se o CEP informado não existir, corespondencias poderão não ser entregues.");
                                    
                                }
                            });
                    } //end if.
                    else {
                        popUpService.alertMsg("CEP inválido!");
                        //cep é inválido.
                        //limpaFormularioCep();
                        //disposeLoadFor($("#" + municipio));
                        //disposeLoadFor($("#" + rua));
                        //disposeLoadFor($("#" + bairro));
                        //disposeLoadFor($("#" + uf));
                        //alertMsgErro("Formato de CEP inválido.");
                    }
                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    //disposeLoadFor($("#" + municipio));
                    //disposeLoadFor($("#" + rua));
                    //disposeLoadFor($("#" + bairro));
                    //limpaFormularioCep();
                }
            });
           
        } catch (e) {
            //
        }
    }
}
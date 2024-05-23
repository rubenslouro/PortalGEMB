export default class MatematicaService {
    constructor() {
        
    }

    isNumber(value) {
        return /^-{0,1}\d+$/.test(value);
    }

    floatToMoeda(valor) {
        var casas = 2;
        var separdorDecimal = ",";
        var separadorMilhar = ".";
        var inteiros = parseInt(parseInt(valor * Math.pow(10, casas)) / parseFloat(Math.pow(10, casas)));
        var centavos = parseInt(parseInt(valor * Math.pow(10, casas)) % parseFloat(Math.pow(10, casas)));

        if (centavos % 10 === 0 && centavos + "".length < 2) {
            centavos = centavos + "0";
        } else if (centavos < 10) {
            centavos = "0" + centavos;
        }

        var milhares = parseInt(inteiros / 1000);
        inteiros = inteiros % 1000;

        var retorno = "";

        if (milhares > 0) {
            retorno = milhares + "" + separadorMilhar + "" + retorno;
            if (inteiros === 0) {
                inteiros = "000";
            } else if (inteiros < 10) {
                inteiros = "00" + inteiros;
            } else if (inteiros < 100) {
                inteiros = "0" + inteiros;
            }
        }
        retorno += inteiros + "" + separdorDecimal + "" + centavos;
        return retorno;
    }

    acrescerPercentual(valor, percentualAcrescido) {
        var re = valor * percentualAcrescido / 100;
        return re + valor;
    }

    getPercentual(valorPercentual, valorTotal) {
        return (100 * valorPercentual) / valorTotal;
    }

    moedaToFloat(valor) {
        var valorRet = 0.00;
        try {
            valor = valor.replace("R$", "");
            valor = valor.replace(" ", "");
            valor = valor.toString().trim();
            valorRet = parseFloat(valor.replace(".", "").replace(",", "."));
        } catch (e) {
            //
        }

        return valorRet;
    }

    numberToReal(numero) {
        numero = numero.toFixed(2).split('.');
        numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
        return numero.join(',');
    }
}
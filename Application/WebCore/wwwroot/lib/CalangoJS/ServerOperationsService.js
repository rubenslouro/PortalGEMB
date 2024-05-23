import Exception from './Exception.js';
export default class ServerOperationsService {
    constructor(urlBase = null) {

        if (urlBase === null) {
            if (location.host.split(":")[0].includes("localhost"))
                this.UrlBase = window.location.protocol + "//" + location.host.split(":")[0] + ":5001";
            else
                this.UrlBase = window.location.protocol + "//" + location.host.split(":")[0];
        } else {
            this.UrlBase = urlBase;
        }       
        
    }

    async callServerMethodAsync(url, parametersJson, verb = 'POST') {

        try {
            if (url.substring(0, 1) !== "/") {
                url = "/" + url;;
            }

            let result = await $.ajax({
                type: verb,
                url: this.UrlBase + url,
                data: parametersJson,
                dataType: "json",                
                timeout: 240000                
            });

            if (result.MessageError != null) {
                throw result;
            }

            return result;

        } catch (e) {
            if (e.responseText != null)              
                throw new Exception(e.responseText);
            else if (e.MessageError != null)
                throw e;
            else 
                throw new Exception("Falha desconhecida.");
        }
    }

    async callServerPartialViewAsync(url, parametersJson, verb = 'POST') {
        try {
            if (url.substring(0, 1) !== "/") {
                url = "/" + url;;
            }

            let result = await $.ajax({
                type: verb,
                url: this.UrlBase + url,
                data: parametersJson
            });

            if (result.MessageError !== undefined) {
                throw result;
            }

            return result;

        } catch (e) {
            if (e.responseText != null)
                throw new Exception(e.responseText);
            else if (e.MessageError != null)
                throw e;
            else
                throw new Exception("Falha desconhecida.");
        }
    }
}


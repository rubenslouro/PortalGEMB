import ServerOperationsService from "./ServerOperationsService.js";
const serverOperationsService = new ServerOperationsService();

export default class LayoutViewService {
    constructor(fromUrl, parametersToMethod = {},verb = "POST") {
        this.Url = fromUrl;
        this.Layout = null;
        this.ParametersToMethod = parametersToMethod;
        this.Verb = verb;
        
    }

    async loadAsync() {
        let jqueryHtmlLayout = await serverOperationsService.callServerPartialViewAsync(this.Url, this.ParametersToMethod, this.Verb);
        this.Layout = $(jqueryHtmlLayout);
    }

    async getHtmlContent() {
        return this.Layout[0].outerHTML;
    }

}
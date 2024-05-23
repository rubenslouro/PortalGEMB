import FormsService from "./FormsService.js";
import ServerOperationsService from "./ServerOperationsService.js";

export default class BasePartialController {
    constructor() {        
        this.FormsService = new FormsService();
        this.ServerOperationsService = new ServerOperationsService();
    }

    async loadAsync() {
        this.FormsService.PopUpService.loadAsync();
    }   

}

import FormsService from "./FormsService.js";
import CameraService from "./CameraService.js";
import CepService from "./CepService.js";
import ColorService from "./ColorService.js";
import CpfCnpjService from "./CpfCnpjService.js";
import DevicesService from "./DevicesService.js";
import ImageService from "./ImageService.js";
import MatematicaService from "./MatematicaService.js";
import ServerOperationsService from "./ServerOperationsService.js";
import ModelService from "./ModelService.js"

export default class BasePageController extends ModelService {
    constructor() {
        super();
        this.FormsService = new FormsService();
        this.CameraService = new CameraService();
        this.CepService = new CepService();
        this.DevicesService = new DevicesService();
        this.ColorService = new ColorService();
        this.CpfCnpjService = new CpfCnpjService();       
        this.ImageService = new ImageService();      
        this.MatematicaService = new MatematicaService();
        this.ServerOperationsService = new ServerOperationsService();
       

    }

    async loadAsync() {
        await this.ImageService.loadAsync(); 
        await this.FormsService.loadAsync();
        this.addModel();
    }
}
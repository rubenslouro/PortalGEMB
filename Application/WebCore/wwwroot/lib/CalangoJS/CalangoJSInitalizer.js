import FormsService from "./FormsService.js";
import ImageService from "./ImageService.js";

let formsService = new FormsService();
await formsService.initAsync();

let imageService = new ImageService();
await imageService.loadAsync();

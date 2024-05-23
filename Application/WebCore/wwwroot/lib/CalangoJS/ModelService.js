/**************************************************************/
//VERSION: 0.0.1.3 - ALPHA              
//CalangoJS - DEVELOPED BY RUBENS LOURO VIEIRA
//God be praised - If this part of the comment is removed, all usage rights will be lost.
/**************************************************************/

const especialInputs = ["checkbox", "radio"];
const stripComents = /((\/\/.*$)|(\/\*[\s\S]*?\*\/))/mg;
const argumentNames = /([^\s,]+)/g;
const virtualDOM = [];
const nameOf = (f) => (f).toString().replace(/[ |\(\)=>]/g, '');

export default class ModelService {
    constructor() {
        this.Model = null;
        this.ModelModerator = null;
    }

    addModel() {

        if (this.Model === null)
            return;

        this.#init();
        this.#convertToProxy();
    }

    //#region private methods
    #createvirtualDOM() {
        //get all tags about Calango attribute nodes
        $("*").filter((n, item) => {
            let att = item.attributes;
            for (let i = 0; i < att.length; i++) {                
                if (att[i].nodeName.indexOf('cm-') === 0) {
                    let obj = $(item);
                    let objVal = obj.attr(att[i].nodeName);
                    let propVal = obj.attr("cm-list-value")
                    let propText = obj.attr("cm-list-text")
                    let subTemplateObject = null;

                    if (obj.attr("cm-each")) {
                        subTemplateObject = $(obj.clone().html());
                    }

                    virtualDOM.push({
                        obj: obj,
                        attribute: att[i].nodeName,
                        model: objVal,
                        fullModel: "this.Model." + objVal,
                        selectListParameters: {
                            propertyToValue: propVal,
                            propertyToText: propText
                        },
                        subTemplate: subTemplateObject
                    });
                }
            };
            return false;
        });        
    }

    #removeCalangoAttributes() {
        virtualDOM.map((item) => {
            item.obj.removeAttr(item.attribute);
        });
    }

    #loadCalangoAttributes() {
        //get all tags about Calango attribute nodes        
        $("*").filter((n, item) => {
            let att = item.attributes;   
            for (let i = 0; i < att.length; i++) {
                let realNodeName = att[i].nodeName.replace("cm-att-","");
                if (att[i].nodeName.indexOf('cm-att-') === 0) {
                 
                    let obj = $(item);
                    let objVal = obj.attr(att[i].nodeName);

                    if (eval("this.Model." + objVal) == null ||
                        eval("this.Model." + objVal) == "") {                        
                        obj.removeAttr(realNodeName);
                    } else {                       
                        obj.attr(realNodeName, eval("this.Model." + objVal));
                    }
                }                
            };
            return false;
        });        
    }

    #loadCMEachs() {
        $("[cm-each]").each((index, item) => {
            let model = this.Model;
            let objHtml = $(item);
            let propTextName = objHtml.attr("cm-each");
            let fullPropPath = "this.Model." + propTextName;
            let modelLevels = propTextName.split(".");

            if (modelLevels.length > 0) {
                for (let i = 0; i < modelLevels.length - 1; i++) {
                    model = model[modelLevels[i]];
                }
                propTextName = modelLevels[modelLevels.length - 1];
            }

            if (propTextName !== undefined &&
                propTextName !== null &&
                propTextName.trim() !== "") {

                let arrObject = eval(fullPropPath);
                let htmlTemplateForObject = $(objHtml.html());
                objHtml.html("");

                arrObject.forEach((item) => {
                   
                    let objLayout = htmlTemplateForObject.clone();

                    //innerText
                    objLayout.find("[cm-each-item-inner-text]").each((index, objItemLayout) => {

                        let currItem = item;
                        let objItemLayoutI = $(objItemLayout);
                        let itemToChangeInnerText = objItemLayoutI.attr("cm-each-item-inner-text");                 
                        let modelPathSplited = itemToChangeInnerText.split(".");
       
                        if (modelPathSplited.length > 1) {                           
                            for (let i = 0; i < modelPathSplited.length - 1; i++) {
                               
                                currItem = currItem[modelPathSplited[i]];
                            }
                        }                          
                        
                        let prop = modelPathSplited[modelPathSplited.length - 1];
                       
                        objItemLayoutI.text(currItem[prop]);
                        objItemLayoutI.removeAttr("cm-each-item-inner-text");

                    });

                    //innerHtml
                    objLayout.find("[cm-each-item-inner-html]").each((index, objItemLayout) => {

                        let currItem = item;
                        let objItemLayoutI = $(objItemLayout);
                        let itemToChangeInnerHtml = objItemLayoutI.attr("cm-each-item-inner-html");
                        let modelPathSplited = itemToChangeInnerHtml.split(".");

                        if (modelPathSplited.length > 1) {
                            for (let i = 0; i < modelPathSplited.length - 1; i++) {

                                currItem = currItem[modelPathSplited[i]];
                            }
                        }

                        let prop = modelPathSplited[modelPathSplited.length - 1];

                        objItemLayoutI.html(currItem[prop]);
                        objItemLayoutI.removeAttr("cm-each-item-inner-html");

                    });

                    //attributes  
                    objLayout.find("[cm-each-item-click]").each((index, objItemLayout) => {
                        let currItem = item;
                        let objItemLayoutI = $(objItemLayout);
                        let clickEvent = objItemLayoutI.attr("cm-each-item-click");

                        objItemLayoutI.click(() => {
                            let nameVar = nameOf(() => currItem);                
                            let code = "let currentItem = " + nameVar + ";";
                            code += clickEvent;
                            eval(code);
                        });
                        objItemLayoutI.removeAttr("cm-each-item-click");
                    });
                  
                    objLayout.find("*").filter((n, objFilterItem) => {                   
                       
                        let att = objFilterItem.attributes;
                       
                        for (let i = 0; i < att.length; i++) {
                            
                            let realNodeName = att[i].nodeName.replace("cm-each-item-att-", "");
                            if (att[i].nodeName.indexOf('cm-each-item-att-') === 0) {
                                let objItemLayoutI = $(objFilterItem);
                                let currItem = item;
                            
                                let itemToChangeAtt = objItemLayoutI.attr(att[i].nodeName);
                                let modelPathSplited = itemToChangeAtt.split(".");

                                if (modelPathSplited.length > 1) {
                                    for (let i = 0; i < modelPathSplited.length - 1; i++) {

                                        currItem = currItem[modelPathSplited[i]];
                                    }
                                }

                                let prop = modelPathSplited[modelPathSplited.length - 1];

                                if (currItem[prop] == null ||
                                    currItem[prop] == "") {
                                    //objItemLayoutI.removeAttr(realNodeName);
                                } else {
                                    objItemLayoutI.attr(realNodeName, currItem[prop]);
                                }
                               
                                objItemLayoutI.removeAttr(att[i].nodeName);
                        }
                        };
                        
                    });
                    
                    objHtml.append(objLayout);
                });
            }
        });
    }
   
    #loadCMInnerHtml() {
        $("[cm-inner-html]").each((index, item) => {
            let model = this.Model;
            let obj = $(item);
            let propTextName = obj.attr("cm-inner-html");
            let fullPropPath = "this.Model." + propTextName;
            let modelLevels = propTextName.split(".");

            if (modelLevels.length > 1) {
                model = model[modelLevels[0]];
                if (modelLevels.length > 2) {
                    for (let i = 1; i < modelLevels.length - 1; i++) {
                        model = model[modelLevels[i]];
                    }
                }
                propTextName = modelLevels[modelLevels.length - 1];
            }

            if (propTextName !== undefined &&
                propTextName !== null &&
                propTextName.trim() !== "") {
                this.#processGeneralHtmlNodesHtml(obj, eval(fullPropPath));
            }
        });
    }

    #loadCMClick() {
        $("[cm-click]").each((index, item) => {
            let model = this.Model;
            let obj = $(item);
            let eventClick = obj.attr("cm-click");      
            

            if (eventClick !== undefined &&
                eventClick !== null &&
                eventClick.trim() !== "") {

                obj.click(() => {                    
                    eval(eventClick);
                });
            }
        });
    }       
    
    #loadCMInnerText() {
        $("[cm-inner-text]").each((index, item) => {
            let model = this.Model;
            let obj = $(item);
            let propTextName = obj.attr("cm-inner-text");
            let fullPropPath = "this.Model." + propTextName;
            let modelLevels = propTextName.split(".");

            if (modelLevels.length > 1) {

                model = model[modelLevels[0]];

                if (modelLevels.length > 2) {

                    for (let i = 1; i < modelLevels.length - 1; i++) {
                        model = model[modelLevels[i]];
                    }

                }

                propTextName = modelLevels[modelLevels.length - 1];
            }

            if (propTextName !== undefined &&
                propTextName !== null &&
                propTextName.trim() !== "") {
                this.#processGeneralHtmlNodesText(obj, eval(fullPropPath));
            }
        });
    }

    #loadList() {
        $("[cm-list]").each((index, item) => {
            let model = this.Model;
            let obj = $(item);
            let propTextName = obj.attr("cm-list");
            let fullPropPath = "this.Model." + propTextName;
            let modelLevels = propTextName.split(".");

            if (modelLevels.length > 1) {

                model = model[modelLevels[0]];

                if (modelLevels.length > 2) {

                    for (let i = 1; i < modelLevels.length - 1; i++) {
                        model = model[modelLevels[i]];
                    }

                }

                propTextName = modelLevels[modelLevels.length - 1];
            }

            if (propTextName !== undefined &&
                propTextName !== null &&
                propTextName.trim() !== "") {

                switch (obj.prop('nodeName')) {

                    case "SELECT":
                        this.#processSelectsLists(obj,
                            fullPropPath,
                            obj.attr("cm-list-value"),
                            obj.attr("cm-list-text"),
                            obj.attr("cm-list"));
                        break;

                }
            }
        });
    }

    #loadBinding() {
        $("[cm-binding]").each((index, item) => {

            let model = this.Model;
            let obj = $(item);
            let propTextName = obj.attr("cm-binding");
            let fullPropPath = "this.Model." + propTextName;
            let modelLevels = propTextName.split(".");

            if (modelLevels.length > 1) {

                model = model[modelLevels[0]];

                if (modelLevels.length > 2) {

                    for (let i = 1; i < modelLevels.length - 1; i++) {
                        model = model[modelLevels[i]];
                    }

                }

                propTextName = modelLevels[modelLevels.length - 1];
            }

            if (propTextName !== undefined &&
                propTextName !== null &&
                propTextName.trim() !== "") {

                switch (obj.prop('nodeName')) {

                    case "INPUT":
                        this.#processInputModels(obj, fullPropPath);
                        break;

                    case "TEXTAREA":
                        this.#processTextAreaModels(obj, fullPropPath);
                        break;

                    case "SELECT":
                        this.#processSelectsModels(obj, fullPropPath);
                        break;

                }
            }
        });
    }

    #init() {
        this.#createvirtualDOM();
        this.#loadCMEachs();
        this.#loadCalangoAttributes();
        this.#loadCMInnerHtml();
        this.#loadCMInnerText();
        this.#loadList();
        this.#loadBinding();
        this.#loadCMClick();
        this.#removeCalangoAttributes();        
    }

    #convertToProxy(arrPtop) {
        const objectLimitLevels = 64;//IN THE FUTURE I WILL ADD AN OPTION TO ADD MORE LEVELS
        arrPtop = [];

        let objStr = "";

        for (let i = 1; i < objectLimitLevels; i++) {
            objStr += "let modelLev" + i + " = this.#getPropsObject(this.Model" + this.#writePropsOrder(i - 1) + ");\n";
            objStr += "modelLev" + i + ".map((lev" + i + ") => {\n";
            //    arrPtop.push({
            objStr += this.#getParamNames(this.#convertToProxy)[0] + ".push({\n";
            //    model: this.Model[lev1],
            objStr += "model: this.Model" + this.#writePropsOrder(i) + ",\n";
            //    levStr: "this.Model." + lev1,
            objStr += "levStr: 'this.Model.' + " + this.#writeSequenceLevels(i) + ",\n";
            //    proxy: this.#createProxyHandler(lev1)
            objStr += " proxy: this.#createProxyHandler(" + this.#writeSequenceLevels(i) + ")\n";
            //});
            objStr += "});\n";             
        }
        //Close all open "});"
        for (let i = 1; i < objectLimitLevels; i++) {
            objStr += "});\n"
        }
        //ONLY FOR DEBUG
        //try {
            eval(objStr);
        //} catch (e) {
        //    alert(e);
        //}
      
        arrPtop.reverse().map((o) => {
            this.#processProxy(o);
        });

        this.Model = new Proxy(this.Model, this.#createProxyHandler(""));
    }
       
    #processProxy(arrProxyitem) {       
        let objectName = this.#getParamNames(this.#processProxy);       
        eval(arrProxyitem.levStr + " = new Proxy( " + arrProxyitem.levStr + ", " + objectName + ".proxy);");
    }  

    #createProxyHandler(nomeModel) { 
        let modelProxyHand = {
            get: (target, key) => {
                return target[key];
            },
            set: (target, key, val) => {
                let params = this.#getParamNames(modelProxyHand.set);
                let propertyMounted = "";
                let isArrayObject = Array.isArray(target[key]);               

                    if (nomeModel !== "")
                        propertyMounted = nomeModel + "." + key;
                    else
                        propertyMounted = key;
               
                    let objToChanged = this.#getObjectFromVirtualDOM(propertyMounted, "cm-binding");
                    if (objToChanged != null) {
                            this.#processControls(objToChanged, val);
                    }
          
                    if (isArrayObject) {
                   
                        if (nomeModel !== "")
                            propertyMounted = nomeModel + "." + key;
                        else
                            propertyMounted = key;

                        let objToChanged = this.#getObjectFromVirtualDOM(propertyMounted, "cm-list");
                        if (objToChanged != null) {
                            let objSelectListParameter = this.#getObjectFromVirtualDOMToSelectValueText(propertyMounted, "cm-list");
                            this.#processArrayControls(objToChanged, val, objSelectListParameter);
                        }
                    }        

                if (propertyMounted === "")
                    return true;

                let isValid = true;
                //IN ARRAY CASES THE MODEL MODERATOR NOT IS APPLYCABLE
                //REMEMBER TO MAKE THE METHOD BE Asynchronous in the future.
                if (!isArrayObject) {
                    if (eval("this.ModelModerator." + propertyMounted.replaceAll(".","?.") + " != undefined")) {
                        
                        isValid = eval("this.ModelModerator." + propertyMounted + "(" + params[0] + "[" + params[1] + "], " + params[2] + ");");

                    }
                }

                if (isValid === false) {
                    eval("this.Model." + propertyMounted + " = " + params[0] + "[" + params[1] + "];");
                } else {
                    target[key] = val;

                    this.#processAttributeNodes(propertyMounted);

                    let objToChangedTagText = this.#getObjectFromVirtualDOM(propertyMounted, "cm-inner-text");
                    if (objToChangedTagText != null) {
                        this.#processGeneralHtmlNodesText(objToChangedTagText, val);
                    }

                    let objToChangedTagHtml = this.#getObjectFromVirtualDOM(propertyMounted, "cm-inner-html");
                    if (objToChangedTagHtml != null) {
                        this.#processGeneralHtmlNodesHtml(objToChangedTagHtml, val);
                    }

                    if (isArrayObject) {

                        if (this.#getObjectFromVirtualDOM(propertyMounted, "cm-list") != null)
                            return true;
                        
                        this.#processEachObjects(propertyMounted, val);
                    }

                }

                return true;
            }
        };

        return modelProxyHand;
    }

    #processEachObjects(propertyMounted,val) {
        let listEachObjects = this.#getObjectsEach(propertyMounted);
        //IF EXISTS ITENS
        if (listEachObjects != null && listEachObjects.length > 0) {
            //MAP ITENS FROM 
            listEachObjects.forEach((objectItem) => {
                //CREATE TEMPLATE
                let subTemplate = objectItem.subTemplate.clone();
                //CLEAR ITENS FROM HTML OBJECT
                objectItem.obj.html("");                
                //EACH ARRAY OBJECT
                val.map((item) => {              
                    //CLONE LAYOUT FOR ITEN
                    let objToAdd = subTemplate.clone();
                    //vai dar merda no minifi esse item ai eval
                    objToAdd.find("[cm-each-item-inner-text]").each((index, objI) => {
                        let currItem = item;
                        let objItemLayoutI = $(objI);
                        let itemToChangeInnerText = objItemLayoutI.attr("cm-each-item-inner-text");
                        let modelPathSplited = itemToChangeInnerText.split(".");
                        if (modelPathSplited.length > 1) {
                            for (let i = 0; i < modelPathSplited.length - 1; i++) {
                               
                                currItem = currItem[modelPathSplited[i]];
                            }
                        }
                        let prop = modelPathSplited[modelPathSplited.length - 1];                       
                        objItemLayoutI.text(currItem[prop]);
                        objItemLayoutI.removeAttr("cm-each-item-inner-text");
                    });

                    objToAdd.find("[cm-each-item-inner-html]").each((index, objItemLayout) => {
                        let currItem = item;
                        let objItemLayoutI = $(objItemLayout);
                        let itemToChangeInnerHtml = objItemLayoutI.attr("cm-each-item-inner-html");
                        let modelPathSplited = itemToChangeInnerHtml.split(".");
                        if (modelPathSplited.length > 1) {
                            for (let i = 0; i < modelPathSplited.length - 1; i++) {

                                currItem = currItem[modelPathSplited[i]];
                            }
                        }
                        let prop = modelPathSplited[modelPathSplited.length - 1];
                        objItemLayoutI.html(currItem[prop]);
                        objItemLayoutI.removeAttr("cm-each-item-inner-html");
                    });
     
                    objToAdd.find("[cm-each-item-click]").each((index, objItemLayout) => {
                        
                        let crntItem = item;
                        let objItemLayoutI = $(objItemLayout);
                        let clickEvent = objItemLayoutI.attr("cm-each-item-click");
                       
                        objItemLayoutI.click(() => {
                            let nameVar = nameOf(() => crntItem);
                            let code = "let currentItem = " + nameVar + ";";
                            code += clickEvent;
                            eval(code);
                        });

                        objItemLayoutI.removeAttr("cm-each-item-click");
                    });

                    objToAdd.find("*").filter((n, objFilterItem) => {

                        let att = objFilterItem.attributes;

                        for (let i = 0; i < att.length; i++) {
                            
                            let realNodeName = att[i].nodeName.replace("cm-each-item-att-", "");
                            if (att[i].nodeName.indexOf('cm-each-item-att-') === 0) {
                                let objItemLayoutI = $(objFilterItem);
                                let currItem = item;

                                let itemToChangeAtt = objItemLayoutI.attr(att[i].nodeName);
                                let modelPathSplited = itemToChangeAtt.split(".");

                                if (modelPathSplited.length > 1) {
                                    for (let i = 0; i < modelPathSplited.length - 1; i++) {
                                        currItem = currItem[modelPathSplited[i]];
                                    }
                                }
                                let prop = modelPathSplited[modelPathSplited.length - 1];

                                objItemLayoutI.attr(realNodeName, currItem[prop]);
                                objItemLayoutI.removeAttr(att[i].nodeName);
                            }
                        };
                    });

                    objectItem.obj.append(objToAdd);
                });
            });
        }
    }


    #processControls(objToChanged, valor) {

        switch (objToChanged.prop('nodeName')) {
            case "INPUT":
                if (!especialInputs.includes(objToChanged.attr("type"))) {
                    if (objToChanged.val() !== valor) {
                        objToChanged.val(valor);
                    }
                } else {
                    switch (objToChanged.attr("type")) {
                        case "checkbox":
                            if (objToChanged.is(":checked") !== valor) {
                                objToChanged.prop("checked", valor);
                            }
                            break;

                        case "radio":
                            for (let i = 0; i < objToChanged.length; i++) {
                                let itemRadio = $(objToChanged[i]);
                                if (itemRadio.val() === valor) {
                                    itemRadio.prop("checked", true);
                                } else {
                                    itemRadio.prop("checked", false);
                                }
                            }
                            break;
                    }
                }
                break;

            case "SELECT":
                if (objToChanged.attr("default-select") === "false") {
                    if (objToChanged.val() !== valor) {
                        objToChanged.val(valor);
                    }
                } else {
                    if (objToChanged.val() !== valor) {
                        objToChanged.val(valor);
                        objToChanged.selectpicker('refresh');//REMOVE THIS IN FUTURE, ITS TO BOOTSTRAP SELECT ONLY;
                    }
                }
                break;

            case "TEXTAREA":

                if (objToChanged.val() !== valor) {
                    objToChanged.val(valor);
                }
                break;

        }
    }

    #processArrayControls(objToChanged, valor, objSelectListParameter) {
        switch (objToChanged.prop('nodeName')) {         
                case "SELECT":
                if (objToChanged.attr("default-select") === "false") {
                    if (objToChanged.val() !== valor) {
                        objToChanged.find("option").remove();
                        let propVal = objSelectListParameter.propertyToValue;
                        let propText = objSelectListParameter.propertyToText;
                        valor.map((item) => {
                            let opt = "<option value='" + item[propVal] + "'>" + item[propText] + "</option>";
                            objToChanged.append(opt);
                        });
                        objToChanged.change();
                    }
                    
                } else {
                    if (objToChanged.val() !== valor) {
                        objToChanged.find("option").remove();
                        let propVal = objSelectListParameter.propertyToValue;
                        let propText = objSelectListParameter.propertyToText;
                        valor.map((item) => {
                            let opt = "<option value='" + item[propVal] + "'>" + item[propText] + "</option>";
                            objToChanged.append(opt);
                        });
                        objToChanged.change();
                        objToChanged.selectpicker('refresh');//REMOVE THIS IN FUTURE, ITS TO BOOTSTRAP SELECT ONLY;
                    }
                }
                break;

        }
    }

    #processInputModels(obj, fullPropPath) {

        let params = this.#getParamNames(this.#processInputModels);

        if (!especialInputs.includes(obj.attr("type"))) {

            if (eval(fullPropPath) !== null &&
                eval(fullPropPath) !== undefined) {

                obj.val(eval(fullPropPath));

            } else {

                eval(fullPropPath + " = " + params[0] + ".val();");
            }
            
            obj.on("input",
                () => {
                    if (eval(fullPropPath) !== obj.val()) {
                        eval(fullPropPath + " = " + params[0] + ".val();");
                    }
                });

        } else {

            switch (obj.attr('type')) {

                case "checkbox":
                  
                    if (eval(fullPropPath) !== null &&
                        eval(fullPropPath) !== undefined) {

                        obj.prop("checked", eval(fullPropPath));

                    } else {

                        eval(fullPropPath + " = " + params[0] + ".is(':checked');");

                    }

                    obj.on("input",
                        () => {
                            if (eval(fullPropPath) !== obj.is(":checked")) {
                                eval(fullPropPath + " = " + params[0] + ".is(':checked');");
                            }
                        });

                    break;

                case "radio":

                    for (let i = 0; i < obj.length; i++) {

                        let itemRadio = $(obj[i]);

                        if (eval(fullPropPath) !== null &&
                            eval(fullPropPath) !== undefined) {

                            if (itemRadio.val() === eval(fullPropPath)) {
                                itemRadio.prop("checked", true);
                            }

                        } else {

                            if (itemRadio.is(":checked")) {
                                eval(fullPropPath + " = " + "$(" + params[0] + "[" + i + "])" + ".val();")
                            }

                        }

                        itemRadio.on("input",
                            () => {
                                if (eval(fullPropPath) !== itemRadio.val()) {
                                    eval(fullPropPath + " = " + "$(" + params[0] + "[" + i + "])" + ".val();")
                                }
                            });
                    }

                    break;
            }
        }
    }

    #processTextAreaModels(obj, fullPropPath) {
        let params = this.#getParamNames(this.#processTextAreaModels);
        if (eval(fullPropPath) !== null &&
            eval(fullPropPath) !== undefined) {

            obj.val(eval(fullPropPath));

        } else {

            eval(fullPropPath + " = " + params[0] + ".val();");

        }

        obj.on("input",
            () => {
                if (eval(fullPropPath) !== obj.val()) {
                    eval(fullPropPath + " = " + params[0] + ".val();");
                }
            });
    }

    #processSelectsModels(obj, fullPropPath) {      
        let params = this.#getParamNames(this.#processSelectsModels);
        if (obj.attr("default-select") === "false") {

            if (eval(fullPropPath) !== null &&
                eval(fullPropPath) !== undefined) {

                obj.val(eval(fullPropPath));

            } else {

                eval(fullPropPath + " = " + params[0] + ".val();");

            }

        } else {

            if (eval(fullPropPath) !== null &&
                eval(fullPropPath) !== undefined) {

                obj.val(eval(fullPropPath));
                obj.selectpicker('refresh');

            } else {

                eval(fullPropPath + " = " + params[0] + ".val();");

            }

        }

        obj.on("change",
            () => {
                if (eval(fullPropPath) !== obj.val()) {       
                    eval(fullPropPath + " = " + params[0] + ".val();");
                }
            });
    }

    #processSelectsLists(obj, fullPropPath, propVal, propText, objModelList) {
        
        let listItens = eval("this.Model." + objModelList);

        if (obj.attr("default-select") === "false") {

            if (eval(fullPropPath) !== null &&
                eval(fullPropPath) !== undefined) {
                obj.find("option").remove();
                listItens.map((item) => {
                    let opt = "<option value='" + item[propVal] + "'>" + item[propText] + "</option>";
                    obj.append(opt);
                });

            }

        } else {

            if (eval(fullPropPath) !== null &&
                eval(fullPropPath) !== undefined) {
                obj.find("option").remove();

                listItens.map((item) => {
                    let opt = "<option value='" + item[propVal] + "'>" + item[propText] + "</option>";
                    obj.append(opt);
                });

                obj.selectpicker('refresh');

            }
        }

    }

    #processGeneralHtmlNodesText(obj, val) {
        obj.text(val);      
    }

    #processGeneralHtmlNodesHtml(obj, val) {
        obj.html(val);
    }

    #processAttributeNodes(propertyMounted) {
        virtualDOM.map((item) => {
   
            if (item.model === propertyMounted &&       
                item.attribute.includes("cm-att-")) {                
                let realTagName = item.attribute.replace("cm-att-", "");
              
                if (eval(item.fullModel) == null ||
                    eval(item.fullModel) == "") {
                    item.obj.removeAttr(realTagName);
                } else {
                    item.obj.attr(realTagName, eval(item.fullModel));
                }                
            }
        });
    }

    #getObjectFromVirtualDOM(propertyMounted, attribute) {    
        let arr = virtualDOM;
        let objectSelected = arr.filter((item) => {
     
            if (item.model == propertyMounted && item.attribute == attribute)
                return true;
            else
                return false;
        });
        return objectSelected[0]?.obj;
    }       

    #getObjectsEach(propertyMounted) {
        let arr = virtualDOM;  
        let objects = arr.filter((item) => {

            if (item.model == propertyMounted && item.attribute == "cm-each")
                return true;
            else
                return false;
        });       

        return objects;
    }

    #getObjectFromVirtualDOMToSelectValueText(propertyMounted, attribute) {
        let arr = virtualDOM;
        let objectSelected = arr.filter((item) => {
            if (item.model == propertyMounted && item.attribute == attribute)
                return true;
            else
                return false;
        });
        return objectSelected[0]?.selectListParameters == null ? $() : objectSelected[0].selectListParameters;
    }

    #getParamNames(func) {
     
        var fnStr = func.toString().replace(stripComents, '');
        var result = fnStr.slice(fnStr.indexOf('(') + 1, fnStr.indexOf(')')).match(argumentNames);
        if (result === null)
            result = [];
        return result;
    }

    //THE METHOD BELOW IS USED BY THE EVAL METHOD.(#convertToProxy)
    #getPropsObject(object) {

        let ret = [];

        let objectLst = Object.getOwnPropertyNames(object);

        objectLst.map(o => {

            if (typeof object[o] === "object" && !Array.isArray(object[o])) {
         
                ret.push(o);
            }

        });

        return ret;
    }

    #writePropsOrder(index) {

        let strIndex = "";

        for (let i = 1; i < (index + 1); i++) {

            strIndex += "[lev" + i + "]";
        }

        return strIndex;
    }

    #writeSequenceLevels(index) {

        let strIndex = "";

        for (let i = 1; i < (index + 1); i++) {

            strIndex += "lev" + i + " + '.' + ";
        }

        return strIndex.substring(0, strIndex.length - 9);
    }

    //#endregion private methods
}
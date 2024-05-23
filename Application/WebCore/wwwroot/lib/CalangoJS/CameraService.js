export default class CameraService {
    constructor() {
        
    }

    listAllCamInSelect(select) {
        try {          

            Instascan.Camera.getCameras().then(cameras => {
                if (cameras.length > 0) {
                    for (var i = 0; i < cameras.length; i++) {
                        select.append($('<option>', {
                            value: i,
                            text: 'Camera ' + i
                        }));
                    }
                }
            });
            select.selectpicker('refresh');
        } catch (e) {
            //sss
        }

    }
}
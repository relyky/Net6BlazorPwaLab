﻿
function QRCodeModule(elementId /* string */) {
  const targetElement = document.getElementById(elementId);
  const qrcodeReader = new Html5Qrcode(elementId, { formatsToSupport: [Html5QrcodeSupportedFormats.QR_CODE] });

  return {
    startScan: (dotNetObject /* object */, f_readStop /* boolean */) => {
      //## 開始掃描
      targetElement.style.visibility = 'visible';
      qrcodeReader.start(
        { facingMode: "environment" }, // to prefer back camera
        {
          fps: 10,    // Optional frame per seconds for qr code scanning
          qrbox: 250  // Optional if you want bounded box UI
        },
        qrCodeMessage => {
          //console.info('Code is read', qrCodeMessage);

          if (f_readStop) {
            qrcodeReader.stop();
            targetElement.style.visibility = 'hidden';
          }

          dotNetObject.invokeMethodAsync('OnScanResponse', 'SUCCESS', qrCodeMessage);
        },
        errorMessage => {
          // console.warn('Parse error, ignore it.', errorMessage);
          dotNetObject.invokeMethodAsync('OnScanResponse', 'WARN', errorMessage);
        }
      ).catch(err => {
        console.error('Start failed, handle it.', err);
        dotNetObject.invokeMethodAsync('OnScanResponse', 'ERROR', 'Start failed! ' + JSON.stringify(err));
      });
    },
    stopScan: (dotNetObject /* object */) => {
      //## 停止掃描
      qrcodeReader.stop().then((ignore) => {
        // console.log('QR Code scanning is stopped.', targetElementId);
        dotNetObject.invokeMethodAsync('OnScanResponse', 'STOP', 'QR Code scanning is stopped.');
        targetElement.style.visibility = 'hidden';
      }).catch((err) => {
        // Stop failed, handle it.
        console.error('stopScan Exception!', err);
        dotNetObject.invokeMethodAsync('OnScanResponse', 'ERROR', 'Stop failed! ' + JSON.stringify(err));
      });
    },
  };

}

export { QRCodeModule };

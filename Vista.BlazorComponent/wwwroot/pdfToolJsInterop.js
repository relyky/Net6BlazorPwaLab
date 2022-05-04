/// 參考：[PDF.js](https://mozilla.github.io/pdf.js/getting_started/)

export function init() {
  console.log('pdfToolJsInterop init');


  // Loaded via <script> tag, create shortcut to access PDF.js exports.
  const pdfjsLib = window['pdfjs-dist/build/pdf'];

  // The workerSrc property shall be specified.
  //pdfjsLib.GlobalWorkerOptions.workerSrc = '//mozilla.github.io/pdf.js/build/pdf.worker.js';
  pdfjsLib.GlobalWorkerOptions.workerSrc = '_content/Vista.BlazorComponent/pdfjs-dist/pdf.worker.min.js';
}

export function renderPdf(url) {
  // Loaded via <script> tag, create shortcut to access PDF.js exports.
  const pdfjsLib = window['pdfjs-dist/build/pdf'];

  // Asynchronous download of PDF
  const loadingTask = pdfjsLib.getDocument(url);
  loadingTask.promise.then(function (pdf) {

    // Fetch the first page
    const pageNumber = 1;
    pdf.getPage(pageNumber).then(function (page) {
      console.log('renderPdf', 'Page loaded');

      const scale = 1.5;
      const viewport = page.getViewport({ scale: scale });

      // Prepare canvas using PDF page dimensions
      const canvas = document.getElementById('the-canvas');

      const context = canvas.getContext('2d');
      canvas.height = viewport.height;
      canvas.width = viewport.width;

      // Render PDF page into canvas context
      const renderContext = {
        canvasContext: context,
        viewport: viewport
      };
      const renderTask = page.render(renderContext);
      renderTask.promise.then(function () {
        console.log('renderPdf', 'Page rendered');
      });
    });
  }, function (reason) {
    // PDF loading error
    console.error('renderPdf', reason);
  });

}
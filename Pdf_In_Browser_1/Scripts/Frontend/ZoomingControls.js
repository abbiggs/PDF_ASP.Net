var zoomFactor = 1.0;
var toolZoom = 1.0;
let customViewerL = $("#MainContent_customViewerL");
let toolbar = $("#MainContent_actionButtonsContainer");


function zoomIn() {
    let body = $('body');

    zoomFactor += .1;
    
    if (zoomFactor <= 1.5) {

        body.css('transform', 'scale(' + zoomFactor + ')');

    } else {
        zoomFactor -= 0.5;
        body.css('transform', 'scale(' + zoomFactor + ')');
        toolbar.css('transform', 'scale(' + zoomFactor + ')');
        zoomFactor = 1.0;
        toolZoom = 1.0;
        return false;
    }

    if (zoomFactor <= 1.3) {
        toolZoom -= .08;
        toolbar.css('transform', 'scale(' + toolZoom + ')');
    } else {
        toolZoom -= .06;
        toolbar.css('transform', 'scale(' + toolZoom + ')');
    }

    return false;
}

function zoomOut() {
    let body = $('body');

    if (zoomFactor > 1.0) {
        zoomFactor -= .1;

        if (zoomFactor >= 1.0) {

            body.css('transform', 'scale(' + zoomFactor + ')');

        } else {

            return false;
        }

        if (zoomFactor > 0.9) {
            toolZoom += .08;
            toolbar.css('transform', 'scale(' + toolZoom + ')');
        } else if (zoomFactor >= 1.24) {
            toolZoom += .06;
            toolbar.css('transform', 'scale(' + toolZoom + ')');
        }
    }


    return false;
} 
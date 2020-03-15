var zoomFactor = 1.0;

function zoomIn() {
    //zoom in max: 2.0
    let element = $('#MainContent_customViewerL');
    zoomFactor += .1;
    headerZoom = zoomFactor - .2;

    if (zoomFactor <= 2.0) {
        element.css('transform', 'scale(' + zoomFactor + ')');
    } else {
        zoomFactor -= 1;
    }
    
    return false;
}

function zoomOut() {
    //zoom out max is 0.4
    let element = $('#MainContent_customViewerL');
    zoomFactor -= .1;
    
    if (zoomFactor >= 0.4) {
        element.css('transform', 'scale(' + zoomFactor + ')');
    } else {
        zoomFactor += 1;
    }
}
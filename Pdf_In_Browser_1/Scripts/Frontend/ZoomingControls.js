var zoomFactor = 1.0;

function zoomIn() {
    let body = $('body');
    
    zoomFactor += .1;
    containerMargin = ((zoomFactor - 1.0) * 100);

    if (zoomFactor <= 3.0) {
        //customViewerL.css('transform', 'scale(' + zoomFactor + ')');
        //customContainer.css('margin-top', '' + containerMargin + '%');

        body.css('transform', 'scale(' + zoomFactor + ')');

        //let toolbarZoom = (zoomFactor * 0.5);
        //toolbar.css('transform', 'scale(' + toolbarZoom + ')');
    } else {
        zoomFactor -= 1;
    }
    
    return false;
}

function zoomOut() {
    //zoom out max is 0.4
    //let element = $('#MainContent_customViewerL');
    let element = $('body')
    zoomFactor -= .1;
    
    if (zoomFactor >= 0.4) {
        element.css('transform', 'scale(' + zoomFactor + ')');
    } else {
        zoomFactor += 1;
    }
}
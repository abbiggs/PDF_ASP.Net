var redacting = false;
var firstPointSet = false;
const xPoints = [];
const yPoints = [];
var lastClickedPage;

function activateRedaction() {

    if (redacting != true) {

        redacting = true;
        document.getElementById("MainContent_customViewerL").addEventListener("click", setPosition);

        $(".pageDiv").click(function (event) {
            lastClickedPage = this.id;
        });

    } else {

        disableRedaction();
    }

    return false;
}

function setPosition(e) {
    e = e || window.event;

    if (firstPointSet == false) {

        xPoints[0] = e.clientX;
        yPoints[0] = e.clientY + (window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0);

        firstPointSet = true;

    } else {

        xPoints[1] = e.clientX;
        yPoints[1] = e.clientY + (window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0);

        configuringRedaction();
    }

    return false;
}

function configuringRedaction() {

    let width, height, xPos, yPos;

    let pageHeight = $("#" + lastClickedPage).height();
    let pageWidth = $("#" + lastClickedPage).width();
    let containerWidth = $("#MainContent_customViewerL").width() + parseInt($("#MainContent_customViewerL").css("marginLeft").replace("px", ""));

    let offsetHeight = $("#" + lastClickedPage).offset().top;
    let offsetWidth = containerWidth - pageWidth;

    xPos = Math.min(xPoints[0], xPoints[1]);
    yPos = Math.min(yPoints[0], yPoints[1]) - offsetHeight;

    width = Math.max(xPoints[0], xPoints[1]) - Math.min(xPoints[0], xPoints[1]);
    height = Math.max(yPoints[0], yPoints[1]) - Math.min(yPoints[0], yPoints[1]);

    xPos = ((xPos - offsetWidth) / (pageWidth + 10)) * 100;
    yPos = (yPos / (pageHeight - 10)) * 100;

    width = (width / (pageWidth + 10)) * 100;
    height = (height / (pageHeight - 10)) * 100;
    
    addElement(xPos, yPos, width, height);
}

function addElement(x, y, width, height) {
    var redaction = document.createElement("p");
    var style = "top: " + y + "%; left: " + x + "%; width: " + width + "%; height: " + height + "%;";

    disableRedaction();

    redaction.setAttribute("class", "redaction");
    redaction.setAttribute("style", style);

    document.getElementById(lastClickedPage).appendChild(redaction);

    return false;
}

function disableRedaction() {

    if (redacting == true) {

        redacting = false;
        document.getElementById("MainContent_customViewerL").removeEventListener("click", setPosition);
    }

    firstPointSet = false;
    xPoints[0] = 0;
    xPoints[1] = 0;
    yPoints[0] = 0;
    yPoints[1] = 0;

    return false;
}
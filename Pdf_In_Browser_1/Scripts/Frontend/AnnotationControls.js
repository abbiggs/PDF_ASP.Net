var redacting = false;
var firstPointSet = false;
const xPoints = [];
const yPoints = [];

function activateRedaction() {

    if (redacting != true) {

        redacting = true;
        document.getElementById("MainContent_customViewerL").addEventListener("click", setPosition);

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

    xPos = Math.min(xPoints[0], xPoints[1]);
    yPos = Math.min(yPoints[0], yPoints[1]);

    width = Math.max(xPoints[0], xPoints[1]) - Math.min(xPoints[0], xPoints[1]);
    height = Math.max(yPoints[0], yPoints[1]) - Math.min(yPoints[0], yPoints[1]);

    addElement(xPos, yPos, width, height);
}

function addElement(x, y, width, height) {
    var redaction = document.createElement("p");
    var style = "top: " + y + "px; left: " + x + "px; width: " + width + "px; height: " + height + "px;";

    redaction.setAttribute("class", "redaction");
    redaction.setAttribute("style", style);
    //redaction.innerHTML = "X: " + xPoints[0] + " | " + xPoints[1] + " Y: " + yPoints[0] + " | " + yPoints[1];

    document.getElementById("MainContent_customContainer").appendChild(redaction);

    disableRedaction();

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
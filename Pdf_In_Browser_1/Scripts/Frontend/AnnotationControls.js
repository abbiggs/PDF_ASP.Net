var redacting = false;

function activateRedaction() {

    if (redacting != true) {

        redacting = true;
        document.getElementById("MainContent_customViewerL").addEventListener("click", setPosition);
    }

    return false;
}

function setPosition(e) {
    e = e || window.event;

    var xPos = e.clientX;
    var yPos = e.clientY;

    addElement(xPos, yPos);

    return false;
}

function addElement(x, y) {
    var redaction = document.createElement("p");
    var style = "top: " + y + "px; left: " + x + "px;";

    redaction.setAttribute("class", "redaction");
    redaction.setAttribute("style", style);
    redaction.innerHTML = "X: " + x + "  Y: " + y;

    document.getElementById("MainContent_customContainer").appendChild(redaction);

    disableRedaction();

    return false;
}

function disableRedaction() {

    if (redacting == true) {

        redacting = false;
        document.getElementById("MainContent_customViewerL").removeEventListener("click", setPosition);
    }

    return false;
}
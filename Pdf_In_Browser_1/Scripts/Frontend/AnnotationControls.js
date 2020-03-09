var redacting = false;
var container = document.getElementsByClassName("leftImageContainer");

function activateRedaction() {

    if (redacting != true) {

        redacting = true;
        container.addEventListener("click", setPosition, false);
    }

    return false;
}

function setPosition(e) {
    var xPos = e.clientX;
    var yPos = e.clientY;

    addElement(xPos, yPos);

    return false;
}

function addElement(x, y) {
    var redaction = document.createElement("span");

    redaction.setAttribute("class", "redaction");
    redaction.style.top = yPos;
    redaction.style.left = xPos;

    container.appendChild(redaction);

    return false;
}
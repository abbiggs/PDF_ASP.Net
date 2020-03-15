var redacting = false;
var container = document.getElementsByClassName("leftImageContainer");

document.addEventListener("click", setPosition);

function activateRedaction() {

    if (redacting != true) {

        redacting = true;
        container.addEventListener("click", setPosition);
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
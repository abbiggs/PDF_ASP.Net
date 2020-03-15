var redacting = false;

document.getElementById("MainContent_customViewerL").addEventListener("click", setPosition);

function activateRedaction() {

    if (redacting != true) {

        redacting = true;
        container.addEventListener("click", setPosition);
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

    redaction.setAttribute("class", "redaction");
    redaction.style.top = y;
    redaction.style.left = x;
    redaction.innerHTML = "X: " + x + "  Y: " + y;

    document.getElementById("MainContent_customContainer").appendChild(redaction);

    return false;
}
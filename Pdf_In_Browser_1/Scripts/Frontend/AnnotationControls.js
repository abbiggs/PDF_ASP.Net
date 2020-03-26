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

            if (redacting == true) {
                //For setting height values
                if (firstPointSet == false) {
                    //yPoints[0] = event.clientY;
                } else {
                    //yPoints[1] = event.clientY;
                }
            }
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
        //yPoints[0] = e.clientY;

        firstPointSet = true;

    } else {

        xPoints[1] = e.clientX;
        yPoints[1] = e.clientY + (window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0);
        //yPoints[1] = e.clientY;

        configuringRedaction();
    }

    return false;
}

function configuringRedaction() {

    let width, height, xPos, yPos;

    //let screenHeight = (window.screenY || document.documentElement.scrollHeight || document.body.scrollHeight || 0);
    //let screenHeight = window.innerHeight;
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
    //xPos = (xPos / pageWidth) * 100;
    yPos = (yPos / (pageHeight - 10)) * 100;

    width = (width / (pageWidth + 10)) * 100;
    height = (height / (pageHeight - 10)) * 100;
    //height = 20;

    let info = " xOffset: " + offsetWidth + " Container: " + containerWidth + " pageWidth: " + pageWidth + " Margin: " + $("#MainContent_customViewerL").css("marginLeft") + " Y: " + Math.min(yPoints[0], yPoints[1]) + " height: " + pageHeight;

    addElement(xPos, yPos, width, height, info);
}

function addElement(x, y, width, height, info) {
    var redaction = document.createElement("p");
    var style = "top: " + y + "%; left: " + x + "%; width: " + width + "%; height: " + height + "%;"; //% has bugs with Y. As you scroll down, the document grows, so 20% is now bigger. vh doesn't work

    disableRedaction();

    redaction.setAttribute("class", "redaction");
    redaction.setAttribute("style", style);
    //redaction.innerHTML = "yPos: " + y + " height: " + height + " scrollHeight: " + (window.screenY || document.documentElement.scrollHeight || document.body.scrollHeight || 0);
    //redaction.innerHTML += info;

    //document.getElementById("MainContent_customContainer").appendChild(redaction); //Added here, because it has issues if I put it in div with pages. Maybe add to last clicked page div?
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
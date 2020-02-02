


function jumpToPage(event) {

    //Executes on enter key press event
    if (event.which == 13 || event.keyCode == 13) {

        try {

            var pageNum = document.getElementById("manualPageInput").value;
            var actualPageNum = pageNum - 1;

            document.getElementById("MainContent_img" + actualPageNum).scrollIntoView();

            return false;

        } catch (error) {

            alert("Page Number Out Of Bounds.");

            return false;
        }
    }
}
function testAPI() {

    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            let newDiv = document.createElement("div");
            let newImg = document.createElement("img");
            
            newImg.src = response;
            document.getElementById("MainContent_customViewerL").appendChild(newImg);

        }
    });

    return false;
}

function loadNextPage() {
    var pageCount = document.getElementById("MainContent_customViewerL").children.length;

    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?pageNum=" + pageCount + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            let newDiv = document.createElement("div");
            let newImg = document.createElement("img");
            newImg.src = response;
            newDiv.appendChild(newImg);
            document.getElementById("MainContent_customViewerL").appendChild(newDiv);
        }
    });

    return false;
}

//Keeps track of the last child to have entered the view, as to not execute loadNextPage() multiple times per element.
var prevChild = null;
$(window).scroll(function () {
    //This function is used to detect if the element is scrolled into view
    function elementScrolled(elem) {
        var docViewTop = $(window).scrollTop();
        var docViewBottom = docViewTop + $(window).height();
        var elemTop = $(elem).offset().top;
        return ((elemTop <= docViewBottom) && (elemTop >= docViewTop));
    }

    var pageTotal = getPdfPageTotal();
    var parentElement = document.getElementById("MainContent_customViewerL");
    var childCount = parentElement.children.length;
    var childElement = parentElement.children.item(childCount - 1);
    if (elementScrolled(childElement) && childElement != prevChild && childCount < pageTotal) {

        prevChild = childElement;
        loadNextPage();
        
    }
});

function getPdfPageTotal() {
    let pageTotal = document.getElementById("MainContent_pageCount").innerText;
    let endIndex = null;
    for (var i = 0; i < pageTotal.length; i++) {
        if (pageTotal.charAt(i) == " ") {
            endIndex = i;
            break;
        }
    }
    pageTotal = pageTotal.substring(1, i);


    return pageTotal;
}

function pageUp() {
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}
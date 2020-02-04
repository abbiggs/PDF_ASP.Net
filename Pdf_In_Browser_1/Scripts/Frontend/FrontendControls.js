
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

//API call to load the next available page
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
//Loads the next available page when the final loaded page is scrolled into view
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

//Pulls the total number of pages from the page count label
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

//Pulls the filename from the page count label
function getFileName() {
    let filename = null;
    let labelText = document.getElementById("MainContent_pageCount").innerText;
    let startIndex = null;
    for (var i = 0; i < labelText.length; i++) {
        if (labelText.charAt(i) == " ") {
            startIndex = i;
            break;
        }
    }
    filename = labelText.substring(i + 1, labelText.length);

    return filename;
}

function testClientClick() {
    
    $.ajax({
        type: "POST",
        url: "api/PdfPageAPI?filename=" + getFileName() + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
        }
    });

    return false;
}

//API call to save the first two images from the current document as images
function saveFirstPages() {
    $.ajax({
        type: "POST",
        url: "api/PdfPageAPI?filename=" + getFileName() + "_f",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            loadFirstPages();
            saveAllPages();
        }
    });

    return false;
}



//API call to save all the pages from the current document as images
function saveAllPages() {
    $.ajax({
        type: "POST",
        url: "api/PdfPageAPI?filename=" + getFileName() + "_a",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //loadFirstPages();
        }
    });

    //loadFirstPages();

    return false;
}

//API call to retrieve the first two pages of the current document
function loadFirstPages() {
    let pageNum = 0

    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?pageNum=" + pageNum + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            let newDiv = document.createElement("div");
            let newImg = document.createElement("img");
            newImg.src = response;

            newDiv.appendChild(newImg);
            document.getElementById("MainContent_customViewerL").appendChild(newDiv);

            pageNum += 1;
            if (pageNum <= 1) {
                loadPage(pageNum);
            }
            
        },
        failure: function (response) {
            loadPage(pageNum);
        }
    });
    return false;
}

//API call that takes a page number as parameter to load a specific page of the current document
function loadPage(pageNum) {
    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?pageNum=" + pageNum + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            let newDiv = document.createElement("div");
            let newImg = document.createElement("img");
            newImg.src = response;
            newDiv.appendChild(newImg);
            document.getElementById("MainContent_customViewerL").appendChild(newDiv);
        },
        failure: function (response) {
            alert("failure");
            loadPage(pageNum);
        }
    });
    return false;
}

function pageUp() {
    alert("!" + getFileName() + "!");
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}
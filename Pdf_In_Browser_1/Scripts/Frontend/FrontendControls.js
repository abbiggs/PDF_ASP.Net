var errorMsgDiv = document.getElementById("errorMsgDiv");
var scrollAnchor = null;
var children = null;

//#region scrollEvents

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
    if (pageTotal < 2) {
        return false;
    }
    var parentElement = document.getElementById("MainContent_customViewerL");
    if (children == null) {
        var childCount = parentElement.children.length;
    } else {
        childCount = children;
    }

    var childElement = parentElement.children.item(childCount - 1);

    if (elementScrolled(childElement) && childElement != prevChild && childCount < pageTotal) {

        prevChild = childElement;
        children = null;
        loadNextPage();

    }
});

function jumpToPage(event) {

    //Executes on enter key press event
    if (event.which == 13 || event.keyCode == 13) {

        try {

            //Checks that the user entered page number is within the page bounds
            var pageNum = document.getElementById("manualPageInput").value;
            if (pageNum <= getPdfPageTotal() && pageNum > 0) {
                var actualPageNum = pageNum - 1;

                //Checks whether or not the page is already loaded on the screen
                var page = document.getElementById("page" + actualPageNum);
                if (page != null) {
                    page.scrollIntoView()
                } else if (page == null) {
                    var totalPages = document.getElementById("MainContent_customViewerL").children.length;
                    if (pageNum == getPdfPageTotal()) {
                        $(window).unbind("scroll");
                        loadPageOutOfSync(actualPageNum, totalPages);
                    } else {
                        loadPageOutOfSync(actualPageNum, totalPages);
                    }
                }

                return false;
            }

            if (pageNum <= 0 || pageNum > getPdfPageTotal()) {
                alert("Page Number Out Of Bounds.")
                return false;
            }

        } catch (error) {

            alert(error);

            return false;
        }
    }
}
//#endregion

//#region asyncPageLoading
//API call to load the next available page
function loadNextPage() {

    var pageCount = document.getElementById("MainContent_customViewerL").children.length;
    GetPage(pageCount);

    return false;
}

//This is the function that makes an API call and returns img path and text data
//It's already returning the correct info, we just stick it in the front end
function loadFirstPages() {
    let pageNum = 0;

    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?filename=" + pageNum + "_" + getFileName() + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            showPage(response, pageNum)

            pageNum += 1;
            if (pageNum < 2 && pageNum < getPdfPageTotal()) {
                GetPage(pageNum)
            }
        },
        failure: function () {
            GetPage(pageNum)
        }
    });

    return false;
}

//API call that returns with a json object containing an image path, and a 2d array of text data/styles
function GetPage(pageNum) {
    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?filename=" + pageNum + "_" + getFileName() + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            showPage(response, pageNum);
        },
        failure: function (response) {
            GetPage(pageNum);
        }
    });
    return false;
}

//Passed 'response' data from API call function GetPage(pageNum) as parameter 'data'
//Calls createNewPageDiv, which returns a new div, containing the page image, and 
//Hiden text for the given pageNum.
function showPage(data, pageNum) {

    let newDiv = createNewPageDiv(data, pageNum);
    document.getElementById("MainContent_customViewerL").appendChild(newDiv);

}
//#endregion

//#region outOfSyncPageJumping
function loadPageOutOfSync(actualPageNum, totalPages) {
    let currentTotal = document.getElementById("MainContent_customViewerL").children.length;

    GetPageOutOfSync(actualPageNum);

    return false;
}

function GetPageOutOfSync(pageNum) {
    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?filename=" + pageNum + "_" + getFileName() + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            children = pageNum + 1;
            showPageOutOfSync(response, pageNum);

            if (pageNum + 1 != getPdfPageTotal()) {
                children = pageNum + 2;
                GetPage(pageNum + 1);

            }
        },
        failure: function (response) {
            alert("failure");
            GetPage(pageNum);
        }
    });
}

//Shows pages that were jumped to before they were loaded on the screen.
function showPageOutOfSync(data, pageNum) {

    let newDiv = createNewPageDiv(data, pageNum);

    let totalPages = document.getElementById("MainContent_customViewerL").children.length;
    let targetNum = pageNum;

    document.getElementById("MainContent_customViewerL").appendChild(newDiv);

    scrollAnchor = newDiv;
    scrollAnchor.scrollIntoView(true);
    loadIntermediaryPages(pageNum, totalPages, targetNum);
}

//Begins recursive calls to load all intermediary pages between the last loaded page
//And the page that was jumped to out of sync.
function loadIntermediaryPages(actualPageNum, totalPages, targetNum) {

    let pageNum = actualPageNum - 1;
    let targetPos = targetNum;

    if (pageNum >= totalPages) {
        $.ajax({
            type: "GET",
            url: "api/PdfPageAPI?filename=" + pageNum + "_" + getFileName() + "",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                let target = document.getElementById("div" + targetPos);
                showIntermediaryPage(response, pageNum, target, totalPages);

            },
            failure: function (response) {
                alert("failure");
                GetPage(pageNum);
            }
        });
    } else {
        return false;
    }

    return false;
}

//Shows the intermediary pages in between the last loaded page and the page that was
//Jumped to out of sync.
function showIntermediaryPage(data, pageNum, target, totalPages) {

    let newDiv = createNewPageDiv(data, pageNum);

    document.getElementById("MainContent_customViewerL").insertBefore(newDiv, target);
    scrollAnchor.scrollIntoView(true);
    loadIntermediaryPages(pageNum, totalPages, pageNum);
}

//#endregion

//#region pageSaving
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

        }
    });

    return false;
}
//#endregion

//#region "pageCreation"
//Returns a new page image as an html img given an image path and page number.
function createNewPageImage(path, pageNum) {
    let newImg = document.createElement("img");
    newImg.src = path;
    newImg.id = "page" + pageNum;
    return newImg;
}

//Returns a new div containing a page image and hidden text for the given pageNum.
function createNewPageDiv(data, pageNum) {
    let newDiv = document.createElement("div");
    newDiv.appendChild(createNewPageImage(data.imgPath, pageNum));
    newDiv.className = "pageDiv";
    newDiv.id = "div" + pageNum;

    let textData = data.textData;

    //Iterates 2d array textData. Extracts each paragraph element and its corresponding styles
    //Extracted elements added to newDiv
    for (var i = 0; i < textData[0].length; i++) {
        let newP = document.createElement("p");
        newP.innerHTML = textData[0][i];
        newP.style = textData[1][i];
        newDiv.appendChild(newP);
    }
    return newDiv;
}
//#endregion

//#region misc
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

function lockScrollPosition() {
    $('body').css({ 'overflow': 'hidden' });
}

function insertAfter(newNode, referenceNode) {
    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
}

//#endregion
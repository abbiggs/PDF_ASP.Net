﻿var errorMsgDiv = document.getElementById("errorMsgDiv");
var scrollAnchor = null;
var children = null;

//#region scrollEvents

//Keeps track of the last child to have entered the view, as to not execute loadNextPage() multiple times per element.
var lastChild = null;
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

    var bottomChild = parentElement.children.item(childCount - 1);
    var topChild = parentElement.children.item(1);

    if (elementScrolled(bottomChild) && bottomChild != lastChild && childCount < pageTotal) {

        lastChild = bottomChild;
        children = null;
        loadNextPage();

    }

    if (elementScrolled(topChild) && topChild.id != "div1" && topChild != prevChild) {
        
        prevChild = topChild;
        let pageNum = parseInt(topChild.id.substring(3, topChild.id.length)) - 2;
        
        GetPage(pageNum, "top");

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
                    if (pageNum == getPdfPageTotal()) {
                        $(window).unbind("scroll");
                        loadPageOutOfSync(actualPageNum);
                    } else {
                        loadPageOutOfSync(actualPageNum);
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

    let parentElement = document.getElementById("MainContent_customViewerL");
    let lastElement = parentElement.children.item(parentElement.children.length - 1);
    let pageNum = parseInt(lastElement.id.substring(3, lastElement.id.length)) + 1;
    
    GetPage(pageNum, "bottom");

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

            showPage(response, pageNum, "bottom");

            pageNum += 1;
            if (pageNum < 2 && pageNum < getPdfPageTotal()) {
                GetPage(pageNum, "bottom");
            }
        },
        failure: function () {
            GetPage(pageNum, "bottom");
        }
    });

    return false;
}

//API call that returns with a json object containing an image path, and a 2d array of text data/styles
function GetPage(pageNum, location) {
    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?filename=" + pageNum + "_" + getFileName() + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            showPage(response, pageNum, location);
        },
        failure: function (_response) {
            GetPage(pageNum, location);
        }
    });
    return false;
}

//Passed 'response' data from API call function GetPage(pageNum) as parameter 'data'
//Calls createNewPageDiv, which returns a new div, containing the page image, and 
//Hiden text for the given pageNum.
function showPage(data, pageNum, location) {

    let newDiv = createNewPageDiv(data, pageNum);
    let parentElement = document.getElementById("MainContent_customViewerL");

    if (location == "bottom") {
        parentElement.appendChild(newDiv);
    } else if (location == "top") {
        parentElement.insertBefore(newDiv, parentElement.children.item(0));
    }

    checkTotalPagesLoaded(pageNum, location);
}

function checkTotalPagesLoaded(_pageNum, location) {

    let parentElement = document.getElementById("MainContent_customViewerL");

    if (parentElement.children.length > 10) {
        if (location == "bottom") {
            parentElement.removeChild(parentElement.children.item(0));
        }
        if (location == "top") {
            parentElement.removeChild(parentElement.children.item(parentElement.children.length - 1));
        }
    }
}

//#endregion

//#region outOfSyncPageJumping

function loadPageOutOfSync(actualPageNum) {

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
                GetPage(pageNum + 1, "bottom");
            }
        },
        failure: function (_response) {
            alert("failure");
            GetPage(pageNum, "bottom");
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
            failure: function (_response) {
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
        success: function (_response) {
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
        success: function (_response) {

        }
    });

    return false;
}
//#endregion

//#region pageCreation
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
    for (var i = 0; i < pageTotal.length; i++) {
        if (pageTotal.charAt(i) == " ") {
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
    for (var i = 0; i < labelText.length; i++) {
        if (labelText.charAt(i) == " ") {
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
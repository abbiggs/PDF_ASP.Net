var errorMsgDiv = document.getElementById("errorMsgDiv");

function jumpToPage(event) {

    //Executes on enter key press event
    if (event.which == 13 || event.keyCode == 13) {

        try {

            //Checks that the user entered page number is within the page bounds
            var pageNum = document.getElementById("manualPageInput").value;
            if(pageNum <= getPdfPageTotal() && pageNum > 0){
                var actualPageNum = pageNum - 1;

                //Checks whether or not the page is already loaded on the screen
                var page = document.getElementById("page" + actualPageNum);
                if (page != null) {
                    page.scrollIntoView()
                } else if (page == null) {
                    var totalPages = document.getElementById("MainContent_customViewerL").children.length;
                    loadPageOutOfSync(actualPageNum, totalPages);
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

function loadPageOutOfSync(actualPageNum, totalPages) {
    let currentTotal = document.getElementById("MainContent_customViewerL").children.length;

    GetPageOutOfSync(actualPageNum);
    //var page = document.getElementById("page" + actualPageNum);
    //page.scrollIntoView();

    //let targetNum = actualPageNum;
    
    //loadIntermediaryPages(actualPageNum - 1, totalPages, targetNum);
    
    

    return false;
}

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
                //alert(targetPos)
                showIntermediaryPage(response, pageNum, target, totalPages);

                //pageNum -= 1;
                //if (pageNum > totalPages) {
                //    loadIntermediaryPages(pageNum, totalPages, targetPos - 1);
                //}

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

function showIntermediaryPage(data, pageNum, target, totalPages) {
    let newDiv = document.createElement("div");
    let newImg = document.createElement("img");
    newImg.src = data.imgPath;
    newImg.id = "page" + pageNum;
    newDiv.appendChild(newImg);
    newDiv.className = "pageDiv";
    newDiv.id = "div" + pageNum;

    //Occurs when image path is invalid (couldn't load image)
    newImg.onerror = function () {
        setTimeout(function () {
            errorMsgDiv.style.display = "block";
        }, 1);
        return false;
    }

    let textData = data.textData;

    //Iterates 2d array textData. Extracts each paragraph element and its corresponding styles
    //Extracted elements added to newDiv
    for (var i = 0; i < textData[0].length; i++) {
        let newP = document.createElement("p");
        newP.innerHTML = textData[0][i];
        newP.style = textData[1][i];
        newDiv.appendChild(newP);
    }
    
    document.getElementById("MainContent_customViewerL").insertBefore(newDiv, target);

    loadIntermediaryPages(pageNum, totalPages, pageNum);

}

//API call to load the next available page
function loadNextPage() {
    var pageCount = document.getElementById("MainContent_customViewerL").children.length;
    GetPage(pageCount);
    
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
            //GetPage(0);
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
            if (pageNum < 2) {
                GetPage(pageNum)
            }
        },
        failure: function () {
            GetPage(pageNum)
        }
    });

    return false;
}

//API call that takes page number as parameter, and returns image path / text data
function loadPage(pageNum) {
    $.ajax({
        type: "GET",
        url: "api/PdfPageAPI?filename=" + pageNum + "_" + getFileName() + "",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            let newDiv = document.createElement("div");
            newDiv.className = "pageDiv";
            let newImg = document.createElement("img");
            newImg.src = response.imgPath;
            newDiv.appendChild(newImg);

            let textData = response.textData;

            for (var i = 0; i < textData[0].length; i++) {
                let newP = document.createElement("p");
                newP.innerHTML = textData[0][i];
                newP.style = textData[1][i];
                newDiv.appendChild(newP);
            }

            document.getElementById("MainContent_customViewerL").appendChild(newDiv);
            
        },
        failure: function (response) {
            alert("failure");
            loadPage(pageNum);
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
            alert("failure");
            GetPage(pageNum);
        }
    });
    return false;
}

function GetPages(startPage) {
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
            alert("failure");
            GetPage(pageNum);
        }
    });
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
            
            showPageOutOfSync(response, pageNum);
        },
        failure: function (response) {
            alert("failure");
            GetPage(pageNum);
        }
    });
}

//Passed 'response' data from API call function GetPage(pageNum) as parameter 'data'
//Uses 'data' to extract image path and text data, then displays the page on the screen
function showPage(data, pageNum) {
    let newDiv = document.createElement("div");
    let newImg = document.createElement("img");
    newImg.src = data.imgPath;
    newImg.id = "page" + pageNum;
    newDiv.appendChild(newImg);
    newDiv.className = "pageDiv";
    newDiv.id = "div" + pageNum;

    //Occurs when image path is invalid (couldn't load image)
    newImg.onerror = function () {
        setTimeout(function () {
            errorMsgDiv.style.display = "block";
        }, 1);
        return false;
    }

    let textData = data.textData;

    //Iterates 2d array textData. Extracts each paragraph element and its corresponding styles
    //Extracted elements added to newDiv
    for (var i = 0; i < textData[0].length; i++) {
        let newP = document.createElement("p");
        newP.innerHTML = textData[0][i];
        newP.style = textData[1][i];
        newDiv.appendChild(newP);
    }

    document.getElementById("MainContent_customViewerL").appendChild(newDiv);
    
}

function showPageOutOfSync(data, pageNum) {
    let newDiv = document.createElement("div");
    let newImg = document.createElement("img");
    newImg.src = data.imgPath;
    newImg.id = "page" + pageNum;
    newDiv.appendChild(newImg);
    newDiv.className = "pageDiv";
    newDiv.id = "div" + pageNum;

    //Occurs when image path is invalid (couldn't load image)
    newImg.onerror = function () {
        setTimeout(function () {
            errorMsgDiv.style.display = "block";
        }, 1);
        return false;
    }

    let textData = data.textData;

    //Iterates 2d array textData. Extracts each paragraph element and its corresponding styles
    //Extracted elements added to newDiv
    for (var i = 0; i < textData[0].length; i++) {
        let newP = document.createElement("p");
        newP.innerHTML = textData[0][i];
        newP.style = textData[1][i];
        newDiv.appendChild(newP);
    }

    let totalPages = document.getElementById("MainContent_customViewerL").children.length;
    let targetNum = pageNum;

    //document.getElementById("MainContent_customViewerL").insertBefore(newDiv, targetDiv);
    document.getElementById("MainContent_customViewerL").appendChild(newDiv);
    document.getElementById("page" + pageNum).scrollIntoView();
    
    //lockScrollPosition();
 
    loadIntermediaryPages(pageNum, totalPages, targetNum);
}


function lockScrollPosition() {
    $('body').css({ 'overflow': 'hidden' });
}



function insertAfter(newNode, referenceNode) {
    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
}





function pageUp() {
    alert("!" + getFileName() + "!");
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}


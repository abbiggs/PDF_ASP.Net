var errorMsgDiv = document.getElementById("errorMsgDiv");

function jumpToPage(event) {

    //Executes on enter key press event
    if (event.which == 13 || event.keyCode == 13) {

        try {

            var pageNum = document.getElementById("manualPageInput").value;
            var actualPageNum = pageNum - 1;

            document.getElementById("page" + actualPageNum).scrollIntoView();

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
    GetPage(pageCount);
    //$.ajax({
    //    type: "GET",
    //    url: "api/PdfPageAPI?filename=" + pageCount + "_" + getFileName() + "",
    //    data: "",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (response) {
            
    //        let newDiv = document.createElement("div");
    //        let newImg = document.createElement("img");
    //        newImg.src = response.imgPath;
    //        newDiv.appendChild(newImg);
    //        newImg.onerror = function () {
    //            errorMsgDiv.style.display = "block";
    //            setTimeout(function () {
    //                errorMsgDiv.style.display = "none";
    //            }, 2);
    //            return false;
    //        }
    //        document.getElementById("MainContent_customViewerL").appendChild(newDiv);
    //    }
    //});

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

            //response.imgPath will return the image path as a string
            //response.textData will return the 2d array containing the text and the styles
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

//Passed 'response' data from API call function GetPage(pageNum) as parameter 'data'
//Uses 'data' to extract image path and text data, then displays the page on the screen
function showPage(data, pageNum) {
    let newDiv = document.createElement("div");
    let newImg = document.createElement("img");
    newImg.src = data.imgPath;
    newImg.id = "page" + pageNum;
    newDiv.appendChild(newImg);
    newDiv.className = "pageDiv";
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












function pageUp() {
    alert("!" + getFileName() + "!");
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}


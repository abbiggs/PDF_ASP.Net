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

//Event executes when the window is scrolled to check if the page is scrolled all the way down
window.addEventListener('scroll', function (e) {
    var a = $("#MainContent_customContainer").offset().top;
    var b = $("#MainContent_customContainer").height();
    var c = $(window).height();
    var d = $(window).scrollTop();
    if ((c + d) > (a + b)) {

        //alert("bottom");
        loadNextPage();
    }
});

function pageUp() {
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}
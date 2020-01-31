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

    //$.getJSON("api/PdfPageAPI",
    //    function (data) {
    //        //Loop through the data
    //        $.each(data, function (key, val) {
    //            alert(val);
    //        });
    //    });


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
            document.getElementById("MainContent_customContainer").appendChild(newImg);


        }


    });

    return false;
}

//function testAPI() {

//    $.getJSON("api/PdfPageAPI",
//        function (response) {
//            alert(response);
            
//        });
//    return false;
//}

function pageUp() {
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}
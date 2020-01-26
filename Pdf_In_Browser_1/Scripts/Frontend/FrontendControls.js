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

//Event executes when the window is scrolled to check if the page is scrolled all the way down
window.addEventListener('scroll', function (e) {
    var a = $("#MainContent_customContainer").offset().top;
    var b = $("#MainContent_customContainer").height();
    var c = $(window).height();
    var d = $(window).scrollTop();
    if ((c + d) > (a + b)) {
        
        alert("bottom");
        $(function () {
            $.ajax({
                type: "POST",
                url: "Default.aspx/testMethod",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false
            })
            return false;
        });
    }
});

function callServerMethod() {
    alert("<%= GetValue() %>");
    return false;
}

function pageUp() {
    return false;
}

function pageDown() {
    //document.getElementById("customViewer1").child
    return false;
}
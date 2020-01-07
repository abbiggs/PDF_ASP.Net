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
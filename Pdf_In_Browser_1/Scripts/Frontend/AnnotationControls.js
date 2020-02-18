
//Text selection
$("p").mouseup(function () {

    var selected = getSelectedText();
    var selected_text = selected.toString();

    var span = document.createElement("SPAN");
    span.textContent = selected_text;
    span.classList.add("selectedText");

    var tag = selected.getRangeAt(0);
    range.insertNode(span);
});
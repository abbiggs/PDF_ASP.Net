
//Text selection
$(".mainContainer").mouseup(function () {

    selecting();
});

$(".mainContainer").mousedown(function () {

    clearingOldSelection();
});

function selecting() {

    var selected = getSelectedText();
    var selected_text = selected.toString();

    var span = creatingSelection(selected_text);

    var tag = selected.getRangeAt(0);
    tag.insertNode(span);
}

function creatingSelection(selected_text) {

    var span = document.createElement("SPAN");
    span.textContent = selected_text;
    span.classList.add("selectedText");

    return span;
}

function clearingOldSelection() {

    $(".selectedText").remove();
}

function getSelectedText() {
    text = (document.all) ? document.selection.createRange().text : document.getSelection();

    return text;
}

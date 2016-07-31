$(function () {
    var counter = 0;
    $("#add-image").on("click", function () {
        counter++;
        $(".images").append("<input type='file' class='form-control'name='pictures[" + counter +"]'/>");
    });
});
$(document).ready(function () {
    //alert("hydra");

    var $popupForm1 = $(".popup-form1");
    var $popupForm2 = $(".popup-form2");
    var $popupForm3 = $(".popup-form3");

    $popupForm1.hide();
    $popupForm2.hide();
    $popupForm3.hide();

    var button1 = $("#ButtonAPI");
    var button2 = $("#ButtonRXO");
    var button3 = $("#ButtonCatalog");

    button1.on("click", function () {
        $popupForm2.hide();
        $popupForm3.hide();
        $popupForm1.slideToggle(1000);
    });

    button2.on("click", function () {
        $popupForm1.hide();
        $popupForm3.hide();
        $popupForm2.slideToggle(1000);
    });

    button3.on("click", function () {
        $popupForm1.hide();
        $popupForm2.hide();
        $popupForm3.slideToggle(1000);
    });


    $('#ButtonGotoSwagger').click(function () {
        //document.location = '@Url.Action("ActionName","ControllerName")';
        location.href = 'swagger';
    });

});



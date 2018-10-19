var x = 0;
var s = "";


//alert("hydra");
console.log("hydra");


//var theForm = document.getElementById("theForm");
//theForm.hidden = true;
var theForm = $("#theForm");
theForm.hide();


//var button = document.getElementById("buyButton");
//button.addEventListener("click", function () {
//    console.log("test button");
//});
var button = $("#buyButton");
button.on("click", function () {
    console.log("Buying Item");
});


//var productInfo = document.getElementsByClassName("product-props");
//var listItems = productInfo.item[0].children;
var productInfo = $(".product-props li");
productInfo.on("click", function () {
    console.log("Click on " + $(this).text());
});

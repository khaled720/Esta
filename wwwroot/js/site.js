// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var ImageChange = function (event) {

    var chosenImage = event.target.files[0];
    var src = window.URL.createObjectURL(chosenImage);
    document.getElementById("photoDiv").style.backgroundImage = "url(" + src + ")";


}


var GetPhoto = function () {
    document.getElementById("upload").click();


}
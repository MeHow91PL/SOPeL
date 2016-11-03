/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {

    //Obsługa menu nawigacyjnego
    var sidebar = $("#sidebar");

    $("#zwijaczMenu").click(function () {
        sidebar.toggleClass("nav-sidebarZwiniety",200);
        $(this).toggleClass("rotate");
    });

});
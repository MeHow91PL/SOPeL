/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {

    //Obsługa menu nawigacyjnego
    var sidebar = $("#sidebarPrzychodnia");
    var sidebar = $("#sidebarPrzychodnia");

    $("#zwijaczMenu").click(function () {
        sidebar.toggleClass("sidebarZwiniety", 200);
        $("#PrzychodniaBodyKontener").toggleClass("przychodniaBodyKontenerRozwinięty");
        $(this).toggleClass("rotate");
    });

});
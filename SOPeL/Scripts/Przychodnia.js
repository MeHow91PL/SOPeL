/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {

    //Obsługa menu nawigacyjnego
    var sidebar = $("#sidebarPrzychodnia");

    $("#zwijaczMenu").click(function () {
        sidebar.toggleClass("sidebarZwiniety", 200);
        $("#PrzychodniaBodyKontener").toggleClass("przychodniaBodyKontenerRozwinięty");
        $(this).toggleClass("rotate");
    });


    var shiftIsPressed = false; //zmienna będzie służyła do rozpoznania czy klawisz shift jest wciśnięty

    $("input").keydown(function (event) {
        if (event.which == 16) { // 16 to kod klawisza Shift
            shiftIsPressed = true;
        }

        if (event.which == 8 && shiftIsPressed) { // 8 to kod klawisza backspace, jeżeli wciśnięty jest shitf i backspace to program wyczyści pole wyszukiwania
            console.log($(this).val(""));
        }
    });

    $("input").keyup(function (event) {
        if (event.which == 16) { // 16 to kod klawisza Shift
            shiftIsPressed = false;
        }
    });
});
/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {

    //Obsługa menu nawigacyjnego
    var sidebar = $("#sidebarPrzychodnia");
    var $PrzychodniaBodyKontener = $("#PrzychodniaBodyKontener");

    $("#zwijaczMenu").click(function () {
        sidebar.toggleClass("sidebarZwiniety", 200);
        $PrzychodniaBodyKontener.toggleClass("przychodniaBodyKontenerRozwinięty");
        $(this).toggleClass("rotate");
    });


    var shiftIsPressed = false; //zmienna będzie służyła do rozpoznania czy klawisz shift jest wciśnięty
    $PrzychodniaBodyKontener.on('keydown', 'input', function (event) {
        if (event.which == 16) { // 16 to kod klawisza Shift
            shiftIsPressed = true;
        }

        if (event.which == 8 && shiftIsPressed) { // 8 to kod klawisza backspace, jeżeli wciśnięty jest shitf i backspace to program wyczyści pole wyszukiwania
            console.log($(this).val(""));
        }
    });

    $PrzychodniaBodyKontener.on('keyup', 'input', function (event) {
        if (event.which == 16) { // 16 to kod klawisza Shift
            shiftIsPressed = false;
        }
    });

    $PrzychodniaBodyKontener.on("click", ".lista-pacjentow-btn", function (event) {
        ZbudujOkienko("ListaPacjentowKontener", "ListaPacjentowOkno", "/Kartoteki/PokazListePacjentow");
    });

    //funkcja po każdym znaku wykonuje submit formularza, który przez zapytanie ajaxowe zwraca listę pacjentów
    $PrzychodniaBodyKontener.on("keyup", ".SzukajPacjentaInput", function () {
        $("#SzukajPacjentowForm").submit();
    });

    $PrzychodniaBodyKontener.on("click", ".dodaj-pacjenta", function () {
        ZbudujOkienko("KartaPacjentaKontener", "KartaPacjentawOkno", "/Kartoteki/DodajPacjenta");
    });

    $PrzychodniaBodyKontener.on("mouseenter", '[data-toggle="popover"]', function () {
        $(this).popover({ html: true, delay: 5000 });
        $(this).popover('show');
    });

    $PrzychodniaBodyKontener.on("mouseleave", '[data-toggle="popover"]', function () {
        $(this).popover('destroy');
    });

    function ZbudujOkienko(kontener, idOkna, urlAction) {
        var html = '<div id="' + kontener + '" class="wysrodkujCentralnie col-lg-12 col-md-12 col-sm-12 col-xs-12">' +
                        '<div id="' + idOkna + '" class="kontenerOkienka  col-lg-10 col-md-12 col-sm-12 col-xs-12 col-xs-12">' +
                        'Ładowanie...' +
                        '</div></div>';
        $PrzychodniaBodyKontener.append(html)

        $.ajax({
            url: urlAction,
            success: function (response) {
                $("#" + idOkna).html(response);
            }
        });
    }
});


/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {

    //Obsługa menu nawigacyjnego
    var sidebar = $("#sidebarPrzychodnia");
    var $PrzychodniaBodyKontener = $("#renderBody");

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

    $PrzychodniaBodyKontener.on("click", ".zamknij-okienko-btn", function (event) {
        UkryjOkienko($(this).data('idokienka'));
    });

    //funkcja po każdym znaku wykonuje submit formularza, który przez zapytanie ajaxowe zwraca listę pacjentów
    $PrzychodniaBodyKontener.on("keyup", ".SzukajPacjentaInput", function () {
        $("#SzukajPacjentowForm").submit();
    });

    $PrzychodniaBodyKontener.on("click", ".dodaj-pacjenta", function () {
        ZbudujOkienko("KartaPacjentaKontener", "KartaPacjentawOkno", "/Kartoteki/DodajPacjenta");


    });

    var atykwyPopover;

    $PrzychodniaBodyKontener.on("mouseenter", '[data-toggle="popover"]', function (e) {
        $(atykwyPopover).popover('destroy');

        $(this).popover({ html: true, trigger: 'manual' });
        $(this).popover('show');
        atykwyPopover = $(this);
    });

    $PrzychodniaBodyKontener.on("mouseover", '[data-toggle="popover"]', function (e) {
        // console.log(e);
    });

    $PrzychodniaBodyKontener.on("mouseleave", '[data-toggle="popover"]', function () {
        $(this).popover('destroy');
    });



    function ZbudujOkienko(kontener, idOkna, urlAction, idRezerwacji) {
        var html = '<div id="' + kontener + '" class="wysrodkujCentralnie col-lg-12 col-md-12 col-sm-12 col-xs-12">' +
                        '<div id="' + idOkna + '" class="kontenerOkienka  col-lg-10 col-md-12 col-sm-12 col-xs-12 col-xs-12">' +
                        'Ładowanie...' +
                        '</div></div>';

        var exitButton = '<div class="exit-button-kontener col-lg-12 col-md-12 col-sm-12 col-xs-12 col-xs-12">' +
                        '<span class="button zamknij-okienko-btn glyphicon glyphicon-remove" data-idokienka="' + kontener + '"></span>' +
                        '</div>';

        if (!$('#' + idOkna).length) {// Jeżeli oknko już jest zbudowane w DOM to tylko je wyświetl
            $PrzychodniaBodyKontener.append(html)
            $.ajax({
                url: urlAction,
                data: {
                    idRez: idRezerwacji
                },
                success: function (response) {
                    var newHtml = exitButton + response;
                    $("#" + idOkna).html(newHtml);
                },
                error: function (resp) {
                    $("#" + idOkna).html(exitButton + "Błąd przy pobieraniu danych");
                }
            });
        }
        else {
            $('#' + kontener).show();
        }

    }

    function UkryjOkienko(idOkienka) {
        if (confirm("Czy na pewno zamknąć okno?")) {
            $('#' + idOkienka).css("display", "none");
        }
    }
});


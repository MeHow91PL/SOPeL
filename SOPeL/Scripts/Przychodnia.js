/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var pacjentHandler;
    var pacjentIdHandler;

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
        if (event.which === 16) { // 16 to kod klawisza Shift
            shiftIsPressed = true;
        }

        if (event.which === 8 && shiftIsPressed) { // 8 to kod klawisza backspace, jeżeli wciśnięty jest shitf i backspace to program wyczyści pole wyszukiwania
            console.log($(this).val(""));
        }
    });

    //wyświetla kalendarz przy wejściu w pole wyboru daty
    $PrzychodniaBodyKontener.on("focus", ".datepicker", function (event) {
        $(".datepicker").datepicker({
            dateFormat: "yy-mm-dd",
            changeYear: true,
            dayNames: ["Niedziela", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota"],
            dayNamesMin: ["Nd", "Pn", "Wt", "Śr", "Cz", "Pt", "So"],
            monthNames: ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"],
            monthNamesShort: ["Sty", "Lut", "Mar", "Kwi", "Maj", "Cze", "Lip", "Sie", "Wrz", "Paź", "Lis", "Grud"],
            firstDay: 1, //który dzień ma być wyświetlany jako pierwszy. 1 oznacza poniedziałek. Domyślnie ustawione na 0
            numberOfMonths: 1, // ilość miesięcy która się wyświtli. Można zrobić np [2,3] - co pokaże 2 wiersze i 3 kolumny mies
            showOtherMonths: true // pokazuje dni z poprzedniego i następnego miesiąca których nie da się wybrać
        });
    });

    $PrzychodniaBodyKontener.on('keyup', 'input', function (event) {
        if (event.which === 16) { // 16 to kod klawisza Shift
            shiftIsPressed = false;
        }
    });

    $PrzychodniaBodyKontener.on("click", ".lista-pacjentow-btn", function (event) {
        var data = {
            searchString: $("#Pacjent").val(),
            WybierzPacjenta: $(this).data("wybierzpacjenta")
        };

        ZbudujOkienko("ListaPacjentowKontener", "ListaPacjentowOkno", "/Kartoteki/PokazListePacjentow", data, "100%");
        pacjentHandler = $("#Pacjent");
        pacjentIdHandler = $("#PacjentID");
    });

    $PrzychodniaBodyKontener.on("click", ".wybierzBtn", function (event) {
        $(pacjentHandler).val($(this).data("pacvalue"));
        $(pacjentIdHandler).val($(this).data("pacid"));
        $("#ListaPacjentowKontener").hide();
    });

    $PrzychodniaBodyKontener.on("keyup", ".SzukajUzytkownikaInput", function () {
        $("#SzukajUzytkownikowForm").submit();
    });

    //funkcja po każdym znaku wykonuje submit formularza, który przez zapytanie ajaxowe zwraca listę pacjentów
    $PrzychodniaBodyKontener.on("keyup", ".SzukajPacjentaInput", function () {
        $("#SzukajPacjentowForm").submit();
    });

    $PrzychodniaBodyKontener.on("click", ".opcje-glowne-btn", function () {
        ZbudujOkienko("opcje-ogolne-kontener", "opcje-ogolne-okno", "/Admin/PokazOpcjeGlowne");
    });

    $PrzychodniaBodyKontener.on("click", ".dodaj-pacjenta", function () {
        ZbudujOkienko("KartaPacjentaKontener", "KartaPacjentawOkno", "/Kartoteki/DodajPacjenta");
    });

    $PrzychodniaBodyKontener.on("click", ".lista-uzytkownikow-btn", function () {
        ZbudujOkienko("ListaUzytkownikowKontener", "ListaUzytkownikowOkno", "/Kartoteki/PokazListeUzytkownikow");
    });

    $PrzychodniaBodyKontener.on("click", ".lista-pracownikow-btn", function (event) {
        var data = {
            searchString: $("#Pracownik").val(),
            WybierzPacjenta: $(this).data("wybierzpracownika")
        };

        ZbudujOkienko("ListaPracownikowKontener", "ListaPracownikowOkno", "/Kartoteki/PokazListePracownikow", data, "100%");
        pacjentHandler = $("#Pracownik");
        pacjentIdHandler = $("#PracownikID");
    });

    //funkcja po każdym znaku wykonuje submit formularza, który przez zapytanie ajaxowe zwraca listę pracownikow
    $PrzychodniaBodyKontener.on("keyup", ".SzukajPracownikaInput", function () {
        $("#SzukajPracownikowForm").submit();
    });

    $PrzychodniaBodyKontener.on("change", ".pokaz-usuniete", function () {
        $("#SzukajPracownikowForm").submit();
    });

    $PrzychodniaBodyKontener.on("submit", "#KartaPacjentaForm", function () {
        $.ajax({
            url: '/Kartoteki/ZapiszPacjenta',
            data: $(this).serialize(),
            success: function (response) {
                if (response.RezultatPozytywny) { //Akcja wykonana poprawnie. Pacjent zapisany
                    alert(response.Komunikat);
                    $("#KartaPacjentaKontener").hide();
                }
                else {
                    alert(response.Komunikat);

                }
            },
            error: function (resp) {
                alert(response.Komunikat);
            }
        });
        return false;// blokada przeładowania strony po wykonaniu akcji
    });


    $PrzychodniaBodyKontener.on('click', '.dodajWizyte', function (event) {//dzięki zastosowaniu takiej formy (delegat) zdarzenia działają również w elementach ładowanych przez AJAX
        ZbudujOkienko("KontenerKartaWizyty", "KartaWizyty", "/Wizyta/dodajWizyte", { idRez: $(this).data("idwizyty") });
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

    $PrzychodniaBodyKontener.on("click", ".zamknij-okienko-btn", function (event) {
        UkryjOkienko($(this).data('idokienka'));
    });


    $PrzychodniaBodyKontener.on('click', '.wyswietlHistorie', function (event) {//dzięki zastosowaniu takiej formy (delegat) zdarzenia działają również w elementach ładowanych przez AJAX
        ZbudujOkienko("HistoriaWizytyKontener", "HistoriaWizyty", "/Wizyta/pokazHistorie", { idwizy: $(this).data("idwiz") });
    });



    function ZbudujOkienko(kontener, idOkna, urlAction, data, kontenerHeight) {
        kontenerHeight = typeof kontenerHeight !== 'undefined' ? kontenerHeight : "auto"; //Jeżeli nie podano wysokości to domyślnie będzie ustawiona na auto

        var exitButton = '<div class="exit-button-kontener col-lg-12 col-md-12 col-sm-12 col-xs-12 col-xs-12">' +
                        '<span class="button zamknij-okienko-btn glyphicon glyphicon-remove" data-idokienka="' + kontener + '"></span>' +
                        '</div>';

        var html = '<div id="' + kontener + '" class="wysrodkujCentralnie col-lg-12 col-md-12 col-sm-12 col-xs-12">' +
                        '<div class="kontenerOkienka  col-lg-10 col-md-12 col-sm-12 col-xs-12 col-xs-12">' +
                        exitButton +
                        '<div id="' + idOkna + '" class="windowContent">' +
                        '<div style="text-align: center">Ładowanie...</div>' +
                        '</div></div></div>';


        if (!$('#' + idOkna).length) {// Jeżeli oknko już jest zbudowane w DOM to tylko je wyświetl
            $PrzychodniaBodyKontener.append(html);
        }
        $.ajax({
            url: urlAction,
            data: data,
            success: function (response) {
                $('#' + kontener + ' .kontenerOkienka').height(kontenerHeight);
                $("#" + idOkna).html(response);
                if ($('#' + idOkna).length) {

                    $('#' + kontener).show();
                }
            },
            error: function (resp) {
                $("#" + idOkna).html("Błąd przy pobieraniu danych");
            }
        });
    }

    function UkryjOkienko(idOkienka) {
        if (confirm("Czy na pewno zamknąć okno?")) {
            console.log($('#' + idOkienka));
            $('#' + idOkienka).css("display", "none");
        }
    }
});


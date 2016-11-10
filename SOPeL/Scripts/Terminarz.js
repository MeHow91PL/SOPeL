/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {

    //wczytanie elementów do zmiennej daje lepszą wydajność, gdyż tylko raz wyszukujemy po id
    var loaderKontener = $("#loader-kontener");
    var wyborDatyTerminarz = $("#wybor-daty-terminarz");
    var dataRezerwacji = $('#wybor-daty-terminarz').val();
    var kontenerMaterialPortalPacjenta = $('#kontenerMaterialPortalPacjenta');
    var opcjeTerminarzaDiv = $('#opcje-terminarza-okno');
    var terminarzDataKontener = $("#terminarzDataKontener");
    var czasTrwaniaWizyty = $("#term_czas_wiz");
    var ogolnyGrafik = $("#term_ogolny_graf_panel");
    var idywidualnyGrafik = $("#term_indw_graf_panel");
    var indwGrafCheckbox = $("#term_indw_graf");
    var menu = $("#menu");


    kontenerMaterialPortalPacjenta.css({
        "height": "100%",
        "visibility": "visible"
    });
    var oknoOpcjiTerminarza = {
        draggable: true,
        hide: { effect: "fade", duration: 250 },
        show: { effect: "fade", duration: 250 },
        resizable: false,
        autoOpen: false,
        width: "500px",
        title: "Opcje terminarza"
    };

    var oknoIndywidualnegoGrafika = {
        draggable: true,
        hide: { effect: "fade", duration: 250 },
        show: { effect: "fade", duration: 250 },
        resizable: false,
        autoOpen: false,
        title: "Zarządzanie godzinami pracy"
    };

    //--------------------------------- WYBÓR DATY -------------------------------------------------------------------------------------------------

    //wyświetla kalendarz przy wejściu w pole wyboru daty
    wyborDatyTerminarz.datepicker({
        dateFormat: "yy-mm-dd",
        changeYear: true
    });


    wyborDatyTerminarz.change(function () {
        dataRezerwacji = wyborDatyTerminarz.val();
        var wzor = /^[0-9]{4}-[0-9]{2}-[0-9]{2}$/;
        var parts = dataRezerwacji.split('-');

        if (!wzor.test(dataRezerwacji)) {
            alert("Wybierz datę z kalendarza");
            kontenerMaterialPortalPacjenta.slideUp(500);
            return;
        }
        pobierzTerminarz(dataRezerwacji);
    });

    function pobierzTerminarz(dataRezerwacji) {
        $.ajax({
            url: pobierzTerminarzAjax,
            data: {
                wybranaData: dataRezerwacji
            },
            success: function (response) {
                kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
                    kontenerMaterialPortalPacjenta.html(response);
                });
                kontenerMaterialPortalPacjenta.slideDown(300);
            },
            error: function () {
                alert("Błąd połączenia z serwerem!");

                kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
                    kontenerMaterialPortalPacjenta.html("");
                });
            }
        });
    }

    //--------------------------------- END WYBÓR DATY -------------------------------------------------------------------------------------------------



    //--------------------------------- OPCJE TERMINARZA -------------------------------------------------------------------------------------------------

    $("#opcje-terminarza-button").click(function () {
        loaderKontener.switchClass("ukryty", "widoczny", 150, "swing");
        opcjeTerminarzaDiv.css("display", "block");
        $.ajax({
            url: pobierzTerminarzAjax,
            type: "POST",
            success: function (response) { //resposne to słownik sparsowany do jsona, klucz to nazwa opcji w bazie
                loaderKontener.switchClass("widoczny", "ukryty", 150, "swing");

                //ładowanie wartości opcji odczytanych z bazy do formularza opcji terminarza
                for (opcja in response) {
                    $("#" + opcja).val(response[opcja]);
                }
                console.log(response['term_indw_graf']);
                console.debug();

                //jeżeli włączona jest opcja indywidualnego grafika to ukryj ogolny grafik i pokaż ust indywidualnych grafików
                if (response['term_indw_graf'] == '1') {
                    indwGrafCheckbox.attr("checked", true);
                    idywidualnyGrafik.switchClass("ukryty", "widoczny", 150, "swing");
                    ogolnyGrafik.switchClass("widoczny", "ukryty", 150, "swing");

                }
                else {
                    indwGrafCheckbox.attr("checked", false);
                    idywidualnyGrafik.switchClass("widoczny", "ukryty", 150, "swing");
                    ogolnyGrafik.switchClass("ukryty", "widoczny", 150, "swing");
                }

                opcjeTerminarzaDiv.dialog(oknoOpcjiTerminarza).dialog("open");
            },
            error: function () {
                alert("Błąd połączenia z serwerem!");
                loaderKontener.switchClass("widoczny", "ukryty", 150, "swing");
            }
        });
    });

    //Kontrola wartości wpisywanych przez użytkownika
    czasTrwaniaWizyty.keyup(function () {
        if ($(this).val().length > 1 && $(this).val().substring(0, 1) == '0') {//jeżeli poda zero na początku to wycina zero i zostawia pozostałe cyfry
            $(this).val($(this).val().substring(1));
        }
        else if ($(this).val() > 60) {// jeżeli wpisze wartość większą niż 60 to ustawi 60
            $(this).val('60');
        }
        else if ($(this).val() < 1) {// jeżeli wpisze wartość mniejszą niż 1 to ustawi 0
            $(this).val('0');
        }
    });
    czasTrwaniaWizyty.focusout(function () {
        if ($(this).val().trim() === '' || $(this).val() < 1) {//jeżeli nic nie wpisze, wpisze 0 lub wpisze coś innego niż cyfra to wstawi 1
            $(this).val('1');
        }
    });

    //Zmiana opcji z grafiku przychodni na osobne grafiki dla każdego pracownika
    indwGrafCheckbox.change(function () {
        if ($(this).is(':checked')) {
            $(this).val("1");//value ustawiany jest po to, aby zapisać stan opcji do bazy
            idywidualnyGrafik.switchClass("ukryty", "widoczny", 150, "swing");
            ogolnyGrafik.switchClass("widoczny", "ukryty", 150, "swing");
        }
        else {
            $(this).val("0");//value ustawiany jest po to, aby zapisać stan opcji do bazy
            idywidualnyGrafik.switchClass("widoczny", "ukryty", 150, "swing");
            ogolnyGrafik.switchClass("ukryty", "widoczny", 150, "swing");
        }
    });

    $("#opcje-terminarza-zapisz-button").click(function () {
        loaderKontener.switchClass("ukryty", "widoczny", 150, "swing");
        var dict = {};
        $("#opcje-terminarza-okno input").each(function (i, val) {
            alert(val.id);
            dict[$(this).attr("id")] = $(this).val();
        });


        $.ajax({
            url: '@Url.Action("zapiszOpcjeTerminarza", "Terminarz")',
            type: 'POST',
            data: {
                opcj: dict
            },
            success: function (response) {
                loaderKontener.switchClass("widoczny", "ukryty", 150, "swing");
                opcjeTerminarzaDiv.dialog("close");
                pobierzTerminarz(dataRezerwacji);
            },
            error: function () {
                loaderKontener.switchClass("widoczny", "ukryty", 150, "swing");
                alert("Error");
            }
        });
    });

    $("#opcje-terminarza-anuluj-button").click(function () {
        opcjeTerminarzaDiv.dialog("close");
    });

    //--------------------------------- END OPCJE TERMINARZA -------------------------------------------------------------------------------------------------


    //--------------------------------- OBSŁUGA CONTEXT MENU -------------------------------------------------------------------------------------------------

    //Wywołanie okienka z menu,
    $(".panel-rezerwacji").contextmenu(function (event) {
        event.preventDefault();// zablokowanie domyślej obsługi zdarzenia ppm

        menu.css({
            "width": "200px",
            "position": "fixed",
            "background": "white",
            "top": event.pageY - $(window).scrollTop(), //od relatywnej pozycji kliknięcia względm dokumenty należy odjąć ilość px o które ktrona jest scrollowana żeby box wyświetlał się w miejscu kliknięcia
            "border-radius": "5px",
            "box-shadow": "1px 1px 5px black",
            "padding": "20px",
            "left": event.pageX
        });

        menu.slideDown(200);
    });

    //var nadMenu = true;

    //menu.mouseenter(function (event) {
    //    nadMenu = true;
    //});

    //menu.mouseleave(function (event) {
    //    nadMenu = false;
    //});

    ////zamknięcie okienka po poruszeniu myszki poza context
    //$(document).mousemove(function (event) {
    //    $("#przychodnia-sidebar-footer").text(nadMenu);
    //    if (!nadMenu) {
    //        menu.hide();
    //    }
    //});

    //--------------------------------- END OBSŁUGA CONTEXT MENU -------------------------------------------------------------------------------------------------

});
/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var $kontenerMaterialPortalPacjenta = $('#kontenerMaterialPortalPacjenta');
    var $dataRezerwacji = $('#dataRezerwacji');
    var $wyborLekarza = $('#wyborLekarzaRejOnline');
    var $wyborSpecjalizacji = $('#wyborSpecjalizacjiPortalPacjenta');


    var podpowiedzi = $('#podpowiedzi'); wyborLekarzaRejOnline
    var dataRezerwacji = $dataRezerwacji.val();
    var selectedLekarz = $wyborLekarza.children(":selected").attr("id");

    $dataRezerwacji.datepicker({
        dateFormat: "yy-mm-dd",
        changeYear: true
    });

    $dataRezerwacji.change(function () {
        dataRezerwacji = $dataRezerwacji.val();
        pobierzTerminarz();
    });

    $wyborLekarza.change(function () {
        dataRezerwacji = $dataRezerwacji.val();
        selectedLekarz = $wyborLekarza.children(":selected").attr("id");
        pobierzTerminarz();
    })


    function pobierzTerminarz() {
        var selected = $wyborLekarza.children(":selected").attr("id");
        var wzor = /^[0-9]{4}-[0-9]{2}-[0-9]{2}$/;

        if (!wzor.test(dataRezerwacji)) {
            alert("Wybierz datę z kalendarza");
            $kontenerMaterialPortalPacjenta.slideUp(300);
            return;
        }

        $.ajax({
            url: '/RejestracjaOnline/pobierzTerminarzWybranegoLekarza',
            data: {
                idi: selected,
                data: dataRezerwacji
            },
            success: function (response) {
                $kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
                    $kontenerMaterialPortalPacjenta.html(response);
                });
                $kontenerMaterialPortalPacjenta.slideDown(300);
                podpowiedzi.slideUp(300);
            },
            error: function () {
                $kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
                    $kontenerMaterialPortalPacjenta.html("");
                    podpowiedzi.slideDown(300);
                });
            }
        });
    }

    $wyborSpecjalizacji.change(function () {
        var specjalizacja = $wyborSpecjalizacji.children(":selected").attr("id");

        $.ajax({
            url: '/RejestracjaOnline/pobiarzWybranaSpecjalizacje',
            type: "POST",
            data: {
                spec: specjalizacja
            },
            success: function (response) {
                console.log(response);
                var s = "<option>Wybierz lekarza</option>";
                for (var i = 0; i < response.length; i++) {
                    if (response[i].ID == selectedLekarz)
                        s += "<option selected id =" + response[i].ID + ">" + response[i].Imie + "" + response[i].Nazwisko + " (" + response[i].Specjalizacja + ")</option>";
                    else
                        s += "<option id =" + response[i].ID + ">" + response[i].Imie + "" + response[i].Nazwisko + " (" + response[i].Specjalizacja + ")</option>";
                }
                $wyborLekarza.html(s);
                pobierzTerminarz();
            }
        });

        selectedLekarz = $wyborLekarza.children(":selected").attr("id");

    });




});

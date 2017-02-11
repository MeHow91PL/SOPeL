/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var $PrzychodniaBodyKontener = $('#PrzychodniaBodyKontener')
    var $dodajWizyte = $("#dodajWizyte");
    var $KontenerKartaWizyty = $('#KontenerKartaWizyty');
    var $kartaRezerwacjiWizytyKontener = $("#kartaRezerwacjiWizytyKontener");
    var $wyborLekarza = $("#wyborLekarza");
    var $statusWizyty = $("#statusWizyty");
    var $wyborDatyWizyta = $("#wybor-daty-wizyta");
    var $kontenerWizyt = $("#kontenerWizyt");
    var $wyswietlHistorie = $("#wyswietlHistorie");
    var $HistoriaWizyty = $("#HistoriaWizyty");


    var $kontenerMaterialPortalPacjenta = $("#kontenerMaterialPortalPacjenta");



    var WizytaFiltersChanged = function () {
        var wybranaData = $wyborDatyWizyta.val();
        var stat = $statusWizyty.children(":selected").attr("id");
        var idLek = $wyborLekarza.children(":selected").attr("id");

        $.ajax({
            url: '/Wizyta/PobierzListeRezerwacji',
            type: 'POST',
            data: {
                data: wybranaData,
                status: stat,
                idLekarza: idLek
            },
            success: function (response) {
                $kontenerWizyt.fadeOut(200, function myfunction() {
                    $kontenerWizyt.html(response);
                    $kontenerWizyt.fadeIn(200);
                });
            },
            error: function () {
                alert("Error daty");
            }
        })
    }

    $wyborLekarza.change(WizytaFiltersChanged);
    $wyborDatyWizyta.change(WizytaFiltersChanged);
    $statusWizyty.change(WizytaFiltersChanged)

    //$PrzychodniaBodyKontener.on('click', '.wyswietlHistorie', function (event) {//dzięki zastosowaniu takiej formy (delegat) zdarzenia działają również w elementach ładowanych przez AJAX
    //    $("#HistoriaWizyty").css("display", "flex");
    //    $.ajax({
    //        url: '/Wizyta/pokazHistorie',
    //        type: 'POST',
    //        data: {
    //            idwizy: $(this).data("idwiz")
    //        },
    //        success: function (response) {
    //            $HistoriaWizyty.html(response);
    //        },
    //        error: function () {

    //            alert("Error dodja Wizyte");
    //        }
    //    });
    //});
})
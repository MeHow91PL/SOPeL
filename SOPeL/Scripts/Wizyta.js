/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var $dodajWizyte = $("#dodajWizyte");
    var $KontenerKartaWizyty = $('#KontenerKartaWizyty');
    var $kartaRezerwacjiWizytyKontener = $("#kartaRezerwacjiWizytyKontener");
    var $wyborLekarza = $("#wyborLekarza");
    var $kontenerWizyt = $("#kontenerWizyt");
    var $wyswietlHistorie = $("#wyswietlHistorie");
    var $HistoriaWizyty = $("#HistoriaWizyty");


    var $kontenerMaterialPortalPacjenta = $("#kontenerMaterialPortalPacjenta");



    $wyborLekarza.change(function () {
        
        var selected = $wyborLekarza.children(":selected").attr("id");
        $.ajax({
            url: '/Wizyta/WyswietlanieLekarzy',
            type: 'POST',
            data: {
                idlekarza: selected
            },
            success: function (response) {
                $kontenerWizyt.html(response);
            },
            error: function () {


                alert("Error zmiana lekarza");
            }
        })
    });


 

    $('#PrzychodniaBodyKontener').on('click', '.wyswietlHistorie', function (event) {//dzięki zastosowaniu takiej formy (delegat) zdarzenia działają również w elementach ładowanych przez AJAX
        alert("kliku kliku");
        $("#HistoriaWizyty").css("display", "flex");
        $.ajax({
            url: '/Wizyta/pokazHistorie',
            type: 'POST',
            data: {
                idwizy: $(this).data("idwiz")
            },
            success: function (response) {
                $HistoriaWizyty.html(response);
            },
            error: function () {

                alert("Error dodja Wizyte");
            }
        });
    });

    $(".dodajWizyte").click(function () {
 
       
        $("#KontenerKartaWizyty").css("display", "flex");
        $.ajax({
            url: '/Wizyta/dodajWizyte',
            type: 'POST',
            data: {
                idrez: $(this).data("idwizyty")
            },
            success: function (response) {
                $KontenerKartaWizyty.html(response);
            },
            error: function () {
                
                alert("Error dodja Wizyte");
            }
        });



    });
})
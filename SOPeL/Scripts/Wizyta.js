/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var $dodajWizyte = $("#dodajWizyte");
    var $KontenerKartaWizyty = $('#KontenerKartaWizyty');
    var $kartaRezerwacjiWizytyKontener = $("#kartaRezerwacjiWizytyKontener");
    var $wyborLekarza = $("#wyborLekarza");
    var $kontenerWizyt = $("#kontenerWizyt");


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


    $(".dodajWizyte").click(function () {
 

        $kartaRezerwacjiWizytyKontener.css("display", "flex");
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


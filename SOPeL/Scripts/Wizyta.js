/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var $dodajWizyte = $("#dodajWizyte");
    var $KontenerKartaWizyty = $('#KontenerKartaWizyty')
    var $kartaRezerwacjiWizytyKontener = $("#kartaRezerwacjiWizytyKontener");
    
    var kontenerMaterialPortalPacjenta = $("#kontenerMaterialPortalPacjenta");

    //wyborLekarza.change(function () {
    //    pobierzListeRezerwacji(wyborLekarza.children(":selected").attr("id"));
    //});




    $(".dodajWizyte").click(function () {
        //alert("alert");
        

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

   






    //function pobierzListeRezerwacji(selectedID) {
    //    $.ajax({
    //        url: '@Url.Action("pobierzRezerwacje", "Poczekalnia")',
    //        data: {
    //            pracID: selectedID
    //        },
    //        success: function (response) {
    //            kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
    //                kontenerMaterialPortalPacjenta.html(response);
    //            });
    //            kontenerMaterialPortalPacjenta.slideDown(300);
    //        },
    //        error: function () {
    //            alert("Błąd połączenia z serwerem!");

    //            kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
    //                kontenerMaterialPortalPacjenta.html("");
    //            });
    //        }
    //    });
    //}
})

/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    alert("alert na strat");
    var wyborLekarza = $("#wyborLekarza");
    var kontenerMaterialPortalPacjenta = $("#kontenerMaterialPortalPacjenta");

    //wyborLekarza.change(function () {
    //    pobierzListeRezerwacji(wyborLekarza.children(":selected").attr("id"));
    //});




    $dodajWizyte.click(function () {
        alert("Pojedyncze klikniecie klikniecie");

        $kartaRezerwacjiWizytyKontener.css("display", "flex");
        $.ajax({
            url: '/Pacjenci/dodajPacjenta',
            type: 'POST',
            success: function (response) {
                $kartaRezerwacjiWizytyKontener.html(response);
            },
            error: function () {


                alert("Error");
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

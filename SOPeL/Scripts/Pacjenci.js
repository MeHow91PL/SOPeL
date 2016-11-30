/// <reference path="jquery-3.1.1.js" />


$(document).ready(function () {
    var $dodajPacjenta = $("#dodajPacjenta");
    var $kartaRezerwacjiWizytyKontener = $("#kartaRezerwacjiWizytyKontener");

    $dodajPacjenta.click(function () {
        //alert("Pojedyncze klikniecie klikniecie");

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
});


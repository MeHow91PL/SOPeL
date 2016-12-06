/// <reference path="jquery-3.1.1.js" />


$(document).ready(function () {
    var $dodajPacjenta = $("#dodajPacjenta");
    var $kartaRezerwacjiWizytyKontener = $("#kartaRezerwacjiWizytyKontener");
    var $kontenerMaterialPortalPacjenta = $("#kontenerMaterialPortalPacjenta");

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

    $(".EdytujPacjenta").click(function () {
        $kartaRezerwacjiWizytyKontener.css("display", "flex");

        $.ajax({
            url: '/Pacjenci/EdytujPacjenta',
            type: 'POST',
            data: {
                id: $(this).data("idpacjenta")
            },
            success: function (response) {
                $kartaRezerwacjiWizytyKontener.html(response);
            },
            error: function () { }
        });

    });

    $(".UsunPacjenta").click(function () {
        
        if (confirm('Czy jesteś pewien że chcesz usunąć pacjenta?')) {
            $.ajax({
                url: '/Pacjenci/UsunPacjenta',
                type: 'POST',
                data: {
                    id: $(this).data("idpacjenta")
                },
                success: function (response) {
                   $kontenerMaterialPortalPacjenta.html(response);
                },
                error: function () {}
            });
        }

        });
    });


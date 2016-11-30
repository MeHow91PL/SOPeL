/// <reference path="jquery-3.1.1.js" />

$(document).ready(function () {
    var $kontenerMaterialPortalPacjenta = $('#kontenerMaterialPortalPacjenta');
    var $wyborLekarzaRejOnline = $('#wyborLekarzaRejOnline');
    var $dataRezerwacji = $('#dataRezerwacji');
    var podpowiedzi = $('#podpowiedzi'); wyborLekarzaRejOnline

    $dataRezerwacji.datepicker({
        dateFormat: "yy-mm-dd",
        changeYear: true
    });

    $dataRezerwacji.change(function () {
        var dataRezerwacji = $dataRezerwacji.val();
        pobierzTerminarz(dataRezerwacji)
    });

    $wyborLekarzaRejOnline.change(function() {
        var dataRezerwacji = $dataRezerwacji.val();
        pobierzTerminarz(dataRezerwacji)
    })


    function pobierzTerminarz(wybranaData) {
        var selected = $wyborLekarzaRejOnline.children(":selected").attr("id");
        var wzor = /^[0-9]{4}-[0-9]{2}-[0-9]{2}$/;

        if (!wzor.test(wybranaData)) {
            alert("Wybierz datę z kalendarza");
            $kontenerMaterialPortalPacjenta.slideUp(300);
            return;
        }

        $.ajax({
            url: '/RejestracjaOnline/pobierzTerminarzWybranegoLekarza',
            data: {
                idi: selected,
                data: wybranaData
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

});
    
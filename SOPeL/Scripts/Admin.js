/// <reference path="jquery-3.1.1.js" />
$(document).ready(function () {

});

function pokazOpcjeGlowne() {
    $.ajax({
        url: "/Admin/PokazOpcjeGlowne",
        type: "POST",
        success: function (response) {
            $("#opcje-ogolne-kontener").html(response);
        },
        error: function (response) {
            alert("Błąd serwera! Sczegóły w konsoli");
            console.log(response);
        }
    });


    $("#opcje-ogolne-kontener").css("display", "flex");
}

function zamknijOpcjeGlowne() {
    if (confirm("Na pewno zamknąć okno opcji?")) {
        $("#opcje-ogolne-kontener").css("display", "none");
    }
}


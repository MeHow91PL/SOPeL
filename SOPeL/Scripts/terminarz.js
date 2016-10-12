//Otwieranie karty rezerwacji jednym clickiem na mobilkach
$(".terminarz .teminarzaPanelRezerwacji").click(function () {
    if ($(document).width() < 1024) {
        $('#kartaRezerwacjiPortalPacjentaNiezalogowany').css({
            "height": "100%",
            "visibility": "visible"
        });

        $('.idRezerwacji').val($(this).attr('id'));

        szerokoscOkna = $('body').width();
        if (szerokoscOkna > 784)
            szerokoscOkna = 800;

        stworzOknoKartyRezerwacjiPortalPacjetnaNiezalogowany(szerokoscOkna, $(this).attr('id'));

        $('#kartaRezerwacjiPortalPacjentaNiezalogowany').dialog("open");
    }
});

$(".terminarz .teminarzaPanelRezerwacji").dblclick(function () {

    $('#kartaRezerwacjiPortalPacjentaNiezalogowany').css({
        "height": "100%",
        "visibility": "visible"
    });

    szerokoscOkna = $('body').width();
    if (szerokoscOkna > 784)
        szerokoscOkna = 800;

    stworzOknoKartyRezerwacjiPortalPacjetnaNiezalogowany(szerokoscOkna, $(this).attr('id'));

    $('#kartaRezerwacjiPortalPacjentaNiezalogowany').dialog("open");

});


function stworzOknoKartyRezerwacjiPortalPacjetnaNiezalogowany(szerokoscOkna, idRezerwacji) {

    var split = idRezerwacji.split("-");
    $('input.idRezerwacji').val(split[0]);
    $('#kartaRezerwacjiPortalPacjentaNiezalogowany #GodzinaRezerwacji').val(split[1].substring(0, 2) + ":" + split[1].substring(3, 5));
    $('#kartaRezerwacjiPortalPacjentaNiezalogowany #DataRezerwacji').val($('#dataRezerwacji').val());

    $('#kartaRezerwacjiPortalPacjentaNiezalogowany').dialog({
        draggable: true,
        hide: { effect: "fade", duration: 250 },
        show: { effect: "fade", duration: 250 },
        resizable: false,
        width: szerokoscOkna,
        autoOpen: false,
        minHeight: $('#kartaRezerwacjiPortalPacjentaNiezalogowany').css("minHeight"),
        title: "Karta rezerwacji"
    });
}

$(".przychodnia .terminarz .terminarzZarezerwowanyTermin").dblclick(function () {
    $('#kartaRezerwacjiPacjenta').css({
        "display": "block"
    });

    szerokoscOkna = $('body').width();
    if (szerokoscOkna > 784)
        szerokoscOkna = 800;


    var split = $(this).attr('id').split("-");
    $('#kartaRezerwacjiPacjenta #GodzinaRezerwacji').val(split[1].substring(0, 2) + ":" + split[1].substring(3, 5));
    $('#kartaRezerwacjiPacjenta #DataRezerwacji').val($('#dataRezerwacji').val());
    $('#kartaRezerwacjiPacjenta #Pacjent_Imie').val($(this).attr('imie'));
    $('#kartaRezerwacjiPacjenta #Pacjent_Nazwisko').val($(this).attr('nazw'));
    $('#kartaRezerwacjiPacjenta #Pacjent_Pesel').val($(this).attr('pesel'));
    $('#kartaRezerwacjiPacjenta #Pacjent_DokumentTozsamosci').val($(this).attr('dokToz'));
    $('#kartaRezerwacjiPacjenta #Pacjent_Telefon').val($(this).attr('tel'));
    $('#kartaRezerwacjiPacjenta #Pacjent_Email').val($(this).attr('email'));
    $('#kartaRezerwacjiPacjenta #Id').val($(this).attr('idrez'));

    $('#kartaRezerwacjiPacjenta').dialog({
        draggable: true,
        hide: { effect: "fade", duration: 250 },
        show: { effect: "fade", duration: 250 },
        resizable: false,
        width: szerokoscOkna,
        autoOpen: false,
        minHeight: $('#kartaRezerwacjiPacjenta').css("minHeight"),
        title: "Karta rezerwacji"
    });


    $('#kartaRezerwacjiPacjenta').dialog("open");

});


function usunPacjenta() {
    $.ajax({
        url: '@Url.Action("usunPacjenta", "Terminarz")',
        data: {
            wybranaData: dataRezerwacji
        },
        success: function (response) {
            kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
                kontenerMaterialPortalPacjenta.html(response);
            });
            kontenerMaterialPortalPacjenta.slideDown(300);
        },
        error: function () {
            kontenerMaterialPortalPacjenta.slideUp(300, function myfunction() {
                kontenerMaterialPortalPacjenta.html("");
            });
        }
    });
}

function zamknijOknoKartyRezerwacjiPacjenta() {
    $('#kartaRezerwacjiPacjenta').dialog("close");
}

function zamknijOknoKartyRezerwacji() {
    $('#kartaRezerwacji').dialog("close");
}


function zamknijOknoKartyRezerwacjiPortalPacjetnaNiezalogowany() {
    $('#kartaRezerwacjiPortalPacjentaNiezalogowany').dialog("close");
}

function zatwierdzKarteRezerwacji() {
    if (walidujKarteRezerwacji()) {
        return true;
    }
    else {
        return false;
    }
}

function walidujKarteRezerwacji() {

    //Walidacja PESEL
    var valid = true;
    var numerPesel = $('#Pacjent_Pesel');
    var numerPeselErrorSpan = $('#peselErrorMessageSpan');
    var poWalidacji = WalidujPesel(numerPesel.val());

    if ($.trim(numerPesel.val()).length === 0) {
        numerPeselErrorSpan.html("Numer PESEL jest wymagany!");
        valid = false;
    }
    else if (!poWalidacji.valid) {
        numerPeselErrorSpan.html("Podany PESEL jest niepoprawny!");
        valid = false;
    }
    else {
        $('#Pacjent_Plec').val(poWalidacji.plec);
        numerPeselErrorSpan.html("");
    }

    //Walidacja dokumentu tożsamości
    var dokTozsamosci = $('#Pacjent_DokumentTozsamosci');
    var dokTozsamosciErrorSpan = $('#dokTozsamosciErrorMessageSpan');
    var validDokument = walidujDokTozsamosci(dokTozsamosci.val());

    if ($.trim(dokTozsamosci.val()).length === 0) {
        dokTozsamosciErrorSpan.html("Dokument tożsamości jest wymagany!");
        valid = false;
    }
    else if (!validDokument.valid) {
        dokTozsamosciErrorSpan.html("Podany dokument tożsamości jest błędny! Poprawny format: {Seria}{Numer} ABC123456");
        valid = false;
    }
    else {
        dokTozsamosciErrorSpan.html("");
    }
    return valid;
}

       $(document).ready(function myfunction() {
           $('#navPortalPacjenta li').click(function () {
               $('li.active').removeClass("active");
               $(this).addClass("active");
           });

           var zawijacz = $('#zwijaczMenu');
         

           var kontenerMaterialPortalPacjenta = $('#kontenerMaterialPortalPacjenta');
           kontenerMaterialPortalPacjenta.css({
               "height": "100%",
               "visibility": "visible"
           });
           //kontenerMaterialPortalPacjenta.slideUp(1);

           var rejestracjaOnlineLekarzInput = $('#rejestracjaOnlineLekarzInput');

           rejestracjaOnlineLekarzInput.autocomplete({
               source: '@Url.Action("GetJsonPrac")'
           });
           $(".ui-autocomplete").css("width", rejestracjaOnlineLekarzInput.width());

       });

       function zawijaczKlik() {
           $('#zwijaczMenu').toggleClass('rotate');
           $('#sidebarContainer').toggleClass('nav-sidebarZwiniety');
           $('#navPortalPacjenta').toggleClass('nav-sidebarZwiniety');
       }
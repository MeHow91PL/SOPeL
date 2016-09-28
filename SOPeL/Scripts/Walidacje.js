function walidujDokTozsamosci(input) {
    var wzorPaszport = /^[A-Z]{2}[0-9]{7}$/;
    var wzorDowod = /^[A-Z]{3}[0-9]{6}$/;
    var rodzaj = "";
    var numer = input.replace(/ /g,'');

    if (wzorPaszport.test(numer.toUpperCase()) == true) {
        rodzaj = "paszport";
        var pozCyfryKontrolnej = 2;
        var wagi =[7, 3, 0, 1 , 7, 3, 1, 7, 3];
        }
    else if (wzorDowod.test(numer.toUpperCase())) {
        rodzaj = "dowod";
        var pozCyfryKontrolnej = 3;
        var wagi = [7, 3, 1, 0, 7, 3, 1, 7, 3];
    }
    else {
        return { valid: false, typ: "" };
    }

    var cyfraKontrolna = wartoscLitery(numer.substring(pozCyfryKontrolnej, pozCyfryKontrolnej + 1));
    var suma = 0;
    for (var i = 0; i < wagi.length; i++) {
            suma += (parseInt(wartoscLitery(numer.toUpperCase().substring(i, i + 1))) * wagi[i]);
    }
    var modulo = suma % 10;
    if (modulo == cyfraKontrolna)
    {
        return { valid: true, typ: rodzaj };
    }
    else {
        return { valid: false, typ: "" };
    }

        function wartoscLitery(literka) {
            var litery =
                ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z'];
            for (j = 0; j < litery.length; j++) {
                if (literka == litery[j]) {
                    return j;
                }
            }
        }
    }

function WalidujPesel(pesel) {
    var wzor = /^[0-9]{11}$/;
    if (wzor.test(pesel) == false) {
        return { valid: false, plec: "", dataUrodzenia: "" };
    }

    // Wagi dla poszczególnych cyfr peselu
    var wagi = [9, 7, 3, 1, 9, 7, 3, 1, 9, 7];
    var suma = 0;

    //dodanie 10 pierwszych liczb z wagą
    for (var i = 0; i < wagi.length; i++) {
        suma += (parseInt(pesel.substring(i, i + 1), 10) * wagi[i]);
    }
    //Sprawdzenie sumy kontrolnej jeżeli ost liczba = modulo suma z pierwszych 10 to jest ok
    suma = suma % 10;
    var valid = (suma === parseInt(pesel.substring(10, 11), 10));

    if (valid == false) {
        return { valid: false, plec: "", dataUrodzenia: "" };
    }

    // Wycinamy daty z numeru
    var rok = parseInt(pesel.substring(0, 2), 10);
    var miesiac = parseInt(pesel.substring(2, 4), 10) - 1;
    var dzien = parseInt(pesel.substring(4, 6), 10);

    //W PESELU do miesiąca dodaje się:
    //dla urodzonych 1800-1899 – 80
    //dla urodzonych 2000-2099 – 20
    //dla urodzonych 2100-2199 – 40
    //dla urodzonych 2200-2299 – 60
    if (miesiac > 80) {
        rok = rok + 1800;
        miesiac = miesiac - 80;
    }
    else if (miesiac > 60) {
        rok = rok + 2200;
        miesiac = miesiac - 60;
    }
    else if (miesiac > 40) {
        rok = rok + 2100;
        miesiac = miesiac - 40;
    }
    else if (miesiac > 20) {
        rok = rok + 2000;
        miesiac = miesiac - 20;
    }
    else {
        rok += 1900;
    }
    // Daty sa ok. Teraz ustawiamy.
    var urodzony = new Date();
    urodzony = rok + "-" + miesiac + "-" + dzien;

    

    //plec
    if (parseInt(pesel.substring(9, 10), 10) % 2 === 1) {
        var plec = 'm';
    } else {
        var plec = 'k';
    }

    //zwraca obiekt z info o walidacji, płci i daty urodzenia
    return { valid: true, plec: plec, dataUrodzenia: urodzony };
}
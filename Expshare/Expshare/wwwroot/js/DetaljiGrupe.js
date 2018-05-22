﻿function detaljiGrupeModal(IdGrupa) {
    var detaljiGrupeTitle = document.getElementById('detaljiGrupeTitle');
    var trenutnaGrupa = document.getElementById('trenutnaGrupa');
    trenutnaGrupa.value = IdGrupa;
    var serviceURL = '/Expshare/DohvatiNazivGrupe/' + IdGrupa;
    $.ajax({
        type: "GET",
        url: serviceURL,
        async: true,
        dataType: "json",
        success: function (data, status) {
            detaljiGrupeTitle.textContent = data;
        },
        error: function () {

        }
    });
    serviceURL = '/Expshare/DohvatiStanjeIzmeduKorisnika/?idGrupa=' + IdGrupa;
    $.ajax({
        type: "GET",
        url: serviceURL,
        async: true,
        dataType: "json",
        success: function (data, status) {
            var mojiDugoviTable = document.getElementById('mojiDugoviTable');
            var dugoviPremaMeniTable = document.getElementById('dugovanjaPremaMeniTable');
            var mojiDugoviRowId = 0;
            var dugoviPremaMeniRowId = 0;
            var ukupniMojiDugovi = 0;
            var ukupniDugoviPremaMeni = 0;
            var trenutniKorisnik = document.getElementById('trenutniKorisnik').value;
            var razrijesiSOsobomSelect = document.getElementById('razrijesiSOsobom');
            for (var i = 0; i < data.length; i++) {
                if (data[i].stanje > 0) {
                    var row = dugoviPremaMeniTable.insertRow(dugoviPremaMeniRowId);
                    var email = row.insertCell(0);
                    var stanje = row.insertCell(1);
                    email.innerHTML = data[i].nickname;
                    stanje.innerHTML = parseFloat(data[i].stanje).toFixed(2) + ' kn';
                    ukupniDugoviPremaMeni = ukupniDugoviPremaMeni + data[i].stanje;
                    dugoviPremaMeniRowId++;
                } else {
                    var row = mojiDugoviTable.insertRow(mojiDugoviRowId);
                    var email = row.insertCell(0);
                    var stanje = row.insertCell(1);
                    email.innerHTML = data[i].nickname;
                    stanje.innerHTML = parseFloat(-data[i].stanje).toFixed(2) + ' kn';
                    ukupniMojiDugovi = ukupniMojiDugovi - data[i].stanje;
                    mojiDugoviRowId++;
                }
                razrijesiSOsobomSelect.add(new Option(data[i].nickname, data[i].email));
                if (i === 0) {
                    var razrijesiIznos = document.getElementById('razrijesiIznos');
                    razrijesiIznos.value = - data[i].stanje;
                }
            }
            var lastRow = mojiDugoviTable.insertRow(mojiDugoviRowId);
            var ukupno = lastRow.insertCell(0);
            var ukupniIznos = lastRow.insertCell(1);
            ukupno.innerHTML = "Ukupno".bold();
            ukupniIznos.innerHTML = (parseFloat(ukupniMojiDugovi).toFixed(2) + ' kn').bold();
            lastRow = dugoviPremaMeniTable.insertRow(dugoviPremaMeniRowId);
            ukupno = lastRow.insertCell(0);
            ukupniIznos = lastRow.insertCell(1);
            ukupno.innerHTML = "Ukupno".bold();
            ukupniIznos.innerHTML = (parseFloat(ukupniDugoviPremaMeni).toFixed(2) + ' kn').bold();
        },
        error: function () {

        }
    });
    serviceURL = '/Expshare/DohvatiGrupeIStanjaKorisnika/' + IdGrupa;
    $.ajax({
        type: "GET",
        url: serviceURL,
        async: true,
        dataType: "json",
        success: function (data, status) {
            var trenutniKorisnik = document.getElementById('trenutniKorisnik').value;
            var groupTableBody = document.getElementById('detaljiGrupeTable');
            var clanoviKreirajUplatu = document.getElementById('clanoviKreirajUplatu');
            for (var i = 0; i < data.length; i++) {
                var row = groupTableBody.insertRow(i);
                var nickname = row.insertCell(0);
                var stanje = row.insertCell(1);
                nickname.innerHTML = data[i].nickname;
                stanje.innerHTML = parseFloat(data[i].stanje).toFixed(2) + ' kn';
                if (data[i].stanje < 0) {
                    stanje.style.color = 'red';
                }
                if (data[i].idKorisnik !== trenutniKorisnik) {
                    clanoviKreirajUplatu.options.add(new Option(data[i].nickname, data[i].idKorisnik, true, true));
                }
            }
        },
        error: function () {

        }
    });
}

function kreirajUplatu() {
    var serviceURL = '/Expshare/KreirajUplatu/';
    var trenutnaGrupa = document.getElementById('trenutnaGrupa').value;
    var trenutniKorisnik = document.getElementById('trenutniKorisnik').value;
    var groupTableBody = document.getElementById('detaljiGrupeTable');
    var clanoviKreirajUplatu = document.getElementById('clanoviKreirajUplatu');
    var iznosKreirajUplatu = document.getElementById('iznosKreirajUplatu').value;
    var raspodijeliSamnom = document.getElementById('raspodijeliSamnomKreirajUplatu').checked;
    var select = document.getElementById('clanoviKreirajUplatu');
    var kreirajUplatuViewModel = {
        IdKorisnik: trenutniKorisnik,
        IdGrupa: trenutnaGrupa,
        Iznos: iznosKreirajUplatu,
        KorisniciZaUplatu: [],
        RaspodijeliSamnom: raspodijeliSamnom
    };
    for (var i = 0; i < select.options.length; i++) {
        var option = select.options[i];
        if (option.selected === true) {
            kreirajUplatuViewModel.KorisniciZaUplatu.push(option.value)
        }
    }

    $.ajax({
        type: "POST",
        url: serviceURL,
        async: true,
        dataType: "json",
        data: JSON.stringify(kreirajUplatuViewModel),
        contentType: "application/json; charset=utf-8",
        success: function (data, status) {
            osvjeziPodatke();
        },
        error: function () {

        }
    });
}

function closeDetaljiGrupeModal(reload = true) {
    var detaljiGrupeTable = document.getElementById('detaljiGrupeTable');
    var mojiDugoviTable = document.getElementById('mojiDugoviTable');
    var dugoviPremaMeniTable = document.getElementById('dugovanjaPremaMeniTable');
    var select = document.getElementById('clanoviKreirajUplatu');
    deleteRows(detaljiGrupeTable);
    deleteRows(mojiDugoviTable);
    deleteRows(dugoviPremaMeniTable);
    deleteOptions(select);
    if (reload === true) {
        window.location.reload();
    }
}

function osvjeziPodatke() {
    var trenutnaGrupa = document.getElementById('trenutnaGrupa').value;
    closeDetaljiGrupeModal(false);
    detaljiGrupeModal(trenutnaGrupa);
}

function dodajClana() {
    var dodajClanaEmail = document.getElementById('dodajClanaEmail').value;
    var trenutnaGrupa = document.getElementById('trenutnaGrupa').value;
    var nickname = document.getElementById('nickname').value;
    var dodajClanaViewModel = {
        Email: dodajClanaEmail,
        IdGrupa: trenutnaGrupa, 
        NickName: nickname
    };
    var serviceURL = '/Expshare/DodajClana/';
    $.ajax({
        type: "POST",
        url: serviceURL,
        async: true,
        data: JSON.stringify(dodajClanaViewModel),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data, status) {
            osvjeziPodatke();
        },
        error: function () {

        }
    });
}

function deleteRows(table) {
    var tableRows = table.getElementsByTagName('tr');
    var rowCount = tableRows.length;

    for (var i = rowCount - 1; i >= 0; i--) {
        table.removeChild(tableRows[i]);
    }
}

function deleteOptions(select) {
    var options = select.options;
    var optionCount = options.length;

    for (var i = optionCount - 1; i >= 0; i--) {
        options.remove(i);
    }
}

function razrijesiDugove() {
    var trenutnaGrupa = document.getElementById('trenutnaGrupa').value;
    var razrijesiSOsobom = document.getElementById('razrijesiSOsobom').value;
    var trenutniKorisnik = document.getElementById('trenutniKorisnik').value;
    var razrijesiIznos = document.getElementById('razrijesiIznos').value;
    var razrijesiDugoveViewModel = {
        IdKorisnik: trenutniKorisnik,
        PrimateljEmail: razrijesiSOsobom,
        IdGrupa: trenutnaGrupa,
        Iznos: razrijesiIznos
    }
    var url = '/Expshare/RazrijesiDugove';
    $.ajax({
        type: "POST",
        url: url,
        async: true,
        dataType: "json",
        data: JSON.stringify(razrijesiDugoveViewModel),
        contentType: "application/json; charset=utf-8",
        success: function (data, status) {
            osvjeziPodatke();
        },
        error: function () {

        }

    });
}

function iznosZaRazrjesavanje() {
    var trenutnaGrupa = document.getElementById('trenutnaGrupa').value;
    var razrijesiSOsobom = document.getElementById('razrijesiSOsobom').value;
    serviceURL = '/Expshare/DohvatiStanjeIzmeduKorisnika/?idGrupa=' + trenutnaGrupa;
    $.ajax({
        type: "GET",
        url: serviceURL,
        async: true,
        dataType: "json",
        success: function (data, status) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].email.toLowerCase() === razrijesiSOsobom.toLowerCase()) {
                    var razrijesiIznos = document.getElementById('razrijesiIznos');
                    razrijesiIznos.value = - data[i].stanje;
                    break;
                }
            }
        },
        error: function () {

        }
    });
}

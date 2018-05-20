function dohvatiIDTrenutnogKorisnika() {
    var serviceURL = '/Expshare/DohvatiIDTrenutnogKorisnika';
    var ID = "";
    $.ajax({
        type: "GET",
        url: serviceURL,
        async: false,
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
    function successFunc(data, status) {
        if(status !== 200) {
            alert("Nije OK");
            return;
        }
        ID = data.id;
    }
    function errorFunc() {
        alert("Greška! Pokušajte ponovno!");
    }
    return ID;
}
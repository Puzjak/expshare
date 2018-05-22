function emailRegex(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validateEmail(email, span) {
    if (email === null || email === "") {
        span.textContent = "";
        return false;
    } else if (emailRegex(email) === false) {
        span.textContent = "Unesite ispravan format Email adrese!";
        return false;
    } else {
        span.textContent = "";
        return true;
    }
}


function validateLozinka(lozinka, span) {
    if (lozinka === null || lozinka === "") {
        span.textContent = "";
        return false;
    } else if (lozinka.length < 8) {
        span.textContent = "Lozinka mora imati barem 8 znakova!";
        return false;
    } else {
        span.textContent = "";
        return true;
    }
}

function validatePotvrdiLozinku(lozinka, potvrdiLozinku, span) {
    if (potvrdiLozinku === null || potvrdiLozinku === "") {
        span.textContent = "";
        return false;
    } else if (potvrdiLozinku !== lozinka) {
        span.textContent = "Lozinke se ne podudaraju!";
        return false;
    } else {
        span.textContent = "";
        return true;
    }
}

function checkLogin() {
    var email = document.getElementById("emailLogin").value;
    var emailSpan = document.getElementById("emailLoginSpan");
    var lozinka = document.getElementById("lozinkaLogin").value;
    var lozinkaSpan = document.getElementById("lozinkaLoginSpan");
    var loginButton = document.getElementById('loginButton');

    var emailCorrect = validateEmail(email, emailSpan);
    var lozinkaCorrect = validateLozinka(lozinka, lozinkaSpan);

    if (emailCorrect === true && lozinkaCorrect === true) {
        loginButton.disabled = false;
    } else {
        loginButton.disabled = true;
    }
}

function checkRegister() {
    var email = document.getElementById("emailRegister").value;
    var emailSpan = document.getElementById("emailRegisterSpan");
    var nickname = document.getElementById("nicknameRegister");
    var nicknameSpan = document.getElementById("nicknameRegisterSpan");
    var lozinka = document.getElementById("lozinkaRegister").value;
    var lozinkaSpan = document.getElementById("lozinkaRegisterSpan");
    var potvrdiLozinku = document.getElementById("potvrdiLozinkuRegister").value;
    var potvrdiLozinkuSpan = document.getElementById("potvrdiLozinkuRegisterSpan");
    var registerButton = document.getElementById('registerButton');

    var emailCorrect = validateEmail(email, emailSpan);
    var lozinkaCorrect = validateLozinka(lozinka, lozinkaSpan);
    var potvrdiLozinkuCorrect = validatePotvrdiLozinku(lozinka, potvrdiLozinku, potvrdiLozinkuSpan);

    if (emailCorrect === true && lozinkaCorrect === true && potvrdiLozinkuCorrect === true) {
        registerButton.disabled = false;
    } else {
        registerButton.disabled = true;
    }
}

function login() {
    var exit = false;
    var email = document.getElementById("emailLogin").value;
    var lozinka = document.getElementById("lozinkaLogin").value;
    var zapamtiMe = document.getElementById("zapamtiMeLogin").checked;

    var serviceURL = '/Expshare/Login';
    $.ajax({
        type: "POST",
        url: serviceURL,
        data: JSON.stringify({
            "Email": email,
            "Lozinka": lozinka,
            "ZapamtiMe": zapamtiMe
        }),
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        if (data.isAuthenticated === true) {
            window.location.reload(true);
        } else {
            alert(data.errorMessage);
        }
    }
    function errorFunc() {
        alert("Greška! Pokušajte ponovno!");
    }
}

function register() {
    var email = document.getElementById("emailRegister").value;
    var nickname = document.getElementById("nicknameRegister").value;
    var lozinka = document.getElementById("lozinkaRegister").value;
    var potvrdiLozinku = document.getElementById("potvrdiLozinkuRegister").value;
    var zapamtiMe = document.getElementById("zapamtiMeRegister").checked;

    var serviceURL = '/Expshare/Register';
    $.ajax({
        type: "POST",
        url: serviceURL,
        data: JSON.stringify({
            "Email": email,
            "Nickname": nickname,
            "Lozinka": lozinka,
            "PotvrdiLozinku": potvrdiLozinku,
            "ZapamtiMe": zapamtiMe
        }),
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
    function successFunc(data, status) {
        if (data.isAuthenticated === true) {
            window.location.reload(true);
        } else {
            alert(data.errorMessage);
        }
    }
    function errorFunc() {
        alert("Greška! Pokušajte ponovno!");
    }
}
$(document).ready(function () {

});

function RealizarLogin() {

    if (VerificaCamposPreenchidos()) {

        var jsonBody = {
            "email": $('#email').val(),
            "senha": $('#senha').val()
        };

        $.ajax({
            url: "/Login/Login",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                console.log(response);
                if (response.erro) {
                    alert(response.mensagem);
                }
                else {
                    alert('tudo certo')

                }
            },
            error: function (response) {
                alert(response);
            }
        });
    }
    
}

function VerificaCamposPreenchidos() {

    if (IsNullOrEmpty($('#email').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Email');
        return false;
    }
    else if (IsNullOrEmpty($('#senha').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Senha');
        return false;
    }

    return true;
}
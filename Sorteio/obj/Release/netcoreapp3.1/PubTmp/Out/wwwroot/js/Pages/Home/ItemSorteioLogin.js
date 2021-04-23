$(document).ready(function () {
   
});

function GerarTicket() {
    $('#modalGerarTicket').modal();
}

//Login
function RealizarLogin() {

    var USUARIO_ADMIN = 1;
    var USUARIO_CLIENTE = 2;

    if (VerificaCamposPreenchidosLogin()) {

        var arrayItensEscolhidos = [];
        $('.numero-escolhido').each(function (i, element) {
            var numero = $(element).text();
            arrayItensEscolhidos.push(numero)
        })

        var jsonBody = {
            "email": $('#emailLogin').val(),
            "senha": $('#senhaLogin').val(),
            "id_sorteio": parseInt($('#idSorteioSelecionado').val()),
            "valor_total": ConverterParaFloat($('#valor_total').text()),
            "numeroSorteios": GerarJsonNumerosSorteios(arrayItensEscolhidos)
        };

        $.ajax({
            url: "/Login/LogarCadastraNumeros/",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(jsonBody),
            success: function (response) {
                if (response.erro) {
                    swal("Erro", response.mensagem, "error");
                }
                else {
                    if (parseInt(response.model.id_tipo_usuario) == USUARIO_ADMIN) {
                        window.location.href = "/Sorteios";
                    }
                    else if (parseInt(response.model.id_tipo_usuario) == USUARIO_CLIENTE) {
                        window.location.href = "/AcessoInterno";
                    }
                    else {
                        swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
                    }
                }
            },
            error: function (response) {
                swal("Erro", response.mensagem, "error");
            }
        });
    }

}

function GerarJsonNumerosSorteios(arrayItensEscolhidos) {

    var item;
    var listaItensEscolhidos = [];

    $(arrayItensEscolhidos).each(function (i, element) {

        item = {
            "numero": parseInt(element)
        };

        listaItensEscolhidos.push(item);

    });

    return listaItensEscolhidos;

}

function VerificaCamposPreenchidosLogin() {

    if (IsNullOrEmpty($('#emailLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Email');
        return false;
    }
    else if (IsNullOrEmpty($('#senhaLogin').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido('Senha');
        return false;
    }

    return true;
}



//Cadastro
function AbrirModalCadastrarUsuario() {
    alert('testando');
    return; 

    CadastrarUsuario();
}

function CadastrarUsuario() {

    var dadosJson = GerarDadosJsonCadastrarUsuario();

    $.ajax({
        url: "/Login/CadastrarUsuarioLogin",
        type: "POST",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        data: JSON.stringify(dadosJson),
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success")
                    .then((okay) => {
                        window.location.href = "/AcessoInterno";
                    });
            }
            else {
                swal("Opss", response.mensagem, "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }
    });

}

function GerarDadosJsonCadastrarUsuario() {
    var USUARIO_CLIENTE = 2;

    return {
        nome: "Teste",
        email: "teste@teste.com",
        senha: "123",
        celular: "(19) 987087683",
        cpf: "459.465.684-45",
        id_tipo_usuario: USUARIO_CLIENTE
    }
}
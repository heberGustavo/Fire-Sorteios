$(document).ready(function () {
    $('.pgwSlideshow').pgwSlideshow();
});

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
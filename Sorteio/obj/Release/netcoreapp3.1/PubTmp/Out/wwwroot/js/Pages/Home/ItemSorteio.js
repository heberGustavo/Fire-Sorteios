$(document).ready(function () {
    $('.pgwSlideshow').pgwSlideshow();

    var textoHtml = $('#texto_longo_html').text();
    $('#contentSummernote').html('');

    setTimeout(function () {
        $('#contentSummernote').append(textoHtml);
    }, 80);

    GerarTicket();

});

function GerarTicket() {
    $('#modalGerarTicket').modal();
}

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

var itens_escolhidos = [];
$('.item-disponivel').click(function (e) {
    e.preventDefault();

    var valor_rifa = ConverterParaFloat($('#valor_rifa').text());

    if ($('#sessao-fixa').hasClass('d-none')) {
        $('#sessao-fixa').removeClass('d-none')
    }

    $('#numeros_selecionados').html('');

    var numero_atual = $(this).text();
    itens_escolhidos.push(numero_atual);

    itens_escolhidos.forEach(function (value) {
        var html = `<div class="numero-escolhido">${value}</div>`;

        $('#numeros_selecionados').append(html);
    });

    $('#quantidade_selecionado').text(itens_escolhidos.length);
    $('#valor_total').text(FormatDinheiro((itens_escolhidos.length) * valor_rifa));

});
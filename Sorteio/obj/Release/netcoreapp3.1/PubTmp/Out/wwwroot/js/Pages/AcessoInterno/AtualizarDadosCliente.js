$(document).ready(function () {

});

function AtualizarDadosCliente() {

    if (VerificarCamposObrigatoriosDadosClientes()) {

        var dadosJson = GerarJsonDadosCliente();

        $.ajax({
            url: "/AcessoInterno/EditarDadosCliente",
            type: "POST",
            contentType: 'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(dadosJson),
            success: function (response) {
                if (!response.erro) {
                    swal("Sucesso", response.mensagem, "success");
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

}

function GerarJsonDadosCliente() {
    return {
        "id_usuario": parseInt($('#idUsuarioLogado').val()),
        "nome": $('#nome').val(),
        "cpf": $('#cpf').val(),
        "data_de_nascimento": $('#data_de_nascimento').val(),
        "celular": $('#telefone').val(),
        "cep": $('#cep').val(),
        "logadouro": $('#logadouro').val(),
        "numero": parseInt($('#numero').val()),
        "bairro": $('#bairro').val(),
        "complemento": $('#complemento').val(),
        "estado": $('#estado').val(),
        "cidade": $('#cidade').val(),
        "referencia": $('#referencia').val()
    }
}

function VerificarCamposObrigatoriosDadosClientes() {
    if (IsNullOrEmpty($('#nome').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido("Nome");
        return false;
    }
    else if (IsNullOrEmpty($('#cpf').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido("CPF");
        return false;
    }
    else if (IsNullOrEmpty($('#data_de_nascimento').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido("Data de Nascimento");
        return false;
    }
    else if (IsNullOrEmpty($('#telefone').val().trim())) {
        MostrarModalErroCampoObrigatorioNaoPreenchido("Telefone");
        return false;
    }
    return true;
}

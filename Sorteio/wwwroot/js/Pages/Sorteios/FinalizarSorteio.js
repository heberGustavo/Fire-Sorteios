$(document).ready(function () {

});

function AbrirModalFinalizarSorteio(id) {
    alert('Falta enviar os dados daqui para cadastrar e finalizar o sorteio')

    $('#idSorteioSelecionado').val(id);

    $('#modalFinalizarSorteio').modal();
}

function FinalizarSorteio() {
    
    var id = parseInt($('#idSorteioSelecionado').val());

    $.ajax({
        url: "/Sorteios/FinalizarSorteio/" + id,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                swal("Sucesso", response.mensagem, "success")
                    .then((okay) => {
                        window.location.href = "/Sorteios";
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
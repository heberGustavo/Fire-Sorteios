$(document).ready(function () {
   
});

function BuscarTodosNumeros(idSorteio) {

    $.ajax({
        url: "/Sorteios/BuscarTodosNumerosSorteioPorId/" + idSorteio,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesTodosNumeros(response);
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

function CriarBotoesTodosNumeros(dados) {
    var STATUS_PENDENTE = 1;
    var STATUS_PAGO = 2;

    $('#lista_numeros_sorteio').html('');
    var quantidadeDeNumeros = parseInt($('#quantidadeNumerosSorteio').val());
    var meuArrayNumero = [];

    $(dados).each(function (i, element) {
        meuArrayNumero.push({ "numero": element.numero, "status": element.id_status_pedido })
    });

    for (i = 0; i < quantidadeDeNumeros; i++) {

        console.log(meuArrayNumero[i].numero)

        /////AQUUIIII

        //if (!meuArrayNumero.includes(i)) { //disponiveis
        //    var item = `<button class="itens-numero-sorteio item-disponivel">${i.toString().padStart(3, "0")}</button>`;
        //    $('#lista_numeros_sorteio').append(item);
        //}
        //else if (meuArrayNumero.includes(i)) { //pendente ou pago
        //    var item = `<button class="itens-numero-sorteio item-disponivel">${i.toString().padStart(3, "0")}</button>`;
        //    $('#lista_numeros_sorteio').append(item);
        //}

    }

}

function BuscarNumerosDisponiveis(idSorteio) {

    $.ajax({
        url: "/Sorteios/BuscarTodosNumerosSorteioPorId/" + idSorteio,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesNumerosDisponiveis(response);
            }
            else {
                swal("Opss", "Erro ao filtrar dados.", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }

    });
}

function CriarBotoesNumerosDisponiveis(dados) {

    $('#lista_numeros_sorteio').html('');
    var quantidadeDeNumeros = parseInt($('#quantidadeNumerosSorteio').val());
    var meuArrayNumero = [];

    $(dados).each(function (i, element) {
        meuArrayNumero.push(element.numero)
    });

    for (i = 0; i < quantidadeDeNumeros; i++) {

        if (!meuArrayNumero.includes(i)) {
            var item = `<button class="itens-numero-sorteio item-disponivel">${i.toString().padStart(3, "0")}</button>`;
            $('#lista_numeros_sorteio').append(item);
        }

    }

}

function BuscarNumerosReservados(idSorteio, idStatusReservado) {

    $.ajax({
        url: "/Sorteios/BuscarNumerosReservadoOuPagoSorteioPorId/" + idSorteio + "/" + idStatusReservado,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesNumerosReservadoOuPago(response, 'item-reservado')
            }
            else {
                swal("Opss", "Erro ao filtrar dados.", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }

    });

}

function BuscarNumerosPagos(idSorteio, idStatusPago) {

    $.ajax({
        url: "/Sorteios/BuscarNumerosReservadoOuPagoSorteioPorId/" + idSorteio + "/" + idStatusPago,
        type: "GET",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        success: function (response) {
            if (!response.erro) {
                CriarBotoesNumerosReservadoOuPago(response, 'item-pago');
            }
            else {
                swal("Opss", "Erro ao filtrar dados.", "error");
            }
        },
        error: function (response) {
            swal("Erro", "Aconteceu um imprevisto. Contate o administrador", "error");
            console.log(response);
        }

    });

}

function CriarBotoesNumerosReservadoOuPago(dados, classeBotao) {

    $('#lista_numeros_sorteio').html('');

    $(dados).each(function (i, element) {

        var item = `<button class="itens-numero-sorteio ${classeBotao}" data-toggle="tooltip" data-placement="top" title="Pago por: ${element.nome_usuario}">${element.numero.toString().padStart(3, "0")}</button>`;
        $('#lista_numeros_sorteio').append(item);
    });

    $('[data-toggle="tooltip"]').tooltip();

}
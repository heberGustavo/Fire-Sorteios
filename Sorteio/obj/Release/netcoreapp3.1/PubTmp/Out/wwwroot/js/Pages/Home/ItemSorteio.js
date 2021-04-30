$(document).ready(function () {
    $('.pgwSlideshow').pgwSlideshow();

    var textoHtml = $('#texto_longo_html').text();
    $('#contentSummernote').html('');

    setTimeout(function () {
        $('#contentSummernote').append(textoHtml);
    }, 80);

});

var itens_escolhidos = [];
function EscolhaItemDisponivel(botao) {

    //Mostra sessão
    if ($('#sessao-fixa').hasClass('d-none')) {
        $('#sessao-fixa').removeClass('d-none')
    }

    $('#numeros_selecionados').html('');

    //Remove dos itens selecionados e habilita o botao
    if ($(botao).hasClass("notactive")) {

        var numeroClicado = $(botao).text();

        if ($.inArray(numeroClicado, itens_escolhidos) !== -1) {
            itens_escolhidos.splice($.inArray(numeroClicado, itens_escolhidos), 1);
            $(botao).removeClass("notactive");

            MostrarItensSelecionados(itens_escolhidos);
        }
        return;
    }
    else {

        var numero_atual = $(botao).text();
        itens_escolhidos.push(numero_atual);

        MostrarItensSelecionados(itens_escolhidos);

        $(botao).addClass("notactive");
    }

}

function MostrarItensSelecionados(itens_escolhidos) {

    itens_escolhidos.forEach(function (value) {
        var html = `<div class="numero-escolhido">${value}</div>`;

        $('#numeros_selecionados').append(html);
    });

    var valor_rifa = ConverterParaFloat($('#valor_rifa').text());
    var valor_total_rifas = itens_escolhidos.length * valor_rifa;

    $('#quantidade_selecionado').text(itens_escolhidos.length);
    $('#valor_total').text(valor_total_rifas);
    $('#valor_total_visual').text(FormatDinheiro(ConverterParaFloat(valor_total_rifas)));
}
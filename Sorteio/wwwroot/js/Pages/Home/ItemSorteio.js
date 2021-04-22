$(document).ready(function () {
    $('.pgwSlideshow').pgwSlideshow();

    var textoHtml = $('#texto_longo_html').text();
    $('#contentSummernote').html('');

    setTimeout(function () {
        $('#contentSummernote').append(textoHtml);
    }, 80);

});

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

    var valor_total_rifas = itens_escolhidos.length * valor_rifa;

    $('#quantidade_selecionado').text(itens_escolhidos.length);
    $('#valor_total').text(valor_total_rifas);

});
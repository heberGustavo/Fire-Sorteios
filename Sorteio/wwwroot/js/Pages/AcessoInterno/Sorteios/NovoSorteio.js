$(document).ready(function () {

    // Inicialzia o Editor
    //$('.textarea-editor').wysihtml5();
    $('.summernote').summernote({
        height: 300,
        minHeight: null,
        maxHeight: null,
        focus: true,
        lang: 'pt-BR',
        toolbar: [
            ['style', ['bold', 'italic', 'underline']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']]
        ], 
        placeholder: 'Descreva as característica do produto',
    });

    $('#edit').click(function (e) {
        e.preventDefault();
        $('.summernote').summernote({ focus: true });
    });

    $('#save').click(function (e) {
        e.preventDefault();
        var markup = $('.summernote').summernote('code');
        $('.summernote').summernote('destroy');
    });
});
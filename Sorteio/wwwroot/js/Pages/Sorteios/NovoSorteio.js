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

document.querySelector('#file-input').addEventListener("change", previewImages);
function previewImages() {

    var lista_arquivos = [];
    var preview = document.querySelector('#preview');

    if (this.files) {
        [].forEach.call(this.files, readAndPreview);
    }

    UploadImage(lista_arquivos); /**/

    function readAndPreview(file) {
        lista_arquivos.push(file)

        // Make sure `file.name` matches our extensions criteria
        if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
            return alert(file.name + " is not an image");
        } // else...

        var reader = new FileReader();

        reader.addEventListener("load", function () {
            var image = new Image();
            image.height = 100;
            image.title = file.name;
            image.src = this.result;
            preview.appendChild(image);
        });

        reader.readAsDataURL(file);

    }

}

function CriarInputsDinamicamenteComLinkDosArquivos(caminhosArquivo) {
    $('#inputs_de_links_gerados').html('');

    var listaInputs = [];
    $(caminhosArquivo).each(function (i, link) {
        var input = `
                        <div class="input_dinamico">
                            <input type="hidden" class="imagens_galeria" value="${link}"/>
                        </div>
                    `;
        listaInputs.push(input);
    });

    $('#inputs_de_links_gerados').append(listaInputs);
}

//$('.input_dinamico .imagens_galeria').each(function (i, elemento) { console.log(elemento) })
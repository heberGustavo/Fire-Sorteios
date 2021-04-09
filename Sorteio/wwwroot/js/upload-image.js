function UploadImage(sender, idElementoLabel, idElementoInputCaminhoArquivo, idInput) {

    var loading = MostrarCarregamentoDaPagina();
    $('.loading').append(loading);

    var fi = document.getElementById(idInput);

    if (fi.files.length < 2) {
        var fsizet = 0;

        for (var i = 0; i <= fi.files.length - 1; i++) {

            // TAMANHO DO ARQUIVO
            var fsize = fi.files.item(i).size;

            // TOTAL
            fsizet = fsizet + fsize;
        }

        if (fsizet > 0 && fsizet <= 5242880) {

            var idFile = $(sender).attr('id');
            var file = sender.files[0];
            var formData = new FormData();

            var totalFiles = document.getElementById(idFile).files.length;
            for (var i = 0; i < totalFiles; i++) {
                file = document.getElementById(idFile).files[i];
                formData.append("file", file);
                $('#' + idElementoLabel).html(file.name);
            }

            $.ajax({
                type: "POST",
                url: "/Arquivos/Upload",
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    $(".loader-wrapper").fadeOut("slow");
                    swal("Sucesso", "Arquivo foi enviado com sucesso!", "success");

                    $('#' + idElementoInputCaminhoArquivo).val(response.encrypted);
                },
                error: function (error) {
                    console.log(error)
                    swal("Ops!", "Erro so submeter a foto." + error, "error");
                    $(".loader-wrapper").fadeOut("slow");
                }
            });

        } else if (fsizet > 5242880) {
            alert('Não permetido maior que 5mb');
            $(".loader-wrapper").fadeOut("slow");
        }

    }
    else {
        alert('Maximo de 1 arquivos.');
        $(".loader-wrapper").fadeOut("slow");
    }

}

function UploadFileCustom(sender, idElementoInputCaminhoArquivo, idInput, idBotaoEnviar) {
    
    var loading = MostrarCarregamentoDaPagina();
    $('.loading').append(loading);

    var fi = document.getElementById(idInput);

    if (fi.files.length < 2) {
        var fsizet = 0;

        for (var i = 0; i <= fi.files.length - 1; i++) {

            // TAMANHO DO ARQUIVO
            var fsize = fi.files.item(i).size;

            // TOTAL
            fsizet = fsizet + fsize;
        }

        if (fsizet > 0 && fsizet <= 5242880) {

            var idFile = $(sender).attr('id');
            var file = sender.files[0];
            var formData = new FormData();

            var totalFiles = document.getElementById(idFile).files.length;
            for (var i = 0; i < totalFiles; i++) {
                file = document.getElementById(idFile).files[i];
                formData.append("file", file);
            }

            $.ajax({
                type: "POST",
                url: "/Arquivos/Upload",
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    $(".loader-wrapper").fadeOut("slow");
                    swal("Sucesso", "Arquivo foi anexado com sucesso!", "success");

                    $('#' + idElementoInputCaminhoArquivo).val(response.encrypted);
                    $('#' + idBotaoEnviar).removeClass('btn-secondary').addClass('btn-success');
                },
                error: function (error) {
                    console.log(error)
                    swal("Ops!", "Erro so submeter a foto." + error, "error");
                    $(".loader-wrapper").fadeOut("slow");
                }
            });

        } else if (fsizet > 5242880) {
            alert('Não permetido maior que 5mb');
            $(".loader-wrapper").fadeOut("slow");
        }

    }
    else {
        alert('Maximo de 1 arquivos.');
        $(".loader-wrapper").fadeOut("slow");
    }

}
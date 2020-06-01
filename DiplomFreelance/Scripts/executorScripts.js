

function previewImages() {

    if (this.files.length > 1) return alert('Файлов слишком много');
    var $preview = $('#pics').empty();
    if (this.files) $.each(this.files, readAndPreview);

    function readAndPreview(i, file) {

        if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
            return alert(file.name + " не является изображением");
        }

        var reader = new FileReader();

        $(reader).on("load", function () {

            $preview.append($('<div>', {
                class: 'col-6 col-md-4'
            })
                .append($('<img/>', {
                    class: 'img-responsive rounded w-100',
                    src: this.result,
                    alt: file.name
                })));
        });

        reader.readAsDataURL(file);

    }

}
$('#Pic').on("change", previewImages);
//document.querySelector('#Pic').addEventListener("change", previewImages);


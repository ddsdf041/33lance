let arrPics = 0;

function showLoaderOrder() {
    $("#loaderOrder").removeClass("d-none");
    $("#loaderOrder").addClass("d-flex");
    $("#resultMyOrders").removeClass("d-flex");
    $("#resultMyOrders").addClass("d-none");
}

function hideLoaderOrder() {
    $("#loaderOrder").removeClass("d-flex");
    $("#loaderOrder").addClass("d-none");
    $("#resultMyOrders").addClass("d-flex");
    $("#resultMyOrders").removeClass("d-none");
}


function showLoaderOrderExecutor() {
    $("#loaderOrdersForEx").removeClass("d-none");
    $("#loaderOrdersForEx").addClass("d-flex");
    $("#OrdersResult").removeClass("d-flex");
    $("#OrdersResult").addClass("d-none");
}

function hideLoaderOrderExecutor() {
    $("#loaderOrdersForEx").removeClass("d-flex");
    $("#loaderOrdersForEx").addClass("d-none");
    $("#OrdersResult").addClass("d-flex");
    $("#OrdersResult").removeClass("d-none");
}


function SelectSubcategory(idSubcategory, nameSubcategory) {
    document.getElementById('selectedSubcategory').value = idSubcategory;
    document.getElementById('nameSubcategory').textContent = 'Выбрано: ' + nameSubcategory;
    $('#subcategoryOrderModalCenter').modal('hide');
}

function deleteOrder(idOrder) {
    if (confirm("Удалить заказ?")) {
        $.ajax({
            url: '/Customer/DeleteOrder',
            data: { 'idOrder': idOrder },
            cache: false,
            success: function (html) {
                $("#resultMyOrders").html(html);
                //$("input:radio[name=SelectStatusOrder][disabled=false]:first");
                
            }
        });
    }
}

function completeOrder(idOrder) {
    if (confirm("Завершить заказ?")) {
        $.ajax({
            url: '/Customer/CompleteOrder',
            data: { 'idOrder': idOrder },
            cache: false,
            success: function (html) {           
                $("#resultMyOrders").html(html);
              
            }
        });
    }
}


//function readmultifiles(files) {
//    var reader = new FileReader();
//    function readFile(index) {
//        if (index >= files.length) return;
//        var file = files[index];
//        reader.onload = function (e) {
//            // get file content  
//            var bin = e.target.result;

//            var output = document.getElementsByName('Images');
//            var emptyIMG = new Array();
//            for (var i = 0; i < output.length; i++) {
//                if (output[i].src == '') {
//                    emptyIMG.push(output[i]);
//                }
//            }

//            if (emptyIMG.length != 0) {
//                let img = emptyIMG.shift();
//                img.src = file.result;
//                arrPics++;
//            }
//            else {

//                alert('Лимит фото ограничен');
//            }
//            readFile(index + 1)
//        }
//        reader.readAsBinaryString(file);
//    }
//    readFile(0);
//}


function previewImages() {

    if (this.files.length > 3) return alert('Файлов слишком много'); 
    var $preview = $('#linePics').empty();
    if (this.files) $.each(this.files, readAndPreview);

    function readAndPreview(i, file) {

        if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
            return alert(file.name + " не является изображением");
        }

        var reader = new FileReader();

        $(reader).on("load", function () {

            $preview.append($('<div>', {
                class:'col-4'
            })
                .append($('<img/>', {
                    class:'img-responsive rounded w-100',
                    src:this.result,
                    alt:file.name                 
                })));
        });

        reader.readAsDataURL(file);

    }

}
$('#Pic').on("change", previewImages);
//document.querySelector('#Pic').addEventListener("change", previewImages);


function selectStatus() {
    showLoaderOrder();
    var status_Id = $('input[name=SelectStatusOrder]:checked').val();
    console.log(status_Id);
    $.ajax({
        url: '/Customer/MyOrders',
        data: { 'idStatus': status_Id },
        cache: false,
        success: function (html) {
            $("#resultMyOrders").html(html);
        }
    }).done(function (res) {
        hideLoaderOrder();
    }).fail(function (res) {
        hideLoaderOrder();
    });
}

function selectStatusBanned() {
    showLoaderOrder();
    $.ajax({
        url: '/Customer/MyOrders',
        data: { 'isBanned': true },
        cache: false,
        success: function (html) {
            $("#resultMyOrders").html(html);
        }
    }).done(function (res) {
        hideLoaderOrder();
    }).fail(function (res) {
        hideLoaderOrder();
    });
}



function selectStatusExecutor() {

    showLoaderOrderExecutor();
    var status_Id = $('input[name=SelectStatusOrder]:checked').val();
    console.log(status_Id);
    $.ajax({
        url: '/Executor/OrderForExecutor',
        data: { 'idStatus': status_Id },
        cache: false,
        success: function (html) {
            $("#OrdersResult").html(html);
        }
    }).done(function (res) {
        hideLoaderOrderExecutor();
    }).fail(function (res) {
        hideLoaderOrderExecutor();
    });
}

function selectPlace() {
    var res = $('input[name=ID_Place]:checked').val();
    sessionStorage.setItem('ID_Place', res);

    if (res == 3) {
        document.getElementById('place').style.display = 'block';
        //var elem = document.getElementById('place');
        //elem.setAttribute("required", "");
        $("#AddressOrder").attr('required', '');
    }
    else {
        document.getElementById('place').style.display = 'none';
        $("#AddressOrder").removeAttr('required');
    }
}

function addPicToLocalStorage() {
    let picURL = $('input[id=Pic]').val();
    //if (localStorage.length != 3) {
    //    let picURL = $('input[id=Pic]').val();
    //    localStorage.setItem(localStorage.length, picURL);
    //    arrPics.push(localStorage.length);
    //}
    //else {
    //    alert('Лимит фото ограничен');
    //}
    let blockimg = '<div class="col-3 mr - 1">< button type = "button" class="close" aria - label="Close" ><span aria-hidden="true">&times;</span></button><img src="" style="height: 100px; width: 100px;" /></div >';
    if (arrPics.size < 3) {
        arrPics.add(picURL);
        $("#linePics").html(blockimg)
    }
    else {
        alert('Лимит фото ограничен');
    }
}
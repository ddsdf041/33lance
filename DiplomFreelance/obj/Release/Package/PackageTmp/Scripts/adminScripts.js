

function selectIsBanned() {


    var isBanned = $('input[name=isBanned]:checked').val();
       $.ajax({
           url: '/Admin/ExecutorForAdmin',
           data: { 'isBanned': isBanned },
           cache: false,
           success: function (html) {
               $("#resultMyOrders").html(html);
               
           }
       });

}

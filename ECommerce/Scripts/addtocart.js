$(function () {
    $("#btn-cart").on('click', function () {

        var productId = $(this).data("product-id");
        var quantity = $(".quantity").val();
       

        $.get("/cart/alreadyadded", { productId: productId }, function (item) {
            if( item == false)
            {
                $.post("/cart/add", { productId: productId, quantity: quantity }, function () {
                    $.get("/cart/getcartcount", function (count) {

                        $("#cart-quantity").text(count);


                    });
                });
            }
            else {
              
                $.post("/cart/updateitem", { ProductId: item.ProductId, CartId: item.CartId, ItemId: item.ItemId, Quantity: item.Quantity, NewQuantity: quantity}, function () {
                    $.get("/cart/getcartcount", function (count) {

                        $("#cart-quantity").text(count);


                    });
                });
            }
           
        });
    });
  

});
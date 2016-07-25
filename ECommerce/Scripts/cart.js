$(function () {
    $(".select").change(function () {
        var productId = $(this).data("productId");
        var cartId = $(this).data("cartId");
        var itemId = $(this).data("itemId");
        var quantity = $(this).val();
        var originalPrice = $(this).data("price");
        $.post('/cart/updatequantity', { ProductId: productId, CartId: cartId, ItemId: itemId, Quantity: quantity }, function () {
            $(".price").html("$" + (originalPrice * quantity).toFixed(2));
            $.get("/cart/getcartcount", function (count) {

                $("#cart-quantity").text(count);
             
              



            });
            
        });
    });
});
var cart = {
    init: function()
    {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function()
    {
        $('#btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            var productId = parseInt($(this).data('id'));//this đại diện cho nút btnAddToCart
            cart.addItem(productId);
        })
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        })
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) //Kiểm tra quantity là số
            {
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
            }
            else
            {
                $('#amount_' + productId).text(0);
            }
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        })
    },
    getTotalOrder: function()
    {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox,function(i,item)
        {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },
    addItem: function(productId)
    {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'Json',
            success: function(response)
            {
                if(response.status)
                {
                    alert("Thêm sản phẩm thành công!")
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'Json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },
    loadData: function()
    {
        $.ajax(
            {
                url: 'ShoppingCart/GetAll',
                type: 'Get',
                dataType: 'json',
                success: function (res) {
                    if(res.status)
                    {
                        var template = $('#tplCart').html();
                        var html = '';
                        var data = res.data;
                        $.each(data, function (i, item) {
                            html += Mustache.render(template,
                            {
                                ProductId: item.ProductId,
                                ProductName: item.Product.Name,
                                Image: item.Product.Image,
                                Price: item.Product.Price,
                                PriceF: numeral(item.Product.Price).format('0,0'),
                                Quantity: item.Quantity,
                                Amount: numeral(item.Quantity*item.Product.Price).format('0,0')
                            });
                        });
                        $('#CartBody').html(html);
                        $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                        cart.registerEvent();
                    }
                }
            })
    }
}
cart.init();
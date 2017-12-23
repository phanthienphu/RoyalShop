var cart = {
    init: function()
    {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function()
    {
        $("#frmPayment").validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Chưa nhập tên kìa! làm gì vội thía!",
                address: "Chưa nhập địa chỉ kìa! làm gì vội thía!",
                email: {
                    required: "Chưa nhập email kìa! làm gì vội thía!",
                    email: "Khộng nhập bậy nhá!"
                },
                phone: {
                    required: "Chưa nhập điện thoại kìa! làm gì vội thía!",
                    number: "Nhập số bạn ei!"
                }
            }
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        })
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {

                var amount = quantity * price;

                $('#amount_' + productid).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productid).text(0);
            }
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
            cart.updateAll();
        });

        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            window.location.href = "/";
        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            cart.deleteAll();
        });
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            $('#divCheckout').show();
        });
        $('#chkUserLoginInfo').off('click').on('click', function (e) {
            if($(this).prop('Checked'))
                cart.getLoginUser();
            else
            {
                $('#txtName').val("");
                $('#txtAddress').val("");
                $('#txtEmail').val("");
                $('#txtPhone').val("");
            }
        });
        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            var isValid = $("#frmPayment").valid();
            if(isValid)
                cart.createOrder();
        });
    },
    getLoginUser: function(){
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function(response)
            {
                if(response.status)
                {
                    var user = response.data;

                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    createOrder: function () {
        var order = {
        CustomerName: $("#txtName").val(),
        CustomerAddress: $("#txtAddress").val(),
        CustomerEmail: $("#txtEmail").val(),
        CustomerMobile: $("#txtPhone").val(),
        CustomerMessage : $("#txtMessage").val(),
        PaymentMethod: "Thanh toán tiền mặt_hệ thống tạo", 
        Status: false,
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data:{
                orderViewModel: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    $("#divCheckout").hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $("#CartContent").html("Cảm ơn bạn đã đặt hàng! Chúng tôi sẽ duyệt đơn hàng và liên hệ sớm nhất!");
                    },2000);
                    
                }
                else
                    $("#CartContent").html(response.message);
            }
        });
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'Json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },
    updateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val
            });
        });
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data:{
                cartData: JSON.stringify(cartList)
            },
            dataType: 'Json',
            success: function (response) {
                if (response.status) {
                    console.log('Ngon zồi!');
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

                        if (html == "")
                        {
                            $('#CartContent').html('Không có sản phẩm nào trong giỏ hàng!');
                        }
                        $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                        cart.registerEvent();
                    }
                }
            })
    }
}
cart.init();
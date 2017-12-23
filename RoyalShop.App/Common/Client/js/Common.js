var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        keyword: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
          .append("<div>" + item.label + "</div>")
          .appendTo(ul);
    };

        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();//xoá sấu thăng trong href
            var productId = parseInt($(this).data('id'));//this đại diện cho nút btnAddToCart
            $.ajax({
                url: '/ShoppingCart/Add',
                data: {
                    productId: productId
                },
                type: 'POST',
                dataType: 'Json',
                success: function (response) {
                    if (response.status) {
                        alert("Thêm sản phẩm thành công!")
                    }
                    else
                    {
                        alert(response. message);
                    }
                }
            });
        })

        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
    }
}
common.init();
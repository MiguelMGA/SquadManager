$('form').on('submit', function (event) {
    event.preventDefault();

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify($("#email").val()),
        url: "https://localhost:44392/api/user/forgot",
        success: function (result) {

        },
        error: function (error) {

        }
    })
})
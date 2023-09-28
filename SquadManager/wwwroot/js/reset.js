$('form').on('submit', function (event) {
    event.preventDefault();

    if ($("#password").val() != $("#confirmPassword").val()) {
        $(".error span").show();

        var senhaold = $("#password").val();
        var confirmold = $("#confirmPassword").val();

        document.getElementById("password").addEventListener('input', verificaCampo);
        document.getElementById("confirmPassword").addEventListener('input', verificaCampo);

        function verificaCampo(event) {
            if (event.target.value.length != senhaold || $("#confirmPassword").val() != confirmold) {
                $(".error span").hide();
            }
        }

        return;
    }
    
    const urlParam = new URLSearchParams(window.location.search);
    const id = urlParam.get("id");

    var formData = {
        password: $("#password").val(),
        confirmPassword: $("#confirmPassword").val(),
        id: id
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(formData),
        url: "https://localhost:44392/api/user/reset",
        success: function (result) {

        },
        error: function (error) {

        }
    })
})
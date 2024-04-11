$(document).ready(function () {
    // Función para el inicio de sesión.
    $("#loginform").submit(function (e) {
        e.preventDefault();
        var formData = {};
        $(this).serializeArray().forEach(function (item) {
            formData[item.name] = item.value;
        });
        var jData = JSON.stringify(formData);

        $.ajax({
            url: 'Login/Login',
            type: 'POST',
            data: jData,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.redirectUrl) {
                    window.location.href = data.redirectUrl;
                } else {
                    $("#result").html(data.message);
                }
            },
            error: function () {
                alert("Error al procesar la solicitud");
            }
        });
    });
})


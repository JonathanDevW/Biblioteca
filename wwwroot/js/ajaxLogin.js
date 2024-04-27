$(document).ready(function () {
    // Funci�n para el inicio de sesi�n.

    $("#loginform").submit(function (e) {
        e.preventDefault();

        // Serializa los datos del formulario a un objeto JSON
        var formData = {};
        $(this).serializeArray().forEach(function (item) {
            formData[item.name] = item.value;
        });

        // Convierte el objeto JSON a una cadena JSON
        var jData = JSON.stringify(formData);

        // Envia la solicitud AJAX al controlador Login/Login
        $.ajax({
            url: '/Login/Login',
            type: 'POST',
            data: jData,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.redirectUrl) {
                    // Redirigir a la p�gina de inicio si se recibe una URL de redirecci�n
                    window.location.href = data.redirectUrl;
                } else {
                    // Mostrar mensaje de error si no se recibe una URL de redirecci�n
                    $("#result").html(data.message);
                }
            },
            error: function () {
                // Mostrar alerta en caso de error
                alert("Error al procesar la solicitud");
            }
        });
    });
});

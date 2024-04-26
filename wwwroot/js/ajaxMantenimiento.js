$('#AddReaderModal').on('hidden.bs.modal', function () {
    alert();
    window.location.reload(true);
});
$('#RestContrasena').on('hidden.bs.modal', function () {
    window.location.reload(true);
});
$('#ModUsuario').on('hidden.bs.modal', function () {
    window.location.reload(true);
});

$(document).ready(function () {
    // Función para el inicio de sesión.
    $("#AgregarRol").submit(function (e) {
        e.preventDefault();
        var formData = {};
        $(this).serializeArray().forEach(function (item) {
            formData[item.name] = item.value;
        });
        var jData = JSON.stringify(formData);

        $.ajax({
            url: 'Mantenimiento/Agregar_Rol',
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
});


function cargarDetalleUsuario(id_usuario) {
    // Hacer una llamada AJAX para obtener los detalles del usuario
    $.ajax({
        url: 'ObtenerUser',
        type: 'GET',
        data: { tabla: 'usuario', id: id_usuario },
        success: function (data) {
            // Rellenar los campos del modal con los datos del usuario
            $('#m_usuario').val(data.nickname);
            $('#m_email').val(data.correo);
            $("#m_rol option[value='" + data.id_rol + "']").attr("selected", true);
            $("#m_estado option[value='" + data.id_estado + "']").attr("selected", true);
            $("#m_persona option[value='" + data.persona + "']").attr("selected", true);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }

    });
}

function cargarDetalleRol(id_rol) {
    // Hacer una llamada AJAX para obtener los detalles del usuario
    $.ajax({
        url: 'ObtenerRol',
        type: 'GET',
        data: { tabla: 'rol', id: id_rol },
        success: function (data) {
            // Rellenar los campos del modal con los datos del usuario
            $('#m_rol').val(data.rol);
            $('#m_descripcion').val(data.descripcion);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }

    });
}

function cargarDetalleEstado(id_estado) {
    // Hacer una llamada AJAX para obtener los detalles del usuario
    $.ajax({
        url: 'ObtenerEstado',
        type: 'GET',
        data: { tabla: 'estado', id: id_estado },
        success: function (data) {
            // Rellenar los campos del modal con los datos del usuario
            $('#m_estado').val(data.estado);
            $('#m_descripcion').val(data.descripcion);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }

    });
}

function cargarDetalleLibro(id_libro) {
    // Hacer una llamada AJAX para obtener los detalles del usuario
    $.ajax({
        url: 'ObtenerLibro',
        type: 'GET',
        data: { tabla: 'libro', id: id_libro },
        success: function (data) {
            // Rellenar los campos del modal con los datos del usuario
            $('#m_titulo').val(data.titulo);
            $('#m_autor').val(data.autor);
            $('#m_isbn').val(data.isbn);
            $('#m_anio_publicacion').val(data.anio_publicacion);
            $('#m_ejemDisp').val(data.ejemplares_disponibles);
            $("#m_categoria option[value='" + data.id_categoria + "']").attr("selected", true);
            $('#m_portada').val(data.portada);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }

    });
}
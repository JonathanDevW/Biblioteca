//function cargarDetalleUsuario(id_usuario) {
//    // Hacer una llamada AJAX para obtener los detalles del usuario
//    $.ajax({
//        url: '@Url.Action("ObtenerUser", "Mantenimiento")',
//        type: 'GET',
//        data: { tabla: 'usuario', id: id_usuario },
//        success: function (data) {
//            // Rellenar los campos del modal con los datos del usuario
//            $('#m_usuario').val(data.nickname);
//            $('#m_nombre').val(data.nombre);
//            $('#m_email').val(data.correo);
//            $('#m_rol').val(data.id_rol);
//            $('#m_estado').val(data.id_estado);
//        },
//        error: function (xhr, status, error) {
//            console.error(xhr.responseText);
//        }

//    });
//}
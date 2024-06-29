$(document).ready(() => {

    $('#IngresarModal').on('click', function (event) {
        debugger
        let libro = new Libro($('#ISBN').val(), $('#Titulo').val(), $('#Total_Paginas').val(), $('#Fecha').val(), $('#Editorial').val(), $('#Autor').val())
        $.ajax({
            type: 'POST',
            url: '/Home/AgregarLibro',
            dataType: 'JSON',
            data: libro,
            success: function (result) {
                debugger
                if (result.Success) {
                    actualizarTabla();
                }
            },
            error: function (result) {

            }
        })
    })
    $('#Eliminar').on('click', function (event) {
        $.ajax({
            type: 'DELETE',
            url: '/Home/EliminarInformacion',
            dataType: 'JSON',
            data: {ISBN : $(event.target).data('libro')},
            success: function (result) {
                if (result.Success) {
                    actualizarTabla();
                }
            },
            error: function (error) { }
        })
        $('#modal-body').empty().prepend(
            $('<div>').addClass('d-flex justify-content-center align-items-center')
                .append(
                    $('<div>').addClass('alert alert-danger').text('Se ha eliminado correctamente el usuario')
                )
        )
    })

    $('#Actualizar').on('click', function (event) {

        //$.ajax({
        //    type: 'PUT',
        //    url: '/Home/ActualizarInformacion',
        //    dataType: 'JSON',
        //    data: {},
        //    success: function (result) { },
        //    error: function (error) { },

        //})
    })
})

let actualizarTabla = () => {
    $('#tabla-contenido').empty()
    $.ajax({
        type: 'GET',
        url: '/Home/CargarLibros',
        dataType: 'JSON',
        success: function (result) {
            debugger
            if (result.Success) {
                let tabla = $('#tabla-contenido')
                $.each(result.Objects, function (index, element) {
                    let fila = $('<tr>');
                    fila.append([
                        $('<td>').addClass('bg-white').text(element.ISN),
                        $('<td>').addClass('bg-white').text(element.Titulo),
                        $('<td>').addClass('bg-white').text(element.Total_Paginas),
                        $('<td>').addClass('bg-white').text(element.Fecha_Publicacion),
                        $('<td>').addClass('bg-white').text(element.Editorial.Nombre),
                        $('<td>').addClass('bg-white').text(element.Autor.Nombre),
                        $('<td>').addClass('bg-white').append(
                            $('<div>').addClass('d-flex justify-content-center align-items-center gap-2 flex-wrap')
                                .append(
                                    $('<button>').addClass('col btn btn-info').text('Editar'),
                                    $('<button>').addClass('col btn btn-danger').text('Eliminar')
                                )
                        )
                    ])

                })
            }

        },
        error: function (error) {
        }
    })
}

let obtenerDato = (ISBN) => {
    $.ajax({
        url: '/Home/ObtenerLibro',
        type: 'GET',
        dataType: 'JSON',
        data: { ISBN: ISBN },
        success: function (result) {
            if (result.Success) {
            }
        },
        error: function (error) { }

    })
}
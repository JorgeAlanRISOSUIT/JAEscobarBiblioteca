$(document).ready(() => {
    //$('#tablaInit').DataTable({
    //    search: "",
    //    language: {
    //        emptyTable: "La tabla no encuentra coincidencias",
    //        info: "Mostrar: _START_ de _END_ con total de _TOTAL_ registros",
    //        infoEmpty: "No se encuentran coincidencias",
    //        lengthMenu: 'Mostrar <select class="form-select form-select-sm">' +
    //            '<option value="4">4</option>' +
    //            '<option value="5">5</option>' +
    //            '<option value="10">10</option>' +
    //            '<option value="20">20</option>' +
    //            '<option value="-1">Todos</option>' +
    //            '</select> registros por página',
    //        search: '<label><span class="icon"><i class="bi bi-search"></i></span>' +
    //            '<input type="search" class="form-control form-control-sm" placeholder="Buscar..." aria-controls="miTabla"></label>',
    //    },

    //});
    $('#IngresarModal').on('click', function (event) {
        let ddlEditorial = $('#ddlEditorial')[0].options.selectedIndex
        let dataEditorial = $('#ddlEditorial')[0].options[ddlEditorial].value
        let ddlAutor = $('#ddlAutor')[0].options.selectedIndex
        let dataAutor = $('#ddlAutor')[0].options[ddlAutor].value
        debugger
        let libro = new Libro(
            $('#ISBN').val(),
            $('#Titulo').val(),
            $('#Total_Paginas').val(),
            new Date($('#Fecha').val()),
            new Editorial(dataEditorial, ""),
            new Autor(dataAutor, "")
        )
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
            data: { ISBN: $(event.target).data('libro') },
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

    $('#Agregar').on('click', function (event) {
        $('#IngresarModal').toggleClass('d-none', false)
        $('#ActualizarModal').toggleClass('d-none', true)
        $('#ISBN').val("")
        $('#Titulo').val("")
        $('#Total_Paginas').val("")
        $('#Fecha').val("")
    })
    $.each($('#Actualizar*'), function (index, value) {
        $(value).on('click', function (event) {
            $('#IngresarModal').toggleClass('d-none', true)
            $('#ActualizarModal').toggleClass('d-none', false)
            let data = $(value).closest('tr').children()
            let libro = new Libro($(data[0]).text(), $(data[1]).text(), parseInt($(data[2]).text()), new Date($(data[3]).text()).toISOString(), $(data[4]).text(), $(data[5]).text())
            console.log(libro)
            $('#ISBN').val(libro.getISN)
            $('#Titulo').val(libro.getTitulo)
            $('#Total_Paginas').val(libro.getNumeroPaginas)
            let [año, mes, dia] = libro.getFecha.split('T')[0].split('-')
            $('#Fecha').val(`${año}-${mes}-${dia}`)
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
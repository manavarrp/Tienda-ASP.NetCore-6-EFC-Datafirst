const MODEL_BASE = {
    idCategory: 0,
    description: "",
    isActive: 1,

}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Category/List',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCategory", "visible": false, "searchable": false },
            { "data": "description" },
            {
                "data": "isActive", render: function (data) {
                    if (data == 1) {
                        return '<span class="badge badge-info">Activo</span>'
                    } else {
                        return '<span class="badge badge-danger">Inactivo</span>'
                    }
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-remove btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Categorias',
                exportOptions: {
                    columns: [1, 2]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});


function showModal(model = MODEL_BASE) {
    $("#txtId").val(model.idCategory)
    $("#txtDescription").val(model.description)
    $("#cboIsActive").val(model.isActive)

    $("#modalData").modal("show")

}

$("#btnNewCategory").click(function () {
    showModal()
})

$("#btnSave").click(function () {
    
    if ($("#txtDescription").val().trim() == "") {
        toastr.warning("", "Debe completar el campo descripcion")
        $("#txtDescription").focus()
        return;
    }

    const model = structuredClone(MODEL_BASE);
    model["idCategory"] = parseInt($("#txtId").val())
    model["description"] = $("#txtDescription").val()
    model["isActive"] = $("#cboIsActive").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show")

    if (model.idCategory == 0) {
        fetch("/Category/CreateCategory", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(model)
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (!responseJson.IsSuccess) {
                tablaData.row.add(responseJson.data).draw(false)
                $("#modalData").modal("hide")
                swal("Listo!", "La categoria fue creada", "success")

            } else {
                swal("Lo sentimos!", responseJson.message, "error")
            }
        })
    } else {
        fetch("/Category/EditCategory", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(model)

        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (!responseJson.IsSuccess) {
                tablaData.row(rowSelected).data(responseJson.data).draw(false);
                rowSelected = null;
                $("#modalData").modal("hide")
                swal("Listo!", "La categoria fue modificada", "success")

            } else {
                swal("Lo sentimos!", responseJson.message, "error")
            }
        })
    }
})

let rowSelected;
$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        rowSelected = $(this).closest("tr").prev();
    } else {
        rowSelected = $(this).closest("tr");
    }

    const data = tablaData.row(rowSelected).data()

    showModal(data)
})



$("#tbdata tbody").on("click", ".btn-remove", function () {
    let deleteRow;
    if ($(this).closest("tr").hasClass("child")) {
        deleteRow = $(this).closest("tr").prev();
    } else {
        deleteRow = $(this).closest("tr");
    }


    const data = tablaData.row(deleteRow).data()
    // showModal(data)
    swal({
        title: "¿Está seguro?",
        text: `Eliminar la categoria "${data.description}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (response) {

            if (response) {
                $(".showSweetAlert").LoadingOverlay("show")
                fetch(`/Category/RemoveCategory?idCategory=${data.idCategory}`, {
                    method: "DELETE"
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJson => {
                    if (!responseJson.IsSuccess) {
                        tablaData.row(deleteRow).remove().draw();

                        swal("Listo!", "La categoria fue eliminada", "success")

                    } else {
                        swal("Lo sentimos!", responseJson.message, "error")
                    }
                })
            }
        }

    )
})
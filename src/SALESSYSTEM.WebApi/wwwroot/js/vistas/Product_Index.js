const MODEL_BASE = {
    idProduct: 0,
    barcode: "",
    brand: "",
    idCategory: 0,
    stock: 0,
    price: 0,
    isActive: 1,

}

let tablaData;

$(document).ready(function () {

    fetch('/Product/CategoryList')
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
           // console.log(responseJson)
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    console.log(item.description);
                    $("#cboCategoria").append(
                        
                        $("<option>").val(item.idCategory).text(item.description)
                    )
                    
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Product/List',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idProduct", "visible": false, "searchable": false },
            { "data": "barcode" },
            { "data": "brand" },
            { "data": "description" },
            { "data": "nameCategory" },
            { "data": "stock" },
            { "data": "price" },
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
                filename: 'Reporte Productos',
                exportOptions: {
                    columns: [1, 8]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});


function showModal(model = MODEL_BASE) {
    $("#txtId").val(model.idProduct)
    $("#txtCodigoBarra").val(model.barcode)
    $("#txtMarca").val(model.brand)
    $("#txtDescripcion").val(model.description)
    $("#cboCategoria").val(model.idCategory == 0 ? $("#cboCategoria option:first").val() : model.idCategory)
    $("#txtStock").val(model.stock)
    $("#txtPrecio").val(model.price)
    $("#cboIsActive").val(model.isActive)

    $("#modalData").modal("show")
}

$("#btnNewProduct").click(function () {
    showModal()
})

$("#btnSave").click(function () {
    
    const inputs = $("input.input-validate").serializeArray();
    const inputs_empty = inputs.filter((item) => item.value.trim() == "")
    if (inputs_empty.length > 0) {
        const message = `Debe completar el campo : "${inputs_empty[0].name}"`
        toastr.warning("", message)
        $(`input[name="${inputs_empty[0]}.name""]`).focus();
        return;
    }


    const model = structuredClone(MODEL_BASE);
    model["idProduct"] = parseInt($("#txtId").val())
    model["barcode"] = $("#txtCodigoBarra").val()
    model["brand"] = $("#txtMarca").val()
    model["description"] = $("#txtDescripcion").val()
    model["idCategory"] = $("#cboCategoria").val()
    model["stock"] = $("#txtStock").val()
    model["price"] = $("#txtPrecio").val()
    model["isActive"] = $("#cboIsActive").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show")

    if (model.idProduct == 0) {
        console.log("CreateProduct")
        fetch("/Product/CreateProduct", {
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
                swal("Listo!", "El prodcuto fue creado", "success")

            } else {
                swal("Lo sentimos!", responseJson.message, "error")
            }
        })
    } else {
        fetch("/Product/EditProduct", {
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
                swal("Listo!", "El producto fue modificado", "success")

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
        text: `Eliminar el producto "${data.brand}"`,
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
                fetch(`/Product/RemoveProduct?idProduct=${data.idProduct}`, {
                    method: "DELETE"
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJson => {
                    if (!responseJson.IsSuccess) {
                        tablaData.row(deleteRow).remove().draw();

                        swal("Listo!", "El producto fue eliminado", "success")

                    } else {
                        swal("Lo sentimos!", responseJson.message, "error")
                    }
                })
            }
        }

    )
})

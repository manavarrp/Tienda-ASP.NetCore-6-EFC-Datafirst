const MODEL_BASE = {
    idUser: 0,
    name: "",
    email: "",
    phone: "",
    idRole: 0,
    isActive: 1,

}

let tablaData;

$(document).ready(function () {

    fetch("/User/ListRols")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson)
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboRol").append(
                        $("<option>").val(item.idRole).text(item.description)
                    )
                })
            }
        })

    tablaData = $('#tbdata').DataTable({
        columnDefs: [{
            "defaultContent": "-",
            "targets": "_all"
        }],
        responsive: true,
        "ajax": {
            "url": '/User/ListUser',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idUser", "visible": false, "searchable": false },
          /*  {
                "data": "photoUrl", render: function (data) {
                    return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>`
                }
            }*/
            { "data": "name" },
            { "data": "email" },
            { "data": "phone" },
            { "data": "nameRole" },
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
                filename: 'Reporte Usuarios',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});


function showModal(model = MODEL_BASE) {
    $("#txtId").val(model.idUser)
    $("#txtName").val(model.name)
    $("#txtEmail").val(model.email)
    $("#txtPhone").val(model.phone)
    $("#cboRol").val(model.idRole == 0 ? $("#cboRol option:first").val() : model.idRole)
    $("#cboIsActive").val(model.isActive)
   // $("#txtPhoto").val("")
    //$("#imgUser").attr("src", model.photoUrl)

    $("#modalData").modal("show")

}

$("#btnNewUser").click(function () {
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
    model["idUser"] = parseInt($("#txtId").val())
    model["name"] = $("#txtName").val()
    model["email"] = $("#txtEmail").val()
    model["phone"] = $("#txtPhone").val()
    model["idRole"] = $("#cboRol").val()
    model["isActive"] = $("#cboIsActive").val()

    console.log("Model to be sent:", model);
    const inputPhoto = document.getElementById("#txtPhoto")

    //const formData = new FormData();
    //formData.append("photo", inputPhoto.files[0])
   // formData.append("model", JSON.stringify(model))

    $("#modalData").find("div.modal-content").LoadingOverlay("show")

    if (model.idUser == 0) {
        fetch("/User/CreateUser", {
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
                swal("Listo!", "El usuario fue creado", "success")

            } else {
                swal("Lo sentimos!", responseJson.message, "error")
            }
        })
    } else {
        fetch("/User/Edituser", {
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
                swal("Listo!", "El usuario fue modificado", "success")

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
        text: `Eliminar al usuario "${data.name}"`,
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
                fetch(`/User/Remove?IdUser=${data.idUser}`, {
                    method: "DELETE"
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJson => {
                    if (!responseJson.IsSuccess) {
                        tablaData.row(deleteRow).remove().draw();
    
                        swal("Listo!", "El usuario fue eliminado", "success")

                    } else {
                        swal("Lo sentimos!", responseJson.message, "error")
                    }
                })
            }
        }

    )
})
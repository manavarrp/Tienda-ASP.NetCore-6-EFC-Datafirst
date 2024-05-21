

$(document).ready(function () {
    $(".card-body").LoadingOverlay("show")
    fetch("/Bussines/List")
        .then(response => {
            $(".card-body").LoadingOverlay("hide")
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            console.log(responseJson)
            if (!responseJson.IsSuccess) {
                const d = responseJson.data
                console.log(d)
                $("#txtDocumentNumber").val(d.documentNumber)
                $("#txtName").val(d.name)
                $("#txtEmail").val(d.email)
                $("#txtAddress").val(d.address)
                $("#txPhone").val(d.phone)
                $("#txtTaxPercentage").val(d.taxPercentage)
                $("#txtCurrencySymbol").val(d.currencySymbol)

            } else {
                swal("Lo sentimos", responseJson.message, "error")
            }
        }) 
})

$("#btnSaveChanges").click(function () {
    const inputs = $("input.input-validate").serializeArray();
    const inputs_empty = inputs.filter((item) => item.value.trim() == "")
    if (inputs_empty.length > 0) {
        const message = `Debe completar el campo : "${inputs_empty[0].name}"`
        toastr.warning("", message)
        $(`input[name="${inputs_empty[0]}.name""]`).focus();
        return;
    }

    const model = {
        documentNumber: $("#txtDocumentNumber").val(),
        name: $("#txtName").val(),
        email: $("#txtEmail").val(),
        address: $("#txtAddress").val(),
        phone: $("#txPhone").val(),
        taxPercentage: $("#txtTaxPercentage").val(),
        currencySymbol: $("#txtCurrencySymbol").val()        

    }
    $(".card-body").LoadingOverlay("show")
    fetch("/Bussines/CreateBussines", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(model)
    })
        .then(reponse => {
            $(".card-body").LoadingOverlay("hide")
            return reponse.ok ? reponse.json() : Promise.reject(reponse);

        }).then(responseJson => {
            if (!responseJson.IsSuccess) {
                swal("Listo!", "El negocio fue editado con exito", "success")
            } else {
                swal("Lo sentimos!", responseJson.message, "success")
            }
        })
})

jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        loadDataTable();
    })
})

var dataTable;

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: "Products/GetAll",
            type: "GET",
            datatype: "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "30%" },
            { "data": "productType.type", "width": "20%" },
            { "data": "category.catName", "width": "15%" },
            { "data": "price", "width": "12%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="Products/Detail/${data}" class="btn btn-primary" style="cursor:pointer; width:40px">
                                    <i class="fas fa-eye" style="color:white"></i>
                                </a>
                                &nbsp;
                                <a href="Products/Upsert/${data}" class="btn btn-primary" style="cursor:pointer; width:40px">
                                    <i class="fas fa-edit" style="color:white"></i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("Products/Delete/${data}") class="btn btn-danger" style="cursor:pointer; width:40px">
                                    <i class="fas fa-trash-alt" style="color:white"></i>
                                </a>
                            </div>`;
                },
                "width": "13%"
            }
        ],
        "language": {
            "emptyTable": "No Record Found!"
        },
        "width": "100%"
    })
}

function Delete(url) {
    swal({
        title: "ARE YOU SURE YOU WANT TO DELETE?",
        text: "YOU WILL NOT BE ABLE TO RESTORE IT!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#BD6B55",
        confirmButtonText: "YES, DELETE IT!",
        closeOnConfirm: true
    },
        function () {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    );
}
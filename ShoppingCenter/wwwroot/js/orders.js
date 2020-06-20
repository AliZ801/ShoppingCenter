jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        var url = window.location.search;

        if (url.includes("approved")) {
            loadDataTable("GetAllApprovedOrders");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("GetAllPendingOrders");
            }
            else {
                if (url.includes("completed")) {
                    loadDataTable("GetAllCompletedOrders");
                }
                else {
                    loadDataTable("GetAllOrders");
                }
            }
        }
    })
})

var dataTable;

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "mobile", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "status", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Order/Details/${data}" class="btn btn-primary text-white" style="cursor:pointer; width:100px">
                                    <i class="fas fa-eye" style="color:white"></i> &nbps; View
                                </a>
                            </div>`
                },
                "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No Record Found!"
        },
        "width": "100%"
    });
}
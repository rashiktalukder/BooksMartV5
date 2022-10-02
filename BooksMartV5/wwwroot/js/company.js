﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name","width":"15%"},
            { "data": "streetAddress", "width": "15%"},
            { "data": "city", "width": "10%"},
            { "data": "state", "width": "10%"},
            { "data": "phoneNumber", "width": "15%"},
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data == true) {
                        return `<input type="checkbox" disabled checked />`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Company/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fa-regular fa-pen-to-square"></i></a>

                                    <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fa-solid fa-trash-can"></i></a>

                            </div>
                            `;
                }, "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure, you want to delete?",
        text: "once you delete, You can not be undone",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete == true) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success == true) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'description', "width": "25%" },
            { data: 'price', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">

        <a class="btn btn-primary mx-2" asp-controller="Product" asp-action="Upsert" asp-route-id="${data}">
        <i class="bi bi-pencil-square"></i>
        Edit
        </a>
        <a onClick=AskDelete(${data}) class="btn btn-danger mx-2">
        <i class="bi bi-trash-fill"></i> Delete
        </a>
        </div>`
                },
                "width": "25%"
            }
        ],
        columnDefs: [{ className: 'align-middle', targets: [4] }]
    });
}
function AskDelete(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Admin/Product/Delete/${id}`,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
function Delete(url) {
}
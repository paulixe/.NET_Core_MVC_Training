var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { data: 'name', "width": "50%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">

        <a class="btn btn-primary mx-2" href="/admin/company/upsert?id=${data}">
        <i class="bi bi-pencil-square"></i>
        Edit
        </a>
        <a onClick=AskDelete(${data}) class="btn btn-danger mx-2">
        <i class="bi bi-trash-fill"></i> Delete
        </a>
        </div>`
                },
                "width": "50%"
            }
        ],
        columnDefs: [{ className: 'align-middle', targets: [1] }]
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
                url: `/Admin/Company/Delete/${id}`,
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
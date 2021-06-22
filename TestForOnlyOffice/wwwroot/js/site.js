var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "firstName", "width": "40%" },
            { "data": "lastName", "width": "60%" },
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}
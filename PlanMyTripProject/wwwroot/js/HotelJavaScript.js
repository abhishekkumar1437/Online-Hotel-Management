var dtable;
$(document).ready(function () {
    dtable=$('#myTable').DataTable({
       
        "ajax": {
            "url":"/Admin/Hotel/AllHotel"
        },
            "columns": [
                { "data":"name" },
                { "data": "description" },
                { "data": "price" },
                { "data": "location" },
                { "data": "category.name" },
                
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                                <a href="/Admin/Hotel/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                      <a onClick=RemoveHotel("/Admin/Hotel/Delete/${data}")><i class="bi bi-trash-fill"></i></a>`

                    }
                }
            ]
     
    });
});

function RemoveHotel(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                       
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
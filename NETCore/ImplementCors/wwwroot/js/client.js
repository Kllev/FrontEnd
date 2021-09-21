$.ajax({
    url: "https://localhost:44316/api/Persons/GetAllProfile"
}).done(result => {
    console.log(result);
});

$(document).ready(function () {
    $('#tableClient').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1,2,3,4]
                },
                className: 'btn btn-sm btn-outline-secondary',
                bom: true
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: 'btn btn-sm btn-outline-secondary',
                bom: true
            },
        ],
        "filter": true,
        "ajax": {
            "url": "https://localhost:44316/api/Persons/GetAllProfile",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            {
                "data": null,
                "sortable": false,
                "orderable": false,
                "targets": -1,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nik"
            },
            {
                "data": null,
                "render": function (data, type, row) {

                    return row["firstName"] + row["lastName"];
                },
                "autoWidth": true
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return row["email"];
                },
                "autoWidth": true
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row['phone'].startsWith('0')) {
                        return `+62${row['phone'].substr(1)}`
                    }
                    return `+62${row["phone"]}`
                },
                "autoWidth": true
            },
            {
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" onclick="detail('${row['nik']}')" data-toggle="modal" data-target="#exampleModal">Detail</button >`
                }
            },
            {
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-danger" onclick="deleted('${row['nik']}')">Delete</button > `
                }
            }
        ]
    });
});

function detail(nik) {
    $.ajax({
        url: `https://localhost:44316/api/Persons/GetById/${nik}`
    }).done((result) => {
        text = ` <table class="table table-bordered">
                <tbody>
                    <tr>
                        <td><b>NIK :</b> ${result.nik}</td>
                    </tr>
                    <tr>
                        <td><b>Name :</b> ${result.firstName + result.lastName}</td>
                    </tr>
                    <tr>
                        <td><b>Gender :</b> ${result.gender}</td>
                    </tr>
                    <tr>
                        <td><b>Phone Number :</b> ${result.phone.startsWith('0') ? '+62' + result.phone.substr(1) : '+62' + result.phone}</td>
                    </tr>
                   
                    <tr>
                        <td><b>Degree :</b> ${result.degree}</td>
                    </tr>
                    <tr>
                        <td><b>GPA :</b> ${result.gpa}</td>
                    </tr>
                    <tr>
                        <td><b>Salary :</b> Rp. ${result.salary}</td>
                    </tr>
                    <tr>
                        <td><b>Birth Date : </b> ${result.birthDate}</td>
                    </tr>
                </tbody>
            </table>
               `;
        $("#bodyModal").html(text);
    }).fail((error) => {
        console.log(error);
    });
}

$.ajax({
    url: "https://localhost:44316/API/University/",

}).done(result => {
    text = ''
    $.each(result.data, function (key, val) {
        console.log(val.id)
        text += `<option value= "${val.id}">${val.name}</option>`
    })
    $('#inputUniversity').html(text)
}).fail(result => {
    console.log(result)
});

$("#registerBtn").click(function (event) {
    event.preventDefault();

    var obj = new Object();
    obj.NIK = $('#inputNIK').val();
    obj.FirstName = $("#inputFirstName").val();
    obj.LastName = $("#inputLastName").val();
    obj.Phone = parseInt($("#inputPhone").val());
    obj.Birthdate = $("#inputBirthDate").val();
    obj.gender = parseInt($("#inputGender").val());
    obj.Salary = parseInt($("#inputSalary").val());
    obj.Email = $("#inputEmail").val();
    obj.Password = $("#inputPassword").val();
    obj.Degree = $("#inputDegree").val();
    obj.GPA = $("#inputGPA").val();
    obj.UniversityId = parseInt($("#inputUniversityName").val());
    console.log(obj);

    $.ajax({
            url: "https://localhost:44316/api/persons/register",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json',
            crossDomain: true,
            data: JSON.stringify(obj),
        }).done((result) => {
            $('#tableClient').DataTable().ajax.reload();
            Swal.fire({
                title: 'Success!',
                text: 'You Have Been Registered',
                icon: 'success',
            })
        }).fail((result) => {
            Swal.fire({
                title: 'Error!',
                text: 'Failed To Register',
                icon: 'error',
                confirmButtonText: 'Back'
            })
        });
})

function deleted(nik) {
    Swal.fire({
        title: 'Yakin, ingin menghapus data ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:44316/API/Persons/${nik}`,
                type: "DELETE",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            }).done((result) => {
                Swal.fire({
                    title: 'Success!',
                    text: 'Data Has Been Deleted',
                    icon: 'success',
                    confirmButtonText: 'Next'
                })
                $('#tableClient').DataTable().ajax.reload();
            }).fail((error) => {
                Swal.fire({
                    title: 'Error!',
                    text: 'Data Cannot Deleted',
                    icon: 'Error',
                    confirmButtonText: 'Next'
                })
            });
        }
    })

}

$(document).ready(function () {
    $.ajax({
        url: `https://localhost:44316/api/Persons/GetAllProfile`,
        type: "GET"
    }).done((result) => {
        console.log(result);
        var female = result.data.filter(data => data.gender === 1).length;
        var male = result.data.filter(data => data.gender === 0).length;
        console.log(male);
        var options = {
            series: [male, female],
            chart: {
                type: 'donut',
            },
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };
        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Deleted',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });
});

$(document).ready(function () {
    $.ajax({
        url: `https://localhost:44316/api/Persons/GetAllProfile`,
        type: "GET"
    }).done((result) => {
        console.log(result);
        var gede = result.data.filter(data => data.salary > 1000000).length;
        var kecil = result.data.filter(data => data.salary < 1000000).length;
        console.log(gede);
        var options = {
            series: [gede, kecil],
            chart: {
                width: 380,
                type: 'pie',
            },
            labels: ['> 1000000', '< 1000000'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };

        var chart = new ApexCharts(document.querySelector("#chartuniv"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Deleted',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });
});

$(document).ready(function () {
    $.ajax({
        url: `https://localhost:44316/api/Persons/GetAllProfile`,
        type: "GET"
    }).done((result) => {
        console.log(result);
        var tarumanagara = result.data.filter(data => data.universityid === 1).length;
        var ciputra = result.data.filter(data => data.universityid === 5).length;
        console.log(tarumanagara);
        var options = {
            series: [{
                name: 'Inflation',
                data: [tarumanagara, ciputra]
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    dataLabels: {
                        position: 'top', // top, center, bottom
                    },
                }
            },
            dataLabels: {
                enabled: true,
                formatter: function (val) {
                    return val;
                },
                offsetY: -20,
                style: {
                    fontSize: '12px',
                    colors: ["#304758"]
                }
            },

            xaxis: {
                categories: ["Tarumanagara", "Ciputra"],
                position: 'top',
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false
                },
                crosshairs: {
                    fill: {
                        type: 'gradient',
                        gradient: {
                            colorFrom: '#D8E3F0',
                            colorTo: '#BED1E6',
                            stops: [0, 100],
                            opacityFrom: 0.4,
                            opacityTo: 0.5,
                        }
                    }
                },
                tooltip: {
                    enabled: true,
                }
            },
            yaxis: {
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false,
                },
                labels: {
                    show: false,
                    formatter: function (val) {
                        return val;
                    }
                }

            }
        };

        var chart = new ApexCharts(document.querySelector("#chartkampus"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Deleted',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });
});
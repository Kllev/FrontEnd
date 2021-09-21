$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    console.log(result);
    console.log(result.results[3]);
    var text = "";
    $.each(result.results, function (key, val) {
        text += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>${val.url}</td>
                    <td>
                        <button type="button" class="btn btn-outline-primary" onClick="detail('${val.url}')" data-toggle="modal" data-target="#exampleModal">Detail</button>
                    </td>
                </tr>`;
    });
    $("#bodyPoke").html(text);
}).fail((result) => {
    console.log(result);
});

/*$(document).ready(function () {
    $('#tablePoke').DataTable({
        "filter": true,
        "ajax": {
            "url": "https://pokeapi.co/api/v2/pokemon/",
            "datatype" : "json",
            "dataSrc" : "results"
        },
        "columns": [
            {
                "data": "name"
            },
            {
                "data": null, "sortable": true,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "render": function (data, type, row) {
                    return row['name'];
                }
            },
            {
                "render": function (data, type, row) {
                    return row['url'];
                }
            },
            {
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" onclick="detail('${row['url']}')" data-toggle="modal" data-target="#exampleModal">Detail</button >`
                }
            }
        ]
    });
});*/

function detail(stringurl) {
    $.ajax({
        url: stringurl
    }).done((result) => {
      
        let abilitySkill = "";
        let types = "";

        result.abilities.map(item => {
            abilitySkill += `${item.ability.name}`;
            if (abilitySkill < 1) {
                abilitySkill += ", ";
            }
        })

        result.types.map(item => {
            if (item.type.name === "grass") {
                types += `<span class="badge badge-success mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "poison" || item.type.name === "ice") {
                types += `<span class="badge badge-info mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "fire") {
                types += `<span class="badge badge-danger mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "water" || item.type.name === "electric") {
                types += `<span class="badge badge-primary mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "rock" || item.type.name === "unknown" || item.type.name === "fighting" || item.type.name === "normal" ) {
                types += `<span class="badge badge-secondary mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "flying" || item.type.name === "steel" || item.type.name === "fairy") {
                types += `<span class="badge badge-light mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "ghost" || item.type.name === "dark" || item.type.name === "shadow" || item.type.name === "psychic") {
                types += `<span class="badge badge-dark mr-1">${item.type.name}</span>`
            }
            if (item.type.name === "dragon" || item.type.name === "bug" ) {
                types += `<span class="badge badge-warning mr-1">${item.type.name}</span>`
            }

        })
        
        text2 = `<h3 class="text-center">${result.name}</h3>
                   <img src="${result.sprites.other.dream_world.front_default}" class="border rounded rounded mx-auto d-block" width="200px" height="200px"></img>
                   <p class="text-center">${types}</p>
                   <p>Ability : ${abilitySkill.toUpperCase()}</p>
                   <p>Height : ${result.height} </p>
                   <p>Weight : ${result.weight} </p>
                 
                  `;
        console.log(result.sprites.front_default);
        $("#bodyModal").html(text2);
    }).fail((error) => {
        console.log(error);
    });
}
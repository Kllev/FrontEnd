////const cat = [];

////animals.forEach((item) => {
////    if (item.species === "dog") {
////        item.class.name = "non mamalia";
////    }
////});

////animals.forEach((item) => {
////    if (item.species === "cat") {
////        cat.push(item);
////    }
////});
////console.log("============");
////console.log("Animal");
////for (let i = 0; i < animals.length; i++) {
////    console.log(animals[i].name);
////}
////console.log("============");
////console.log("Cat");
////const result = animals.filter((animals) => animals.species === "cat");
////console.log(result);

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    var text = "";
    $.each(result.results, function (key, val) {
        text += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name.toUpperCase()}</td>
                    <td>
                        <button class="open-Detail btn btn-primary" data-toggle="modal" data-target="#exampleModal" onClick="detailItem('${val.url}')">More Detail</button>
                    </td>
                 </tr>`
    });
    $("#starwars").html(text);
}).fail((result) => {
    console.log(result);
});

const detailItem = (url) => {
    $.ajax({
        url: url
    }).done((result) => {
        let detail = "";
        let abilitys = "";
        let type = "";
        let typebadge = "";
        for (let i = 0; i < 2; i++) {
            abilitys += result.abilities[i].ability.name
            if (i < 1) {
                abilitys += ", "
            }
        };
        for (let i = 0; i < 2; i++) {
            type += result.types[i].type.name
            if (i < 1) {
                type += ", "
            }
        };
        detail += `<img src="${result.sprites.other.dream_world.front_default}"
                class="border rounded mx-auto d-block"  width="200px" height="200px"></img>
                   <p>Ability: ${abilitys.toUpperCase()}</p >
                   <p>Type: ${type.toUpperCase()}</p >`;
        $('.modal-body').html(detail);
    }).fail((result) => {
        console.log(result);
    });
}
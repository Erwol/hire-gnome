function editGnome(id) {
    window.location.assign("/Admin/EditGnome/" + id);
}
function deleteGnome(id) {
    var r = confirm("Are you sure you want to REMOVE this gnome and all his associated data?");
    if (r == true) {
        window.location.assign("/Admin/DeleteGnome/" + id);
    }
}

function hideGnome(id) {
    var r = confirm("Are you sure you want to change the visibility status of this gnome?");
    if (r == true) {
        window.location.assign("/Admin/HideGnome/" + id);
    }
}

function showGnome(id) {
    window.location.assign("/Product/Show?gnome_id=" + id);
}

function addProductToCart(id) {
    var request = new XMLHttpRequest();
    request.open("GET", "/Cart/Add/" + id);
    request.onreadystatechange = function () {
        if (request.readyState == XMLHttpRequest.DONE) {
            var gnomes = JSON.parse(request.responseText);
            console.log(gnomes);
        };
    };
    request.send();
    /*
    var http = new XMLHttpRequest();
    var url = "/Cart/Add";
    var params = product_id;
    http.open("POST", url, true);

    //Send the proper header information along with the request
    //http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

    http.onreadystatechange = function () {//Call a function when the state changes.
        if (http.readyState == 4 && http.status == 200) {
            alert(http.responseText);
        }
    }
    http.send(params*/
}
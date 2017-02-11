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
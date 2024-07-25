function setCookie(name, value) {
    var expires = "";
    var date = new Date();
    date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000)); // Set cookie to expire in 7 days
    expires = "; expires=" + date.toUTCString();
    document.cookie = name + "=" + (value || "") + expires + "; path=/; secure; samesite=None";
}

function deleteCookie(name) {
    console.log("Deleting cookie:", name);
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; secure; samesite=None";
}

//function alertMessage() { // Renamed to avoid conflict with built-in alert function
//    console.log("pspasalslalps");
//}

//function showModal(modalId) {
//    console.log("Showing modal:", modalId);
//    $('#' + modalId).modal('show');
//}

//function hideModal(modalId) {
//    console.log("Hiding modal:", modalId);
//    $('#' + modalId).modal('hide');
//}

//// Ensure the functions are available globally
//window.setCookie = setCookie;
//window.deleteCookie = deleteCookie;
//window.alertMessage = alertMessage;
//window.showModal = showModal;
//window.hideModal = hideModal;

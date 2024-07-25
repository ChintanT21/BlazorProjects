function setCookie(name, value) {
  var expires = "";
  var date = new Date();
  date.setTime(date.getTime() + 7 * 24 * 60 * 60 * 1000); // Set cookie to expire in 7 days
  expires = "; expires=" + date.toUTCString();
  document.cookie =
    name + "=" + (value || "") + expires + "; path=/; secure; samesite=None";
}

function deleteCookie(name) {
  document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
}
function showModal(modalId) {
  console.log("sasas");
  $("#" + modalId).modal("show");
}
function hideModal(modalId) {
  $("#" + modalId).modal("hide");
}

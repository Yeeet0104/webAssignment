
// Function to show the snackbar with a message type
window.showSnackbar = function (message, type) {
    var snackbar = document.getElementById("snackbar");
    var snackbarMessage = document.getElementById("snackbarMessage");
    var snackbarIcon = document.getElementById("snackbarIcon");

    snackbarMessage.textContent = message;
    // Set the type of snackbar
    switch (type) {
        case 'success':
            snackbar.classList.add("bg-green-500");
            snackbarIcon.classList.add("fa-check");
            break;
        case 'warning':
            snackbar.classList.add("bg-red-500");
            snackbarIcon.classList.add("fa-triangle-exclamation");
            break;
        default:
            snackbar.classList.add("bg-blue-500");
            break;
    }

    snackbar.classList.remove('hidden');
    snackbar.classList.add('flex');

    setTimeout(function () {
        snackbar.classList.remove('flex');
        snackbar.classList.add('hidden');
    }, 5000);
}

// Function to hide the snackbar
function hideSnackbar() {
    var snackbar = document.getElementById("snackbar");
    snackbar.classList.remove('flex');
    snackbar.classList.add('hidden');
}
// Toggle the dropdown menu visibility
function toggleDropdown() {
    var dropdown = document.getElementById("dropdown");
    if (dropdown.classList.contains("opacity-0")) {
        dropdown.classList.remove("opacity-0", "scale-95", "invisible", "z-[-1]");
        dropdown.classList.add("opacity-100", "scale-100", "visible");
        dropdown.classList.add("z-[3]");
    } else {
        dropdown.classList.remove("opacity-100", "scale-100", "visible", "z-[3]");
        dropdown.classList.add("z-[-1]");
        dropdown.classList.add("opacity-0", "scale-95", "invisible");
    }
}

// Log out function
function logOut() {
    // You could also make an AJAX request here to log out the user server-side
    window.location.href = 'LogoutHandler.ashx'; // Replace with your server-side logout handler
}

//window.addEventListener('click', function (e) {
//    var dropdown = document.getElementById("dropdown");
//    if (!dropdown.contains(e.target) && !e.target.matches('[data-dropdown-button]')) {
//        dropdown.classList.add("opacity-0", "scale-95", "invisible");
//        dropdown.classList.remove("opacity-100", "scale-100", "visible");
//    }
//});
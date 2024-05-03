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
            // Optionally set a default background class
            snackbar.classList.add("bg-blue-500");
            break;
    }

    snackbar.classList.remove('hidden');
    snackbar.classList.add('flex');

    // After 3 seconds, hide the snackbar
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
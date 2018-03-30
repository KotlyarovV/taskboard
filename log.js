var buttons = document.querySelectorAll('.btn');
var currentLocation = window.location;
var currentDateTime = new Date().toLocaleString();

console.log("logger initializated " + "[" + currentDateTime + "]");

[].forEach.call( buttons, function(button) {
    button.onclick = function(event) {
        buttonText = button.firstChild.nodeValue;
        logString = "Log: [" + currentDateTime + "], BUTTON_CLICK:" + buttonText + " (" + currentLocation + ")";
        console.log(logString);
        return false;
    }
});
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7179/notificationHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});
connection.on("ReceiveNotifications", function (noti) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    //debugger
    var text = JSON.stringify(noti);
    li.textContent = `Notify : ` + text;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
function GetNoti() {

    //
    jQuery.ajax({
        url:window.location.origin+"/Owner/GetNotify",
        type: "GET",
        cache: false
        //,
        //success: function Redirect(dataOut) {
        //    if (dataOut == true) {
        //        RedirectToLink(window.location.origin+"/Owner/ManageYard");
        //        /*alert("delete user success!");*/
        //    }
        //    else {
        //        alert("delete court failed!");
        //    }
        //}
    })
}

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    var mesDTO = JSON.parse(message);
//    debugger
//    connection.invoke("SendNotificationList", user, mesDTO).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});
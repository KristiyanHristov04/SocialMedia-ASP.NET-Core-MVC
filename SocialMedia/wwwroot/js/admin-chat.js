let currentAdminUsername = '';


var connection = new signalR.HubConnectionBuilder().withUrl("/adminChatHub").build();

//Disable the send button until connection is established.
document.getElementById("send-button").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messages-list").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user}: ${message}`;
});

connection.start().then(function () {
    document.getElementById("send-button").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("send-button").addEventListener("click", function (event) {
    /*var user = document.getElementById("user-input").value;*/
    var message = document.getElementById("message-input").value;
    connection.invoke("SendMessage", "Admin", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
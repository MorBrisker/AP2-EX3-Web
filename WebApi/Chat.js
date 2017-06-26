// Declare a proxy to reference the hub
var chat = $.connection.multiMazeHub; 
// Create a function that the hub can call to broadcast messages
chat.client.broadcastMessage = function (sender, message) {
    // Add the message to the page
    $('#chat').append('<li><strong>' + sender
        + '</strong>:&nbsp;&nbsp;' + message + '</li>');
};
// Get the user name and store it to prepend to messages
//var username = prompt('Enter your name:');
// Set initial focus to message input box
//$('#message').focus();
// Start the connection
$.connection.hub.start().done(function () {
    $('#start').click(function () {
        // Call the Send method on the hub
        chat.server.send($('#mazeName').val(), $('#mazeRows').val(), $('#mazeCols').val());
        // Clear text box and reset focus for next comment
        $('#mazeName').val('').focus();
        $('#mazeRows').val('').focus();
        $('#mazeCols').val('').focus();
    });

    $('#join').click(function () {
        // Call the Send method on the hub
        chat.server.connect($('#listOfGames').val());
        // Clear text box and reset focus for next comment
        $('#listOfGames').val('').focus();
    });
});

//var game = $.connection.multiMazeHub; 
//
//game.client.playMove = function (message) {
//    //alert(message);
//    // Add the message to the page
//    //$('#chat').append('<li><strong>' + name
//       // + '</strong>:&nbsp;&nbsp;' + message + '</li>');
//};
//
//$.connection.hub.start().done(function () {
//    $('#btnSendMessage').click(function () {
//        // Call the Send method on the hub
//        chat.server.send("someone pressed");
//        // Clear text box and reset focus for next comment
//        //$('#message').val('').focus();
//    });
//});
//
    /*
    @description:
    ask server for all the users and their amount of victories and losses
    sort their rank and present it in the ranking table 
    */
var usersUrl = "api/Users/";
$.get(usersUrl, {}).done(function (data) {
    length = data.length;
    //sort the users by their rank
    data.sort(compareByRank);
    var users = [[]];
    var str;
    //add each user to the table
    for (index = 0; index < length; ++index) {
        str = $('<tr/>');
        if (index % 2) {
            str = $('<tr style="background-color: #f5f5f5;"/>');
        }
        str.append("<td>" + (data[index].Victories - data[index].Losses) + "</td>");
        str.append("<td>" + data[index].UserNameId + "</td>");
        str.append("<td>" + data[index].Victories + "</td>");
        str.append("<td>" + data[index].Losses + "</td>");
        $('table').append(str);
    }
}).fail(function (response) {
    //if the request failed show error alert
    alert('Something went worng with server...');
});

    /*
    *@description:
    * @param {firstUser} radius The radius of the circle.
    * @param {secondUser} radius The radius of the circle.
    * @return {number} if the -1 if the first user rank is better the the second user,
    *int the opposite case return 1, otherwise return 0 (equals case)
    */
function compareByRank(firstUser, secondUser) {
    if (firstUser.Victories - firstUser.Losses < secondUser.Victories - secondUser.Losses)
        return 1;
    if (firstUser.Victories - firstUser.Losses > secondUser.Victories - secondUser.Losses)
        return -1;
    return 0;
}
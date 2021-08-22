var playersTable;
var gamesTable;

$(document).ready(function () {
    loadPlayersTable();
    loadGamesTable();
})

function loadPlayersTable() {
    playersTable = $("#DT_load").DataTable({
        "ajax": {
            "url": "/api/player",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "name", "width": "20%" },
            { "data": "wins", "width": "20%" },
            { "data": "draws", "width": "20%" },
            { "data": "loses", "width": "20%" },
        ],
        "language": {
            "emptyTable": "no data found"
        }, "width": "100%"
    });
}

function loadGamesTable() {
    gamesTable = $("#DT_games").DataTable({
        "ajax": {
            "url": "/games",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "playerId", "width": "20%" },
            { "data": "date", "width": "20%" },
            { "data": "result", "width": "20%" },
        ],
        "language": {
            "emptyTable": "no data found"
        }, "width": "100%"
    });
}
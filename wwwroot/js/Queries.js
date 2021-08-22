let totalGamesTable = document.getElementById("totalgames");
let totalGamesDesc = document.getElementById("totalgamesasc");
let allPlayers = document.getElementById("all-players");
let playerInfo = document.getElementById("player-info");

axios.get("/api/queries/getallplayers").then(res => {
    const data = res.data;
    for (let i = 0; i < data.length; i++) {
        let name = data[i].name;
        allPlayers.insertAdjacentHTML("afterbegin", `<a class="dropdown-item" href="#" name=${name} onclick="check(event)">${name}</a>`)
    }
});

function check(e) {
    playerInfo.innerHTML = '';

    axios.get(`/api/queries/getplayer?name=${e.target.name}`).then(res => {
        const data = res.data;
        axios.get(`/api/player/playergames?id=${data.id}`).then(res => {
            const games = res.data.data;
            if (games == undefined) {
                playerInfo.insertAdjacentHTML("afterbegin", `<tr><td>No Data To Show</td></tr>`);
            } else {
                for (let i = 0; i < games.length; i++) {
                    playerInfo.insertAdjacentHTML("afterbegin",
                        `<tr>
                            <td>${games[i].date}</td>
                            <td>${games[i].result}</td>
                         </tr>`
                    );
                }
            }
        });
    })
}

axios.get("/api/queries/getallplayers").then(res => {
    const data = res.data;
    for (let i = 0; i < data.length; i++) {
        let tot = data[i].loses + data[i].draws + data[i].wins;
        let name = data[i].name;

        totalGamesTable.insertAdjacentHTML("afterbegin", `<tr><td>${name}</td><td>${tot}</td></tr>`)
    }
});

axios.get("/api/queries/getallplayersasc").then(res => {
    const data = res.data;
    for (let i = 0; i < data.length; i++) {
        let tot = data[i].loses + data[i].draws + data[i].wins;
        let name = data[i].name;

        totalGamesDesc.insertAdjacentHTML("afterbegin", `<tr><td>${name}</td><td>${tot}</td></tr>`)
    }
});


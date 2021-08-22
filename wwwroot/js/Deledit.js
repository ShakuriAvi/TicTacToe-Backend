let allPlayers1 = document.getElementById("all-players1");
let allPlayers2 = document.getElementById("all-players2");
let deletePlayer = document.getElementById("delete-player");
let playerInfo = document.getElementById("player-info");
let dropdownMenuLink1 = document.getElementById("dropdownMenuLink1");
let dropdownMenuLink2 = document.getElementById("dropdownMenuLink2");

var player_id_to_delete1 = undefined;
var player_id_to_delete2 = undefined;

axios.get("/api/queries/getallplayers").then(res => {
    const data = res.data;
    for (let i = 0; i < data.length; i++) {
        let name = data[i].name;
        allPlayers1.insertAdjacentHTML("afterbegin", `<a class="dropdown-item" href="#" id=${data[i].id} name=${name} onclick="check(event)">${name}</a>`)
    }
});

axios.get("/api/queries/getallplayers").then(res => {
    const data = res.data;
    for (let i = 0; i < data.length; i++) {
        let name = data[i].name;
        allPlayers2.insertAdjacentHTML("afterbegin", `<a class="dropdown-item" href="#" id=${data[i].id} name=${name} onclick="assign(event)">${name}</a>`)
    }
});

function assign(e) {
    player_id_to_delete2 = e.target.id;
    dropdownMenuLink2.innerText = e.target.name;
}

function check(e) {
    playerInfo.innerHTML = '';
    player_id_to_delete1 = e.target.id;
    dropdownMenuLink1.innerText = e.target.name;
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
                            <td>
                                <a onclick=deleteGameFromDb('/api/queries/deletegame?id=${games[i].id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px;">Delete</a>
                            </td>
                         </tr>`
                    );
                }
            }
        });
    })
}

function deleteGameFromDb(url) {
    swal({
        title: "Are your sure?",
        text: "Once deleted, you wont be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success("Deleted Successfully!");
                        window.location.reload();
                    }
                    else {
                        toastr.error("Could not delete..");
                    }
                }
            });
        }
    });
}

deletePlayer.addEventListener("click", () => {
    if (player_id_to_delete1)
        deletePlayerFromDb(`/api/queries/deleteplayer?id=${player_id_to_delete1}`);
    else error("Please Choose Player To Delete!");
})

function deletePlayerFromDb(url) {
    swal({
        title: "Are your sure?",
        text: "Once deleted, you wont be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success("Deleted Successfully!");
                        setTimeout(() => {
                            window.location.replace("./Index");
                        }, 1000)
                    }
                    else {
                        toastr.error("Could not delete..");
                    }
                }
            });
        }
    });
}

function editUser(e) {
    e.preventDefault();
    const nameElement = e.target[0];
    const pwdElement = e.target[1];
    const nameValue = e.target[0].value;
    const pwdValue = e.target[1].value;
    const res = validate(nameElement, pwdElement);
    if (!res.status) error(res.msg);
    else {
        if (player_id_to_delete2) {
            success();
            let url = `/api/queries/updateplayer?id=${player_id_to_delete2}&username=${nameValue}&pwd=${pwdValue}`;
            axios.put(url, {}).then(res => {
                if (res.data.status == 400) {
                    error("Username already taken!");
                } else {
                    success().then(_ => {
                        window.location.replace("./index");
                    });
                }
            });
        } else {
            error("Please Choose Player To Edit!");
        }
    }
}

function validate(nameElement, pwdElement) {
    nameValue = nameElement.value;
    pwdValue = pwdElement.value;
    const isNameValid = validateName(nameValue);
    if (!isNameValid) return { status: false, msg: "Name must be greather than 3.." };
    const isPwdValid = validateName(pwdValue);
    if (!isPwdValid) return { status: false, msg: "Password must be number greather than 5 figures.." };
    return { status: true };
}

function validateName(name) {
    if (name.length < 4) return false;
    return true;
}

function validatePwd(pwd) {
    if (pwd > 99999) return false; //must be above 5 figures
    return true;
}

function error(msg) {
    return swal({
        title: "Error!",
        text: msg,
        icon: "error",
    });
}

function success() {
    return swal({
        title: "Success!",
        text: "User has been registered successfully",
        icon: "success",
    });
}
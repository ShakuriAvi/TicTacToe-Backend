function mySubmitFunction(e) {
    e.preventDefault();
    const nameElement = e.target[0];
    const pwdElement = e.target[1];
    const nameValue = e.target[0].value;
    const pwdValue = e.target[1].value;
    const res = validate(nameElement, pwdElement);
    if (!res.status) error(res.msg);
    else {
        success();
        let url = `https://localhost:44368/api/player/addplayer?username=${nameValue}&pwd=${pwdValue}`;
        axios.post(url, {}).then(res => {
            if (res.data.status == 400) {
                error("Username already taken!")
            } else {
                success().then(_ => {
                    window.location.replace("./index");
                });
            }
        });
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
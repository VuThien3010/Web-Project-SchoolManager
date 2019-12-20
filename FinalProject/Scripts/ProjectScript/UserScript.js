function login() {
    var username = $("#id-username").val();
    var password = $("#id-password").val();
    var data = {
        "username": username,
        "password": password
    };
    $.post("/User/Login",
        data,
        (response) => {
            if (response && response.Result === false) {
                alert("Login fail!");
            }
            else {
                switch (response.Result) {
                    case 0:
                        window.location.href = "/Admin/Index";
                        break;
                    case 1:
                        window.location.href = "/Teacher/ListCourseTeaching";
                        break;
                    case 2:
                        window.location.href = "/Student/Home";
                        break;
                    default:
                        alert("Login fail!");
                }
            }
        }
    )
}

function changePasswword() {
    var oldPass = $("#id-pass").val();
    var newPass = $("#id-newpass").val();
    var confirmPass = $("#id-confirmpass").val();
    if (newPass !== confirmPass) {
        alert("Confirm password and new password have to equal!");
        return false;
    }
    var data = {
        "oldPass": oldPass,
        "confirmPass": confirmPass
    };
    $.post("/User/ChangePassword",
        data,
        (response) => {
            if (response && response.Result === false) {
                alert(response.Message);
            }
            else {
                alert("Successfully");
                 window.location.href = "/User/Login";
            }
        }
    )
}
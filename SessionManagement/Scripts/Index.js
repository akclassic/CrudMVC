$(function () {
    loadLoginForm();
});
function loadLoginForm() {
    $.ajax({
        type: "GET",
        url: '/Home/Login',
        dataType: 'html',
        success: function (response) {
            $('#formcontainer').html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function login(){
    const user = {
        Email: "",
        Id: 0,
        Username: $('#username').val(),
        Password: $('#password').val()
    };

    $.ajax({
        type: "POST",
        url: 'http://localhost:52924/api/Users',
        data: JSON.stringify(user),
        contentType: 'application/json',
        //success: function (response) {
        //    console.log(response.status);
        //},
        complete: function (response) {
            if (response.status == 200) {
                //$.ajax({
                //    type: "POST",
                //    url: 'Home/Login',
                //    contentType: 'application/json',
                //    data: response.responseText,
                //    success: function () {
                //        alert('here')
                //    }
                //})
                $('#loginform').submit();
            }
        },
        error: function (response) {
            if (response.status == 404) {
                alert("user not found")
            }
        }
    });
}
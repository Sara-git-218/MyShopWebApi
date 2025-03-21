
Register = async () => {

    const userName = document.getElementById("user_name").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("first_name").value;
    const lastName = document.getElementById("last_name").value;
    if (!userName || !password) {
        alert("username and password are required");
        return;
    }
    const user = {
        user_name: userName,
        password: password,
        first_name: firstName,
        last_name: lastName
    }

    try {

        const responsePost = await fetch("api/Users/register", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(user)
        });
        if (responsePost.ok) {
            alert("user registered successfully")
        }

        else {
            switch (responsePost.status) {
                case 400:
                    const badRequestData = await responsePost.json();
                    alert(`Bad request: ${badRequestData.message || 'Invalid input. Please check your data.'}`);
                    break;
                case 401:
                    alert("Unauthorized: Please check your credentials.");
                    break;
                case 500:
                    alert("Server error. Please try again later.");
                    break;
                default:
                    alert(`Unexpected error: ${responsePost.status}`);
            }
        }



    }
    catch (e) {
        alert("Error: " + e.message);
    }

}


Login = async () => {
    const userName = document.getElementById("user_name_login").value;
    const password = document.getElementById("password_login").value;

    if (!userName || !password) {
        alert("username and password are required");
        return;
    }

    const user = {
        user_name: userName,
        password: password,
        first_name: "",
        last_name: ""

    }
    try {

        const responsePost = await fetch("api/Users/login", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(user)
        });
        if (responsePost.ok) {
            alert("Login successfully")
        }

        else {
            switch (responsePost.status) {
                case 400:
                    const badRequestData = await responsePost.json();
                    alert(`Bad request: ${badRequestData.message || 'Invalid input. Please check your data.'}`);
                    break;
                case 401:
                    alert("Unauthorized: Please check your credentials.");
                    break;
                case 500:
                    alert("Server error. Please try again later.");
                    break;
                default:
                    alert(`Unexpected error: ${responsePost.status}`);
            }
        }



    }
    catch (e) {
        alert("Error: " + e.message);
    }

}








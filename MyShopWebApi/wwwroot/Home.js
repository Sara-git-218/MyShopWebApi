
Register = async () => {

    const userName = document.getElementById("user_name").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("first_name").value;
    const lastName = document.getElementById("last_name").value;
    if (!userName || !password) {
        alert("username and password are required");
        return;
    }
    const hardPassword = await CheckPassword();
    if (hardPassword < 2) {
        alert("your password is low....")
        return;
    }
   
    const user = {
        userName: userName,
        password: password,
        firstName: firstName,
        lastName: lastName
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
        userName: userName,
        password: password,
       

    }
    try {

        const responsePost = await fetch("api/Users/login", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(user)
        });
        console.log(responsePost);
        //alert(responsePost)
       const userDatails = await responsePost.json()
        if (responsePost.ok) {
            alert(`${userDatails.firstName} loginnnnn`);
            console.log(userDatails);
            localStorage.setItem("UserId", userDatails.id);
            localStorage.setItem("UserName", userDatails.userName.trim());
            localStorage.setItem("FirstName", userDatails.firstName.trim());
            localStorage.setItem("LastName", userDatails.lastName.trim());
            console.log("Redirecting to welcome.html...");

            window.location.href = "welcome.html";
            
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


const UpDate = async () => {
    const firstname = document.getElementById("first_name_u").value;
    const lastname = document.getElementById("last_name_u").value;
    const password = document.getElementById("password_u").value;
    const username = document.getElementById("user_name_u").value;
  
    const id = localStorage.getItem("UserId")
    alert(id)
    const user = {
        UserName: username,
        Password: password,
        FirstName: firstname,
        LastName: lastname,
        Id: id
    }
    const responsePost = await fetch(`api/Users/${id}`, {
        method: 'Put',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)

    });

    if (responsePost.ok) {
        alert("updated")
    }
    else {
        alert("error")
    }

}
CheckPassword = async () => {
    const password = document.getElementById("password").value;
    try {
        const responsePost = await fetch("api/Users/checkPassword", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(password)
        });
        const result =await responsePost.json()
        if (responsePost.ok) {
 
            return result;
        }
    }
    catch (e){

    }

}
const CheckMyPassword = async () => {
    const result = await CheckPassword();
    alert(result);
}




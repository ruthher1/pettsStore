
newUser = () => {
    const div = document.querySelector("#newUser")
    div.setAttribute("style","visibility:visible")
}




logIn = async () => {
    const login_username = document.querySelector("#login_username").value;
    const login_password = document.querySelector("#login_password").value;
    const user = { Username: login_username, Password: login_password,FirstName:'',LastName:'' }
    try {
        const response = await fetch("https://localhost:44337/api/users/login", {
            method: "POST",
            body: JSON.stringify(user),
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} - ${response.statusText}`);
        }
        const userResponse = await response.json()
        sessionStorage.setItem("user", JSON.stringify(userResponse));
        return window.location.href = "https://localhost:44337/site.html";
    } catch (error) {
        alert(error.message);
    }


}
const signUp = async () => {
    const Username = document.querySelector("#username").value;
    const LastName = document.querySelector("#lastname").value;
    const FirstName = document.querySelector("#firstname").value;
    const Password = document.querySelector("#password").value;

    const user = { Username, LastName, FirstName, Password };

    try {
        const response = await fetch("https://localhost:44337/api/users", {
            method: "POST",
            body: JSON.stringify(user),
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} - ${response.statusText}`);
        }

        alert("User added");
    }   catch (error) {
        alert(error.message);
    }
};

checkPassword = async () => {
    const Password = document.getElementById("password").value;
    const response = await fetch('https://localhost:44337/api/users/password',
        { method: 'POST', body: JSON.stringify(Password), headers: { "Content-Type": 'application/json' } })
    if (!response.ok)
        throw new Error("Http error. status:" + response.status);
    const passStrength = await response.json()
    alert(passStrength > 2 ? "Password is strong" : "Password is weak")


};

const update = async () => {
    const update_username = document.querySelector("#update_username").value;
    const update_lastname = document.querySelector("#update_lastname").value;
    const update_firstname = document.querySelector("#update_firstname").value;
    const update_password = document.querySelector("#update_password").value;
    const user = JSON.parse(sessionStorage.getItem("user"));
    const userUpdate = {
        Id:user.id,
        Username: update_username,
        LastName: update_lastname,
        FirstName: update_firstname,
        Password: update_password
    };
    try {
        const response = await fetch(`https://localhost:44337/api/users/${userUpdate.Id}`, {
            method: "PUT",
            body: JSON.stringify(userUpdate),
            headers: { "Content-Type": "application/json" }
        });
        if (!response.ok) {
            throw new Error(`Error: ${response.status} -- ${response.statusText}`);
        }

        alert("User updated");
    } catch (error) {
        alert(error.message);
    }
};
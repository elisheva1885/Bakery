const Register = async () => {

    const firstName = document.getElementById("first_name").value
    const lastName = document.getElementById("last_name").value
    const password = document.getElementById("psw").value
    const userName = document.getElementById("user_name").value
    console.log(firstName, lastName, userName, password)

    if (!firstName || !lastName || !password || !userName) {
        alert("All fields are required");
        return;
    }

    const user = {
        firstName: firstName,
        lastName: lastName,
        userName: userName,
        password: password
    }

    const responsePost = await fetch('api/User/register', {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)

    });
    if (!responsePost.ok) {
        const errorMessage = await responsePost.text();
        alert(errorMessage);
    } else {
        alert("User registered successfully");
    }
    }


const login = async () => {
    const password = document.getElementById("Lpsw").value
    const userName = document.getElementById("Luser_name").value
    const user = {
        password: password,
        userName: userName
    }
    if (!password || !userName) {
        alert("username and password are required");
        return;
    }

    const response = await fetch('api/User/login', {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)
    });
    const text = await response.text();

    if (response.ok) {
        const userDetails = JSON.parse(text);
        alert(userName + " login success");
        localStorage.setItem("userId", userDetails.id);
        window.location.href = "updatepage.html";
    } else {
        alert(text); // טקסט השגיאה שמגיע מהשרת
    }

}



const upDate = async () => {
    const firstName = document.getElementById("Ufirst_name").value
    const lastName = document.getElementById("Ulast_name").value
    const password = document.getElementById("Upsw").value
    const userName = document.getElementById("Uuser_name").value

    const id = localStorage.getItem("userId")

    const user = {
        firstName: firstName,
        lastName: lastName,
        userName: userName,
        password: password,
        Id:id
    }
    console.log(user)

    const responsePost = await fetch(`api/User/${id}`, {
        method: 'Put',
        headers: {
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(user)

    });

    if (!responsePost.ok) {
        const errorMessage = await responsePost.text();
        alert(errorMessage);
    } else {
        alert("User updated successfully");
    }
}


const checkPasswordStrong = async() => {
    const password = document.getElementById("psw").value;
    const response = await fetch('api/User/checkPassword', {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(password)
    })
    const strong = await response.json();
    alert(strong);
}
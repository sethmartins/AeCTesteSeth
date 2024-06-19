document.addEventListener('DOMContentLoaded', function () {
  const loginForm = document.getElementById('loginForm');
  const message = document.getElementById('message');

  loginForm.addEventListener('submit', function (event) {
    event.preventDefault();
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    alert(username + " " + password)
    fetch('/api/login/entar', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
    })
      .then(response => response.json())
      .then(data => {
        if (data.message === "Login successful") {
          alert("ok");
          message.style.color = 'green';
          message.textContent = data.message;
        } else {
          alert("ok");
          message.style.color = 'red';
          message.textContent = data.message;
        }
      });
  });
});

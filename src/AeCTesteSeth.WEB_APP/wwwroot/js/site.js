// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
  const registerForm = document.getElementById('registerForm');
  const loginForm = document.getElementById('loginForm');
  const addressForm = document.getElementById('addressForm');
  //const addressesTable = document.getElementById('addressesTable').getElementsByTagName('tbody')[0];
  const message = document.getElementById('message');

  if (registerForm) {
    registerForm.addEventListener('submit', function (event) {
      event.preventDefault();
      const usuario_ = document.getElementById('usuario_').value;
      const senha = document.getElementById('senha').value;
      const nome = document.getElementById('nome').value;

      fetch('/api/v1/usuarios/adicionar', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Usuario_: usuario_, Senha: senha, Nome: nome })
      })
        .then(response => response.json())
        .then(data => {
          message.textContent = data.message;
        });
    });
  }

  if (loginForm) {
    loginForm.addEventListener('submit', function (event) {
      event.preventDefault();
      const username = document.getElementById('username').value;
      const password = document.getElementById('password').value;

      fetch('/api/login/entrar', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Usuario: username, Senha: password })
      })
        .then(response => response.json())
        .then(data => {
          if (data.message === "Login successful") {
            sessionStorage.setItem('userId', data.userId);
            sessionStorage.setItem('userUsuario', data.userUsuario)
            sessionStorage.setItem('userName', data.userName);
            sessionStorage.setItem('userSenha', data.userSenha);
           
            window.location.href = 'Endereco.html';
          } else {
            message.textContent = data.message;
          }
        });
    });
  }

  if (addressForm) {
    const userId = sessionStorage.getItem('userId');
    const userUsuario = sessionStorage.getItem('userUsuario');
    const userName = sessionStorage.getItem('userName');
    const userSenha = sessionStorage.getItem('userSenha');

    function loadAddresses() {
      fetch('/api/enderecos/all/')
        .then(response => response.json())
        .then(addresses => {
          addressesTable.innerHTML = '';
          addresses.forEach(address => {
            const row = addressesTable.insertRow();
            row.innerHTML = `
                             <td>${address.logradouro}</td>
          <td>${address.numero}</td>
          <td>${address.cep}</td>
          <td>${address.complemento}</td>
          <td>${address.bairro}</td>
          <td>${address.cidade}</td>
          <td>${address.uf}</td>
                            <td>
                                <button onclick="editAddress(${address.id})" class="btn btn-primary">Edit</button>
                                <button onclick="deleteAddress(${address.id})" class="btn btn-danger">Delete</button>
                            </td>
                        `;
          });
        });
    }

    loadAddresses();

    addressForm.addEventListener('submit', function (event) {
      event.preventDefault();
      const Id = document.getElementById('Id').value;
      const bairro = document.getElementById('bairro').value;
      const rua = document.getElementById('logradouro').value;
      const cidade = document.getElementById('cidade').value;
      const uf = document.getElementById('uf').value;
      const cep = document.getElementById('cep').value;
      const complemento = document.getElementById('complemento').value;
      const numero = document.getElementById('numero').value;

      const endereco = {
        'Logradouro': rua, 'Cidade': cidade, 'Bairro': bairro, 'Uf': uf, 'Cep': cep,
        'Usuario':
        {
           'Nome': userName, 'Usuario_': userUsuario, 'Senha': userSenha 
        }, 'UsuarioId': userId, 
'Complemento': complemento, 'Numero': numero
      };
      const enderecoput = { 'Id': Id,
        'Logradouro': rua, 'Cidade': cidade, 'Bairro': bairro, 'Uf': uf, 'Cep': cep,
        'Usuario':
        {
          'Nome': userName, 'Usuario_': userUsuario, 'Senha': userSenha
        }, 'UsuarioId': userId,
        'Complemento': complemento, 'Numero': numero
      };

      if (Id) {
        fetch(`/api/enderecos/${Id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(enderecoput)
        })
          .then(() => {
            loadAddresses();
            addressForm.reset();
          });
      } else {
        fetch('/api/enderecos/adicionar', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(endereco)
        })
          .then(() => {
            loadAddresses();
            addressForm.reset();
          });
      }
    });

    window.editAddress = function (id) {
      fetch(`/api/enderecos/${id}`)
        .then(response => response.json())
        .then(endereco => {
          document.getElementById('Id').value = id;
          document.getElementById('logradouro').value = endereco.logradouro;
          document.getElementById('cidade').value = endereco.cidade;
          document.getElementById('uf').value = endereco.uf;
          document.getElementById('numero').value = endereco.numero;
          document.getElementById('complemento').value = endereco.complemento;
          document.getElementById('bairro').value = endereco.bairro;
          document.getElementById('cep').value = endereco.cep;
        });
    }

    window.deleteAddress = function (id) {
      fetch(`/api/enderecos/${id}`, {
        method: 'DELETE'
      })
        .then(() => {
          loadAddresses();
        });
    }


    const cepInput = document.getElementById('cep');
    cepInput.addEventListener('blur', function () {
      const zipCode = cepInput.value;
      if (zipCode.length === 8) {
        fetch(`https://viacep.com.br/ws/${zipCode}/json/`)
          .then(response => response.json())
          .then(endereco => {
            if (!endereco.erro) {
              document.getElementById('logradouro').value = endereco.logradouro;
              document.getElementById('cidade').value = endereco.cidade;
              document.getElementById('uf').value = endereco.uf;
              document.getElementById('numero').value = endereco.numero;
              document.getElementById('complemento').value = endereco.complemento;
              document.getElementById('bairro').value = endereco.bairro;
              document.getElementById('cep').value = endereco.cep;
            } else {
              alert('CEP não encontrado.');
            }
          });
      } else {
        alert('CEP deve ter 8 dígitos.');
      }
    });

    const exportCsvButton = document.getElementById('exportarCsv');
    exportCsvButton.addEventListener('click', function () {
      const rows = Array.from(addressesTable.rows);
      const csvContent = rows.map(row => {
        const cells = Array.from(row.cells);
        return cells.map(cell => cell.textContent).join(',');
      }).join('\n');

      const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
      const link = document.createElement('a');
      const url = URL.createObjectURL(blob);
      link.setAttribute('href', url);
      link.setAttribute('download', 'enderecos.csv');
      link.style.visibility = 'hidden';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    });

  }
});


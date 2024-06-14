using AeCTesteSeth.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AeCTesteSeth.BLL.Models
{
    public class Endereco : Entity
    {
        public required string Cep { get; set; }
        public required string Logradouro { get; set; }
        public string Complemento { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Uf { get; set; }
        public required string Numero { get; set; }
        public required Usuario Usuario { get; set; }
        public required int UsuarioId { get; set; }

    }
}

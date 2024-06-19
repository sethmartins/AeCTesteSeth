using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeCTesteSeth.BLL.Models
{
    public  class Login
    {
        public Login(string usuario, string senha)
        {
            this.Usuario = usuario;
            this.Senha = senha;
        }
        public required string Usuario { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Informe a Senha")]
        [StringLength(10, MinimumLength = 6)]
        public required string Senha { get; set; }
    }
}

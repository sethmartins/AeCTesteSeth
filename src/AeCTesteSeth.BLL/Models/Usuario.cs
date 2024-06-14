using AeCTesteSeth.BLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeCTesteSeth.BLL.Models
{
    public class Usuario : Entity
    {
        public Usuario()
        {
            Enderecos = new List<Endereco>();
        }
        public string Nome { get; set; }
        public required string Usuario_ { get; set; }

        //Usando Data Annotations
        [DataType(DataType.Password)]
        [Display(Name = "Informe a Senha")]
        [StringLength(10, MinimumLength = 6)]
        public required string Senha { get; set; }

        public IEnumerable<Endereco> Enderecos { get; set; }

    }
}

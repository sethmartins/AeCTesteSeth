using AeCTesteSeth.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeCTesteSeth.BLL.Builder
{
    public class LoginBuilder : Login
    {
        public LoginBuilder(string usuario, string senha) : base(usuario, senha)
        {
        }
    }
}

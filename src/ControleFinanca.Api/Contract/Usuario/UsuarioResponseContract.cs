using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanca.Api.Contract.Usuario
{
    public class UsuarioResponseContract : UsuarioLoginRequestContract
    {
        public int Id {get; set;}
        public DateTime DataCadastro {get; set;}
    }
}
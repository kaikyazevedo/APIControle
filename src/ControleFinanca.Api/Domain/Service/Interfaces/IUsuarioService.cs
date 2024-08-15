using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.Usuario;

namespace ControleFinanca.Api.Domain.Service.Interfaces
{
    public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, int>
    {
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequestContract);
    
        Task<UsuarioResponseContract> Obter (string email);
    }
}
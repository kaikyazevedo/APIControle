using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ControleFinanca.Api.Domain.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, int>
    {
        Task<Usuario?> Obter(string email);
       
    }
}
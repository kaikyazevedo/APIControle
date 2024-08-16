using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Domain.Models;

namespace ControleFinanca.Api.Domain.Repository.Interfaces
{
    public interface IApagarRepository : IRepository<Apagar, int>
    {
        Task<IEnumerable<Apagar>> ObterPeloIdUsuario(int idUsuario);
    }
}
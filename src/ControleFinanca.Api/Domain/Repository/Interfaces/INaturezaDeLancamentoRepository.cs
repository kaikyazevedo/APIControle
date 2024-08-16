using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Domain.Models;

namespace ControleFinanca.Api.Domain.Repository.Interfaces
{
    public interface INaturezaDeLancamentoRepository : IRepository<NaturezaDeLancamento, int>
    {
        Task<IEnumerable<NaturezaDeLancamento>> ObterPeloIdUsuario(int idUsuario);
    }
}
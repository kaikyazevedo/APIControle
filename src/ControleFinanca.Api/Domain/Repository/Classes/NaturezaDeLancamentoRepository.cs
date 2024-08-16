using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Data;
using ControleFinanca.Api.Domain.Models;
using ControleFinanca.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanca.Api.Domain.Repository.Classes
{
   public class NaturezaDeLancamentoRepository : INaturezaDeLancamentoRepository
    {

        private readonly ApplicationContext _contexto;

        public NaturezaDeLancamentoRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<NaturezaDeLancamento> Adicionar(NaturezaDeLancamento entidade)
        {
            await _contexto.NaturezaDeLancamento.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<NaturezaDeLancamento> Atualizar(NaturezaDeLancamento entidade)
        {
            NaturezaDeLancamento entidadeBanco = _contexto.NaturezaDeLancamento
                .Where(u => u.Id == entidade.Id)
                .FirstOrDefault();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<NaturezaDeLancamento>(entidadeBanco);

            await _contexto.SaveChangesAsync();
            
            return entidadeBanco;
        }

        public async Task Deletar(NaturezaDeLancamento entidade)
        {
            // Deletar logico, só altero a data de inativação.
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> Obter()
        {
            return await _contexto.NaturezaDeLancamento.AsNoTracking()
                                           .OrderBy(u => u.Id)
                                           .ToListAsync();
        }

        public async Task<NaturezaDeLancamento?> Obter(int id)
        {
            return await _contexto.NaturezaDeLancamento.AsNoTracking()
                                                       .Where(u => u.Id == id)
                                                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> ObterPeloIdUsuario(int idUsuario)
        {
            return await _contexto.NaturezaDeLancamento.AsNoTracking()
                                                       .Where(n => n.IdUsuario == idUsuario)
                                                        .OrderBy(n => n.Id)
                                                        .ToListAsync();
        }
    }
}
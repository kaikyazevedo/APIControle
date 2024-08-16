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
   public class ApagarRepository : IApagarRepository
    {

        private readonly ApplicationContext _contexto;

        public ApagarRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<Apagar> Adicionar(Apagar entidade)
        {
            await _contexto.Apagar.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<Apagar> Atualizar(Apagar entidade)
        {
            Apagar entidadeBanco = _contexto.Apagar
                .Where(u => u.Id == entidade.Id)
                .FirstOrDefault();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<Apagar>(entidadeBanco);

            await _contexto.SaveChangesAsync();
            
            return entidadeBanco;
        }

        public async Task Deletar(Apagar entidade)
        {
            // Deletar logico, só altero a data de inativação.
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }

        public async Task<IEnumerable<Apagar>> Obter()
        {
            return await _contexto.Apagar.AsNoTracking()
                                           .OrderBy(u => u.Id)
                                           .ToListAsync();
        }

        public async Task<Apagar?> Obter(int id)
        {
            return await _contexto.Apagar.AsNoTracking()
                                                       .Where(u => u.Id == id)
                                                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Apagar>> ObterPeloIdUsuario(int idUsuario)
        {
            return await _contexto.Apagar.AsNoTracking()
                                                       .Where(n => n.IdUsuario == idUsuario)
                                                        .OrderBy(n => n.Id)
                                                        .ToListAsync();
        }
    }
}
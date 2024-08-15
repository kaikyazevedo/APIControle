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
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly ApplicationContext _context;
        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Usuario> Adicionar(Usuario entidade)
        {
            await _context.Usuario.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Usuario> Atualizar(Usuario entidade)
        {
            Usuario entidadeBanco = await _context.Usuario
            .Where(u => u.Id == entidade.Id)
            .FirstOrDefaultAsync();

            _context.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _context.Update<Usuario>(entidadeBanco);

            await _context.SaveChangesAsync();

            return entidadeBanco;
        }

        public async Task Deletar(Usuario entidade)
        {
            // Aqui deleta Fisico, deleta no contexto e depois no banco.
            _context.Entry(entidade).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> Obter(string email)
        {
            return await _context.Usuario.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();

            
        }

        public async Task<IEnumerable<Usuario>> Obter()
        {
            return await _context.Usuario.AsNoTracking()
            .OrderBy(u => u.Id)
            .ToListAsync();
        }

        public async Task<Usuario?> Obter(int id)
        {
            return await _context.Usuario.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        }
    }
}
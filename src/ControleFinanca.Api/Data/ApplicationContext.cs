using ControleFinanca.Api.Data.Mappings;
using ControleFinanca.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanca.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public  ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

    }
    
} 
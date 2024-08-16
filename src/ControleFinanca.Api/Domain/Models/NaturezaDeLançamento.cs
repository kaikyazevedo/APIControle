using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.Usuario;
using Microsoft.AspNetCore.SignalR;

namespace ControleFinanca.Api.Domain.Models
{
     public class NaturezaDeLancamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        public string? Observacao { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set;} 
        public DateTime? DataInativacao { get; set; }

    }
}
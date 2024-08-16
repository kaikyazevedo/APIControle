using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.Usuario;
using Microsoft.AspNetCore.SignalR;

namespace ControleFinanca.Api.Domain.Models
{
     public class Apagar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

         [Required]
        public int IdNaturezaDeLancamento { get; set; }

        public NaturezaDeLancamento NaturezaDeLancamento { get; set; }

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo de ValorOriginal é obrigatório.")]
        
        public double ValorOriginal { get; set; }

         [Required(ErrorMessage = "O campo de ValorPago é obrigatório.")]
        
        public double ValorPago { get; set; }

        public string? Observacao { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set;}

         [Required(ErrorMessage = "O campo de DATAvENCIMENTO é obrigatório.")]
        public DateTime DataVencimento { get; set;}

        public DateTime? DataInativacao { get; set; }

        public DateTime? DataReferencia { get; set; }

         public DateTime? DataPagamento { get; set; }

    }
}
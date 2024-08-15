using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.Usuario;
using Microsoft.AspNetCore.SignalR;

namespace ControleFinanca.Api.Domain.Models
{
    public class Usuario
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "O campo de email é obrigatorio")]
        public string Email {get; set;} = string.Empty;   

        [Required(ErrorMessage = "O campo de senha é obrigatorio")]
        public string Senha {get; set;} = string.Empty;
        [Required]
        public DateTime DataCadastro {get; set;}
        public DateTime? DataInativacao {get; set;}

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanca.Api.Contract.NaturezaDeLancamento
{
    public class NaturezaDeLancamentoResponseContract : NaturezaDeLancamentoRequestContract
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }

    }
}
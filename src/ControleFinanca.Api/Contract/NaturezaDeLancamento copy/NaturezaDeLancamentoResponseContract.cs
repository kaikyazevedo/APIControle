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
        public DateTime IDataCadastro { get; set; }
        public DateTime? IDateInativacao { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanca.Api.Contract.NaturezaDeLancamento
{
  public class ApagarRequestContract 
    {
        public double ValorPago { get; set; }

        public DateTime? DataPagamento { get; set; }

    }
}
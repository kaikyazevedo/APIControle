using AutoMapper;
using ControleFinanca.Api.Contract.NaturezaDeLancamento;
using ControleFinanca.Api.Domain.Models;

namespace ControleFinanca.Api.AutoMapper
{
    public class NaturezaDeLancamentoProfile : Profile
    {
        public NaturezaDeLancamentoProfile()
        {
            CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoRequestContract>().ReverseMap();
            CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoResponseContract>().ReverseMap();
        }
    }
}
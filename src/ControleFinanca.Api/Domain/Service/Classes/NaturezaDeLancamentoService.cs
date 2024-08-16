using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFinanca.Api.Contract.NaturezaDeLancamento;
using ControleFinanca.Api.Domain.Models;
using ControleFinanca.Api.Domain.Repository.Interfaces;
using ControleFinanca.Api.Domain.Service.Interfaces;

namespace ControleFinanca.Api.Domain.Service.Classes
{
   public class NaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, int>
    {
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;
        private readonly IMapper _mapper;

        public NaturezaDeLancamentoService(
            INaturezaDeLancamentoRepository naturezaDeLancamentoRepository,
            IMapper mapper)
        {
            _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
            _mapper = mapper;
        }

        public async Task<NaturezaDeLancamentoResponseContract> Adicionar(NaturezaDeLancamentoRequestContract entidade, int idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(entidade);

            naturezaDeLancamento.DataCadastro = DateTime.Now;
            naturezaDeLancamento.IdUsuario = idUsuario;

            naturezaDeLancamento = await _naturezaDeLancamentoRepository.Adicionar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        public async Task<NaturezaDeLancamentoResponseContract> Atualizar(int id, NaturezaDeLancamentoRequestContract entidade, int idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            naturezaDeLancamento.Descricao = entidade.Descricao;
            naturezaDeLancamento.Observacao = entidade.Observacao;

            naturezaDeLancamento = await _naturezaDeLancamentoRepository.Atualizar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        public async Task Inativar(int id, int idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _naturezaDeLancamentoRepository.Deletar(naturezaDeLancamento);
        }

        public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> Obter(int idUsuario)
        {
            var naturezasDelancamento = await _naturezaDeLancamentoRepository.ObterPeloIdUsuario(idUsuario);
            return naturezasDelancamento.Select(natureza => _mapper.Map<NaturezaDeLancamentoResponseContract>(natureza));
        }

        public async Task<NaturezaDeLancamentoResponseContract> Obter(int id, int idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            
            return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
        }

        private async Task<NaturezaDeLancamento> ObterPorIdVinculadoAoIdUsuario(int id, int idUsuario)
        {
            var naturezaDeLancamento = await _naturezaDeLancamentoRepository.Obter(id);

            if (naturezaDeLancamento is null || naturezaDeLancamento.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrada nenhuma natureza de lançamento pelo id {id}");
            }

            return naturezaDeLancamento;
        }

    }
}
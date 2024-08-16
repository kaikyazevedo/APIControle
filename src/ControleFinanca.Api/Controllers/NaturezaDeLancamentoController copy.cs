using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.NaturezaDeLancamento;
using ControleFinanca.Api.Contract.Usuario;
using ControleFinanca.Api.Domain.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanca.Api.Controllers
{
    [ApiController]
    [Route("naturezas-de-lancamento")]
    public class NaturezaDeLancamentoController : BaseController
    {
        private readonly IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, int> _naturezaDeLancamentoService;

        public NaturezaDeLancamentoController(
            IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, int> naturezaDeLancamentoService)
        {
            _naturezaDeLancamentoService = naturezaDeLancamentoService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(NaturezaDeLancamentoRequestContract contrato)
        {
            try
            {  
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _naturezaDeLancamentoService.Adicionar(contrato, _idUsuario));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Obter(_idUsuario));
            }
            
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(int id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Obter(id, _idUsuario));
            }
            
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(int id, NaturezaDeLancamentoRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Atualizar(id, contrato, _idUsuario));
            }
            
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _naturezaDeLancamentoService.Inativar(id, _idUsuario);
                return NoContent();
            }
            
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
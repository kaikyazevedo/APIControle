using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.Usuario;
using ControleFinanca.Api.Domain.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanca.Api.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Adicionar(UsuarioRequestContract contrato)
        {
            try
            {
                return Created("", await _usuarioService.Adicionar(contrato, 0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Obter()
        {
            try
            {
                return Ok(await _usuarioService.Obter(0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Obter(int id)
        {
            try
            {
                return Ok(await _usuarioService.Obter(id, 0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Atualizar(int id, UsuarioRequestContract contrato)
        {
            try
            {
                return Ok(await _usuarioService.Atualizar(id, contrato, 0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _usuarioService.Inativar(id, 0);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
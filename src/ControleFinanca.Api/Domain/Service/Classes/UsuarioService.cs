using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ControleFinanca.Api.Contract.Usuario;
using ControleFinanca.Api.Domain.Models;
using ControleFinanca.Api.Domain.Repository.Interfaces;
using ControleFinanca.Api.Domain.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleFinanca.Api.Domain.Repository.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract entidade, int idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario = await _usuarioRepository.Adicionar(usuario);
            
            return _mapper.Map<UsuarioResponseContract>(usuario);

        }

        private string GerarHashSenha(string senha)
        {
            string hashSenha;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
                byte[] byteHashSenha = sha256.ComputeHash(bytesSenha);
                hashSenha = BitConverter.ToString(byteHashSenha).ToLower();
            }

            return hashSenha;
        }

        public async Task<UsuarioResponseContract> Atualizar(int id, UsuarioRequestContract entidade, int idUsuario)
        {
            _ = await Obter(id) ?? throw new Exception("Usuario nao encontrado para atualização.");

            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.Id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);

            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequestContract)
        {
            throw new NotImplementedException();
        }

        public async Task Inativar(int id, int idUsuario)
        {
          var usuario = await Obter(id) ?? throw new Exception("Usuario nao encontrado para inativação.");
            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));
        }

        public async Task<IEnumerable<UsuarioResponseContract>> Obter(int idUsuario)
        {
            return await Obter(idUsuario);
        }

        public async Task<UsuarioResponseContract> Obter(int id, int idUsuario)
        {
            var usuario = await _usuarioRepository.Obter(id);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task<UsuarioResponseContract> Obter(string email)
        {
            var usuario = await _usuarioRepository.Obter(email);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }
    }
}
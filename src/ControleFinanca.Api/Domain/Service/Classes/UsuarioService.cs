using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ControleFinanca.Api.Contract.Usuario;
using ControleFinanca.Api.Domain.Models;
using ControleFinanca.Api.Domain.Repository.Interfaces;
using ControleFinanca.Api.Domain.Service.Interfaces;
using ControleFinancas.Api.Domain.Services.Classes;

namespace ControleFinanca.Api.Domain.Repository.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        private readonly TokenService _tokenService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract entidade, int idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;

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
                hashSenha = BitConverter.ToString(byteHashSenha).Replace("-","").Replace("-","").ToLower();
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

        public async Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequestContract)
        {
            var usuario = await Obter(usuarioLoginRequestContract.Email);
        
            var hashSenha = GerarHashSenha(usuarioLoginRequestContract.Senha);

            if(usuario is null || usuario.Senha != hashSenha)
            {

                throw new AuthenticationException("Usuário ou senha inválida.");
            }

            return new UsuarioLoginResponseContract {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenService.GerarToken(_mapper.Map<Usuario>(usuario))
            };        
        }


        public async Task Inativar(int id, int idUsuario)
        {
          var usuario = await _usuarioRepository.Obter(id) ?? throw new Exception("Usuario nao encontrado para inativação.");
            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));
        }

        public async Task<IEnumerable<UsuarioResponseContract>> Obter(int idUsuario)
        {
            var usuarios = await _usuarioRepository.Obter();

            return usuarios.Select(usuario => _mapper.Map<UsuarioResponseContract>(usuario));
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
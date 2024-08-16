using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFinanca.Api.Contract.Usuario;
using ControleFinanca.Api.Controllers;
using ControleFinanca.Api.Domain.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ControleFinanca.Api.Tests
{
    public class UsuarioControllerTests
    {
        private readonly UsuarioController _controller;
        private readonly Mock<IUsuarioService> _mockUsuarioService;

        public UsuarioControllerTests()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _controller = new UsuarioController(_mockUsuarioService.Object);
        }

        [Fact]
        public async Task Autenticar_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var request = new UsuarioLoginRequestContract
            {
                Email = "email@exemplo.com",
                Senha = "senhaSegura123"
            };

            var response = new UsuarioLoginResponseContract
            {
                Id = 1,
                Email = "email@exemplo.com",
                Token = "token-exemplo"
            };

            _mockUsuarioService.Setup(service => service.Autenticar(It.IsAny<UsuarioLoginRequestContract>()))
                               .ReturnsAsync(response);

            // Act
            var result = await _controller.Autenticar(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Autenticar_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var request = new UsuarioLoginRequestContract
            {
                Email = "email@exemplo.com",
                Senha = "senhaIncorreta"
            };

            _mockUsuarioService.Setup(service => service.Autenticar(It.IsAny<UsuarioLoginRequestContract>()))
                               .ThrowsAsync(new AuthenticationException("Invalid credentials"));

            // Act
            var result = await _controller.Autenticar(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public async Task Adicionar_ShouldReturnCreated_WhenUserIsAdded()
        {
            // Arrange
            var request = new UsuarioRequestContract
            {
                Email = "email@exemplo.com",
                Senha = "senhaSegura123",
                DataInativacao = null
            };

            var response = new UsuarioResponseContract
            {
                Id = 1,
                Email = "email@exemplo.com",
                Senha = "senhaSegura123",
                DataCadastro = DateTime.Now
            };

            _mockUsuarioService.Setup(service => service.Adicionar(It.IsAny<UsuarioRequestContract>(), It.IsAny<int>()))
                               .ReturnsAsync(response);

            // Act
            var result = await _controller.Adicionar(request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(response, createdResult.Value);
        }

        [Fact]
        public async Task Obter_ShouldReturnOk_WithUserData()
        {
            // Arrange
            var response = new UsuarioResponseContract
            {
                Id = 1,
                Email = "email@exemplo.com",
                Senha = "senhaSegura123",
                DataCadastro = DateTime.Now
            };

            _mockUsuarioService.Setup(service => service.Obter(It.IsAny<int>()))
                  .ReturnsAsync(new List<UsuarioResponseContract> { response });

            // Act
            var result = await _controller.Obter();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task ObterById_ShouldReturnOk_WithUserData()
        {
            // Arrange
            var id = 1;
            var response = new UsuarioResponseContract
            {
                Id = id,
                Email = "email@exemplo.com",
                Senha = "senhaSegura123",
                DataCadastro = DateTime.Now
            };

            _mockUsuarioService.Setup(service => service.Obter(It.IsAny<int>(), It.IsAny<int>()))
                               .ReturnsAsync(response);

            // Act
            var result = await _controller.Obter(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Atualizar_ShouldReturnOk_WhenUserIsUpdated()
        {
            // Arrange
            var id = 1;
            var request = new UsuarioRequestContract
            {
                Email = "email@exemplo.com",
                Senha = "novaSenhaSegura123",
                DataInativacao = null
            };

            var response = new UsuarioResponseContract
            {
                Id = id,
                Email = "email@exemplo.com",
                Senha = "novaSenhaSegura123",
                DataCadastro = DateTime.Now
            };

            _mockUsuarioService.Setup(service => service.Atualizar(It.IsAny<int>(), It.IsAny<UsuarioRequestContract>(), It.IsAny<int>()))
                               .ReturnsAsync(response);

            // Act
            var result = await _controller.Atualizar(id, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Deletar_ShouldReturnNoContent_WhenUserIsDeleted()
        {
            // Arrange
            var id = 1;

            _mockUsuarioService.Setup(service => service.Inativar(It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Deletar(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}

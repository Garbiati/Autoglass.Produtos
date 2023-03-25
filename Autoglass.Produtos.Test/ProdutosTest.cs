using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoglassAPI.Controllers;
using AutoglassAPI.Application.DTOs;
using AutoglassAPI.Application.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace Autoglass.Produtos.Test
{
    public class ProdutosControllerTeste : IClassFixture<WebApplicationFactory<AutoglassAPI.Startup>>
    {
        private readonly WebApplicationFactory<AutoglassAPI.Startup> _factory;

        public ProdutosControllerTeste(WebApplicationFactory<AutoglassAPI.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TesteListarProdutos()
        {
            // Arrange
            var filtro = new FiltroProdutosDTO();
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IProdutoService, ProdutoServiceMock>();
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/api/produtos");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<object>();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task TesteGetProdutosAsync()
        {
            // Arrange
            var filtro = new FiltroProdutosDTO();
            var produtos = new ProdutoListaDTO();
            var mock = new Mock<IProdutoService>();
            mock.Setup(servico => servico.GetProdutosAsync(filtro, true))
                .ReturnsAsync(produtos);
            var produtoService = mock.Object;

            // Act
            var resultado = await produtoService.GetProdutosAsync(filtro, true);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<ProdutoListaDTO>(resultado);
            Assert.Equal(produtos, resultado);
        } 

        private class ProdutoServiceMock : IProdutoService
        {
            public async Task<ProdutoDTO> GetByIdAsync(int id)
            {
                await Task.Delay(100);
                return new ProdutoDTO();
            }

            public async Task<ProdutoDTO> AddAsync(ProdutoCreateDTO produtoCreateDTO)
            {
                await Task.Delay(100);
                return new ProdutoDTO();
            }

            public async Task UpdateByIdAsync(int id, ProdutoUpdateDTO produtoUpdateDTO)
            {
                await Task.Delay(100);
            }

            public async Task DeleteAsync(int id)
            {
                await Task.Delay(100);
            }

            public async Task<IEnumerable<ProdutoDTO>> GetAllAsync()
            {
                await Task.Delay(100);
                return new ProdutoDTO[0];
            }

            public async Task<ProdutoListaDTO> GetProdutosAsync(FiltroProdutosDTO filtroProdutosDTO, bool includeFornecedor)
            {
                await Task.Delay(100);
                return new ProdutoListaDTO();
            }
        }

    }
}

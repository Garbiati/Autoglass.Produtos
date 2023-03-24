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
            var produtos = new List<ProdutoDTO>();
            var mock = new Mock<IProdutoService>();
            mock.Setup(servico => servico.GetProdutosAsync(filtro, true))
                .ReturnsAsync(produtos);
            var produtoService = mock.Object;

            // Act
            var resultado = await produtoService.GetProdutosAsync(filtro, true);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<List<ProdutoDTO>>(resultado);
            Assert.Equal(produtos, resultado);
        }


        [Fact]
        public async Task TestePostDataValidadeMenorQueFabricacao()
        {
            // Arrange
            var produtoCreateDTO = new ProdutoCreateDTO
            {
                Descricao = "Produto Teste",
                DataFabricacao = DateTime.Now.ToShortDateString(),
                DataValidade = DateTime.Now.AddDays(-1).ToShortDateString(),
                FornecedorId = 1
            };
            var mock = new Mock<IProdutoService>();
            var produtoService = mock.Object;
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(produtoService);
                });
            }).CreateClient();

            // Act & Assert
            var response = await client.PostAsJsonAsync("/api/produtos", produtoCreateDTO);
            Console.WriteLine(response.RequestMessage);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task TestePostDataValidadeMaiorQueFabricacao()
        {
            // Arrange
            var produtoCreateDTO = new ProdutoCreateDTO
            {
                Descricao = "Produto Teste",
                DataFabricacao = DateTime.Now.ToShortDateString(),
                DataValidade = DateTime.Now.AddDays(1).ToShortDateString(),
                FornecedorId = 1
            };
            var mock = new Mock<IProdutoService>();
            var produtoService = mock.Object;
            var controller = new ProdutosController(produtoService);

            // Act & Assert        
            
            Assert.IsType<ActionResult<ProdutoDTO>>(await controller.Post(produtoCreateDTO));
        }
        
        // [Fact]
        // public async Task TestePostDataValidadeMenorQueFabricacao2()
        // {
        //     // Arrange
        //     var produtoCreateDTO = new ProdutoCreateDTO
        //     {
        //         Descricao = "Produto Teste",
        //         DataFabricacao = DateTime.Now.ToShortDateString(),
        //         DataValidade = DateTime.Now.AddDays(-1).ToShortDateString(),
        //         FornecedorId = 1
        //     };
        //     var mock = new Mock<IProdutoService>();
        //     var produtoService = mock.Object;
        //     var controller = new ProdutosController(produtoService);

        //     // Act & Assert
        //     await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await controller.Post(produtoCreateDTO));
        // }


        private class ProdutoServiceMock : IProdutoService
        {
            public async Task<IEnumerable<ProdutoDTO>> GetProdutosAsync(FiltroProdutosDTO filtro, bool incluirImagens)
            {
                await Task.Delay(100);
                return new ProdutoDTO[0];
            }

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

            public async Task UpdateAsync(ProdutoUpdateDTO produtoUpdateDTO)
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
        }

    }
}

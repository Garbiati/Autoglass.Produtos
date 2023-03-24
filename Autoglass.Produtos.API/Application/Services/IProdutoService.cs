using System.Collections.Generic;
using System.Threading.Tasks;
using AutoglassAPI.Application.DTOs;
using AutoglassAPI.Domain.Entities;

namespace AutoglassAPI.Application.Services
{
    public interface IProdutoService
    {
        Task<ProdutoDTO> GetByIdAsync(int id);                
        Task<IEnumerable<ProdutoDTO>> GetAllAsync();
        Task<IEnumerable<ProdutoDTO>> GetProdutosAsync(FiltroProdutosDTO filtroProdutosDTO, bool includeFornecedor);
        Task<ProdutoDTO> AddAsync(ProdutoCreateDTO produtoCreateDTO);
        Task UpdateAsync(ProdutoUpdateDTO produtoUpdateDTO);
        Task DeleteAsync(int id);

    }
}

using AutoglassAPI.Domain.Entities;
using System.Linq.Expressions;

namespace AutoglassAPI.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();   
        Task<List<Produto>> GetProdutosQueryable(bool includeFornecedor , Expression<Func<Produto, bool>> predicate , Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy , int? skip , int? take );
        Task<Produto> GetByIdAsync(int id);
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
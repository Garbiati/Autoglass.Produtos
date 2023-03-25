using System.Linq.Expressions;
using AutoglassAPI.Domain.Entities;

namespace AutoglassAPI.Domain.Interfaces
{
    public interface IFornecedorRepository
    {

        Task AddAsync(Fornecedor fornecedor);
        Task<IEnumerable<Fornecedor>> GetAllAsync();
        Task<List<Fornecedor>> GetFornecedorQueryable( Expression<Func<Fornecedor, bool>> predicate , Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy , int? skip , int? take );
        Task<int> GetFornecedorQueryableCount( Expression<Func<Fornecedor, bool>> predicate);
        Task<Fornecedor> GetByIdAsync(int id);
        Task<Fornecedor> GetByCNPJAsync(string cnpj);

        Task UpdateAsync(Fornecedor fornecedor);
    }
}
using Microsoft.EntityFrameworkCore;
using AutoglassAPI.Infrastructure.Data;
using AutoglassAPI.Domain.Interfaces;
using AutoglassAPI.Domain.Entities;
using System.Linq.Expressions;


namespace AutoglassAPI.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.Include(p => p.Fornecedor).ToListAsync();
        }



        public  Task<List<Produto>> GetProdutosQueryable(bool includeFornecedor = true, Expression<Func<Produto, bool>> predicate = null, Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy = null, int? skip = null, int? take = null)
        {
            var query = _context.Produtos.AsQueryable();

            if (includeFornecedor)
            {
                query = query.Include(p => p.Fornecedor);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return query.ToListAsync();
        } 

        public  Task<int> GetProdutosQueryableCount(Expression<Func<Produto, bool>> predicate)
        {
            var query = _context.Produtos.AsQueryable();

            query = query.Where(predicate);

           return query.CountAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Set<Produto>().AddAsync(produto);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                produto.Status = false;
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
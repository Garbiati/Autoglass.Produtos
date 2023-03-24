using Microsoft.EntityFrameworkCore;
using AutoglassAPI.Infrastructure.Data;
using AutoglassAPI.Domain.Interfaces;
using AutoglassAPI.Domain.Entities;
using System.Linq.Expressions;

namespace AutoglassAPI.Infrastructure.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AppDbContext _context;

        public FornecedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Fornecedor fornecedor)
        {
            await _context.Set<Fornecedor>().AddAsync(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor> GetByIdAsync(int id)
        {
            return await _context.Fornecedores.FindAsync(id);
        }

        public async Task<Fornecedor> GetByCNPJAsync(string cnpj)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync( x=> x.CNPJ == cnpj );
        }

       public  Task<List<Fornecedor>> GetFornecedorQueryable( Expression<Func<Fornecedor, bool>> predicate = null, Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = null, int? skip = null, int? take = null)
        {
            var query = _context.Fornecedores.AsQueryable();   

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


        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
            await _context.SaveChangesAsync();
        }

    }
}
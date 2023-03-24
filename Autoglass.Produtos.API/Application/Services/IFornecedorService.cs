using System.Collections.Generic;
using System.Threading.Tasks;
using AutoglassAPI.Application.DTOs;

namespace AutoglassAPI.Application.Services
{
    public interface IFornecedorService
    {
        Task<FornecedorDTO> GetByIdAsync(int id);
        Task<IEnumerable<FornecedorDTO>> GetAllAsync();
        Task<IEnumerable<FornecedorDTO>> GetFornecedorAsync(FiltroFornecedorDTO filtroFornecedorDTO);
        Task<FornecedorDTO> AddAsync(FornecedorCreateDTO fornecedorCreateDTO);
        Task UpdateByIdAsync(int id, FornecedorUpdateDTO fornecedorUpdateDTO);
    }
}

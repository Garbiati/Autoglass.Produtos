using AutoMapper;
using AutoglassAPI.Domain.Interfaces;
using AutoglassAPI.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoglassAPI.Domain.Entities;
using System.Linq.Expressions;

namespace AutoglassAPI.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<FornecedorDTO> GetByIdAsync(int id)
        {
            var fornecedor = await _fornecedorRepository.GetByIdAsync(id);
            return _mapper.Map<FornecedorDTO>(fornecedor);
        }

        public async Task<IEnumerable<FornecedorDTO>> GetAllAsync()
        {
            var fornecedores = await _fornecedorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);
        }

        public async Task<FornecedorListaDTO> GetFornecedorAsync(FiltroFornecedorDTO filtroFornecedorDTO)
        {            
            if( !filtroFornecedorDTO.Pagina.HasValue)
                filtroFornecedorDTO.Pagina = 1;

            if( !filtroFornecedorDTO.TamanhoPagina.HasValue)
                filtroFornecedorDTO.TamanhoPagina = 10;

            
            Expression<Func<Fornecedor, bool>> predicate = p => 
            (
               (!filtroFornecedorDTO.Codigo.HasValue || p.Id == filtroFornecedorDTO.Codigo)
            && (String.IsNullOrEmpty(filtroFornecedorDTO.Descricao) || p.Descricao.Contains( filtroFornecedorDTO.Descricao))
            && (String.IsNullOrEmpty(filtroFornecedorDTO.CNPJ) || p.CNPJ == filtroFornecedorDTO.CNPJ)
            );            

            //caso queira a lista de forma ordenada, já deixei pronto
            Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = x => x.OrderBy(p => p.Id );
            int skip = (filtroFornecedorDTO.Pagina.Value -1) * filtroFornecedorDTO.TamanhoPagina.Value;
            int take = filtroFornecedorDTO.TamanhoPagina.Value;


            int total = await _fornecedorRepository.GetFornecedorQueryableCount(predicate);    
            var fornecedores = await _fornecedorRepository.GetFornecedorQueryable(predicate, orderBy, skip, take);

            var fornecedoresDTO = _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores);


            return new FornecedorListaDTO()
            {
                Total = total,
                Exibindo = fornecedoresDTO.Count(),
                Pagina = filtroFornecedorDTO.Pagina.Value,
                TamanhoPagina = filtroFornecedorDTO.TamanhoPagina.Value,
                Fornecedores = fornecedoresDTO
            };

        }

        public async Task<FornecedorDTO> AddAsync(FornecedorCreateDTO FornecedorCreateDTO)
        {
            Fornecedor fornecedor = _mapper.Map<Fornecedor>(FornecedorCreateDTO);
            await _fornecedorRepository.AddAsync(fornecedor);
            return _mapper.Map<FornecedorDTO>(fornecedor);
        }

       public async Task UpdateByIdAsync( int id,FornecedorUpdateDTO fornecedorUpdateDTO)
        { 
            var fornecedor = await _fornecedorRepository.GetByIdAsync(id);

            if(fornecedor == null)            
                throw new Exception("Fornecedor não encontrado");            

             _mapper.Map(fornecedorUpdateDTO, fornecedor);
            await _fornecedorRepository.UpdateAsync(fornecedor);          
        }
    }
}

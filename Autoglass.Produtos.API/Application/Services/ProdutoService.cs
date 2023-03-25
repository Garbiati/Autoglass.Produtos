using AutoMapper;
using FluentValidation;
using AutoglassAPI.Domain.Entities;
using AutoglassAPI.Domain.Interfaces;
using AutoglassAPI.Domain.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoglassAPI.Application.DTOs;
using System.Linq.Expressions;

namespace AutoglassAPI.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;    
        private readonly IFornecedorRepository _fornecedorRepository;   
        private readonly IMapper _mapper;
        private readonly ProdutoValidation _produtoValidation;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, IFornecedorRepository fornecedor)
        {
            _produtoRepository = produtoRepository; 
            _fornecedorRepository = fornecedor;         
            _mapper = mapper;
            _produtoValidation = new ProdutoValidation();
        }  
 
        public async Task<ProdutoDTO> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        }

        public async Task<ProdutoListaDTO> GetProdutosAsync(FiltroProdutosDTO filtroProdutosDTO, bool includeFornecedor)
        {    

            if( String.IsNullOrEmpty(filtroProdutosDTO.Situacao))
                filtroProdutosDTO.Situacao = "Ativo";
            
            if( !filtroProdutosDTO.Pagina.HasValue)
                filtroProdutosDTO.Pagina = 1;

            if( !filtroProdutosDTO.TamanhoPagina.HasValue)
                filtroProdutosDTO.TamanhoPagina = 10;
            
            Expression<Func<Produto, bool>> predicate = p => 
            (
               (!filtroProdutosDTO.Codigo.HasValue || p.Id == filtroProdutosDTO.Codigo)
            && (String.IsNullOrEmpty(filtroProdutosDTO.Descricao) || p.Descricao.Contains(filtroProdutosDTO.Descricao))
            && (String.IsNullOrEmpty(filtroProdutosDTO.Situacao) || p.Status == (filtroProdutosDTO.Situacao == "Ativo"? true: false))
            && (!filtroProdutosDTO.DataFabricacao.HasValue || p.DataFabricacao == filtroProdutosDTO.DataFabricacao)
            && (!filtroProdutosDTO.DataValidade.HasValue || p.DataValidade == filtroProdutosDTO.DataValidade)
            && (!filtroProdutosDTO.CodigoFornecedor.HasValue || p.FornecedorId == filtroProdutosDTO.CodigoFornecedor)
            && (String.IsNullOrEmpty(filtroProdutosDTO.DescricaoFornecedor) || p.Fornecedor.Descricao.Contains( filtroProdutosDTO.DescricaoFornecedor))
            && (String.IsNullOrEmpty(filtroProdutosDTO.CNPJFornecedor) || p.Fornecedor.CNPJ == filtroProdutosDTO.CNPJFornecedor)
            );            

            //caso queira a lista de forma ordenada, já deixei pronto
            Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy = x => x.OrderBy(p => p.Id );
            int skip = (filtroProdutosDTO.Pagina.Value -1) * filtroProdutosDTO.TamanhoPagina.Value;
            int take = filtroProdutosDTO.TamanhoPagina.Value;


            
            int total = await _produtoRepository.GetProdutosQueryableCount( predicate);
            var produtos = await _produtoRepository.GetProdutosQueryable(includeFornecedor, predicate, orderBy, skip, take);
            
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return new ProdutoListaDTO()
            {
                Total = total,
                Exibindo = produtosDTO.Count(),
                Pagina = filtroProdutosDTO.Pagina.Value,
                TamanhoPagina = filtroProdutosDTO.TamanhoPagina.Value,
                Produtos = produtosDTO
            };
           

        }


        public async Task<ProdutoDTO> AddAsync(ProdutoCreateDTO produtoCreateDTO)
        {
            var produto = _mapper.Map<Produto>(produtoCreateDTO);    
            _produtoValidation.ValidateAndThrow(produto);    

            produto.Fornecedor = await _fornecedorRepository.GetByIdAsync(produtoCreateDTO.FornecedorId);

            if(produto.Fornecedor == null)            
                throw new Exception($"Nenhum fornecedor com o Id {produtoCreateDTO.FornecedorId} encontrado na base de dados." );
            
            await _produtoRepository.AddAsync(produto);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task UpdateByIdAsync(int id, ProdutoUpdateDTO produtoUpdateDTO)
        {      

            var produto = await _produtoRepository.GetByIdAsync(id);

            if(produto == null)
                throw new ArgumentException("Produto não encontrado"); 

            if(produtoUpdateDTO.FornecedorId.HasValue)
                produto.Fornecedor = await _fornecedorRepository.GetByIdAsync(produtoUpdateDTO.FornecedorId.Value);

            if(produto.Fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado");

            _mapper.Map(produtoUpdateDTO, produto);
            _produtoValidation.ValidateAndThrow(produto);             
            
            if(produto.Fornecedor == null)            
                throw new Exception($"Nenhum fornecedor com o Id {produtoUpdateDTO.FornecedorId} encontrado na base de dados." );

            await _produtoRepository.UpdateAsync(produto);
        }

        public async Task DeleteAsync(int id)
        {
            await _produtoRepository.DeleteAsync(id);
        } 
    }
}

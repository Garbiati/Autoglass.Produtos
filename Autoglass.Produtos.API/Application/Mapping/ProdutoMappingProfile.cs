using AutoMapper;
using AutoglassAPI.Application.DTOs;
using AutoglassAPI.Domain.Entities;

namespace AutoglassAPI.Application.Mapping
{
    public class ProdutoMappingProfile : Profile
    {
        public ProdutoMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>()
                .ForPath(dto => dto.Codigo, opt => opt.MapFrom(src => src.Id))
                .ForPath(dto => dto.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForPath(dto => dto.Situacao, opt => opt.MapFrom(src => src.Status ? "Ativo" : "Inativo" ))
                .ForPath(dto => dto.DataFabricacao, opt => opt.MapFrom(src => src.DataFabricacao))
                .ForPath(dto => dto.DataValidade, opt => opt.MapFrom(src => src.DataValidade))
                .ForPath(dto => dto.CodigoFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Id))
                .ForPath(dto => dto.DescricaoFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Descricao))
                .ForPath(dto => dto.CNPJFornecedor, opt => opt.MapFrom(src => src.Fornecedor.CNPJ));

            CreateMap<ProdutoDTO, Produto>()
                .ForPath(entity => entity.Id, opt => opt.MapFrom(src => src.Codigo))
                .ForPath(entity => entity.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForPath(entity => entity.Status, opt => opt.MapFrom(src => src.Situacao == "Ativo" ? true :false ))
                .ForPath(entity => entity.DataFabricacao, opt => opt.MapFrom(src => src.DataFabricacao))
                .ForPath(entity => entity.DataValidade, opt => opt.MapFrom(src => src.DataValidade))
                .ForPath(entity => entity.Fornecedor.Id, opt => opt.MapFrom(src => src.CodigoFornecedor))
                .ForPath(entity => entity.Fornecedor.CNPJ, opt => opt.MapFrom(src => src.CNPJFornecedor))
                .ForPath(entity => entity.Fornecedor.Descricao, opt => opt.MapFrom(src => src.DescricaoFornecedor));
 
            
            CreateMap<ProdutoCreateDTO, Produto>()
                .ForPath(dest => dest.Id, opt => opt.Ignore())    
                .ForPath(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao)) 
                .ForPath(dest => dest.DataFabricacao, opt => opt.MapFrom(src => DateTime.Parse(src.DataFabricacao)))
                .ForPath(dest => dest.DataValidade, opt => opt.MapFrom(src => DateTime.Parse(src.DataValidade)))
                .ForPath(dest => dest.FornecedorId, opt => opt.MapFrom(src => src.FornecedorId))
                .ForPath(dest => dest.Status, opt =>  opt.MapFrom(src => true));
                

            CreateMap<ProdutoUpdateDTO, Produto>()
                .ForPath(dest => dest.Id, opt => opt.Ignore())                
                .ForPath(dest => dest.Status, opt =>  opt.Ignore()) 
                .ForMember(dest => dest.Descricao, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.Descricao));
                    opt.MapFrom(src => src.Descricao);
                    opt.UseDestinationValue(); 
                })            
                .ForMember(dest => dest.DataFabricacao, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.DataFabricacao));
                    opt.MapFrom(src => DateTime.Parse(src.DataFabricacao));
                    opt.UseDestinationValue(); 
                })
                .ForMember(dest => dest.DataValidade, opt => 
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.DataValidade));
                    opt.MapFrom(src => DateTime.Parse(src.DataValidade));
                    opt.UseDestinationValue();   
                })
                .ForMember(dest => dest.FornecedorId, opt => 
                {
                    opt.Condition(src => !src.FornecedorId.HasValue);
                    opt.MapFrom(src => src.FornecedorId);
                    opt.UseDestinationValue();   
                });
                
 
            }
    }
}
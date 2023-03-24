using AutoMapper;
using AutoglassAPI.Application.DTOs;
using AutoglassAPI.Domain.Entities;

namespace AutoglassAPI.Application.Mapping
{
    public class FornecedorMappingProfile : Profile
    {
        public FornecedorMappingProfile()
        {
            CreateMap<Fornecedor, FornecedorDTO>()
                .ForPath(dto => dto.Codigo, opt => opt.MapFrom(src => src.Id))
                .ForPath(dto => dto.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForPath(dto => dto.CNPJ, opt => opt.MapFrom(src => src.CNPJ));

            CreateMap<FornecedorDTO, Fornecedor>()
                .ForPath(entity => entity.Id, opt => opt.MapFrom(src => src.Codigo))
                .ForPath(entity => entity.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForPath(entity => entity.CNPJ, opt => opt.MapFrom(src => src.CNPJ));

            CreateMap<FornecedorCreateDTO, Fornecedor>()
                .ForPath(entity => entity.Id, opt => opt.Ignore())
                .ForPath(entity => entity.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForPath(entity => entity.CNPJ, opt => opt.MapFrom(src => src.CNPJ));

            CreateMap<FornecedorUpdateDTO, Fornecedor>()
                .ForPath(entity => entity.Id, opt => opt.Ignore())
                .ForMember(entity => entity.Descricao, opt => 
                {  
                    opt.Condition(src => !string.IsNullOrEmpty(src.Descricao));
                    opt.MapFrom(src => src.Descricao);
                    opt.UseDestinationValue();
                })
                .ForMember(entity => entity.CNPJ, opt =>                 
                {  
                    opt.Condition(src => !string.IsNullOrEmpty(src.CNPJ));
                    opt.MapFrom(src => src.CNPJ);
                    opt.UseDestinationValue();
                });
        }
    }
}
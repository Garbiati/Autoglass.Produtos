using FluentValidation;
using AutoglassAPI.Domain;
using AutoglassAPI.Domain.Entities;

namespace AutoglassAPI.Domain.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.Descricao).NotEmpty().WithMessage("A descrição do produto não pode ser nula.");
            RuleFor(p => p.Descricao).MinimumLength(5).WithMessage("A descrição deve ter no mínimo 5 caracteres.");
            RuleFor(p => p.DataFabricacao)
                .LessThan(p => p.DataValidade)
                .WithMessage("A data de fabricação não pode ser maior ou igual à data de validade.");
            
        }
    }
}

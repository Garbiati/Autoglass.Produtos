using AutoglassAPI.Domain.Entities;
using FluentValidation;

namespace AutoglassAPI.Domain.Validation
{

    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(p => p.Descricao).NotEmpty().WithMessage("A descrição do fornecedor não pode ser nula.");
            RuleFor(p => p.Descricao).MinimumLength(5).WithMessage("A descrição deve ter no mínimo 5 caracteres.");
            RuleFor(p => p.CNPJ).NotEmpty().WithMessage("O CNPJ do fornecedor não pode ser nula.");
            RuleFor(p => p.CNPJ).MinimumLength(18).WithMessage("O CNPJ deve ter no mínimo 5 caracteres.");

        }
    }
}
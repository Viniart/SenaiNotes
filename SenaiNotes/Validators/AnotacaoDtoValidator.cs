using FluentValidation;
using SenaiNotes.DTO;
using SenaiNotes.Models;

namespace SenaiNotes.Validators
{
    public class AnotacaoDtoValidator : AbstractValidator<CadastroAnotacaoDto>
    {
        public AnotacaoDtoValidator()
        {
            RuleFor(a => a.TituloAnotacao)
           .NotEmpty().WithMessage("O título da anotação é obrigatório.")
           .MaximumLength(100).WithMessage("O título da anotação deve ter no máximo 100 caracteres.");

            RuleFor(a => a.DescricaoAnotacao)
                .NotEmpty().WithMessage("A descrição da anotação é obrigatória.");

            RuleFor(a => a.DataCriacao)
                .NotEmpty().WithMessage("A data de criação é obrigatória.");

            RuleFor(a => a.ImagemAnotacao)
                .MaximumLength(255).WithMessage("O caminho da imagem deve ter no máximo 255 caracteres.");

            RuleFor(a => a.IdUsuario)
                .NotNull().WithMessage("O usuário da anotação é obrigatório.");
        }
    }
}

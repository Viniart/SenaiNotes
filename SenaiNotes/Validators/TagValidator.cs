using FluentValidation;
using SenaiNotes.Models;

namespace SenaiNotes.Validators
{
    public class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(t => t.NomeTag)
            .NotEmpty().WithMessage("O nome da tag é obrigatório.")
            .MaximumLength(50).WithMessage("O nome da tag deve ter no máximo 50 caracteres.");

            RuleFor(t => t.IdUsuario)
                .NotNull().WithMessage("O usuário da tag é obrigatório.");
        }
    }
}

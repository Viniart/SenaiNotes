using FluentValidation;
using SenaiNotes.Models;

namespace SenaiNotes.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.NomeUsuario)
           .NotEmpty().WithMessage("O nome do usuário é obrigatório.")
           .MaximumLength(100).WithMessage("O nome do usuário deve ter no máximo 100 caracteres.");

            RuleFor(u => u.EmailUsuario)
                .NotEmpty().WithMessage("O e-mail do usuário é obrigatório.")
                .EmailAddress().WithMessage("O e-mail do usuário deve ser válido.");

            RuleFor(u => u.SenhaUsuario)
                .NotEmpty().WithMessage("A senha do usuário é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }
    }
}

using Microsoft.AspNetCore.Identity;
using SenaiNotes.Models;

namespace SenaiNotes.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public string HashPassword(Usuario usuario)
        {
            return _passwordHasher.HashPassword(usuario, usuario.SenhaUsuario);
        }

        public bool VerificarSenha(Usuario usuario, string senhaInformada)
        {
            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaUsuario, senhaInformada);

            return resultado == PasswordVerificationResult.Success;
        }
    }
}

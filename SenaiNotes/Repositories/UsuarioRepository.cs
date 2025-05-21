using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Services;

namespace SenaiNotes.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SenaiNotesDatabaseContext context) : base(context)
        {
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            var passwordService = new PasswordService();

            usuario.SenhaUsuario = passwordService.HashPassword(usuario);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? BuscarPorEmailSenha(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.EmailUsuario == email);

            if (usuario == null)
            {
                return null;
            }

            var passwordService = new PasswordService();

            var resultado = passwordService.VerificarSenha(usuario, senha);

            if (resultado == true) return usuario;

            return null;
        }
    }
}

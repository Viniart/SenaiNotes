using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Services;

namespace SenaiNotes.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly SenaiNotesDatabaseContext _context;
        public UsuarioRepository(SenaiNotesDatabaseContext context)
        {
            _context = context;
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

        public Usuario? ObterPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public List<Usuario> ListarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public void Cadastrar(Usuario? entidade)
        {
            _context.Usuarios.Add(entidade);
            _context.SaveChanges();
        }

        public Usuario? Atualizar(int id, Usuario entidade)
        {
            var existente = _context.Usuarios.Find(id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(entidade);
            _context.SaveChanges();

            return existente;

        }

        public Usuario? Deletar(int id)
        {
            var existente = _context.Usuarios.Find(id);
            if (existente == null) return null;

            _context.Usuarios.Remove(existente);
            _context.SaveChanges();

            return existente;
        }
    }
}

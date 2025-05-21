using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        void CadastrarUsuario(Usuario usuario);
        Usuario? BuscarPorEmailSenha(string email, string password);
    }
}

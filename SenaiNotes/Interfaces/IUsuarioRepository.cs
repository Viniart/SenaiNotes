using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface IUsuarioRepository
    {
        void CadastrarUsuario(Usuario usuario);
        Usuario? BuscarPorEmailSenha(string email, string password);
        Usuario? ObterPorId(int id);
        List<Usuario> ListarTodos();
        void Cadastrar(Usuario? entidade);
        Usuario? Atualizar(int id, Usuario entidade);
        Usuario? Deletar(int id);
    }
}

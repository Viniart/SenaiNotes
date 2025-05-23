using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        List<Tag> BuscarPorUsuario(int id);
        Tag BuscarPorUsuarioeId(int id, string nome);
        Tag? ObterPorId(int id);
        List<Tag> ListarTodos();
        void Cadastrar(Tag? entidade);
        Tag? Atualizar(int id, Tag entidade);
        Tag? Deletar(int id);
    }
}

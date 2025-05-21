using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        List<Tag> BuscarPorUsuario(int id);
        Tag BuscarPorUsuarioeId(int id, string nome);
    }
}

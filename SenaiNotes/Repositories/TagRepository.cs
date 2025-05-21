using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;

namespace SenaiNotes.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        
        public TagRepository(SenaiNotesDatabaseContext context) : base(context)
        {

        }

        public List<Tag> BuscarPorUsuario(int id)
        {
            var tags = _context.Tags.Where(t => t.IdUsuario == id).ToList();

            return tags;
        }

        public Tag BuscarPorUsuarioeId(int id, string nome)
        {
            var tags = _context.Tags.FirstOrDefault(t => t.IdUsuario == id && t.NomeTag == nome);

            return tags;
        }
    }
}

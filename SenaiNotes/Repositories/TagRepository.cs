using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;

namespace SenaiNotes.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesDatabaseContext _context;
        public TagRepository(SenaiNotesDatabaseContext context)
        {
            _context = context;
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
        public Tag? ObterPorId(int id)
        {
            return _context.Tags.Find(id);
        }

        public List<Tag> ListarTodos()
        {
            return _context.Tags.ToList();
        }

        public void Cadastrar(Tag? entidade)
        {
            _context.Tags.Add(entidade);
            _context.SaveChanges();
        }

        public Tag? Atualizar(int id, Tag entidade)
        {
            var existente = _context.Tags.Find(id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(entidade);
            _context.SaveChanges();

            return existente;

        }

        public Tag? Deletar(int id)
        {
            var existente = _context.Tags.Find(id);
            if (existente == null) return null;

            _context.Tags.Remove(existente);
            _context.SaveChanges();

            return existente;
        }
    }
}

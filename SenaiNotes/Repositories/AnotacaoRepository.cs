using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.DTO;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModels;

namespace SenaiNotes.Repositories
{
    public class AnotacaoRepository : IAnotacaoRepository
    {
        private ITagRepository _tagRepository;
        private readonly SenaiNotesDatabaseContext _context;
        public AnotacaoRepository(SenaiNotesDatabaseContext context, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            _context = context;
        }

        public Anotacao? ArquivarAnotacao(int id)
        {
            var anotacao = _context.Anotacaos.Find(id);

            if (anotacao == null) return null;

            anotacao.AnotacaoArquivada = !anotacao.AnotacaoArquivada;

            _context.SaveChanges();

            return anotacao;
        }

        public List<Anotacao> BuscarPorUsuario(int id)
        {
            var anotacoes = _context.Anotacaos.Where(a => a.IdUsuario == id).ToList();

            return anotacoes;
        }

        public CadastroAnotacaoDto? CadastrarAnotacao(CadastroAnotacaoDto anotacao)
        {
            List<int> idTags = new List<int>();

            foreach (var item in anotacao.Tags)
            {
                var tag = _tagRepository.BuscarPorUsuarioeId(anotacao.IdUsuario, item);

                if (tag == null)
                {
                    
                    tag = new Tag
                    {
                        NomeTag = item,
                        IdUsuario = anotacao.IdUsuario
                    };

                    _context.Add(tag);
                    _context.SaveChanges();
                }

                idTags.Add(tag.IdTag);
            }

            var novaAnotacao = new Anotacao
            {
                TituloAnotacao = anotacao.TituloAnotacao,
                DescricaoAnotacao = anotacao.DescricaoAnotacao,
                ImagemAnotacao = anotacao.ImagemAnotacao,
                AnotacaoArquivada = false,
                DataCriacao = DateTime.Now,
                DataEdicao = DateTime.Now,
                IdUsuario = anotacao.IdUsuario
            };

            _context.Add(novaAnotacao);
            _context.SaveChanges();

            foreach (var item in idTags)
            {
                var tagAnotacao = new TagAnotacao
                {
                    IdAnotacao = novaAnotacao.IdAnotacao,
                    IdTag = item
                };

                _context.Add(tagAnotacao);
                _context.SaveChanges();
            }

            return anotacao;
        }

        public List<ListarAnotacaoViewModel> ListarTodosInclude()
        {
            var anotacoes = _context.Anotacaos.Include(a => a.TagAnotacaos).ThenInclude(t => t.IdTagNavigation)
                .Select(a => new ListarAnotacaoViewModel
                {
                    IdAnotacao = a.IdAnotacao,
                    TituloAnotacao = a.TituloAnotacao,
                    DescricaoAnotacao = a.DescricaoAnotacao,
                    DataCriacao = a.DataCriacao,
                    DataEdicao = (DateTime)(a.DataEdicao),
                    ImagemAnotacao = a.ImagemAnotacao,
                    AnotacaoArquivada = a.AnotacaoArquivada,
                    Tags = a.TagAnotacaos.Select(t => new ListarTagViewModel
                    {
                        IdTag = t.IdTagNavigation.IdTag,
                        NomeTag = t.IdTagNavigation.NomeTag
                    }).ToList()
                })
                .ToList();

            return anotacoes;
        }

        public Anotacao? ObterPorId(int id)
        {
            return _context.Anotacaos.Find(id);
        }

        public List<Anotacao> ListarTodos()
        {
            return _context.Anotacaos.ToList();
        }

        public void Cadastrar(Anotacao? entidade)
        {
            _context.Anotacaos.Add(entidade);
            _context.SaveChanges();
        }

        public Anotacao? Atualizar(int id, Anotacao entidade)
        {
            var existente = _context.Anotacaos.Find(id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(entidade);
            _context.SaveChanges();

            return existente;

        }

        public Anotacao? Deletar(int id)
        {
            var existente = _context.Anotacaos.Find(id);
            if (existente == null) return null;

            _context.Anotacaos.Remove(existente);
            _context.SaveChanges();

            return existente;
        }
    }
}

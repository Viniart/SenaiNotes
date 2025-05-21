using SenaiNotes.DTO;
using SenaiNotes.Models;
using SenaiNotes.ViewModels;

namespace SenaiNotes.Interfaces
{
    public interface IAnotacaoRepository : IGenericRepository<Anotacao>
    {
        List<ListarAnotacaoViewModel> ListarTodosInclude();
        Anotacao? ArquivarAnotacao(int id);
        CadastroAnotacaoDto? CadastrarAnotacao(CadastroAnotacaoDto anotacao);
        List<Anotacao> BuscarPorUsuario(int id);
    }
}

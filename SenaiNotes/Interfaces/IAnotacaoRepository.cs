using SenaiNotes.DTO;
using SenaiNotes.Models;
using SenaiNotes.ViewModels;

namespace SenaiNotes.Interfaces
{
    public interface IAnotacaoRepository
    {
        List<ListarAnotacaoViewModel> ListarTodosInclude();
        Anotacao? ArquivarAnotacao(int id);
        CadastroAnotacaoDto? CadastrarAnotacao(CadastroAnotacaoDto anotacao);
        List<Anotacao> BuscarPorUsuario(int id);
        Anotacao? ObterPorId(int id);
        List<Anotacao> ListarTodos();
        void Cadastrar(Anotacao? entidade);
        Anotacao? Atualizar(int id, Anotacao entidade);
        Anotacao? Deletar(int id);
    }
}

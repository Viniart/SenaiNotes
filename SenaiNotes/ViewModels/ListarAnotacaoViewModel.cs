namespace SenaiNotes.ViewModels
{
    public class ListarAnotacaoViewModel
    {
        public int IdAnotacao { get; set; }
        public string TituloAnotacao { get; set; }
        public string DescricaoAnotacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEdicao { get; set; }
        public string ImagemAnotacao { get; set; }
        public bool AnotacaoArquivada { get; set; }
        public List<ListarTagViewModel> Tags { get; set; }
    }
}

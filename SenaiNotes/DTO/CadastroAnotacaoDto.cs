namespace SenaiNotes.DTO
{
    public class CadastroAnotacaoDto
    {

        public string? TituloAnotacao { get; set; } = null!;

        public string? DescricaoAnotacao { get; set; } = null!;

        public DateTime? DataCriacao { get; set; }

        public DateTime? DataEdicao { get; set; }

        public string? ImagemAnotacao { get; set; } = null!;

        public bool? AnotacaoArquivada { get; set; }

        public int IdUsuario { get; set; }

        public List<string> Tags { get; set; }
    }
}

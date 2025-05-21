using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Anotacao
{
    public int IdAnotacao { get; set; }

    public string TituloAnotacao { get; set; } = null!;

    public string? DescricaoAnotacao { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataEdicao { get; set; }

    public string ImagemAnotacao { get; set; } = null!;

    public bool AnotacaoArquivada { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<TagAnotacao> TagAnotacaos { get; set; } = new List<TagAnotacao>();
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SenaiNotes.Models;

public partial class Tag
{
    public int IdTag { get; set; }

    public string? NomeTag { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<TagAnotacao> TagAnotacaos { get; set; } = new List<TagAnotacao>();
}

using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NomeUsuario { get; set; } = null!;

    public string EmailUsuario { get; set; } = null!;

    public string SenhaUsuario { get; set; } = null!;

    public virtual ICollection<Anotacao> Anotacaos { get; set; } = new List<Anotacao>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}

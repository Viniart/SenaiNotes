using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SenaiNotes.Models;

public partial class TagAnotacao
{
    public int IdTagAnotacao { get; set; }

    public int? IdAnotacao { get; set; }

    public int? IdTag { get; set; }

    public virtual Anotacao? IdAnotacaoNavigation { get; set; }

    public virtual Tag? IdTagNavigation { get; set; }
}

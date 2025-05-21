using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class AuditoriaGeral
{
    public int Id { get; set; }

    public string? NomeTabela { get; set; }

    public string? TipoAcao { get; set; }

    public string? Usuario { get; set; }

    public DateTime? DataAcao { get; set; }

    public string? DadosAntigos { get; set; }

    public string? DadosNovos { get; set; }
}

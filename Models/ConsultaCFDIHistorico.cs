namespace CFDIEstatusMXBSv.Models;

public class ConsultaCFDIHistorico
{
    public string Id { get; set; } = string.Empty;
    public string Emisor { get; set; } = string.Empty;
    public string Receptor { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string FE { get; set; } = string.Empty;
    public string? CodigoEstatus { get; set; }
    public string? Estatus { get; set; }
    public string? EsCancelable { get; set; }
    public string? EstatusCancelacion { get; set; }
    public DateTime FechaConsulta { get; set; }
}

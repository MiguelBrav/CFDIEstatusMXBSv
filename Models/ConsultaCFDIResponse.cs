namespace CFDIEstatusMXBSv.Models;

public class ConsultaCFDIResponse
{
    public string CodigoEstatus { get; set; } = string.Empty;
    public string Estatus { get; set; } = string.Empty;
    public string EsCancelable { get; set; } = string.Empty;
    public string? EstatusCancelacion { get; set; }
}

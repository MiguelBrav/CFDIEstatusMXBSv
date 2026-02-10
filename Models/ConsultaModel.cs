using System.ComponentModel.DataAnnotations;

namespace CFDIEstatusMXBSv.Models;

public class ConsultaModel
{
    [Required(ErrorMessage = "El RFC del emisor es obligatorio")]
    public string Emisor { get; set; } = string.Empty;

    [Required(ErrorMessage = "El RFC del receptor es obligatorio")]
    public string Receptor { get; set; } = string.Empty;

    [Required(ErrorMessage = "El total es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
    public decimal Total { get; set; } = 0m;

    [Required(ErrorMessage = "El sello FE es obligatorio")]
    public string FE { get; set; } = string.Empty;

    [Required(ErrorMessage = "El Uuid es obligatorio")]
    public string Id { get; set; } = string.Empty;
}

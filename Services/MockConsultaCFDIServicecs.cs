using CFDIEstatusMXBSv.Interfaces;
using CFDIEstatusMXBSv.Models;
using CFDIEstatusMXBSv.Utils;

namespace CFDIEstatusMXBSv.Services;

public class MockConsultaCFDIService : IConsultaCFDIService
{
    private static readonly Random _random = new();

    public  async Task<ConsultaCFDIResponse> ConsultarAsync(ConsultaModel expresion)
    {
        await Task.Delay(500);
        if (!ValidationUtils.IsValidRfc(expresion.Emisor) || !ValidationUtils.IsValidRfc(expresion.Receptor) ||
            expresion.Total <= 0 || !ValidationUtils.IsValidUuidOrFolio(expresion.Id) || !ValidationUtils.IsValidFE(expresion.FE))
        {
            return await Task.FromResult(new ConsultaCFDIResponse
            {
                CodigoEstatus = "N – 601: La expresión impresa proporcionada no es válida.",
                Estatus = string.Empty,
                EsCancelable = string.Empty,
                EstatusCancelacion = null
            });
        }

        var vigente = _random.Next(0, 2) == 0;

        if (vigente)
        {
            return await Task.FromResult(new ConsultaCFDIResponse
            {
                CodigoEstatus = "S – Comprobante obtenido satisfactoriamente.",
                Estatus = "Vigente",
                EsCancelable = Pick(
                    "Cancelable con aceptación",
                    "Cancelable sin aceptación",
                    "No cancelable"
                ),
                EstatusCancelacion = null
            });
        }

        var esCancelableCancelado = Pick(
            "Cancelable con aceptación",
            "Cancelable sin aceptación",
            "No cancelable"
        );

        var estatusCancelacion = esCancelableCancelado switch
        {
            "Cancelable con aceptación" => "Cancelado con aceptación",
            "Cancelable sin aceptación" => "Cancelado sin aceptación",
            "No cancelable" => "Solicitud rechazada",
            _ => "Solicitud rechazada"
        };

        return await Task.FromResult(new ConsultaCFDIResponse
        {
            CodigoEstatus = "S – Comprobante obtenido satisfactoriamente.",
            Estatus = "Cancelado",
            EsCancelable = esCancelableCancelado,
            EstatusCancelacion = estatusCancelacion
        });
    }

    private static string Pick(params string[] values)
        => values[_random.Next(values.Length)];
}


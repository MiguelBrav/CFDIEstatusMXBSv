using CFDIEstatusMXBSv.Models;

namespace CFDIEstatusMXBSv.Interfaces;

public interface IConsultaCFDIService
{
    Task<ConsultaCFDIResponse> ConsultarAsync(ConsultaModel expresion);
}

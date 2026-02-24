using CFDIEstatusMXBSv.Models;

namespace CFDIEstatusMXBSv.Interfaces;

public interface IXmlImportaService
{
    /// Parsea un XML de CFDI y devuelve el modelo de consulta prellenado y una lista de errores si los hay.
    Task<(ConsultaModel? Model, List<string> Errors)> ParseXmlAsync(System.IO.Stream xmlStream);
}

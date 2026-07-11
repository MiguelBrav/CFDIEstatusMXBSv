using CFDIEstatusMXBSv.Documents;
using CFDIEstatusMXBSv.Models;
using QuestPDF.Fluent;

namespace CFDIEstatusMXBSv.Services;

public class PdfService
{
    public byte[] GenerarConsultaPdf(ConsultaModel model, ConsultaCFDIResponse? response = null)
    {
        var document = new ConsultaPdfDocument(model, response);

        return document.GeneratePdf();
    }
}

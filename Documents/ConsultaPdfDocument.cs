using System.Globalization;
using CFDIEstatusMXBSv.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CFDIEstatusMXBSv.Documents;

public class ConsultaPdfDocument : IDocument
{
    private readonly ConsultaModel model;
    private readonly ConsultaCFDIResponse? response;
    private static readonly CultureInfo MexicoCulture = CultureInfo.GetCultureInfo("es-MX");

    public ConsultaPdfDocument(ConsultaModel model, ConsultaCFDIResponse? response = null)
    {
        this.model = model;
        this.response = response;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);
            page.DefaultTextStyle(text => text.FontSize(11));

            page.Header()
                .Text("Consulta SAT")
                .SemiBold()
                .FontSize(22)
                .FontColor(Colors.Blue.Darken2);

            page.Content()
                .PaddingVertical(24)
                .Column(column =>
                {
                    column.Spacing(12);

                    AddField(column, "Emisor", model.Emisor);
                    AddField(column, "Receptor", model.Receptor);
                    AddField(column, "Total", model.Total.ToString("C", MexicoCulture));
                    AddField(column, "UUID", model.Id);

                    column.Item().PaddingTop(12).Text("FE (sello digital)").SemiBold();
                    column.Item()
                        .Border(1)
                        .BorderColor(Colors.Grey.Lighten2)
                        .Padding(10)
                        .Text(model.FE)
                        .FontSize(8);

                    if (TieneRespuestaConsulta())
                    {
                        column.Item().PaddingTop(16).Text("Resultado de la consulta").SemiBold().FontSize(14);
                        AddField(column, "Codigo", response!.CodigoEstatus);
                        AddField(column, "Estatus", response.Estatus);
                        AddField(column, "Es cancelable", response.EsCancelable);
                        AddField(column, "Estado cancelacion", response.EstatusCancelacion ?? "Sin proceso");
                    }
                });

            page.Footer()
                .AlignRight()
                .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                .FontSize(9)
                .FontColor(Colors.Grey.Darken1);
        });
    }

    private bool TieneRespuestaConsulta()
    {
        return response is not null
            && (!string.IsNullOrWhiteSpace(response.CodigoEstatus)
                || !string.IsNullOrWhiteSpace(response.Estatus)
                || !string.IsNullOrWhiteSpace(response.EsCancelable)
                || !string.IsNullOrWhiteSpace(response.EstatusCancelacion));
    }

    private static void AddField(ColumnDescriptor column, string label, string value)
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(90).Text(label).SemiBold();
            row.RelativeItem().Text(string.IsNullOrWhiteSpace(value) ? "N/A" : value);
        });
    }
}

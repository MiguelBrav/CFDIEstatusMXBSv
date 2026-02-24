using CFDIEstatusMXBSv.Interfaces;
using CFDIEstatusMXBSv.Models;
using System.Xml.Linq;

namespace CFDIEstatusMXBSv.Services;

public class XmlImportaService : IXmlImportaService
{
    public async Task<(ConsultaModel? Model, List<string> Errors)> ParseXmlAsync(System.IO.Stream xmlStream)
    {
        var errors = new List<string>();
        try
        {
            XDocument doc;
            using (var reader = new System.IO.StreamReader(xmlStream))
            {
                var xml = await reader.ReadToEndAsync();
                doc = XDocument.Parse(xml);
            }

            XNamespace cfdi = "http://www.sat.gob.mx/cfd/4";
            XNamespace tfd = "http://www.sat.gob.mx/TimbreFiscalDigital";

            var comprobante = doc.Root;
            if (comprobante == null)
            {
                errors.Add("XML no contiene elemento Comprobante");
                return (null, errors);
            }

            var emisor = comprobante.Element(cfdi + "Emisor")?.Attribute("Rfc")?.Value;
            var receptor = comprobante.Element(cfdi + "Receptor")?.Attribute("Rfc")?.Value;
            var totalAttr = comprobante.Attribute("Total")?.Value;
            decimal total = 0m;
            if (!string.IsNullOrWhiteSpace(totalAttr) && !decimal.TryParse(totalAttr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out total))
            {
                errors.Add("Total no es un número válido");
            }

            // Buscar TimbreFiscalDigital para UUID y SelloCFD
            var complemento = comprobante.Element(cfdi + "Complemento");
            var tfdElem = complemento?.Descendants().FirstOrDefault(x => x.Name.Namespace == tfd && x.Name.LocalName == "TimbreFiscalDigital");

            var uuid = tfdElem?.Attribute("UUID")?.Value;
            var sello = tfdElem?.Attribute("SelloCFD")?.Value ?? comprobante.Attribute("Sello")?.Value;

            var model = new ConsultaModel();

            if (!string.IsNullOrWhiteSpace(emisor)) model.Emisor = emisor.Trim();
            else errors.Add("No se encontró RFC Emisor");

            if (!string.IsNullOrWhiteSpace(receptor)) model.Receptor = receptor.Trim();
            else errors.Add("No se encontró RFC Receptor");

            if (total > 0) model.Total = total;
            else errors.Add("No se encontró Total o es inválido");

            if (!string.IsNullOrWhiteSpace(uuid)) model.Id = uuid.Trim();
            else errors.Add("No se encontró UUID en el timbre fiscal");

            if (!string.IsNullOrWhiteSpace(sello))
            {
                var fe = sello.Length > 8 ? sello.Substring(sello.Length - 8) : sello;
                model.FE = fe;
            }
            else
            {
                errors.Add("No se encontró Sello (FE)");
            }

            return (model, errors);
        }
        catch (Exception ex)
        {
            return (null, new List<string> { "Error al parsear XML: " + ex.Message });
        }
    }
}

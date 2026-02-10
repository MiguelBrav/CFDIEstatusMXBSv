using CFDIEstatusMXBSv.Interfaces;
using CFDIEstatusMXBSv.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace CFDIEstatusMXBSv.Services;

public class ConsultaCFDIService : IConsultaCFDIService
{
    private readonly HttpClient _client;

    public ConsultaCFDIService(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("text/xml"));
    }

    public async Task<ConsultaCFDIResponse> ConsultarAsync(ConsultaModel expresion)
    {
        var soapEnvelope = $@"
        <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                          xmlns:tem='http://tempuri.org/'>
           <soapenv:Header/>
           <soapenv:Body>
              <tem:Consulta>
                 <tem:expresionImpresa>
                    <![CDATA[?re={expresion.Emisor}&rr={expresion.Receptor}&tt={expresion.Total}&id={expresion.Id}&fe={expresion.FE}]]>
                 </tem:expresionImpresa>
              </tem:Consulta>
           </soapenv:Body>
        </soapenv:Envelope>";

        var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
        content.Headers.Add(
            "SOAPAction",
            "http://tempuri.org/IConsultaCFDIService/Consulta");

        var response = await _client.PostAsync("ConsultaCFDIService.svc", content);
        response.EnsureSuccessStatusCode();

        var xml = await response.Content.ReadAsStringAsync();

        return MapSoapResponse(xml);
    }

    private static ConsultaCFDIResponse MapSoapResponse(string xml)
    {
        var doc = XDocument.Parse(xml);

        XNamespace ns = "http://tempuri.org/";
        XNamespace a = "http://schemas.datacontract.org/2004/07/Sat.Cfdi.Negocio.ConsultaCfdi.Servicio";

        var result = doc.Descendants(ns + "ConsultaResult").FirstOrDefault();

        if (result is null)
        {
            return new ConsultaCFDIResponse
            {
                CodigoEstatus = "N – 601",
                Estatus = "Error al interpretar respuesta del SAT"
            };
        }

        return new ConsultaCFDIResponse
        {
            CodigoEstatus = result.Element(a + "CodigoEstatus")?.Value ?? "",
            Estatus = result.Element(a + "Estado")?.Value ?? "", // El XML del SAT dice "Estado", no "Estatus"
            EsCancelable = result.Element(a + "EsCancelable")?.Value ?? "",
            EstatusCancelacion = result.Element(a + "EstatusCancelacion")?.Value
        };
    }
}
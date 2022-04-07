using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pdf_microservice.Models;
using Spire.Pdf;
using Spire.Pdf.Graphics;


namespace pdf_microservice.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Pdf : ControllerBase
    {
        [HttpPost("requestPdf")]
        public async Task<IActionResult> CreatePdf([FromBody] GeoArgument[] geoArguments)
        {
            if (geoArguments.Length <= 0)
            {
                return BadRequest("No geo arguments passed");
            }
            
            System.Diagnostics.Debug.WriteLine($"Geo arg lenght: {geoArguments.Length}");

            var document = new PdfDocument();
            var page = document.Pages.Add();

            page.Canvas.DrawString(
                $"Timestamp Report Date: {DateTime.Now.ToShortDateString()}",
                new PdfFont(PdfFontFamily.Helvetica, 13f),
                new PdfSolidBrush(Color.Black),
                new PointF(10, 25)
            );
            
            for (var i = 0; i < geoArguments.Length; i++)
            {
                var geoArgument = geoArguments[i];

                page.Canvas.DrawString(
                    $"{i}: long ({geoArgument.Longitude}), lat ({geoArgument.Latitude}) at {geoArgument.Timestamp.ToLongDateString()}",
                    new PdfFont(PdfFontFamily.Helvetica, 13f),
                    new PdfSolidBrush(Color.Black),
                    new PointF(10, 25 + 15 * (i + 1))
                );
            }

            var stream = new MemoryStream();
            document.SaveToStream(stream);
            stream.Position = 0;

            return File(stream, "application/pdf", "report.pdf");
        }
    }
}
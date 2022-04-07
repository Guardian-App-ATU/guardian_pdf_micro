using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf;
using Spire.Pdf.Graphics;


namespace pdf_microservice.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Pdf : ControllerBase
    {
        [HttpGet("requestPdf")]
        public async Task<IActionResult> CreatePdf()
        {
            var document = new PdfDocument();
            var page = document.Pages.Add();

            page.Canvas.DrawString(
                $"Timestamp Report Date: {DateTime.Now.ToShortDateString()}",
                new PdfFont(PdfFontFamily.Helvetica, 13f),
                new PdfSolidBrush(Color.Black),
                new PointF(10, 25)
            );

            var stream = new MemoryStream();
            document.SaveToStream(stream);
            stream.Position = 0;

            return File(stream, "application/pdf", "report.pdf");
        }
    }
}
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PdfReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaticPDFReport()
        {
            var guid = Guid.NewGuid();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PdfReports/" + "file_" + guid + ".pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);

            document.Open();
            Paragraph p = new Paragraph("Traversal Pdf Raporu");
            document.Add(p);
            document.Close();

            return File("/PdfReports/file_" + guid + ".pdf", "application/pdf", "file" + guid + ".pdf");
        }


        public IActionResult StaticCustomerReport()
        {
            string Arial_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");
            BaseFont bf = BaseFont.CreateFont(Arial_TFF, BaseFont.IDENTITY_H, true);
            Font f = new Font(bf, 12, Font.NORMAL);
            Paragraph paragraph = new Paragraph("Traversal - Statik Müşteri Raporu\n\n", f);
            var guid = Guid.NewGuid();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PdfReports/" + "file_" + guid + ".pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);

            document.Open();
            PdfPTable pdfTable = new PdfPTable(3);
            pdfTable.AddCell(new Phrase("Müşteri Adı", f));
            pdfTable.AddCell(new Phrase("Müşteri Soyadı", f));
            pdfTable.AddCell(new Phrase("Müşteri TC Kimlik", f));

            pdfTable.AddCell(new Phrase("Murat Yücedağ", f));
            pdfTable.AddCell(new Phrase("test1", f));
            pdfTable.AddCell(new Phrase("1234567890", f));

            pdfTable.AddCell(new Phrase("test", f));
            pdfTable.AddCell(new Phrase("test2", f));
            pdfTable.AddCell(new Phrase("1234567890", f));

            pdfTable.AddCell(new Phrase("test3", f));
            pdfTable.AddCell(new Phrase("test3", f));
            pdfTable.AddCell(new Phrase("1234567890", f));


            document.Add(pdfTable);

            document.Close();

            return File("/PdfReports/file_" + guid + ".pdf", "application/pdf", "file" + guid + ".pdf");
        }
    }
}

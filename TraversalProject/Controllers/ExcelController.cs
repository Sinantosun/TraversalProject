using BussinessLayer.Abstract;
using ClosedXML.Excel;
using DataAccsesLayer.Concrete;
using DocumentFormat.OpenXml.Office2010.Ink;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Org.BouncyCastle.Utilities;
using System;
using TraversalProject.Dtos.DestinationDtos;

namespace TraversalProject.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<ResultDestinationDto> DestinationList()
        {
            List<ResultDestinationDto> destinationDtos = new List<ResultDestinationDto>();
            using var context = new Context();
            destinationDtos = context.Destinations.Select(x => new ResultDestinationDto
            {
                City = x.City,
                DayNight = x.DayNight,
                Price = x.Price,
                Capacity = x.Capacity

            }).ToList();
            return destinationDtos;
        }

        public IActionResult ExcelReport()
        {

            var guid = Guid.NewGuid();
            return File(_excelService.ExcelList(DestinationList()), "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", $"Yeni_Rapor_{guid}.xlsx");

        }

        public IActionResult DestinationExcelReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Tur Listesi");
                worksheet.Cell(1, 1).Value = "Şehir";
                worksheet.Cell(1, 2).Value = "Konaklama Süresi";
                worksheet.Cell(1, 3).Value = "Fiyat";
                worksheet.Cell(1, 4).Value = "Kapasite";

                int rowCount = 2;

                foreach (var item in DestinationList())
                {
                    worksheet.Cell(rowCount, 1).Value = item.City;
                    worksheet.Cell(rowCount, 2).Value = item.DayNight;
                    worksheet.Cell(rowCount, 3).Value = item.Price;
                    worksheet.Cell(rowCount, 4).Value = item.Capacity;

                    rowCount++;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var guid = Guid.NewGuid();
                    return File(content, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", $"Yeni_Rapor_{guid}.xlsx");

                }

            };
        }
    }
}

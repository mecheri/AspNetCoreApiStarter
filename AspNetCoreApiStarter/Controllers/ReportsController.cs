using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreApiStarter.Bll.Itf;
using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Controllers.Core;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.ViewModels;
using AspNetCoreApiStarter.ViewModels.Core;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NSwag.Annotations;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;

namespace AspNetCoreApiStarter.Controllers
{
    /// <summary>
    /// User Controller.
    /// </summary>
    // [Authorize(Policy = "ApiUser")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IHostingEnvironment env;
        private readonly IUserBll userBll;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController"/> class.
        /// </summary>
        /// <param name="env">Hosting environment.</param>
        /// <param name="userBll">User BLL.</param>
        public ReportsController(IHostingEnvironment env, IUserBll userBll)
        {
            this.env = env;
            this.userBll = userBll;
        }

        /// <summary>
        /// Gets pdf sample file.
        /// </summary>
        /// <returns>Report. Http 200 if Ok.</returns>
        [HttpGet("Pdf")]
        public ActionResult<byte[]> Get()
        {
            string reportingPath = @"C:\DepTech\1_Projects\starters\aspnetcoreapistarter\AspNetCoreApiStarter\Reporting\";
            StiReport report = new StiReport();
            report.Load(string.Format(@"{0}\{1}", reportingPath, @"Templates\ReportSample.mrt"));

            // set parameters and variables here
            // TODO

            // render the report
            report.Render(false);
            report.RenderedPages.Clear();

            foreach (StiPage page in report.RenderedPages)
            {
                report.RenderedPages.Add(page);
            }

            report.ExportDocument(StiExportFormat.Pdf, string.Format(@"{0}\{1}", reportingPath, @"Exports\ReportSample.pdf"));

            return report.SaveDocumentToByteArray();
        }

        /// <summary>
        /// Gets excel sample file with OpenXml.
        /// </summary>
        /// <returns>Excel file.</returns>
        [HttpGet("Excel")]
        public async Task<ActionResult<byte[]>> ExportExcel()
        {
            string exportsPath = @"C:\DepTech\1_Projects\starters\aspnetcoreapistarter\AspNetCoreApiStarter\Reporting\Exports";
            string fileName = @"DemoOPENXML.xlsx";

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(Path.Combine(exportsPath, fileName), SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Users" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                List<User> users = new List<User>()
                {
                    new User() {Id = 1, FirstName = "Mehdi", LastName = "Mecheri"},
                    new User() {Id = 2, FirstName = "Nicolas", LastName = "Delaval"},
                };

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row row = new Row();

                row.Append(
                    new Cell() { CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Id"), DataType = new EnumValue<CellValues>(CellValues.String) },
                    new Cell() { CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("FirstName"), DataType = new EnumValue<CellValues>(CellValues.String) },
                    new Cell() { CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("LastName"), DataType = new EnumValue<CellValues>(CellValues.String) }
                );

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                // Inserting each employee
                foreach (var user in users)
                {
                    row = new Row();

                    row.Append(
                        new Cell() { CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(user.Id.ToString()), DataType = new EnumValue<CellValues>(CellValues.Number) },
                        new Cell() { CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(user.FirstName), DataType = new EnumValue<CellValues>(CellValues.String) },
                        new Cell() { CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(user.LastName), DataType = new EnumValue<CellValues>(CellValues.String) }
                    );

                    sheetData.AppendChild(row);
                }

                worksheetPart.Worksheet.Save();
                document.Close();

                var memory = new MemoryStream();
                using (var stream = new FileStream(Path.Combine(exportsPath, fileName), FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return this.File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        /// <summary>
        /// Gets excel sample file with NPOI library.
        /// </summary>
        /// <returns>Excel file.</returns>
        [HttpGet("ExcelNPOI")]
        public async Task<IActionResult> ExportExcelWithNPOI()
        {
            string exportsPath = @"C:\DepTech\1_Projects\starters\aspnetcoreapistarter\AspNetCoreApiStarter\Reporting\Exports";
            string fileName = @"DemoNPOI.xlsx";
            using (var fs = new FileStream(Path.Combine(exportsPath, fileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("FirstName");
                row.CreateCell(2).SetCellValue("LastName");

                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Mehdi");
                row.CreateCell(2).SetCellValue("Mecheri");

                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Nicolas");
                row.CreateCell(2).SetCellValue("Delaval");

                workbook.Write(fs);
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(Path.Combine(exportsPath, fileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return this.File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}

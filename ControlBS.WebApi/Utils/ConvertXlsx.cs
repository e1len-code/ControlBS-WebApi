using ControlBS.BusinessObjects.Models;
using OfficeOpenXml;

namespace ControlBS.WebApi.Utils
{
    public static class ConvertXlsx
    {
        public static String ConvertListToExcel(List<CTATTNResponseReport> listReport, int? PERSIDEN, String fileName = "file")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string directoryPath = Path.Combine(Environment.CurrentDirectory, "Reports");
            Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist
            string filePath = Path.Combine(directoryPath, fileName + ".xlsx");

            using (ExcelPackage pck = new ExcelPackage())
            {
                // Create a new worksheet in the Excel package
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1:H1"].Merge = true;
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Value = "REGISTRO DE CONTROL DE ASISTENCIAS";
                ws.Cells["A1:A5"].Style.Font.Bold = true;
                ws.Cells["A2"].Value = "EMPRESA: ";
                ws.Cells["A3"].Value = "RUC: ";
                ws.Cells["A4"].Value = "MES DE: ";
                ws.Cells["A5"].Value = "TRABAJADOR: ";
                ws.Cells["B2:G2"].Merge = true;
                ws.Cells["B3:G3"].Merge = true;
                ws.Cells["B4:G4"].Merge = true;
                ws.Cells["B5:G5"].Merge = true;
                ws.Cells["B2"].Value = "BRAIN SYSTEMS S.R.L.";
                ws.Cells["B3"].Value = "20527513490";
                ws.Cells["H2:H4"].Merge = true;
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Signatures", $"{PERSIDEN}.png")))
                {
                    var picture = ws.Drawings.AddPicture("signature", Path.Combine(Environment.CurrentDirectory, "Signatures", $"{PERSIDEN}.png"));
                    picture.From.Column = 7; // Column index
                    picture.From.Row = 1;    // Row index
                    picture.SetSize(70, 60);
                }
                ws.Cells["A6"].LoadFromCollection(listReport, true);

                ws.Cells["B4"].Formula = "=TEXTO(D7;\"mmmm\")";
                ws.Cells["B5"].Formula = "=A7";
                // Save the package to a file
                FileInfo fi = new FileInfo(filePath);
                pck.SaveAs(fi);
            }
            return ConvertFileToBase64(filePath);
        }
        public static string ConvertFileToBase64(string filePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileBase64 = Convert.ToBase64String(fileBytes);
            return fileBase64;
        }
    }
}
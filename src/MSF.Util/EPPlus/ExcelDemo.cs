using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MSF.Util.EPPlus
{
    public static class ExcelDemo
    {
        public static void CreateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo(fileName: "C:\\Users\\Falt_\\Documentos\\github\\Log\\EPPlus.xlsx");

            var people = GetData();

            SaveExcelFile(people, file);

            List<PersonModel> peopleFromExcel = LoadExcelFile(file);

            foreach (var person in peopleFromExcel)
            {
                Console.WriteLine($"{person.ID} - {person.Name} - {person.Description}");
            }
            Console.ReadLine();
        }

        private static List<PersonModel> LoadExcelFile(FileInfo file)
        {
            var output = new List<PersonModel>();

            if (file.Exists)
            {
                using (var pkg = new ExcelPackage(file))
                {
                    pkg.LoadAsync(file);
                    var ws = pkg.Workbook.Worksheets[PositionID: 0];

                    var row = 3;
                    var column = 1;

                    while (string.IsNullOrWhiteSpace(ws.Cells[row, column].Value?.ToString()) == false)
                    {
                        var person = new PersonModel();

                        person.ID = int.Parse(ws.Cells[row, column].Value.ToString());
                        person.Name = ws.Cells[row, column + 1].Value.ToString();
                        person.Description = ws.Cells[row, column + 2].Value.ToString();

                        output.Add(person);
                        row += 1;
                    }
                    return output;
                }
            }

            return output;
        }

        private static void SaveExcelFile(List<PersonModel> people, FileInfo file)
        {
            DeleteIfExists(file);

            using (ExcelPackage pkg = new ExcelPackage(file))
            {
                var ws = pkg.Workbook.Worksheets.Add("MainReport");

                ws.Cells["A1"].Value = "Tim Corey Course";
                ws.Cells["A1:C1"].Merge = true;

                ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(1).Style.Font.Size = 24;
                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Font.Color.SetColor(Color.Blue);

                ws.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(2).Style.Font.Bold = true;
                ws.Row(2).Style.Font.Size = 20;

                var range = ws.Cells[Address: "A2"].LoadFromCollection(people, true);
                range.AutoFitColumns();

                pkg.Save();
            }
        }

        private static void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

        private static List<PersonModel> GetData()
        {
            var list = new List<PersonModel>()
            {
                new PersonModel() { ID = 1, Name = "Nome 1", Description = "Descricao 1"},
                new PersonModel() { ID = 2, Name = "Nome 2", Description = "Descricao 2"},
                new PersonModel() { ID = 3, Name = "Nome 3", Description = "Descricao 3"}
            };

            return list;
        }

    }
}

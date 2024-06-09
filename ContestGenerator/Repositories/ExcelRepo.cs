using ContestGenerator.Abstractions;
using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Viewmodels;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ContestGenerator.Repositories
{
    public class ExcelRepo : IExcelRepo
    {
        private void AddHeaders(ExcelWorksheet sheet, IEnumerable<string> headers)
        {
            for (int i = 0; i < headers.Count(); i++)
            {
                int column = i + 1;
                var header = headers.ElementAt(i);
                sheet.Cells[1, column].Value = header;
                sheet.Cells[1, column].Style.Font.Bold = true;
                sheet.Cells[1, column].Style.Locked = true;
            }
        }

        private void AddRow(ExcelWorksheet sheet, IEnumerable<string> fields, int row)
        {
            for (int i = 0; i < fields.Count(); i++)
            {
                int column = i + 1;
                sheet.Cells[row, column].Value = fields.ElementAt(i);
            }
        }

        public async Task<byte[]> Generate(IEnumerable<Question> questions)
        {
            var firstQuestion = questions.FirstOrDefault();
            var contest = firstQuestion.Contest;
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add(contest.Name);
                var headers = new[]
                {
                    "Id",
                    "Почта",
                    "Имя",
                    "Вопрос"
                };

                // Добавляем заголовки
                AddHeaders(sheet, headers);

                // Добавляем колонки
                for (int row = 0; row < questions.Count(); row++)
                {
                    var question = questions.ElementAt(row);
                    var sheetRow = row + 2;
                    var fields = new string[]
                    {
                        question.Id.ToString(),
                        question.Email,
                        question.Name,
                        question.Description
                    };
                    AddRow(sheet, fields, sheetRow);
                }
                return await package.GetAsByteArrayAsync();
            }
        }

        public async Task<byte[]> Generate(IEnumerable<ResponseViewmodel> responses)
        {
            var firstResponse = responses.FirstOrDefault();
            var contest = firstResponse.Response.Contest;
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add(contest.Name);
                var headers = new List<string>()
                {
                    "Id"
                };
                headers.AddRange(firstResponse.Response.Responses.Select(x => x.Name));
                headers.Add("Средняя оценка");
                // Добавляем заголовки
                AddHeaders(sheet, headers);

                // Добавляем колонки
                for (int row = 0; row < responses.Count(); row++)
                {
                    var response = responses.ElementAt(row);
                    var sheetRow = row + 2;
                    var fields = new List<string>()
                    {
                        response.Response.Id.ToString(),
                    };
                    fields.AddRange(response.Response.Responses.Select(x => x.Value));
                    var avgEvaluation = string.IsNullOrEmpty(response.AverageEvaluation.ToString()) ? "Не оценено" : response.AverageEvaluation.ToString();
                    fields.Add(avgEvaluation);
                    AddRow(sheet, fields, sheetRow);
                }
                return await package.GetAsByteArrayAsync();
            }
        }
    }
}

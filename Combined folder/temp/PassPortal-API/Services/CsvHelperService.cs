/*using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using PassPortal_API.DTOs;

namespace PassPortal_API.Services
{
    public class CsvHelperService
    {
        public async Task<List<PassportOfficeDTO>> ParseCsvFileAsync(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = new List<PassportOfficeDTO>();
                await foreach (var record in csv.GetRecordsAsync<PassportOfficeDTO>())
                {
                    records.Add(record);
                }
                return records;
            }
        }
    }
}
*/
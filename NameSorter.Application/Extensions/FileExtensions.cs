using Microsoft.AspNetCore.Http;
using System.Text;

namespace NameSorter.Application.Extensions
{
    public static class FileExtensions
    {
        public static async Task<List<string>> ReadLinesAsync(this IFormFile file)
        {
            List<string> lines = new();

            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                var content = await reader.ReadToEndAsync();
                lines = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                               .ToList();
            }

            return lines;
        }
    }
}

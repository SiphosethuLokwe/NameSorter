using NameSorter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Application.Services
{
    public class FileService : IFileService
    {
        public async Task<IEnumerable<string>> ReadLinesAsync(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), "File path cannot be empty.");

            if(!File.Exists(path))
                throw new FileNotFoundException("File not found.", path);

            return await File.ReadAllLinesAsync(path);
        }

        public async Task WriteLinesAsync(string path, IEnumerable<string> lines)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), "Output file path cannot be empty.");

            if (lines == null || !lines.Any())
                throw new ArgumentException("Lines to write cannot be null or empty.", nameof(lines));

            await File.WriteAllLinesAsync(path, lines);
        }
    }
}

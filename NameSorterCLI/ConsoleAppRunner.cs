using NameSorter.Domain.Interfaces;
using NameSorter.Application.Services;

namespace SorterCLI
{
    public class ConsoleAppRunner
    {
        private readonly ISortService _sortService;
        private readonly IFileService _fileService;

        public ConsoleAppRunner(ISortService sortService, IFileService fileService)
        {
            _sortService = sortService;
            _fileService = fileService;
        }

        public async Task RunAsync(string[] args)
        {
            string filePath;

            if (args.Length == 0)
            {
                filePath = Path.Combine(AppContext.BaseDirectory, "unsorted-names-list.txt");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Default file not found: " + filePath);
                    return;
                }

                Console.WriteLine("No args provided. Using default file: " + filePath);
            }
            else
            {
                filePath = args[0];
            }
;

            try
            {
                var lines = await _fileService.ReadLinesAsync(filePath);

                if (lines == null || !lines.Any())
                {
                    Console.WriteLine("No lines found in the file.");
                    return;
                }

                var parsedNames = _sortService.ParseNames(lines);
                var sortedNames = _sortService.Sort(parsedNames);
                var outputLines = sortedNames.Select(n => n.ToString()).ToList();

                Console.WriteLine("Sorted Names:\n-------------");
                foreach (var name in outputLines)
                {
                    Console.WriteLine(name);
                }

                var outputPath = Path.Combine(Path.GetDirectoryName(filePath)!, "sorted-names-list.txt");
                await _fileService.WriteLinesAsync(outputPath, outputLines);

                Console.WriteLine($"Sorted names saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}

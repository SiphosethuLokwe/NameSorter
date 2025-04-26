using Microsoft.AspNetCore.Mvc;
using NameSorter.Application.Interfaces;
using NameSorter.Application.Services;

namespace NameSorterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NameSortController : Controller
    {
        private readonly ISortService _sortService;
        private readonly IFileService _fileService;
        public NameSortController(ISortService sortService, IFileService fileservice) {
            _sortService = sortService;
            _fileService = fileservice;
        }

        [HttpPost("upload-and-sort")]
        public async Task<IActionResult> UploadAndSort([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded or file is empty.");

            List<string> lines = new();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (line != null)
                        lines.Add(line);
                }
            }

            var parsedNames = _sortService.ParseNames(lines);
            var sortedNames = _sortService.Sort(parsedNames);
            var outputLines = sortedNames.Select(n => n.ToString()).ToList();

            await _fileService.WriteLinesAsync("sorted-names-list.txt", outputLines);

            return Ok(outputLines);
        }

        [Route("/error")]
        public IActionResult HandleError()
        {
            return Problem();
        }

    }


}

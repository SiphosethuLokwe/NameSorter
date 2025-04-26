using Microsoft.AspNetCore.Mvc;
using NameSorter.Application.Interfaces;
using NameSorter.Application.Services;
using NameSorterAPI.Models;
using SorterAPI.Helpers;

namespace SorterAPI.Controllers
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
        public async Task<IActionResult> UploadAndSort([FromForm] UploadFileRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentNullException("No file uploaded or file is empty.");


            var lines = await request.File.ReadLinesAsync();      

            var parsedNames = _sortService.ParseNames(lines);
            var sortedNames = _sortService.Sort(parsedNames);
            var outputLines = sortedNames.Select(n => n.ToString()).ToList();

            await _fileService.WriteLinesAsync("sorted-names-list.txt", outputLines);

            return Ok(outputLines);
        }

        

    }


}

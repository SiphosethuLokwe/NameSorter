using Microsoft.AspNetCore.Mvc;

namespace NameSorterAPI.Models
{
    public class UploadFileRequest
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}

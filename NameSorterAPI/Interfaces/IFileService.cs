using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Application.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<string>> ReadLinesAsync(string path); 
        Task WriteLinesAsync(string path, IEnumerable<string> lines); 
    }
}

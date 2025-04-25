using NameSorter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Application.Services
{
    public  interface ISortService
    {
        List<PersonName> ParseNames(IEnumerable<string> lines); // Parse raw name strings into model objects
        List<PersonName> Sort(List<PersonName> names); // Sort name objects by last name then given names
    }
}

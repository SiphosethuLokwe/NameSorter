using NameSorter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Domain.Interfaces
{
    public  interface ISortService
    {
        List<PersonName> ParseNames(IEnumerable<string> lines); 
        List<PersonName> Sort(List<PersonName> names); 
}

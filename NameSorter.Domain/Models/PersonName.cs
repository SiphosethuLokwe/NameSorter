using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Domain.Models
{
    public class PersonName
    {
        public List<string> GivenNames { get; set; } = new(); 
        public string LastName { get; set; } = string.Empty; 

        public override string ToString()
        {
            // Convert back to full name string
            return string.Join(" ", GivenNames.Append(LastName));
        }
    }
}

using NameSorter.Domain.Interfaces;
using NameSorter.Domain.Models;

namespace NameSorter.Application.Services
{
    public class SortService:ISortService
    {
        public List<PersonName> ParseNames(IEnumerable<string> lines)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines), "Input lines cannot be null.");

            var result = new List<PersonName>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries); 
                if (parts.Length < 2)
                    continue; 

                var givenNames = parts.Take(parts.Length - 1).ToList(); 
                var lastName = parts[parts.Length - 1];

                result.Add(new PersonName
                {
                    GivenNames = givenNames,
                    LastName = lastName
                });
            }
            if (!result.Any())
                throw new InvalidOperationException("No valid names were parsed from the input.");

            return result;
        }

        public List<PersonName> Sort(List<PersonName> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentException("List of names cannot be null or empty.", nameof(names));

            return names
                .OrderBy(n => n.LastName)
                .ThenBy(n => string.Join(" ", n.GivenNames))
                .ToList();
        }
    }
}
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
                    continue; // Skip empty lines

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries); // Split name into parts
                if (parts.Length < 2)
                    continue; // Name must have at least 2 parts

                var givenNames = parts.Take(parts.Length - 1).ToList(); // All but last = given names
                var lastName = parts[parts.Length - 1]; // Last part = last name

                result.Add(new PersonName
                {
                    GivenNames = givenNames,
                    LastName = lastName
                });
            }
            if (!result.Any())
                throw new InvalidOperationException("No valid names were parsed from the input.");

            return result; // Return list of parsed names
        }

        public List<PersonName> Sort(List<PersonName> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentException("List of names cannot be null or empty.", nameof(names));

            // Sort by last name, then by given names
            return names
                .OrderBy(n => n.LastName)
                .ThenBy(n => string.Join(" ", n.GivenNames))
                .ToList();
        }
    }
}
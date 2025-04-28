using Bogus;
using Moq;
using NameSorter.Domain.Interfaces;
using NameSorter.Application.Services;
using NameSorter.Domain.Models;
using SorterCLI;

namespace NameSorterUnitTests
{
    public class ConsoleAppRunnerTests
    {
        private readonly Mock<ISortService> _mockSortService;
        private readonly Mock<IFileService> _mockFileService;
        private readonly ConsoleAppRunner _appRunner;
        public ConsoleAppRunnerTests()
        {
            _mockSortService = new Mock<ISortService>();
            _mockFileService = new Mock<IFileService>();
            _appRunner = new ConsoleAppRunner(_mockSortService.Object, _mockFileService.Object);
        }

        [Fact]
        public async Task RunAsync_WithNoArguments_PrintsUsageMessage()
        {
            // Arrange
            var args = new string[] { };

            // Act
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                await _appRunner.RunAsync(args);
                var result = sw.ToString();

                // Assert
                Assert.Contains("Usage: NameSortAPI <path-to-input-file>", result);
            }
        }

        [Fact]
        public async Task RunAsync_WithEmptyFile_PrintsNoLinesFoundMessage()
        {
            // Arrange
            var args = new[] { "input.txt" };
            _mockFileService.Setup(f => f.ReadLinesAsync(It.IsAny<string>())).ReturnsAsync(new List<string>());

            // Act
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw); // Redirect Console output to capture itfewe
                await _appRunner.RunAsync(args);
                var result = sw.ToString();

                // Assert
                Assert.Contains("No lines found in the file.", result);
            }
        }

        [Fact]
        public async Task RunAsync_WithValidFile_SortsAndSavesNames()
        {
            // Arrange
            var args = new[] { "input.txt" };

            // Prepare the names as PersonName objects
            var names = new List<PersonName>
            {
                new PersonName
                {
                  GivenNames = new List<string> { "Alice" },
                  LastName = "Smith"
                },
                new PersonName
                {
                  GivenNames = new List<string> { "John" },
                  LastName = "Does"
                },
                new PersonName
                {
                  GivenNames = new List<string> { "Bob" },
                  LastName = "Johnson"
                }
            };


            _mockFileService.Setup(f => f.ReadLinesAsync(It.IsAny<string>())).ReturnsAsync(names.Select(n => n.ToString()).ToList());
            _mockSortService.Setup(s => s.ParseNames(It.IsAny<IEnumerable<string>>())).Returns(names);

            _mockSortService.Setup(s => s.Sort(It.IsAny<List<PersonName>>())).Returns((List<PersonName> list) =>
            {
                var sortService = new SortService();
                return sortService.Sort(list); 
            });

            _mockFileService.Setup(f => f.WriteLinesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>())).Returns(Task.CompletedTask);

            // Act
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw); 
                await _appRunner.RunAsync(args);
                var result = sw.ToString();

                // Assert
                Assert.Contains("Sorted names saved to:", result);


                var writtenNames = names.OrderBy(n => n.LastName).ThenBy(n => string.Join(" ", n.GivenNames)).Select(n => n.ToString()).ToList();

                _mockFileService.Verify(f => f.WriteLinesAsync(It.IsAny<string>(), It.Is<IEnumerable<string>>(list => list.SequenceEqual(writtenNames))), Times.Once);
            }
        }


        [Fact]
        public async Task RunAsync_WithException_PrintsErrorMessage()
        {
            // Arrange
            var args = new[] { "input.txt" };
            _mockFileService.Setup(f => f.ReadLinesAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));

            // Act
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw); 
                await _appRunner.RunAsync(args);
                var result = sw.ToString();

                // Assert
                Assert.Contains("Error: Test exception", result);
            }
        }

        [Fact]
        public async Task RunAsync_With1000Records_SortsAndSavesNames()
        {
            // Arrange
            var args = new[] { "input.txt" };

            var names = GeneratePersonNames(1000);

            // Set up mock behavior
            _mockFileService.Setup(f => f.ReadLinesAsync(It.IsAny<string>())).ReturnsAsync(names.Select(n => n.ToString()).ToList());
            _mockSortService.Setup(s => s.ParseNames(It.IsAny<IEnumerable<string>>())).Returns(names);
            _mockSortService.Setup(s => s.Sort(It.IsAny<List<PersonName>>())).Returns((List<PersonName> list) =>
            {
                var sortService = new SortService();
                return sortService.Sort(list); 
            });

            _mockFileService.Setup(f => f.WriteLinesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>())).Returns(Task.CompletedTask);

            // Act
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw); 
                await _appRunner.RunAsync(args);
                var result = sw.ToString();

                // Assert
                Assert.Contains("Sorted names saved to:", result);

                _mockFileService.Verify(f => f.WriteLinesAsync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Once);

                var writtenNames = names.OrderBy(n => n.LastName).ThenBy(n => string.Join(" ", n.GivenNames)).Select(n => n.ToString()).ToList();
                _mockFileService.Verify(f => f.WriteLinesAsync(It.IsAny<string>(), It.Is<IEnumerable<string>>(list => list.SequenceEqual(writtenNames))), Times.Once);
            }
        }



        public List<PersonName> GeneratePersonNames(int count)
        {

            var faker = new Faker();

            return Enumerable.Range(1, count)
                             .Select(i => new PersonName
                             {
                                 GivenNames = new List<string> { faker.Name.FirstName() },
                                 LastName = faker.Name.LastName()
                             })
                             .ToList();
        }
    }

 }

# NameSorter

NameSorter is a .NET 8 application designed to sort a list of names. This project is built using C# and leverages modern .NET features to ensure high performance and maintainability.

## Features

- Sorts names alphabetically by last name, then by first name.
- Built with .NET 8 for optimal performance and compatibility.
- Includes unit tests to ensure code reliability.

## Prerequisites

To build and run this project, you need:

- .NET 8 SDK installed. You can download it from [Microsoft's .NET website](https://dotnet.microsoft.com/).
- A code editor like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

## Getting Started

1. Clone the repository:
git clone [<repository-url>](https://github.com/SiphosethuLokwe/NameSorter.git) cd NameSorter

2. Restore dependencies:
dotnet restore
   
3. Build the project:
   dotnet build --configuration Release
   
4. Run the application:
   dotnet run NameSorterCLI ./unsorted-names-list

   The Project also has an API with a swagger endpoint where you can upload an unsorted list and it will sort and display it for you : SorterAPI
   

## Running Tests

To run the unit tests, execute the following command:
dotnet test


## CI/CD Integration

This project uses Travis CI and github Actions for continuous integration. The `.travis.yml` file is configured to:

- Restore dependencies.
- Build the project in Release mode.
- Run unit tests.


## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes and push the branch.
4. Open a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Acknowledgments

- Built with .NET 8 and C#.
- Inspired by the need for efficient name sorting solutions.

---

Happy coding!

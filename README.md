# NameSorter

NameSorter is a .NET 8 application designed to sort a list of names. This project is built using C# and leverages modern .NET features to ensure high performance and maintainability.

## Features

- Sorts names alphabetically by last name, then by first name.
- Built with .NET 8 for optimal performance and compatibility.
- Includes unit tests to ensure code reliability.

  
## Clean Architecture Overview

This project is structured using Clean Architecture, which separates the application into distinct layers:

1. **Domain Layer**:
   - Contains the core business logic and entities.
   - Defines interfaces (e.g., `ISortService`) to abstract dependencies.

2. **Application Layer**:
   - Implements the business logic defined in the Domain layer.
   - Contains services like `SortService` that handle parsing and sorting logic.

3. **Infrastructure Layer**:
   - Handles external concerns such as data access, file I/O, or third-party integrations.
   - Implements interfaces defined in the Domain layer.

4. **Presentation Layer**:
   - Responsible for user interaction, such as CLI or GUI.
   - Invokes the Application layer to perform operations.

This architecture ensures that the core business logic is independent of external frameworks or technologies.


## Prerequisites

To build and run this project, you need:

- .NET 8 SDK installed. You can download it from [Microsoft's .NET website](https://dotnet.microsoft.com/).
- A code editor like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

## Getting Started

1. Clone the repository:
git clone (https://github.com/SiphosethuLokwe/NameSorter.git) cd NameSorter

2. Restore dependencies:
dotnet restore
   
3. Build the project:
   dotnet build --configuration Release
   
4. Run the application:
   dotnet run NameSorterCLI ./unsorted-names-list

   The Project also has an API with a swagger endpoint where you can upload an unsorted list and it will sort and display it for you : SorterAPI
   
   Curl
   curl -X 'POST' \
  'https://localhost:7119/api/NameSort/upload-and-sort' \
  -H 'accept: */*' \
  -H 'Content-Type: multipart/form-data' \
  -F 'file=@Tetsdata.txt;type=text/plain'

   

## Running Tests

To run the unit tests, execute the following command:
dotnet test NameSorterUnitTests


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



Getting Started
Prerequisites
.NET 8 SDK installed

Build and Run (CLI)
Open a terminal/command prompt at the solution root.

Build the solution:
dotnet build


run the CLI app with an input file:

bash
Copy
Edit
cd NameSorterCLI
dotnet run NameSorterCLI ./unsorted-names-list.txt


How It Works
The console app accepts the path to a file containing an unsorted list of names.

It reads the names asynchronously using IFileService (implemented in Infrastructure).

It sorts the names using ISortService (implemented in Application).

It writes the sorted names back to an output file.

All operations are injected using Dependency Injection.


Testing
You can unit test the core logic easily:

Mock IFileService and ISortService for service unit tests.

Test sorting behavior independently from file system interactions.

The CLI project is thin — it delegates work to the services.


 Design Choices
Separation of Concerns:

Domain models live in Domain

Business logic lives in Application

Infrastructure services (file system operations) live in Infrastructure

Console execution is handled in CLI

Dependency Injection:

Host.CreateDefaultBuilder is used to configure services for CLI.

Error Handling:

Console outputs a friendly error message if something fails.

Extensibility:

Easy to add Web API later (solution structure is ready for it).




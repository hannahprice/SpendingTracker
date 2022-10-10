# Spending Tracker

For those that know me.. I like to be organised 😀 So I'm building a simple app that I can use to manage and analyse my personal spending. I've chosen to use Blazor wasm because I wanted to explore something new. 

## Project goals 

- Provide spending insights and statistics 
- Provide a rich searching/filtering page 
- Implement an AI/ML solution to automatically categorise transactions

## Tech stack
- Blazor wasm
- EF Core (currently using localdb but will move to a real DB)
- Fluxor for state management
- MudBlazor for UI components and styling
- BUnit for UI component tests
- TestWebApplicationFactory and Docker for integration tests
- Verify for capturing test output
- Mediatr and CQRS pattern for API project

## Running the app

- `dotnet run --project .\SpendingTracker\Server\SpendingTracker.Server.csproj`
- Navigate to https://localhost:7248/
- Add some transactions (currently manual, but will be adding file upload functionality) 
- Add some budgets

# Spending Tracker

For those that know me.. I like to be organised 😀 So I'm building a simple app that I can use to manage and analyse my personal spending. I've chosen to use Blazor wasm because I wanted to explore something new. 

## Project goals 

- Provide spending insights and statistics 
- Provide a rich searching/filtering page 
- Implement an AI/ML solution to automatically categorise transactions

## Tech stack
- Blazor wasm
- Fluxor for state management
- MudBlazor for UI components and styling
- BUnit for UI component tests
- EF Core (MSSQL hosted in Docker)
- TestWebApplicationFactory and Docker for integration tests
- Verify for capturing test output
- Mediatr and CQRS pattern for API project

## Running the app

If using Visual Studio, set the docker compose project as the startup project and run (making sure Docker Desktop is running).

- Add some transactions (currently manual, but will be adding file upload functionality) 
- Add some budgets

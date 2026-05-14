# GameStore API

A minimal .NET 10 Web API for managing games and genres, backed by SQLite and Entity Framework Core.

## Tech Stack

- ASP.NET Core Minimal APIs
- Entity Framework Core 10
- SQLite

## Project Structure

- `GameStore.slnx`: solution file
- `GameStore.Api/`: API project
- `GameStore.Api/games.http`: ready-to-run HTTP requests for testing

## Prerequisites

- .NET SDK 10.0+

## Setup

1. Open a terminal in `GameStore.Api`.
2. Ensure `appsettings.json` contains:

```json
{
  "ConnectionStrings": {
    "GameStore": "Data Source=gamestore.db"
  }
}
```

3. Run the API:

```powershell
dotnet run
```

## Database and Seeding

On startup, the app applies migrations and seeds genres if the genres table is empty.

Seeded genres:

- Action
- Adventure
- RPG
- Strategy
- Sports

SQLite files are created in `GameStore.Api/` when the app first runs:

- `gamestore.db`
- `gamestore.db-shm`
- `gamestore.db-wal`

## API Endpoints

Base URL (default): `http://localhost:5034`

### Games

- `GET /games` - Get all games
- `GET /games/{id}` - Get game by id
- `POST /games` - Create game
- `PUT /games/{id}` - Update game
- `DELETE /games/{id}` - Delete game

Create/Update payload shape:

```json
{
  "name": "Flight Simulator",
  "genreId": 5,
  "price": 89.99,
  "releaseDate": "2020-11-14"
}
```

Note: `genreId` must reference an existing genre row.

### Genres

- `GET /genres` - Get all genres

## Test Requests Quickly

Use `GameStore.Api/games.http` inside VS Code REST Client.

## Common Issues

### Connection string 'GameStore' not found

Make sure `GameStore.Api/appsettings.json` includes `ConnectionStrings:GameStore`.

### SQLite foreign key constraint failed

This usually means `genreId` in your request does not exist in the `Genres` table. Use a valid id from `GET /genres`.

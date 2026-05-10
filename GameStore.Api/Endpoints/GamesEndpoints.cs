
using GameStore.Api.Dtos;


public static class GamesEndpoints
    {

        const string GetGameEndpoint = "GetGame";
        private static readonly List<GameDto> games = [
            new (1, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26)),
            new (2, "Hades", "Rogue-like", 24.99M, new DateOnly(2020, 9, 17)),
            new (3, "The Witcher 3: Wild Hunt", "Action RPG", 39.99M, new DateOnly(2015, 5, 19))    
        ];

         public static void MapGamesEndpoints(this WebApplication app)
        {


            var group = app.MapGroup("/games");
        //Get all games
            group.MapGet("/", () => games);

            //Get game by id/1
            group.MapGet("/{id}", (int id) =>
            {
                var game = games.Find(game => game.Id == id);

                return game is not null ? Results.Ok(game) : Results.NotFound();
            })
            .WithName(GetGameEndpoint);

            //POST a new game
            group.MapPost("/", (CreateGameDto newGame) =>
            {
                GameDto game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate
                );
                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpoint, new { id = game.Id }, game);
            });

            //PUT update game by id/1
            group.MapPut("/{id}", (int id, UpdateDto updatedGame) =>
            {
                var index =  games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }
                
                games[index] = new GameDto(
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

                return Results.NoContent();
            });

            //DELETE game by id/4
            group.MapDelete("/{id}", (int id) =>
            {
                // var index = games.FindIndex(game => game.Id == id);
                // if (index == -1)
                // {
                //     return Results.NotFound();
                // }
                // games.RemoveAt(index);
                // return Results.NoContent();

                games.RemoveAll(game => game.Id == id);
                return Results.NoContent();
            });
         }

    }


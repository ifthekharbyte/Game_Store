
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;


public static class GamesEndpoints
    {

        const string GetGameEndpoint = "GetGame";


         public static void MapGamesEndpoints(this WebApplication app)
        {


            var group = app.MapGroup("/games");
        //Get all games
            group.MapGet("/", async (GameStoreContext dbContext) 
            => await dbContext.Games.Include(g => g.Genre)
                                    .Select(game => new GameSummaryDto(
                                    game.Id,
                                    game.Name,
                                    game.Genre!.Name,
                                    game.Price,
                                    game.ReleaseDate
            ))
            .AsNoTracking()
            .ToListAsync());

            //Get game by id/1
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);

                if (game is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                ));
            })
            .WithName(GetGameEndpoint);

            //POST a new game
            group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {


                Game game = new ()
                {
                    Name = newGame.Name,
                    GenreId = newGame.GenreId,
                    Price = newGame.Price,
                    ReleaseDate = newGame.ReleaseDate
                    
                };
                
                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();

                GameDetailsDto gameDto = new (
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                );

                return Results.CreatedAtRoute(GetGameEndpoint, new { id = gameDto.Id }, gameDto);
            });

            //PUT update game by id/1
            group.MapPut("/{id}", async
             (int id, UpdateDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);

                 if (existingGame is null)
                {
                    return Results.NotFound();
                }
                existingGame.Name = updatedGame.Name;
                existingGame.GenreId = updatedGame. GenreId;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;


                
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            //DELETE game by id/4
            group.MapDelete("/{id}", async(int id, GameStoreContext dbContext) =>
            {
                // var index = games.FindIndex(game => game.Id == id);
                // if (index == -1)
                // {
                //     return Results.NotFound();
                // }
                // games.RemoveAt(index);
                // return Results.NoContent();
                   // await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();

                    var existingGame = await dbContext.Games.FindAsync(id);
    
                    if (existingGame is null)
                    {
                        return Results.NotFound();
                    }
                    dbContext.Games.Remove(existingGame);
                    await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
         }

    }


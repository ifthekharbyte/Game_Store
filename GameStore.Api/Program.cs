using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string getGamesEndpoint = "GetGame";

List<GameDto> games = [
    new (1, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26)),
    new (2, "Hades", "Rogue-like", 24.99M, new DateOnly(2020, 9, 17)),
    new (3, "The Witcher 3: Wild Hunt", "Action RPG", 39.99M, new DateOnly(2015, 5, 19))    
];


//Get all games
app.MapGet("/games", () => games);

//Get game by id/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName("GetGameEndpoint");

//POST a new game
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute("GetGameEndpoint", new { id = game.Id }, game);
});

//PUT update game by id/1
app.MapPut("/games/{id}", (int id, UpdateDto updatedGame) =>
{
    var index =  games.FindIndex(game => game.Id == id);
    
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
app.MapDelete("/games/{id}", (int id) =>
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

app.Run();

using GameStore.Api.Data;
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.Services.AddSqlite<GameStoreContext>("Data Source=gamestore.db");

var app = builder.Build();


app.MapGamesEndpoints();

app.Run();

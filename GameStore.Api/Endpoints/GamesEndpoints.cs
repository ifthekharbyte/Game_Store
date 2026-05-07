
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpoint = "GetGame";
        private static readonly List<GameDto> games = [
            new (1, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26)),
            new (2, "Hades", "Rogue-like", 24.99M, new DateOnly(2020, 9, 17)),
            new (3, "The Witcher 3: Wild Hunt", "Action RPG", 39.99M, new DateOnly(2015, 5, 19))    
        ];

    }
}

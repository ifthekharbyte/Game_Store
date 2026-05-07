namespace GameStore.Api.Dtos
{
    public record UpdateDto(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}

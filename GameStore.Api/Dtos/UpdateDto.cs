using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos
{
    public record UpdateDto(
    [Required] [StringLength(50)] string Name, 
    [Range(1, 50)] int GenreId,
    [Range(0, 100)] decimal Price,
    DateOnly ReleaseDate
    );
}

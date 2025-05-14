using skillJas.Application.DTOs;

public interface IFavoriteService
{
    Task<int> AddFavoriteAsync(string userId, int courseId);
    Task<bool> RemoveFavoriteAsync(string userId, int favoriteId);
    Task<IEnumerable<CourseDto>> GetFavoritesAsync(string userId);


}

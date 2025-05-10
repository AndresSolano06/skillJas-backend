using skillJas.Application.DTOs;

namespace skillJas.Application.Interfaces;

public interface IFavoriteService
{
    Task<List<FavoriteDto>> GetFavoritesByUserAsync(string userId);
    Task<int> AddToFavoritesAsync(string userId, AddFavoriteDto dto);
    Task<bool> RemoveFavoriteAsync(int favoriteId, string userId);
}

using Microsoft.EntityFrameworkCore;
using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;
using skillJas.Domain.Entities;

namespace skillJas.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ISkillJasDbContext _context;

        public FavoriteService(ISkillJasDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddFavoriteAsync(string userId, int courseId)
        {
            // Validar si el usuario existe
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                // Crear el usuario si no existe
                user = new User
                {
                    Id = userId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Validar si ya existe ese favorito (opcional para evitar duplicados)
            var exists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.CourseId == courseId);

            if (exists)
                throw new InvalidOperationException("Favorite already exists.");

            // Crear el favorito
            var favorite = new Favorite
            {
                UserId = userId,
                CourseId = courseId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return favorite.Id;
        }

        public async Task<bool> RemoveFavoriteAsync(string userId, int favoriteId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.Id == favoriteId && f.UserId == userId);

            if (favorite == null)
                return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetFavoritesAsync(string userId)
        {
            var favorites = await _context.Favorites
                .Include(f => f.Course) // Incluye navegación a Course si existe la relación
                .Where(f => f.UserId == userId)
                .ToListAsync();

            var result = favorites.Select(f => new CourseDto
            {
                Id = f.Course.Id,
                Title = f.Course.Title,
                Description = f.Course.Description,
                Category = f.Course.Category,
                CreatedDate = f.Course.CreatedDate,
                UpdatedDate = f.Course.UpdatedDate,
                IsActive = f.Course.IsActive,
                CourseUrl = f.Course.CourseUrl
            });

            return result;
        }

    }
}

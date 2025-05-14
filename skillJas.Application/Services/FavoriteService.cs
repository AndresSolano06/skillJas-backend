using Microsoft.EntityFrameworkCore;
using skillJas.Application.DTOs;

public class FavoriteService : IFavoriteService
{
    private readonly ISkillJasDbContext _context;

    public FavoriteService(ISkillJasDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddFavoriteAsync(string userId, int courseId)
    {
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);
        if (!courseExists)
            throw new ArgumentException($"Course with ID {courseId} does not exist.");

        var favoriteExists = await _context.Favorites.AnyAsync(f => f.UserId == userId && f.CourseId == courseId);
        if (favoriteExists)
            throw new InvalidOperationException("Favorite already exists.");

        var favorite = new Favorite
        {
            UserId = userId,
            CourseId = courseId
        };

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        return favorite.Id;
    }

    public async Task<bool> RemoveFavoriteAsync(string userId, int favoriteId)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.Id == favoriteId && f.UserId == userId);

        if (favorite == null) return false;

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CourseDto>> GetFavoritesAsync(string userId)
    {
        var favorites = await _context.Favorites
            .Include(f => f.Course)
            .Where(f => f.UserId == userId)
            .Select(f => new CourseDto
            {
                Id = f.Course.Id,
                Title = f.Course.Title,
                Description = f.Course.Description,
                Category = f.Course.Category,
                CourseUrl = f.Course.CourseUrl
            })
            .ToListAsync();

        return favorites;
    }


}

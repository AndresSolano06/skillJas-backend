namespace skillJas.Application.Interfaces;

public interface IAnalyticsService
{
    Task<int> GetTotalCoursesAsync();
    Task<int> GetTotalFavoritesAsync();
}

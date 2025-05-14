using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skillJas.Application.DTOs;
using skillJas.Application.Interfaces;

namespace skillJas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user token.");

            var favoriteId = await _favoriteService.AddFavoriteAsync(userId, dto.CourseId);
            return CreatedAtAction(nameof(AddFavorite), new { id = favoriteId }, favoriteId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFavorite(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user token.");

            var success = await _favoriteService.RemoveFavoriteAsync(userId, id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user token.");

            var favorites = await _favoriteService.GetFavoritesAsync(userId);
            return Ok(favorites);
        }

    }
}

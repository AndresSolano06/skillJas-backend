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
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user token.");

            try
            {
                var favoriteId = await _favoriteService.AddFavoriteAsync(userId, dto.CourseId);
                return CreatedAtAction(nameof(AddFavorite), new { id = favoriteId }, favoriteId);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFavorite(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user token.");
            try
            {

                var success = await _favoriteService.RemoveFavoriteAsync(userId, id);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid user token.");
            try
            {
                var favorites = await _favoriteService.GetFavoritesAsync(userId);
                return Ok(favorites);
            }
            catch (Exception)
            {

                return NoContent();
            }


        }
    }
}

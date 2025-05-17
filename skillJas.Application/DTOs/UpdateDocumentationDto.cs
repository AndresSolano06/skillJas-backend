
namespace skillJas.Application.DTOs
{
    public class UpdateDocumentationDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsActive { get; set; }
    }


}

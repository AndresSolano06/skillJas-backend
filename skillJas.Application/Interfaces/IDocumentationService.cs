using skillJas.Application.DTOs;


namespace skillJas.Application.Interfaces
{
    public interface IDocumentationService
    {
        Task<int> CreateAsync(CreateDocumentationDto dto);
        Task<List<DocumentationDto>> GetAllAsync();
    }

}

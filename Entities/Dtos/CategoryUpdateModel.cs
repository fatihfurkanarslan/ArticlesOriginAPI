using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; }
        public string Categoryname { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public IFormFile File { get; set; }
        public string PublicId { get; set; }
    }
}
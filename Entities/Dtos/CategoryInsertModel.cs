using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class CategoryInsertModel
    {
        public string Categoryname { get; set; }
        public string Description { get; set; }
        public string Photourl { get; set; }
        public IFormFile File { get; set; }
        public string PublicId { get; set; }

    }
}
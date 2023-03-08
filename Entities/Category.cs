using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Category name is a required for that field.")]
        [MaxLength(60, ErrorMessage ="Maximum length is 60 characters for category name")]
        public string Categoryname { get; set; }


        [Required(ErrorMessage = "Category description is a required for that field.")]
        [MaxLength(120, ErrorMessage = "Maximum length is 120 characters for category description")]
        public string Description { get; set; }
        public DateTime OnModified { get; set; }
        public string OnModifiedUsername { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
        public bool Deleted { get; set; }


        public IList<Note> Notes { get; set; }
    }
}

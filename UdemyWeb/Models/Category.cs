using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace UdemyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Obolebolerder")]
        [Range(1,100, ErrorMessage = "This field must be a number between 1 and 100")]
        public int DisplayOrder { get; set; }

        public Category()
        { 
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BlazorServerProject.Entities
{
    public class Document
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name is too Long")]
        [MinLength(1, ErrorMessage = "Name is too Short")]
        public string Name { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "The range is between 0 to 100")]
        public int StatusCode { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "The range is between 0 to 1")]
        public int Freeze { get; set; }

    }
}

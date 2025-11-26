using System.ComponentModel.DataAnnotations;

namespace Marry_Me.DTOs
{
    public class PersonDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [MaxLength(13)]
        public string? IdNumber { get; set; }
    }
}

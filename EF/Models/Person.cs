using System.ComponentModel.DataAnnotations;

namespace Marry_Me.EF.Models
{
public class Person
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [MaxLength(13)]
        public string? IdNumber { get; set; }

        public bool IsMarried { get; set; } = false;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Marriage>? MarriagesAsMale { get; set; }
        public ICollection<Marriage>? MarriagesAsFemale { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Marry_Me.EF.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Person>? CreatedPersons { get; set; }
        public ICollection<Marriage>? CreatedMarriages { get; set; }
        public ICollection<Divorce>? CreatedDivorces { get; set; }
    }
}



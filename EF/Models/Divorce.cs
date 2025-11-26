using System.ComponentModel.DataAnnotations;

namespace Marry_Me.EF.Models
{
    public class Divorce
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        public int? MarriageId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Marriage? Marriage { get; set; }
    }
}

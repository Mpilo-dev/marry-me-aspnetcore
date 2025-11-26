using System.ComponentModel.DataAnnotations;

namespace Marry_Me.EF.Models
{
    public class Marriage
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        [Required]
        public int FemaleId { get; set; }

        [Required]
        public int MaleId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Person Female { get; set; }
        public Person Male { get; set; }
        public Divorce Divorce { get; set; } 
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Marry_Me.DTOs
{
    public class MarriageDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FemaleId { get; set; }

        [Required]
        public int MaleId { get; set; }
    }

    
}
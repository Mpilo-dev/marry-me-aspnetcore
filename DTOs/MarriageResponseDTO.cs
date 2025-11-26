namespace Marry_Me.DTOs
{
    public class MarriageResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public SimplePersonDTO? Female { get; set; }
        public SimplePersonDTO? Male { get; set; }
    }

    public class SimplePersonDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

}


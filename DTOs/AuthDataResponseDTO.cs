namespace Marry_Me.DTOs
{
    public class AuthDataResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public object ? Data { get; set; }
        public string ? Token { get; set; }
    }
}


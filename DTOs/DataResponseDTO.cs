namespace Marry_Me.DTOs
{
    public class DataResponseDTO
    {
        public string? Message { get; set; }
        public bool IsSuccessful { get; set; }
        public object? Data { get; set; }
    }
}
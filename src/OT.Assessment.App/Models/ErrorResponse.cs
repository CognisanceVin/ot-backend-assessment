namespace OT.Assessment.App.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = default!;
        public string? Details { get; set; }
    }
}

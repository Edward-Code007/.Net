namespace tests.DTOs
{
    public class ResponseDTO<T>()
    {
        
        public required string Message { get; set; } 
        public required T Data { get; set; } 
    }
}

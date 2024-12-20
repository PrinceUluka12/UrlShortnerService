namespace Api.Models
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Successful";
        public object Result { get; set; }
    }
}

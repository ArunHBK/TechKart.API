namespace com.TechKart.CartApi.Models
{
    public class ResponseObject
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public object Value { get; set; }
    }
}

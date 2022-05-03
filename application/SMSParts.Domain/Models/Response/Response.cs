namespace SMSParts.Domain.Models.Response
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }

        public string Problem { get; set; }
        
        public bool IsValid => string.IsNullOrEmpty(Problem);
    }
}

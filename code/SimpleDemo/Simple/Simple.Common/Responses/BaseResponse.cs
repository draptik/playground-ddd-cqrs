namespace Simple.Common.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
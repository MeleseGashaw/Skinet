namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode ,string message=null)
        {
            StatusCode=statusCode;
            Message = message?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
    {
        400=>"A bad request ,you have made",
        401=>"Authorized ,you are not",
        404=>"Resource Found, it wasnot",
        500=>"Errors arethe path to the dark side",
        _ => null
    };
        }

        public int StatusCode { get; set; }
       public string Message { get; set; }
       
       
       
        
    }
}
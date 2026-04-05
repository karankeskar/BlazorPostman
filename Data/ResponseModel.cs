namespace BlazorPostman.Data
{
    public class ResponseModel
    {
        public int StatusCode{get; set;}
        public string Body{get; set;} = "";
        public Dictionary<string, string> Headers {get; set;} = new();
        public long ResponseTimeMs {get; set;}
        public bool IsSuccess {get; set;}
    }
}
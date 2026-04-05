namespace BlazorPostman.Data
{
    public class RequestModel
    {
        public string Id{get; set;} = Guid.NewGuid().ToString();
        public string Method{get; set;} = "GET";
        public string Url{get; set;} = "";
        public string Name{get; set;} = "";
        public string Body{get; set;} = "";
        public DateTime CreatedAt{get; set;} = DateTime.Now;
        public List<HeaderItem> Headers {get; set;} = new();
    }

    public class HeaderItem
    {
        public string Key{get; set;} = "";
        public string Value{get; set;} = "";
    }
}
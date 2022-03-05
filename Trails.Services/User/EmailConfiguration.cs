namespace Trails.Services.User
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string ApiKey { get; set; }

        public string ContactTo { get; set; }
    }
}

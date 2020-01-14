namespace DotNetCqrsApi.Host.Infrastructure.Authentication
{
    public class AzureOptions
    {
        public string Authority { get; set; }
        public string AppIdUri { get; set; }
        public string ClientId { get; set; }
    }
}
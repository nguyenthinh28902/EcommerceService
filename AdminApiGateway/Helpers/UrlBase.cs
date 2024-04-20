namespace AdminApiGateway.Helpers
{
    public class UrlBase
    {
        public string UrlName { get; set; }
        public string UrlValue { get; set; }
        public string PortName { get; set; }
        public int PortValue { get; set; }
    }
    public class UrlBaseSettings
    {
        List<UrlBase> UrlBases { get; set; } = new List<UrlBase>();
    }
    public class OcelotSettings
    {
      public List<string> OcelotNames { get; set; }= new List<string>();
    }
}

namespace EcommerceAuthenticationService.Helpers
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}

namespace AddsEats.Api.Configurarions
{
    public class JwtOptions
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        /// <summary>
        /// Secret used to sign JWTs. Configure it via Jwt:SigningKey.
        /// </summary>
        public string SigningKey { get; set; } = string.Empty;
        public int ExpiryMinutes { get; set; } = 30;
    }
}
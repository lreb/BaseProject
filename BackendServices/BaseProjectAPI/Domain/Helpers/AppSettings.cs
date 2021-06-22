namespace BaseProjectAPI.Domain.Helpers
{
    public class AppSettings
    {
        public JwtOptions JwtOptions { get; set; }
    }

    /// <summary>
    /// Defines JWT configure strongly typed settings objects
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Domain that generates JWT
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Secret private key to encode and decode JWT
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// Expiration in days
        /// </summary>
        public int ExpirationInDays { get; set; }
        /// <summary>
        /// Expiration in hours
        /// </summary>
        public int ExpirationInHours { get; set; }
    }
}

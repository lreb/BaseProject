using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BaseProjectAPI.Infraestructure.Extensions
{
    /// <summary>
    /// Custom environments extension
    /// </summary>
    public static class HostingEnvironmentExtension
    {
        /// <summary>
        /// Quality assurance environment
        /// </summary>
        private const string Staging = "Staging";
        /// <summary>
        /// Local environment
        /// </summary>
        private const string Local = "Local";

        /// <summary>
        ///  Checks if the current host environment name is quality assurance.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="IWebHostEnvironment"/>.</param>
        /// <returns>True if the environment name is quality assurance.</returns>
        public static bool IsStaging(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(Staging);
        }

        /// <summary>
        ///  Checks if the current host environment name is Local.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="IWebHostEnvironment"/>.</param>
        /// <returns>True if the environment name is local</returns>
        public static bool IsLocal(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(Local);
        }
    }
}

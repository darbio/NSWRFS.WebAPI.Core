namespace NSWRFS.WebAPI.Core.Models
{
    using System;
    using System.Reflection;

    public class HealthViewModel_GET
    {
        /// <summary>
        /// Overall health status. True if all downstream systems are healthy, False is any downstream systems are not healthy.
        /// </summary>
        public bool IsHealthy { get; set; }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
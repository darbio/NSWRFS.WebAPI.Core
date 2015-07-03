namespace NSWRFS.Base.Api.Models
{
    public class HealthViewModel_GET
    {
        /// <summary>
        /// Overall health status. True if all downstream systems are healthy, False is any downstream systems are not healthy.
        /// </summary>
        public bool IsHealthy { get; set; }
    }
}
#nullable enable
namespace Core
{
    public static class SystemInfo
    {
        /// <summary>
        /// Gets the CPU identifier string or "Unknown" if not available.
        /// </summary>
        public static string Cpu => Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") ?? "Unknown";

        /// <summary>
        /// Gets the total available memory size in gigabytes (GB).
        /// </summary>
        public static int MemorySize => (int) Math.Ceiling((double) GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1073741824.0);

        /// <summary>Gets the number of available processor threads.</summary>
        public static int Threads => Environment.ProcessorCount;

        /// <summary>Gets the operating system version string.</summary>
        public static string Os => Environment.OSVersion.VersionString;
    }
}
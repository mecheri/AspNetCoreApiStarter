using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AspNetCoreApiStarter
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">main arguments.</param>
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            host.Run();
        }

        /// <summary>
        /// Build Web Host.
        /// </summary>
        /// <param name="args">arguments.</param>
        /// <returns>Configured web host.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}

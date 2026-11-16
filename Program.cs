using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load .env only when it exists (local development).
            // Production should inject ConnectionStrings__DefaultConnection
            // as an environment variable from the hosting/deployment platform.
            var envFile = Path.Combine(Directory.GetCurrentDirectory(), ".env");
            if (File.Exists(envFile))
            {
                DotNetEnv.Env.Load(envFile);
            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

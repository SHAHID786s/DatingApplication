using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider; // allows us to get other classes/services 
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync(); // creates a  database and automatically creates / adds migration if db already created
                await Seed.SeedUsers(context); // add each user to db method from seed class

            }
            catch (Exception e)
            {
                var log = services.GetRequiredService<ILogger<Program>>();
                log.LogError(e, "Error during database migration ");

            }
            await host.RunAsync(); // we have to call ths to complete the function
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Cardfactory.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Cardfactory.API
{
    public class Program
    {
        private static readonly object scope;

        public static void Main(string[] args)
        {

            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>())
            {

                context.Database.EnsureCreated();
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}

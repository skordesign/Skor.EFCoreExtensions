using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skor.EFCoreExtensions.DI;
using Skor.EFCoreExtensions.Example.Models;
using Skor.EFCoreExtensions.Repositories;

namespace Skor.EFCoreExtensions.Example
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<ExampleDbContext>(options => {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Example;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }, ServiceLifetime.Transient);
            services.AddTransientRepository<Author, ExampleDbContext>();
            services.AddTransientRepository<Book, ExampleDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBaseRepository<Author> author)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
                await author.GetAllAsync();
                await author.GetAllAsync(f => f.Name == "Example");
                await author.AddAsync(new Author {  Name ="Example" }); // Without tracking
            });
        }
    }
}

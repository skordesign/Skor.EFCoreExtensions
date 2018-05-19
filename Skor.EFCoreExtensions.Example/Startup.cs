using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            services.AddEntityFrameworkSqlServer().AddDbContext<ExampleDbContext>(options =>
            {
                options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Example;Integrated Security=SSPI;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }, ServiceLifetime.Transient);
            services.AddTransientRepository<Author, ExampleDbContext>();
            services.AddTransientRepository<Book, ExampleDbContext>();
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBaseRepository<Author> author, ExampleDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            db.Database.EnsureCreated();
            if (author.GetAll().Count() == 0)
            {
                for (int i = 0; i < 10000; i++)
                {
                    author.Add(new Author { Name = Guid.NewGuid().ToString() });
                }
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

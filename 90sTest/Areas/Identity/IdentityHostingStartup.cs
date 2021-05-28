using System;
using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(_90sTest.Areas.Identity.IdentityHostingStartup))]
namespace _90sTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RetroNetContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("_90sTestContextConnection")));

                services.AddDefaultIdentity<RetroNetUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<RetroNetContext>();

                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            });
        }
    }
}
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
                    options.UseMySql(
                        context.Configuration.GetConnectionString("retronet-dev"), ServerVersion.AutoDetect(context.Configuration.GetConnectionString("retronet-dev"))));

                services.AddDefaultIdentity<RetroNetUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<RetroNetContext>();

                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            });
        }
    }
}
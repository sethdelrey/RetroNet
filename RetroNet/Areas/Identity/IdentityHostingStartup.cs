using System;
using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: HostingStartup(typeof(_90sTest.Areas.Identity.IdentityHostingStartup))]
namespace _90sTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IdentityHostingStartup() : base() { }
        public IdentityHostingStartup(IOptions<DBConfigOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public DBConfigOptions Options { get; } // set only via Secret Manager

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RetroNetContext>(options =>
                    options.UseMySql(
                        RDSHelper.GetRDSConnectionString(context.Configuration), ServerVersion.AutoDetect(RDSHelper.GetRDSConnectionString(context.Configuration))));

                services.AddDefaultIdentity<RetroNetUser>(options => 
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        options.User.RequireUniqueEmail = true;
                    })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<RetroNetContext>()
                    .AddDefaultTokenProviders();

                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            });
        }
    }
}
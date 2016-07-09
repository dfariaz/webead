using Hangfire;
using Hangfire.MySql;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Data;
using WebEAD.Application;

[assembly: OwinStartupAttribute(typeof(WebEAD.Startup))]
namespace WebEAD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            string connectionString = ConfigurationManager.AppSettings.Get("MYSQL_CONNECTION_STRING") ??
                   ConfigurationManager.ConnectionStrings["BDConnection"].ConnectionString;
            GlobalConfiguration.Configuration
                .UseStorage( new MySqlStorage (connectionString,
                new MySqlStorageOptions
                {
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),
                    PrepareSchemaIfNecessary = true,
                    DashboardJobListLimit = 50000,
                    TransactionTimeout = TimeSpan.FromMinutes(1),
                }));
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(
                () => JobApplication.PublishContent("GO"), Cron.Daily);
        }
    }
}

namespace SGDE.API.Configurations
{
    #region Using

    using System.Runtime.InteropServices;
    using Domain.DbInfo;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using DataEFCoreSQL;
    using DataEFCoreMySQL;
    using SGDE.Domain.Helpers;

    #endregion

    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = string.Empty;

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            switch(appSettings.DI) {
                case "SQL":
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        connection = configuration.GetConnectionString("SGDEContextSQL") ??
                                         "Data Source=WMAD01-014687\\SQLEXPRESS;Initial Catalog=People;Integrated Security=True;";
                    }

                    services.AddDbContextPool<EFContextSQL>(options => options.UseSqlServer(connection));
                    services.AddSingleton(new DbInfo(connection));
                    break;
                case "MySQL":
                    connection = configuration.GetConnectionString("SGDEContextMySQL");
                    services.AddDbContextPool<EFContextMySQL>(options => options.UseMySql(connection));
                    services.AddSingleton(new DbInfo(connection));
                    break;

                default:
                    connection = configuration.GetConnectionString("SGDEContextMySQL");
                    services.AddDbContextPool<EFContextMySQL>(options => options.UseMySql(connection));
                    services.AddSingleton(new DbInfo(connection));
                    break;
            }

            return services;
        }
    }
}
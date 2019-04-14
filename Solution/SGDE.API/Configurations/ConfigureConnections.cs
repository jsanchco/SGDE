namespace SGDE.API.Configurations
{
    #region Using

    using System.Runtime.InteropServices;
    using Domain.DbInfo;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using DataEFCoreSQL;

    #endregion

    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = string.Empty;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = configuration.GetConnectionString("SGDEContext") ??
                                 "Data Source=WMAD01-014687\\SQLEXPRESS;Initial Catalog=People;Integrated Security=True;";
            }
   
            services.AddDbContextPool<EFContext>(options => options.UseSqlServer(connection));

            services.AddSingleton(new DbInfo(connection));

            return services;
        }
    }
}
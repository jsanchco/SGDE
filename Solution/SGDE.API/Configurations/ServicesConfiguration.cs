namespace SGDE.API.Configurations
{
    #region Using

    using Domain.Repositories;
    using Domain.Supervisor;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Domain.Helpers;

    #endregion

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            switch (appSettings.DI)
            {
                case "SQL":
                    services
                        .AddScoped<IUserRepository, DataEFCoreSQL.Repositories.UserRepository>()
                        .AddScoped<IAddressRepository, DataEFCoreSQL.Repositories.AddressRepository>()
                        .AddScoped<IProfessionRepository, DataEFCoreSQL.Repositories.ProfessionRepository>();
                    break;
                case "MySQL":
                    services
                        .AddScoped<IUserRepository, DataEFCoreMySQL.Repositories.UserRepository>()
                        .AddScoped<IAddressRepository, DataEFCoreMySQL.Repositories.AddressRepository>()
                        .AddScoped<IProfessionRepository, DataEFCoreMySQL.Repositories.ProfessionRepository>();
                    break;

                default:
                    services
                        .AddScoped<IUserRepository, DataEFCoreMySQL.Repositories.UserRepository>()
                        .AddScoped<IAddressRepository, DataEFCoreMySQL.Repositories.AddressRepository>()
                        .AddScoped<IProfessionRepository, DataEFCoreMySQL.Repositories.ProfessionRepository>();
                    break;
            }

            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<ISupervisor, Supervisor>();

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            });

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<AppSettings>();

            return services;
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestionDocente.Application.Interfaces;
using System.Reflection;
using GestionDocente.Application.Services;
using FluentValidation;

namespace GestionDocente.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();

            return services;
        }
    }
}

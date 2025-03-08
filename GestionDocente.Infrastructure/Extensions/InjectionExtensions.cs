﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestionDocente.Infrastructure.Persistences.Context;
using GestionDocente.Infrastructure.Persistences.Repositories;
using GestionDocente.Infrastructure.Models;
using System.Reflection;
using GestionDocente.Domain.Interfaces;
using GestionDocente.Infrastructure.Settings;

namespace GestionDocente.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var passwordSettings = new PasswordSettings();
            configuration.GetSection("PasswordSettings").Bind(passwordSettings);

            var lockoutSettings = new LockoutSettings();
            configuration.GetSection("LockoutSettings").Bind(lockoutSettings);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("IdentityDB_Connection")));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<IdentityOptions>(options =>
            {                 // Password settings
                options.Password.RequireDigit = passwordSettings.RequireDigit;
                options.Password.RequiredLength = passwordSettings.RequiredLength;
                options.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = passwordSettings.RequireUppercase;
                options.Password.RequireLowercase = passwordSettings.RequireLowercase;
                options.Password.RequiredUniqueChars = passwordSettings.RequiredUniqueChars;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettings.DefaultLockoutTimeSpan);
                options.Lockout.MaxFailedAccessAttempts = lockoutSettings.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = lockoutSettings.AllowedForNewUsers;
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddIdentity<ApplicationUserModel, ApplicationRoleModel>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            return services;
        }
    }
}

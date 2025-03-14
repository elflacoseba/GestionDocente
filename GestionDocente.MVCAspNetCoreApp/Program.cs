using GestionDocente.MVCAspNetCoreApp.Interfaces;
using GestionDocente.MVCAspNetCoreApp.Services;
using Polly;
using Polly.Extensions.Http;

using System.Net.Http.Headers;

namespace GestionDocente.MVCAspNetCoreApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Registrar IUserService
            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetCircuitBreakerPolicy());

            // Políticas de resiliencia
            static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .Or<TimeoutException>()
                    .WaitAndRetryAsync(3, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            }

            static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}

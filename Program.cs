using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Repositories;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using AspNetCoreHero.ToastNotification;
using System.Configuration;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;

namespace Dental_Clinic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var ConnStr = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(ConnStr));
            builder.Services.AddHangfireServer();
            builder.Services.AddDbContext<AppDbContext>(o=>o.UseSqlServer(ConnStr));
            builder.Services.AddScoped<INotyfService, NotyfService>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            builder.Services.AddScoped<IMedicalHistoryRepo, MedicalHistoryRepo>();
            builder.Services.AddScoped<IDentalHistoryRepo, DentalHistoryRepo>();
            builder.Services.AddAutoMapper(typeof(Program));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
            app.UseHangfireDashboard("/Dashboard");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Patients}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

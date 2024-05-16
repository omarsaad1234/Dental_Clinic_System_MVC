using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Repositories;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Dental_Clinic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var ConnStr = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<AppDbContext>(o=>o.UseSqlServer(ConnStr));
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            builder.Services.AddScoped<IMedicalHistoryRepo, MedicalHistoryRepo>();
            builder.Services.AddScoped<IDentalHistoryRepo, DentalHistoryRepo>();
            builder.Services.AddScoped<IImageRepo, ImageRepo>();
            builder.Services.AddAutoMapper(typeof(Program));

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
                {
                    ProgressBar = true,
                    PositionClass = ToastPositions.BottomRight,
                    TimeOut=10000,
                    CloseButton=true
                }
            );

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
            app.UseNToastNotify();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Patients}/{action=Index}/{id?}");

            app.Run();
        }
    }
}


using MedicalAppointmentBookingSystem.configurations;
using MedicalAppointmentBookingSystem.Repository;
using MedicalAppointmentBookingSystem.Security;
using MedicalAppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MedicalAppointmentBookingSystem.Validators;
using FluentValidation;
using FluentValidation.AspNetCore; // Add this using directive



namespace MedicalAppointmentBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IloginRepository, LoginRepository>(); 
            builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IPatientRepository,PatientRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<PatientService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<LoginService>();

            builder.Services.AddScoped(
                provider =>
                {
                    return new TokenService(jwtOptions);
                }
              ); 


            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions?.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Signingkey))

                };
            });


            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddFluentValidationAutoValidation(); 
            builder.Services.AddValidatorsFromAssemblyContaining<DoctorValidator>(); 

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            
            app.UseAuthorization();
           
            app.UseHttpsRedirection();



            app.MapControllers();

            app.Run();
        }
    }
}

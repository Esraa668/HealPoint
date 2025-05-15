using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Bussiness.Profiles;
using Project.Bussiness.Services.AttachmentService;
using Project.Bussiness.Services.Classes;
using Project.Bussiness.Services.Interfaces;
using Project.DataAccess.Data.Contexts;
using Project.DataAccess.Models.IdentityModel;
using Project.DataAccess.Repositories.Classes;
using Project.DataAccess.Repositories.Interfaces;

namespace Project.presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); //Enable AntiForgeryToken
            });

            //builder.Services.AddScoped<ApplicationDbContext>(); //Register to Service in DI Container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies(); //Enable Lazy Loading
            }, ServiceLifetime.Scoped);

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //Register to Service in DI Container.
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //Register to Service in DI Container.
            builder.Services.AddScoped<IDepartmentService, DepartmentService>(); //Register to Service in DI Container.
            builder.Services.AddScoped<IEmployeeService, EmployeeService>(); //Register to Service in DI Container.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //Register to Service in DI Container.
            builder.Services.AddScoped<IAttachmentService, AttachmentService>(); //Register to Service in DI Container.

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //Require Unique Email
                options.Password.RequireLowercase = true; //Require Lowercase
                options.Password.RequireUppercase = true; //Require Uppercase
                //options.Password.RequireDigit = true; //Require Digit
                options.Password.RequiredLength = 6; //Required Length
                //options.Password.RequireNonAlphanumeric = false; //Require Non Alphanumeric
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly); //Register to Service in DI Container.
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            #endregion             
            var app = builder.Build();

            #region Configure the HTTP request pipeline

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            #endregion

            app.Run();
        }
    }
}

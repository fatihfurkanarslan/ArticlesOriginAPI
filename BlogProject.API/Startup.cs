using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.AbstractManager;
using BusinessLayer.ConcreteManager;
using DataAccessLayer;
using DataAccessLayer.AbstractRepositories;
using DataAccessLayer.UnitOfWork;
using Entities;
using Helper;
using Helper.ErrorHandler;
using Helper.Filters;
using Helper.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;


namespace BlogProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //support on .net core 2.2
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //   .AddJsonOptions(option => {
            //       option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //   });

            services.AddMvc()
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
                );


            string connection = @"Server=BIM-FURKANA1\SQLEXPRESS;Database=BlogProject;Trusted_Connection=True;MultipleActiveResultSets=true";
            //db connection
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection,
                b => b.MigrationsAssembly("BlogProject.API")
                ));

            //user settings
            services.AddIdentity<User, IdentityRole>(
             user =>
             {
                 user.Password.RequireDigit = true;
                 user.Password.RequireLowercase = false;
                 user.Password.RequireUppercase = false;
                 user.Password.RequireNonAlphanumeric = false;
                 user.Password.RequiredLength = 10;
                 user.User.RequireUniqueEmail = true;
             }).AddEntityFrameworkStores<BlogContext>();


            //register response caching in the IOC 
            services.AddResponseCaching();

            //register marvin.cache.header service support like cache-control, expires, etag, and last-modified
            services.AddHttpCacheHeaders();
            //created for dependency injection

            //services.AddAutoMapper();

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                //cache duration setter 
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
            });
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<Tag>, Repository<Tag>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Note>, Repository<Note>>();
            services.AddScoped<IRepository<Photo>, Repository<Photo>>();
            services.AddScoped<IRepository<Like>, Repository<Like>>();
            services.AddScoped<IRepository<Follower>, Repository<Follower>>();
            //LoggerManager includes Nlog Ilogger object instance.
            services.AddScoped<ILoggerManager, LoggerManager>();
            //unitofwork scoped is added here.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //filter class for validation
            services.AddScoped<ValidationFilterAttribute>();

            //services.AddScoped(typeof(CommentManager));
            services.AddScoped<ICommentManager, CommentManager>();  

            services.AddScoped(typeof(UserManager));
            services.AddScoped(typeof(TagManager));
            services.AddScoped(typeof(CategoryManager));
            services.AddScoped(typeof(NoteManager));
            services.AddScoped(typeof(PhotoManager));
            services.AddScoped(typeof(LikeManager));
            services.AddScoped(typeof(CommentManager));
            services.AddScoped(typeof(FollowerManager));
            services.AddSingleton(typeof(MailHelper));
            services.AddScoped(typeof(BlogContext));
            services.AddTransient<MyInitiliazer>();


            //signalr
            services.AddSignalR();



            // appsettings den okumak için startup da configure etmek gerekiyor.
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));


         
            services.Configure<MailSettings>(Configuration.GetSection("MailHelperSettings"));



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyInitiliazer seeder, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //ConfigureExceptionHandler method is prepared in ErrorHandler folder to catch error and log them.
            app.ConfigureExceptionHandler(logger);
            seeder.Seed();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        
            //app.UseHttpsRedirection();
            //useauthentication methodu sayesinde
            app.UseAuthentication();

            //to use response caching(to use response-caching attribute)
            app.UseResponseCaching();

            //marvin.cache.header supports like cache-control, expires, etag, last-modified
            app.UseHttpCacheHeaders();

            //support on .core 2.2
            //app.UseMvc();

            //instead of app.UseMvc()
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Burada NotificationHub gelen isteklerin yönetildiği sınıfı /notification ise sunucu adresini ifade eder.
                endpoints.MapHub<NotificationHub>("/notification");
            }
           ) ;

            
            //signalr maphub
            //app.MapHub<NotificationHub>("/Follower");
        }
    }
}

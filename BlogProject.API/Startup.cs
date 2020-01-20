using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using DataAccessLayer;
using Entities;
using Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;



namespace BlogProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
              services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddJsonOptions(option => {
                     option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                 });

            //string connection = @"Server=DESKTOP-LDVGTNI\SQLEXPRESS;Database=BlogProject;Trusted_Connection=True;MultipleActiveResultSets=true";
            string connection = @"Server=DESKTOP-LDVGTNI\SQLEXPRESS;Database=BlogProject;Trusted_Connection=True;";
            //db connection
            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connection, b=> b.MigrationsAssembly("BlogProject.API")));
             services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
             services.Configure<MailSettings>(Configuration.GetSection("MailHelperSettings"));
            //services.BuildServiceProvider().GetService<BlogContext>().Database.Migrate();
           
            //created for dependency injection
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Note>, Repository<Note>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<Like>, Repository<Like>>();
            services.AddScoped<IRepository<Photo>, Repository<Photo>>();
            services.AddScoped<IRepository<Tag>, Repository<Tag>>();

            //
            //services.AddScoped(typeof(IHostingEnvironment));

            services.AddScoped(typeof(UserManager));
            services.AddScoped(typeof(CategoryManager));
            services.AddScoped(typeof(NoteManager));
            services.AddScoped(typeof(CommentManager));
            services.AddScoped(typeof(LikeManager));
            services.AddScoped(typeof(PhotoManager));
            services.AddScoped(typeof(TagManager));
            services.AddScoped(typeof(BlogContext));

            services.AddScoped(typeof(MailHelper));
            //added automapper
            services.AddMvc();
            services.AddAutoMapper();
            //Mapper.Initialize(cfg => cfg.AddProfile<AutomapperHelper>());

            services.AddTransient<MyInitiliazer>();
          
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MyInitiliazer seeder)
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
            // seeder.Seed();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseHttpsRedirection();
            //useauthentication methodu sayesinde
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

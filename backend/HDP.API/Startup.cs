using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAFA.Domain.Usuario;
using HDP.Application.Services.Contracts;
using HDP.Application.Services.Implementations;
using HDP.Persistence;
using HDP.Persistence.Repository.Contracts;
using HDP.Persistence.Repository.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HDP.API
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
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<ITutorRepository, TutorRepository>();

            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            // services.AddScoped<IMaterialService,MaterialService>();
            // services.AddScoped<IMaterialRepository, MaterialRepository>();


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HDPContext>()
                .AddDefaultTokenProviders();


            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);

            services.AddDbContext<HDPContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("HDPDBConnectionString")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddControllers(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = (context =>
                        new BadRequestObjectResult(context.ModelState.Values.SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)));
                });

            // IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            // {
            //     options.Password.RequireDigit = true;
            //     options.Password.RequireNonAlphanumeric = false;
            //     options.Password.RequireLowercase = false;
            //     options.Password.RequireUppercase = false;
            //     options.Password.RequiredLength = 8;
            //     options.Lockout.AllowedForNewUsers = true;
            //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //     options.Lockout.MaxFailedAccessAttempts = 3;
            // });
            // builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            // builder.AddEntityFrameworkStores<HDPContext>();
            // builder.AddRoleValidator<RoleValidator<IdentityRole>>();
            // builder.AddRoleManager<RoleManager<IdentityRole>>();
            // builder.AddSignInManager<SignInManager<User>>();
            // services.ConfigureApplicationCookie(options =>
            // {
            //     options.LoginPath = new PathString("/account/login");
            //     options.AccessDeniedPath = new PathString("/account/login");
            //
            //     options.Events.OnRedirectToLogin = context =>
            //     {
            //         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //         return Task.CompletedTask;
            //     };
            // });
            // services.AddAuthentication(x =>
            //     {
            //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddJwtBearer(x =>
            //     {
            //         x.RequireHttpsMetadata = false;
            //         x.SaveToken = true;
            //         x.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(key),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HDP.API", Version = "v1" }); });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (env.IsDevelopment())
            {
                app.UseCors(opt =>
                    {
                        opt.AllowAnyHeader();
                        opt.AllowAnyOrigin();
                        opt.AllowAnyMethod();
                    }
                );
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDP.API"));
            }
            else
            {
                app.UseHsts();
                app.UseCors(opt =>
                    {
                        opt.AllowAnyHeader();
                        opt.AllowAnyOrigin();
                        opt.AllowAnyMethod();
                    }
                );
            }

            app.UseAuthentication();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PassportApi.Data;
using PassportApi.interfaces;
using PassportApi.Models;
using PassportApi.Repositories;
using PassportApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PassportApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<PassportDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("PassportConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                option.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                option.MapType<IFormFile>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "file",
                    Properties = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiSchema>()
                    {
                        { "Content", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string", Format = "binary" } }
                    }
                });
            });

            //JWt
            builder.Services.AddIdentity<PassportUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;

            })
                .AddEntityFrameworkStores<PassportDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignOutScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
                            )

                    };

                });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IMasterDetailsRepository,MasterDetailsRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped(typeof(IRootRepository<>), typeof(RootRepository<>));
            builder.Services.AddTransient<IEmailSender, EmailSender>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder => builder
                        .WithOrigins("http://localhost:4200") // Angular app URL
                        .AllowAnyMethod()
                        .AllowAnyHeader().AllowCredentials());
            });

            
            //builder.Services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = 104857600; // 100 MB limit, adjust as needed
            //});

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<PassportUser>>();

                await SeedRolesAndAdminUser(roleManager, userManager);
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            

            app.UseHttpsRedirection();

            app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials()
                  //.WithOrigins("https://localhost:44351))
                  .SetIsOriginAllowed(origin => true));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAngularApp");
            app.MapControllers();

            app.Run();
        }

        private static async Task SeedRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<PassportUser> userManager)
        {
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create an Admin user if it doesn't already exist
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new PassportUser
                {
                    UserName = "Admin",
                    Email = adminEmail
                };

                var result = await userManager.CreateAsync(adminUser, "Pass@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

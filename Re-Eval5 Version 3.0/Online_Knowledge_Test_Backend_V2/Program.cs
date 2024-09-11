
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Online_Knowledge_Test_Backend_V2.Data;
using Online_Knowledge_Test_Backend_V2.Mappings;
using Online_Knowledge_Test_Backend_V2.Models;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Implementation;
using Online_Knowledge_Test_Backend_V2.RepositoryLayer.Interfaces;
using Online_Knowledge_Test_Backend_V2.Services.Implementation;
using Online_Knowledge_Test_Backend_V2.Services.Interfaces;
using System.Text;

namespace Online_Knowledge_Test_Backend_V2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container

            //add connection string
            builder.Services.AddDbContext<ExamDbContext>(options =>
                            options.UseSqlServer(builder.Configuration
                                .GetConnectionString("OnlineExamConnection")));

            builder.Services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<ExamDbContext>()
                            .AddDefaultTokenProviders();

            // register the repositories in the pipeline
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

            builder.Services.AddScoped<IAuthService, AuthService>();

            // Register repositories and services

            builder.Services.AddScoped<IExamRepository, ExamRepository>();
            builder.Services.AddScoped<IExamService, ExamService>();

            builder.Services.AddScoped<IExamResultRepository, ExamResultRepository>();
            builder.Services.AddScoped<IExamResultService, ExamResultService>();

            builder.Services.AddScoped<IOptionRepository, OptionRepository>();
            builder.Services.AddScoped<IOptionService, OptionService>();

            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();

            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IReportService, ReportService>();

            builder.Services.AddScoped<ISectionActionRepository, SectionActionRepository>();
            builder.Services.AddScoped<ISectionActionService, SectionActionService>();

            builder.Services.AddScoped<ISectionRepository, SectionRepository>();
            builder.Services.AddScoped<ISectionService, SectionService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
   
            //registering and adding the authentication in the pipeline
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    
                   
                };
            }
            );

            // add cors policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOnlineExamApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            //register automapper class
            builder.Services.AddAutoMapper(typeof(AutoMapperClass));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // adding the authentication for swagger 
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Passport Web Application API", Version = "v1" });

                // Add JWT Authentication support to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",
                });
                //Add the security requirement for OpenApi 
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Seed the roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedAdminRoleAndUser(userManager, roleManager);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.UseAuthorization();

            app.UseCors("AllowOnlineExamApp");


            app.MapControllers();

            app.Run();
        }

        //Helper method to seed the admin role
        private static async Task SeedAdminRoleAndUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Define the admin role name
            var adminRole = "Admin";

            // Create the Admin role if it doesn't exist
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Seed an admin user
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new User
                {
                    Fullname = "Administrator",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    MobileNo = "1234567890",
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, "Pass@123"); // password

                if (result.Succeeded)
                {
                    // Add user to Admin role
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
            else
            {
                // Ensure the admin user is in the Admin role
                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}

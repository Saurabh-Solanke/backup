using Microsoft.EntityFrameworkCore;
using PassPortal_API.Data;
using PassPortal_API.Repositories;
using PassPortal_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Define CORS policy to allow localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Allow only localhost:4200
                   .AllowAnyHeader() // Allow any header
                   .AllowAnyMethod(); // Allow any method (GET, POST, PUT, DELETE, etc.)
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PassportOfficeService>();

builder.Services.AddScoped<PassportOfficeRepository>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PassPortal API", Version = "v1" });
    // Additional configuration for file upload
    options.OperationFilter<SwaggerFileUploadOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Enable Swagger middleware
    app.UseSwaggerUI(); // Enable Swagger UI middleware
}

app.UseHttpsRedirection();
app.UseRouting();

// Apply the CORS policy
app.UseCors("AllowOrigin");

app.UseAuthorization();
app.MapControllers();
app.Run();

-Packaages to install

Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.SqlServer
Newtonsoft.Json

Newtonsoft.Json
Microsoft.AspNetCore.Mvc.NewtonsoftJson
=> builder.Services.AddControllers()
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
     });
=>This method adds the services required to use controllers in your ASP.NET Core application.
 Controllers are classes that handle HTTP requests, typically associated with routes and actions 
that respond to those requests.

This method adds support for using the Newtonsoft.Json JSON serializer instead of the default 
System.Text.Json serializer provided by ASP.NET Core. 
Newtonsoft.Json is a popular and widely-used library for handling JSON in .NET.

This setting is part of the JsonSerializerSettings class provided by Newtonsoft.Json.
 The ReferenceLoopHandling property controls how the serializer handles reference loops.

When ReferenceLoopHandling.Ignore is set, the serializer will ignore objects in a reference loop.
 This prevents serialization errors or infinite loops when objects reference each other circularly.

Why This Configuration is Used
Preventing Circular References: If your data model contains circular references (e.g., an Employee class 
has a reference to a Department, and the Department class has a reference back to the Employee), serializing
 these objects could lead to an infinite loop. By setting ReferenceLoopHandling.Ignore, Newtonsoft.Json will 
prevent such issues by not serializing properties that would create a loop.

Using Newtonsoft.Json: While ASP.NET Core uses System.Text.Json by default, some developers prefer using
 Newtonsoft.Json due to its maturity, flexibility, and additional features. The AddNewtonsoftJson method 
allows you to configure your application to use Newtonsoft.Json instead of the default serializer.

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Employee> Employees { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Department Department { get; set; }
}

Without ReferenceLoopHandling.Ignore, trying to serialize an Employee object that references a 
Department object (which in turn references the Employee) would result in a circular reference, 
potentially causing an exception or an infinite loop during serialization.
With this setting, the circular reference is ignored, and the object is serialized without issues.



-Configure First Identity and JWT:-

-  public class PassportUser :IdentityUser
  {
  }

-
 public class PassportDbContext : IdentityDbContext<PassportUser>
{
    public PassportDbContext(DbContextOptions options) : base(options)
    {

       
    }
}

-builder.Services.AddDbContext<PassportDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PassportConnection"));
});

-
"ConnectionStrings": {
  "PassportConnection": "Data Source=DESKTOP-5TA0H0L\\SQLEXPRESS;Initial Catalog=PassportDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
}
-
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
});



- //JWt
 builder.Services.AddIdentity<PassportUser, IdentityRole>(options =>
 {
     options.Password.RequireDigit = true;
     options.Password.RequireNonAlphanumeric = true;
     options.Password.RequireUppercase= true;
     options.Password.RequireLowercase= true;
     options.Password.RequiredLength = 12;
 
 })
     .AddEntityFrameworkStores<PassportDbContext> ();

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
         options.TokenValidationParameters = new TokenValidationParameters { 
             ValidateIssuer= true,
             ValidIssuer = builder.Configuration["JWT:Issuer"],
             ValidateAudience= true,
             ValidAudience = builder.Configuration["JWT:Audience"],
             ValidateIssuerSigningKey= true,
             IssuerSigningKey = new SymmetricSecurityKey(
                 System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
                 )

         };

     });


-
"Jwt": {
  "SigningKey": "sdgfijjh3466iu345g87g08c24g7204gr803g30587ghh35807fg39074fvg80493745gf082b507807g807fgf",
  "Issuer": "http://localhost:5246",
  "Audience": "http://localhost:5246",
  "DurationInMinutes": 120
},



-
User Manager:-
-register/login

-TokenService-ITokenService
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
    }

    public string CreateToken(PassportUser passUser)
    {
        var claims = new List<Claim> { 
            new Claim(JwtRegisteredClaimNames.Email,passUser.Email),
            new Claim(JwtRegisteredClaimNames.GivenName,passUser.UserName)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(3),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]

        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token =tokenHandler.CreateToken(tokenDescriptor); ;//till here it will make aa obj represenation of token

        return tokenHandler.WriteToken(token);//string representatiton of token
    }
}

-AuthController
-CreateAsync(user,"password")----> hash and salted
 [Route("api/auth")]
 [ApiController]
 public class AuthController :ControllerBase
 {
     private readonly UserManager<PassportUser> _userManager;
     private readonly ITokenService _tokenService;
     private readonly SignInManager<PassportUser> _signinManager;
     public AuthController(UserManager<PassportUser> userManager, ITokenService tokenService, SignInManager<PassportUser> signInManager)
     {
         _userManager = userManager;
         _tokenService = tokenService;
         _signinManager = signInManager;
     }

     [HttpPost("register")]
     public async Task<IActionResult> ResiterUser([FromBody] RegisterDto registerDto)
     {
         try
         {
             if (!ModelState.IsValid)
                 return BadRequest(ModelState);

             var passUser = new PassportUser
             {
                 UserName = registerDto.Name,

                 Email = registerDto.Email,
             };

             var createdUser = await _userManager.CreateAsync(passUser, registerDto.Password);

             if (createdUser.Succeeded)
             {
                 var roleResult = await _userManager.AddToRoleAsync(passUser, "User");
                 if (roleResult.Succeeded)
                 {
                     return Ok(
                         new NewUserDto
                         {
                             Name = passUser.UserName,
                             Email = passUser.Email,
                             Token = _tokenService.CreateToken(passUser)
                         }
                         );
                 }
                 else
                 {
                     return StatusCode(500, roleResult.Errors);
                 }

             }
             else
             {
                 return StatusCode(500, createdUser.Errors);
             }
         }catch (Exception e)
         {
             return StatusCode(500, e);
         }
     }

     [HttpPost("login")]
     public async Task<IActionResult> Login(LoginDto loginDto)
     {
         if (!ModelState.IsValid)
             return BadRequest(ModelState);

         var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

         if (user == null) return Unauthorized("Invalid Email!");

         var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

         if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

         return Ok(
             new NewUserDto
             {
                 Name = user.UserName,
                 Email = user.Email,
                 Token = _tokenService.CreateToken(user)
             }
         );
     }

 }


-builder.Services.AddScoped<ITokenService, TokenService>();

-
Add-migration identity
Update-database



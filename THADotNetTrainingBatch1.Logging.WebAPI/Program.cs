using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Auth;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Student;



// Disable default claim mapping
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("StudentDb"));

// Register Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configure JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSecretKeyShouldBeAtLeast32CharsLong!!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                System.Diagnostics.Debug.WriteLine("JWT Auth Failed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                System.Diagnostics.Debug.WriteLine("JWT Auth Success!");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();

    app.MapSwagger("/openapi/{documentName}.json");

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData(context);
}

app.MapControllers();

app.Run();

void SeedData(AppDbContext context)
{
    if (context.Students.Any()) return;

    // Seed Students
    context.Students.AddRange(
 new Student { StudentName = "Alice", StudentEmail = "alice@example.com", StudentAge = 20 },
    new Student { StudentName = "Bob", StudentEmail = "bob@example.com", StudentAge = 22 },
    new Student { StudentName = "Charlie", StudentEmail = "charlie@example.com", StudentAge = 19 },
    new Student { StudentName = "Diana", StudentEmail = "diana@example.com", StudentAge = 21 },
    new Student { StudentName = "Ethan", StudentEmail = "ethan@example.com", StudentAge = 23 },
    new Student { StudentName = "Fiona", StudentEmail = "fiona@example.com", StudentAge = 18 },
    new Student { StudentName = "George", StudentEmail = "george@example.com", StudentAge = 24 },
    new Student { StudentName = "Hannah", StudentEmail = "hannah@example.com", StudentAge = 20 },
    new Student { StudentName = "Ian", StudentEmail = "ian@example.com", StudentAge = 22 },
    new Student { StudentName = "Julia", StudentEmail = "julia@example.com", StudentAge = 25 },
    new Student { StudentName = "Kevin", StudentEmail = "kevin@example.com", StudentAge = 21 },
    new Student { StudentName = "Laura", StudentEmail = "laura@example.com", StudentAge = 19 },
    new Student { StudentName = "Michael", StudentEmail = "michael@example.com", StudentAge = 23 },
    new Student { StudentName = "Nina", StudentEmail = "nina@example.com", StudentAge = 20 },
    new Student { StudentName = "Oliver", StudentEmail = "oliver@example.com", StudentAge = 22 },
    new Student { StudentName = "Paula", StudentEmail = "paula@example.com", StudentAge = 18 },
    new Student { StudentName = "Quinn", StudentEmail = "quinn@example.com", StudentAge = 24 },
    new Student { StudentName = "Rachel", StudentEmail = "rachel@example.com", StudentAge = 21 },
    new Student { StudentName = "Sam", StudentEmail = "sam@example.com", StudentAge = 25 },
    new Student { StudentName = "Tina", StudentEmail = "tina@example.com", StudentAge = 19 },
    new Student { StudentName = "Umar", StudentEmail = "umar@example.com", StudentAge = 23 },
    new Student { StudentName = "Vanessa", StudentEmail = "vanessa@example.com", StudentAge = 20 },
    new Student { StudentName = "William", StudentEmail = "william@example.com", StudentAge = 22 },
    new Student { StudentName = "Xavier", StudentEmail = "xavier@example.com", StudentAge = 18 },
    new Student { StudentName = "Yasmine", StudentEmail = "yasmine@example.com", StudentAge = 24 },
    new Student { StudentName = "Zachary", StudentEmail = "zachary@example.com", StudentAge = 21 },
    new Student { StudentName = "Amber", StudentEmail = "amber@example.com", StudentAge = 20 },
    new Student { StudentName = "Brian", StudentEmail = "brian@example.com", StudentAge = 23 },
    new Student { StudentName = "Chloe", StudentEmail = "chloe@example.com", StudentAge = 19 },
    new Student { StudentName = "Daniel", StudentEmail = "daniel@example.com", StudentAge = 22 }
    );

    // Seed Permissions
    var p1 = new Permission { Menu = "Student", Action = "Read" };
    var p2 = new Permission { Menu = "Student", Action = "Create" };
    var p3 = new Permission { Menu = "Student", Action = "Update" };
    var p4 = new Permission { Menu = "Student", Action = "Delete" };
    context.Permissions.AddRange(p1, p2, p3, p4);

    // Seed Roles
    var adminRole = new Role { RoleName = "Admin" };
    var staffRole = new Role { RoleName = "Staff" };
    context.Roles.AddRange(adminRole, staffRole);
    context.SaveChanges();

    // Role-Permissions
    context.RolePermissions.AddRange(
        new RolePermission { RoleId = adminRole.Id, PermissionId = p1.Id },
        new RolePermission { RoleId = adminRole.Id, PermissionId = p2.Id },
        new RolePermission { RoleId = adminRole.Id, PermissionId = p3.Id },
        new RolePermission { RoleId = adminRole.Id, PermissionId = p4.Id },
        new RolePermission { RoleId = staffRole.Id, PermissionId = p1.Id }
    );

    // Users
    var adminUser = new User { Username = "admin", Password = "123" };
    var staffUser = new User { Username = "staff", Password = "123" };
    context.Users.AddRange(adminUser, staffUser);
    context.SaveChanges();

    // User-Roles
    context.UserRoles.AddRange(
        new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id },
        new UserRole { UserId = staffUser.Id, RoleId = staffRole.Id }
    );

    context.SaveChanges();
}


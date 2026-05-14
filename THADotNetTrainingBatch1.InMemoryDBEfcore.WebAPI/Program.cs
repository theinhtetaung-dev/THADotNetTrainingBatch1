using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Auth;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Student;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("StudentDB"));
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AuthService>();
var jwtKey = builder.Configuration["Jwt:Key"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)
        )
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StudentView", policy =>
        policy.RequireClaim("permission", "Student.View"));

    options.AddPolicy("StudentCreate", policy =>
        policy.RequireClaim("permission", "Student.Create"));

    options.AddPolicy("StudentUpdate", policy =>
        policy.RequireClaim("permission", "Student.Update"));

    options.AddPolicy("StudentDelete", policy =>
        policy.RequireClaim("permission", "Student.Delete"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference();

}





app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Users.Any())
    {
        context.Users.AddRange(new Tbl_User[]
        {
            new Tbl_User 
            { 
                UserName = "admin", 
                Password = "123", 
                Role = "admin", 
                Permissions = new List<string> { "Student.View", "Student.Create","Student.Update","Student.Delete" } 
            },
            new Tbl_User 
            { 
                UserName = "staff", 
                Password = "123", 
                Role = "staff", 
                Permissions = new List<string> { "Student.View"} 
            }
        });
    }

    if (!context.Students.Any())
    {
        context.Students.AddRange(new Tbl_Students[]
        {
            new Tbl_Students { Name = "Alice", Email = "alice@example.com" },
            new Tbl_Students { Name = "Bob", Email = "bob@example.com" },
            new Tbl_Students { Name = "Charlie", Email = "charlie@example.com" },
            new Tbl_Students { Name = "David", Email = "david@example.com" },
            new Tbl_Students { Name = "Emma", Email = "emma@example.com" },
            new Tbl_Students { Name = "Frank", Email = "frank@example.com" },
            new Tbl_Students { Name = "Grace", Email = "grace@example.com" },
            new Tbl_Students { Name = "Henry", Email = "henry@example.com" },
            new Tbl_Students { Name = "Ivy", Email = "ivy@example.com" },
            new Tbl_Students { Name = "Jack", Email = "jack@example.com" }
        });
    }
    context.SaveChanges();
}

app.Run();

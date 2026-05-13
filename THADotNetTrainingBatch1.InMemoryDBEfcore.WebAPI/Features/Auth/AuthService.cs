using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Auth;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext db,IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task<AuthResponse?> LoginAsync(AuthRequest reqModel)
    {
        var user =await _db.Users.FirstOrDefaultAsync(u => 
        u.UserName == reqModel.Username &&
        u.Password == reqModel.Password);

        if (user is null) return null;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name , user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        foreach (var permission in user.Permissions)
        {
            claims.Add(new Claim("Permission", permission));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: credintials

            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponse
        {
            AccessToken = jwt,
            Role = user.Role,
            Permissions = user.Permissions
        };

    }
}

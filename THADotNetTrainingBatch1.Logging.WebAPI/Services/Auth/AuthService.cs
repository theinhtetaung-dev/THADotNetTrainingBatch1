using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponseModel?> LoginAsync(LoginRequestModel request)
    {
        var users = await _context.Users.ToListAsync();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
        if (user == null) return null;

        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Join(_context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r)
            .ToListAsync();

        var roleIds = roles.Select(r => r.Id).ToList();

        var permissions = await _context.RolePermissions
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Join(_context.Permissions,
                rp => rp.PermissionId,
                p => p.Id,
                (rp, p) => p)
            .ToListAsync();

        var permissionStrings = permissions.Select(p => $"{p.Menu}.{p.Action}").Distinct().ToList();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add roles as claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
        }

        // Add permissions as claims
        foreach (var p in permissionStrings)
        {
            claims.Add(new Claim("Permission", p));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "YourSecretKeyShouldBeAtLeast32CharsLong!!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"] ?? "1"));

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new LoginResponseModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Roles = roles.Select(r => r.RoleName).ToList(),
            Permissions = permissionStrings
        };
    }
}

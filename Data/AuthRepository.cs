using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace dotnetRPG.Data;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;

    public AuthRepository(DataContext dataContext, IConfiguration configuration)
    {
        _dataContext = dataContext;
        _configuration = configuration;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (await UserExists(user.Username))
            {
                response.Message = $"username {user.Username} has already been taken!";
                response.Success = false;

                return response;
            }

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            response.Data = user.Id;
            response.Message = $"User added successfully";

            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            return response;
        }
    }


    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
        var response = new ServiceResponse<string>();

        var foundUser =
            await _dataContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

        if (foundUser is null)
        {
            response.Message = $"User with username {username} doesn't exist";
            response.Success = false;
            return response;
        }


        if (!VerifyPasswordHash(password, foundUser.PasswordHash, foundUser.PasswordSalt))
        {
            response.Message = "Invalid Credentials!";
            response.Success = false;
            return response;
        }

        response.Data = $"{CreateToken(foundUser)}";
        response.Message = "Successfully Logged In";
        response.Success = true;
        return response;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _dataContext.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower()));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual((passwordHash));
        }
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    // TODO 
    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        // Get the value from the file appsettings
        var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

        if (appSettingsToken is null) throw new Exception("AppToken Token string empty!");

        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes((appSettingsToken)));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}
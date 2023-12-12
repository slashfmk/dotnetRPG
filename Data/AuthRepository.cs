using System.ComponentModel.DataAnnotations;

namespace dotnetRPG.Data;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _dataContext;

    public AuthRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        var response = new ServiceResponse<int>();

        try
        {
            this.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (await this.UserExists(user.Username))
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

    // TODO
    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserExists(string username)
    {
        return await _dataContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
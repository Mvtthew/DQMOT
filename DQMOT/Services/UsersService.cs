using System.Security.Claims;
using DQMOT.Data;
using DQMOT.Entities;
using DQMOT.Models;
using Microsoft.EntityFrameworkCore;

namespace DQMOT.Services;

public class UsersService : IUsersService
{
    private readonly ICipherService _cipherService;
    private readonly DataContext _dataContext;
    
    public UsersService(
        ICipherService cipherService,
        DataContext dataContext)
    {
        _cipherService = cipherService;
        _dataContext = dataContext;
    }
    
    public async Task<UserModel?> GetUserByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

        return user?.toModel();
    }

    public async Task<UserModel?> GetUserByUsernameAndPasswordAsync(
        UserLoginModel userLoginModel, 
        CancellationToken cancellationToken = default)
    {
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => 
                u.Username == userLoginModel.Username, 
                cancellationToken);
        
        if (user == null)
        {
            return null;
        }
        
        if (_cipherService.Decrypt(user.Password) != userLoginModel.Password)
        {
            return null;
        }

        return user.toModel();
    }

    public async Task CreateUserAsync(
        UserRegisterModel userRegisterModel,
        CancellationToken cancellationToken = default)
    {
        var user = new User()
        {
            Username = userRegisterModel.Username,
            Password = _cipherService.Encrypt(userRegisterModel.Password)
        };
        
        _dataContext.Users.Add(user);
        
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<User> GetUserFromPrincipalAsync(
        ClaimsPrincipal userPrincipal,
        CancellationToken cancellationToken = default)
    {
        var userIdClaim = userPrincipal.Claims.FirstOrDefault(c => c.Type == "userId");
        
        if (userIdClaim == null)
        {
            throw new Exception("User not found in principal");
        }
        
        var id = int.Parse(userIdClaim.Value);
        
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        if (user == null)
        {
            throw new Exception("User not found in database");
        }

        return user;
    }
    
    public async Task<UserModel> GetUserModelFromPrincipalAsync(
        ClaimsPrincipal userPrincipal,
        CancellationToken cancellationToken = default)
    {
        return (await GetUserFromPrincipalAsync(userPrincipal, cancellationToken)).toModel();
    }
}
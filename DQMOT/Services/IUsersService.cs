using System.Security.Claims;
using DQMOT.Entities;
using DQMOT.Models;

namespace DQMOT.Services;

public interface IUsersService
{
    Task<UserModel?> GetUserByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default);
    
    Task<UserModel?> GetUserByUsernameAndPasswordAsync(
        UserLoginModel userLoginModel,
        CancellationToken cancellationToken = default);
    
    Task CreateUserAsync(
        UserRegisterModel userRegisterModel,
        CancellationToken cancellationToken = default);

    Task<User> GetUserFromPrincipalAsync(
        ClaimsPrincipal userPrincipal,
        CancellationToken cancellationToken = default);
    
    Task<UserModel> GetUserModelFromPrincipalAsync(
        ClaimsPrincipal userPrincipal,
        CancellationToken cancellationToken = default);
}
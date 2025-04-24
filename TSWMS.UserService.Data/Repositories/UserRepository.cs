#region Usings

using Microsoft.EntityFrameworkCore;
using TSWMS.UserService.Shared.Interfaces;
using TSWMS.UserService.Shared.Models;

#endregion

namespace TSWMS.UserService.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _userDbContext;

    public UserRepository(UsersDbContext userContext)
    {
        _userDbContext = userContext;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _userDbContext.Users.ToListAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _userDbContext.Users.Add(user);

        await _userDbContext.SaveChangesAsync();

        return user;
    }
}

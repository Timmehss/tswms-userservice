using TSWMS.UserService.Shared.Helpers;
using TSWMS.UserService.Shared.Interfaces;
using TSWMS.UserService.Shared.Models;

namespace TSWMS.UserService.Business.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly AesEncryptionHelper _encryptionHelper;

    public UserManager(IUserRepository userRepository, AesEncryptionHelper encryptionHelper)
    {
        _userRepository = userRepository;
        _encryptionHelper = encryptionHelper;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepository.GetUsers();
    }

    public async Task<Guid> CreateUserAsync(User user)
    {
        // Encrypt email
        user.Email = _encryptionHelper.EncryptString(user.Email);

        // Hash the password
        user.PasswordHash = PasswordHasherHelper.HashPassword(user.PasswordHash);

        // Save the user and return the newly created UserId
        var createdUser = await _userRepository.CreateUserAsync(user);
        return createdUser.UserId;
    }
}

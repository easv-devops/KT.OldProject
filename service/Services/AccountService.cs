using System.Security.Authentication;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Service;
using PasswordHash = Service.PasswordHash;

namespace service.Services;

public class AccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly PasswordHashRepository _passwordHashRepository;
    private readonly UserRepository _userRepository;

    public AccountService(ILogger<AccountService> logger, UserRepository userRepository,
        PasswordHashRepository passwordHashRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordHashRepository = passwordHashRepository;
    }

    public User? Authenticate(LoginCommandModel model)
    {
        
        try
        {
            var passwordHash = _passwordHashRepository.GetByEmail(model.Email);
           Console.WriteLine("Test  : "+passwordHash.Hash);
            var hashAlgorithm = new PasswordHash();
            var isValid = hashAlgorithm.VerifyHashedPassword(model.Password, passwordHash.Hash, passwordHash.Salt);
            if (isValid) return _userRepository.GetById(passwordHash.UserId);
        }
        catch (Exception e)
        {
            _logger.LogError("Authenticate error: {Message}", e);
        }

        return null;
    }

    public User Register(RegisterCommandModel model)
    {
        var hashAlgorithm = new PasswordHash();
        var salt = hashAlgorithm.GenerateSalt();
        var hash = hashAlgorithm.HashPassword(model.password, salt);
        var user = _userRepository.Create(model);
        _passwordHashRepository.Create(user.user_id, hash, salt, hashAlgorithm.GetName());
        return user;
    }

    public User GetAccountInfo()
    {
        return _userRepository.GetAccountInfo();

    }
    
}
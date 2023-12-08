using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace service.Services;

public class LoginService
{
    private PasswordHashRepository _passwordHashRepository;
    private UserRepository _userRepository;
    private IConfiguration _config;

    public LoginService(ILogger<LoginService> logger, UserRepository userRepository,
        PasswordHashRepository passwordHashRepository, IConfiguration config)
    {
       _userRepository = userRepository;
        _passwordHashRepository = passwordHashRepository;
        _config = config;
    }
    
    
    
    
    public UserModel Authenticate(LoginModel model)
    {
     
            var passwordHash = _passwordHashRepository.GetByEmail(model.Mail);
            var hashAlgorithm = new PasswordHashService();
            var isValid = hashAlgorithm.VerifyHashedPassword(model.Password, passwordHash.hash, passwordHash.salt);
            if (isValid) return _userRepository.GetById(passwordHash.user_id);
            else throw new ValidationException("Wrong username or password");
    }

    public string Generate(UserModel model)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, model.full_name),
            new Claim(ClaimTypes.Email, model.email),
            new Claim(ClaimTypes.Role, model.admin)
        };

        var token = new JwtSecurityToken(_config.GetValue<string>("Jwt:Issuer"),
            _config.GetValue<string>("Jwt:Audience"),
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public UserModel Register(RegisterModel model)
    {
        var hashAlgorithm = new PasswordHashService();
        var salt = hashAlgorithm.GenerateSalt();
        var hash = hashAlgorithm.HashPassword(model.password, salt);
        var user = _userRepository.Create(model);
        _passwordHashRepository.Create(user.user_id, hash, salt);
        return user;
    }

}
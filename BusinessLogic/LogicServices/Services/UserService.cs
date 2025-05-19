using BusinessLogic.LogicServices.Interfaces;
using BusinessLogic.LogicServices.Interfaces.Auth;
using DataAccess.DataRepositories.Interfaces;
using DataAccess.DataRepositories.Repositories;
using DataAccess.Models;
using DataAccess.Requests;

namespace BusinessLogic.LogicServices.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null) 
            {
                var result = _passwordHasher.Verify(password, user.PasswordHash);

                if (result == false)
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }

                var token = _jwtProvider.GenerateToken(user);

                return token;
            }

            return "";
        }

        public async Task Register(string username, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new ApplicationUser
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateAsync(user);
        }
    }
}

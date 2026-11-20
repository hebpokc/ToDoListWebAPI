using BusinessLogic.LogicServices.Interfaces.Auth;
using BusinessLogic.LogicServices.Services;
using DataAccess.DataRepositories.Interfaces;
using DataAccess.Models;
using DataAccess.Requests;
using Moq;
using Xunit;

namespace ToDoListWebAPI.Tests
{
    /// <summary>
    /// Unit-тесты AuthService с использованием Moq.
    /// </summary>
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IJwtProvider> _jwtProviderMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _jwtProviderMock = new Mock<IJwtProvider>();

            _authService = new AuthService(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object,
                _jwtProviderMock.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                PasswordHash = "hashed"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(h => h.Verify("password123", user.PasswordHash))
                .Returns(true);

            _jwtProviderMock
                .Setup(j => j.GenerateToken(user))
                .Returns("jwt-token");

            var token = await _authService.Login(user.Email, "password123");

            Assert.Equal("jwt-token", token);

            _jwtProviderMock.Verify(j => j.GenerateToken(user), Times.Once);
        }

        [Fact]
        public async Task Login_UserNotFound_ThrowsUnauthorized()
        {
            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.Login("unknown@example.com", "password123"));
        }

        [Fact]
        public async Task Login_WrongPassword_ThrowsUnauthorized()
        {
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                PasswordHash = "hashed"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.Login(user.Email, "wrong-password"));

            _jwtProviderMock.Verify(j => j.GenerateToken(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Fact]
        public async Task Register_NewUser_HashesPasswordAndCreates()
        {
            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync("new@example.com"))
                .ReturnsAsync((ApplicationUser?)null);

            _passwordHasherMock
                .Setup(h => h.Generate("password123"))
                .Returns("hashed-password");

            ApplicationUser? createdUser = null;
            _userRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<ApplicationUser>()))
                .Callback<ApplicationUser>(u => createdUser = u)
                .ReturnsAsync((ApplicationUser u) => u);

            await _authService.Register("vasya", "new@example.com", "password123");

            Assert.NotNull(createdUser);
            Assert.Equal("vasya", createdUser!.Username);
            Assert.Equal("new@example.com", createdUser.Email);
            Assert.Equal("hashed-password", createdUser.PasswordHash);

            _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Fact]
        public async Task Register_EmailAlreadyExists_ThrowsInvalidOperation()
        {
            var existing = new ApplicationUser { Email = "taken@example.com" };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync("taken@example.com"))
                .ReturnsAsync(existing);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _authService.Register("vasya", "taken@example.com", "password123"));

            _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }
    }

    /// <summary>
    /// Unit-тесты UserService с использованием Moq.
    /// </summary>
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_UserExists_ReturnsUser()
        {
            var user = new ApplicationUser { Id = Guid.NewGuid(), Username = "vasya" };

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            var result = await _userService.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("vasya", result!.Username);
        }

        [Fact]
        public async Task GetByIdAsync_UserNotFound_ReturnsNull()
        {
            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ApplicationUser?)null);

            var result = await _userService.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_UserNotFound_ThrowsInvalidOperation()
        {
            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ApplicationUser?)null);

            var request = new UserUpdateRequest
            {
                Username = "vasya",
                Email = "vasya@example.com",
                CurrentPassword = "old",
                NewPassword = "newpass"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _userService.UpdateAsync(Guid.NewGuid(), request));
        }

        [Fact]
        public async Task UpdateAsync_WrongCurrentPassword_ThrowsArgument()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                PasswordHash = "hashed"
            };

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var request = new UserUpdateRequest
            {
                Username = "vasya",
                Email = "vasya@example.com",
                CurrentPassword = "wrong",
                NewPassword = "newpass"
            };

            await Assert.ThrowsAsync<ArgumentException>(
                () => _userService.UpdateAsync(user.Id, request));

            _userRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ValidData_UpdatesUserAndHashesNewPassword()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Username = "old-name",
                Email = "old@example.com",
                PasswordHash = "old-hash"
            };

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(h => h.Verify("current-pass", "old-hash"))
                .Returns(true);

            _passwordHasherMock
                .Setup(h => h.Generate("new-pass"))
                .Returns("new-hash");

            var request = new UserUpdateRequest
            {
                Username = "new-name",
                Email = "new@example.com",
                CurrentPassword = "current-pass",
                NewPassword = "new-pass"
            };

            await _userService.UpdateAsync(user.Id, request);

            Assert.Equal("new-name", user.Username);
            Assert.Equal("new@example.com", user.Email);
            Assert.Equal("new-hash", user.PasswordHash);

            _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            var id = Guid.NewGuid();

            await _userService.DeleteAsync(id);

            _userRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}

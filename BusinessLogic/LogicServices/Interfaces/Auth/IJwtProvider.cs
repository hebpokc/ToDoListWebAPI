using DataAccess.Models;

namespace BusinessLogic.LogicServices.Interfaces.Auth
{
    public interface IJwtProvider
    {
        public string GenerateToken(ApplicationUser user);
    }
}

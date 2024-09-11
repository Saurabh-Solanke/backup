using PassportApi.Models;

namespace PassportApi.interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(PassportUser user);
    }
}

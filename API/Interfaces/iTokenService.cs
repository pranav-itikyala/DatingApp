using API.Entities;

namespace API;

public interface iTokenService
{
    Task<string> CreateToken (AppUser user);
    
}

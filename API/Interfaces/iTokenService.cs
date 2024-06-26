using API.Entities;

namespace API;

public interface iTokenService
{
    string CreateToken (AppUser user);
    
}

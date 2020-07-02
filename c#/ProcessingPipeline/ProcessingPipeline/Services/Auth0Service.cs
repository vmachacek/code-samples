using System.Threading.Tasks;

namespace ProcessingPipeline.Services
{
    public class Auth0Service
    {
        internal Task CreateUserIfDoesNotExistAsync(Auth0UserCreationDto auth0UserCreationDto)
        {
            return Task.CompletedTask;
        }
    }

    public class Auth0UserCreationDto
    {
        internal string email;
        internal string password;
        internal string name;
    }
}

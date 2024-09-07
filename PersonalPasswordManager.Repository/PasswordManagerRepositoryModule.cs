using Microsoft.Extensions.DependencyInjection;
using PersonalPasswordManager.Repository.Implementation;
using PersonalPasswordManager.Repository.Interface;

namespace PersonalPasswordManager.Repository
{
    public class PasswordManagerRepositoryModule
    {
        public PasswordManagerRepositoryModule(IServiceCollection services)
        {
            services.AddTransient<IPasswordManagerRepository, PasswordManagerRepository>();
        }
    }
}

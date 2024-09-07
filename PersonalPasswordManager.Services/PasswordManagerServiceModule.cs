using Microsoft.Extensions.DependencyInjection;
using PersonalPasswordManager.Services.Implementation;
using PersonalPasswordManager.Services.Interface;

namespace PersonalPasswordManager.Services
{
    public class PasswordManagerServiceModule
    {
        public PasswordManagerServiceModule(IServiceCollection services)
        {
            services.AddTransient<IPasswordManagerService, PasswordManagerService>();

        }
    }
}

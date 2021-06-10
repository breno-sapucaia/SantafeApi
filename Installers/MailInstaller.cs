using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Options;
using SantafeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Installers
{
    public class MailInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var emailSettingsSection = configuration.GetSection("Email");
            services.Configure<EmailSettings>(emailSettingsSection);
            services.AddSingleton(emailSettingsSection);
            services.AddSingleton<IMailService, MailService>();
        }
    }
}

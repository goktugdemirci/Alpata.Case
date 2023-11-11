using Alpata.Case.Domain.Repository;
using Alpata.Case.Domain.Shared.Current;
using Alpata.Case.Infrastructure.Repository;
using Alpata.Case.Service.Contracts.Common;
using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Services.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Alpata.Case.Services.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMeetingService, MeetingService>();
            services.AddTransient<IMeetingAttachmentService, MeetingAttachmentService>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IFileService, FileService>();
            services.AddScoped<AlpataCurrentUser>();
            return services;
        }
    }
}

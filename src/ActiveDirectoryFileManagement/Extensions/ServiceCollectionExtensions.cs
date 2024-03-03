using ActiveDirectoryFileManagement.Interfaces;
using ActiveDirectoryFileManagement.Models;
using ActiveDirectoryFileManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveDirectoryFileManagement.Extensions;
public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddActiveDirectoryFileManagementServices(this IServiceCollection services, string userName, string password, string domain)
	{
		var settings = new ActiveDirectorySettings(userName, password, domain);

		services.AddSingleton(settings);
		services.AddActiveDirectoryFileManagementServices();

		return services;
	}

	public static IServiceCollection AddActiveDirectoryFileManagementServices(this IServiceCollection services, ActiveDirectorySettings activeDirectorySettings)
	{
		services.AddSingleton(activeDirectorySettings);
		services.AddActiveDirectoryFileManagementServices();

		return services;
	}

	public static IServiceCollection AddActiveDirectoryFileManagementServices(this IServiceCollection services)
	{
		services.AddScoped<IFileService, FileService>();
		services.AddScoped<IDirectoryService, DirectoryService>();
		services.AddScoped<IActiveDirectoryService, ActiveDirectoryService>();
		services.AddScoped<IActiveDirectoryUserManager, ActiveDirectoryUserManager>();

		return services;
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SALESSYSTEM.BLL.Implementation;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Context;
using SALESSYSTEM.DAL.Implementation;
using SALESSYSTEM.DAL.Interfaces;

namespace SALESSYSTEM.IOC.Extensions
{
	public static class InjectionExtensions
	{
		public static IServiceCollection AddInjectionIOC(this IServiceCollection services, IConfiguration configuration) 
		{
			var assembly = typeof(SALESSYSDBContext).Assembly.FullName;

			services.AddSingleton(configuration);


            services.AddDbContext<SALESSYSDBContext>(
				options => options.UseSqlServer(
					configuration.GetConnectionString("SALESSYSDB"), b => b.MigrationsAssembly(assembly)
				), ServiceLifetime.Transient );

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			services.AddScoped<ISaleRepository, SaleRepository>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IUtilitiesService, UtilitiesServices>();
			services.AddScoped<IRolService, RolService>();

			return services;
        
		}
	}
}

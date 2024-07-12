using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CuttingFacadePanels
{
	public class Program
	{
		public static AssemblyName ServiceName = Assembly.GetExecutingAssembly().GetName();

		public static async Task Main(string[] args)
		{
			try
			{
				var host = CreateHostBuilder(args).Build();
				await host.RunAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine("The {@serviceName} service startup failed", ServiceName.Name);
				throw;
			}
			finally
			{
				Console.WriteLine("The {@serviceName} service was stopped", ServiceName.Name);
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host
				.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						.ConfigureKestrel(options =>
						{
							options.Limits.MinRequestBodyDataRate = null;
						})
						.UseStartup<Startup>();
				});
		}
	}
}
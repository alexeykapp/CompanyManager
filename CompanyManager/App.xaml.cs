using CompanyManager.Database;
using CompanyManager.View;
using CompanyManager.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace CompanyManager
{
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
       
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, builder);
            serviceProvider = serviceCollection.BuildServiceProvider();

            var startWindow = serviceProvider.GetRequiredService<StartWindow>();
            startWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services, IConfigurationRoot builder)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.GetConnectionString("DefaultConnection")));
            services.AddScoped<StartWindow>();
            services.AddScoped<StartWindowVM>();
        }
    }
}

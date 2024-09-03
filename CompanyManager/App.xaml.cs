using CompanyManager.Base;
using CompanyManager.Database;
using CompanyManager.Interfaces;
using CompanyManager.Services;
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
            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services, IConfigurationRoot builder)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.GetConnectionString("DefaultConnection")));
            services.AddScoped<StartWindow>(provider => new StartWindow
            {
                DataContext = provider.GetRequiredService<StartWindowVM>()
            });
            services.AddSingleton<INavigationService, NavigationServices>();
            services.AddScoped<StartWindowVM>();
            services.AddScoped<ViewModel.EmployeeWindowVM>();
            services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));
        }
    }
}

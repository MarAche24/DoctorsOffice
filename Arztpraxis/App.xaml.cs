using Arztpraxis.Auth;
using Arztpraxis.Auth.Interfaces;
using Arztpraxis.Auth.Models;
using Arztpraxis.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Arztpraxis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly IHost host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Singletons can be passed through constructors. Leaving this here to see how it can be done.
                services.AddDbContextFactory<DoctorsOfficeDbContext>((optinonsBuilder) =>
                    optinonsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DoctorsOffice;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;"));
                services.AddSingleton<IPasswordHasher, PasswordHasherService>();

                services.AddSingleton<AuthenticationService>((serviceProvider) =>
                {
                    return new AuthenticationService(serviceProvider.GetService<IDbContextFactory<DoctorsOfficeDbContext>>()!, serviceProvider.GetService<IPasswordHasher>()!);
                });
            })
            .Build();

        public new static App Current => (App)Application.Current;

        public new MainWindow MainWindow { get => (MainWindow)base.MainWindow; }

        public AuthenticatedUser? CurrentUser { get; set; }

        // use this insted of StartupURI
         
        //public App()
        //    : base()
        //{
        //    //this.MainWindow = new MainWindow();
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    //base.OnStartup(e);
        //    //this.MainWindow.Show();
        //}
    }
}

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
                // TODO: Add Authentication service
                services.AddDbContextFactory<DoctorsOfficeDbContext>((optinonsBuilder) =>
                    optinonsBuilder.UseSqlServer("Data Source=TESTVM-MAC\\SQLEXPRESS;Initial Catalog=DoctorsOffice;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;"));
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

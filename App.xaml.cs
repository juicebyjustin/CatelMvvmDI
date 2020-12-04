using Catel.MVVM;
using Catel.Services;
using CatelMvvmDI.Models;
using CatelMvvmDI.Services;
using CatelMvvmDI.ViewModels;
using CatelMvvmDI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CatelMvvmDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            _host = CreateHostBuilder().Build();

            var service = _host.Services.GetRequiredService<Service>();
            var logger= _host.Services.GetRequiredService<ILogger>();
            
            if(service != null)
                logger.Information("DI is working");

            var viewModelLocator = _host.Services.GetRequiredService<IViewModelLocator>();
            viewModelLocator.Register<MainView, MainViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //var main = new MainView();

            //how to get DI to inject services into MainViewModel and use MainViewModel as the data contextfor MainView?
            var main = _host.Services.GetRequiredService<MainView>();
            main.Show();

            //correct view model type is resolved
            var viewModelLocator = _host.Services.GetRequiredService<IViewModelLocator>();
            var viewModelType = viewModelLocator.ResolveViewModel(typeof(MainView));


            base.OnStartup(e);
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<Service>();

                    services.AddSingleton<MainView>();
                    services.AddTransient<MainViewModel>();

                    //add View/Model Locator & URL/Navigation services
                    services.AddSingleton<IUrlLocator, UrlLocator>();
                    services.AddSingleton<IViewLocator, ViewLocator>();
                    services.AddSingleton<IViewModelLocator, ViewModelLocator>();
                    services.AddSingleton<IMessageService, MessageService>();
                    services.AddSingleton<INavigationService, NavigationService>();
                });
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred. \n\nMessage: {0}", e.Exception.Message);

            e.Handled = true;
        }
    }
}

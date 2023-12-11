﻿using Maui.SmartWindow.ArchProposal.Helpers;
using Maui.SmartWindow.ArchProposal.Services;
using Maui.SmartWindow.ArchProposal.Views;
using Maui.SmartWindow.Configuration;
using Microsoft.Extensions.Logging;

namespace Maui.SmartWindow.ArchProposal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSmartWindow()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainPage>(); 
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<MyView>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            var app = builder.Build();

            ContainerHelper.Provider = app.Services;
            return app;
        }
    }
}
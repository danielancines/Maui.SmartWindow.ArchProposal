namespace Maui.SmartWindow.Configuration;

public static class MauiConfiguration
{
    #region Initial Config

    public static MauiAppBuilder UseSmartWindow(this MauiAppBuilder app)
    {
        ConfigureHandlers(app);

        return app;
    }

    #endregion

    #region Methods

    private static void ConfigureHandlers(MauiAppBuilder app)
    {
        app.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(SmartWindow), typeof(SmartWindowHandler));
        });
    }

    #endregion
}

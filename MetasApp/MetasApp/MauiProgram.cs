using Microsoft.Maui.Hosting;
using CommunityToolkit.Maui;

namespace MetasApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit();

        var app = builder.Build();

        AppServices.MetaService = new Services.MetaService();

        return app;
    }
}

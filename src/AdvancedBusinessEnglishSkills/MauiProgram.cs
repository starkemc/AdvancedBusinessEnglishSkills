using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using DevExpress.Maui;

namespace AdvancedBusinessEnglishSkills;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            .UseDevExpress(useLocalization: true)
            .UseDevExpressControls()
            .UseDevExpressCollectionView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
    	builder.Logging.AddDebug();
#endif

    DevExpress.Maui.CollectionView.Initializer.Init();
    DevExpress.Maui.Controls.Initializer.Init();

    return builder.Build();
    }
}


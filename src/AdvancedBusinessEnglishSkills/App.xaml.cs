using Microsoft.Maui.Controls.PlatformConfiguration;
using Syncfusion.Licensing;


namespace AdvancedBusinessEnglishSkills
{
    public partial class App : Application
    {
        private const string fileName = "abe.db3"; //Source Database File Name

        public App()
        {
            InitializeComponent();

            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWH1fcnZWQ2ZdU0F2XUQ=");

            #if ANDROID
             CopyDb(fileName);
            #endif
        }

        //to call asyn method synchronously
        public static void CopyDb(string fileName)
        {
            var task = CopyFileToAppDataDirectory(fileName);
            Func<System.Runtime.CompilerServices.TaskAwaiter> getAwaiter = task.GetAwaiter;
            Func<System.Runtime.CompilerServices.TaskAwaiter> result = getAwaiter; // Blocks until the task completes
        }

        //Copying method from Maui File System Helper
        public static async Task CopyFileToAppDataDirectory(string fileName)
        {
            // Open the source file
            using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
            {
                //string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                string targetFile = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Copy the file to the AppDataDirectory
                using FileStream outputStream = File.Create(targetFile);
                await inputStream.CopyToAsync(outputStream);
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
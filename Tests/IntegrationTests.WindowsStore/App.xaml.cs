using System.Reflection;

using CrossPlatformLibrary.Bootstrapping;

using Xunit.Runners.UI;

namespace IntegrationTests.WindowsStore
{
    public sealed partial class App : RunnerApplication
    {
        protected override void OnInitializeRunner()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            //this.AddTestAssembly(typeof(SettingsServiceTests).GetTypeInfo().Assembly);
        }
    }
}
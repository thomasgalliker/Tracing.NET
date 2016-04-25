using CrossPlatformLibrary.Bootstrapping;

using Xunit.Runners.UI;

namespace IntegrationTests.WindowsPhone8
{
    public partial class MainPage : RunnerApplicationPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnInitializeRunner()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            //this.AddTestAssembly(typeof(SettingsServiceTests).Assembly);
        }
    }
}
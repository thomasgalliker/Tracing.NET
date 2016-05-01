
using Android.App;
using Android.OS;

using CrossPlatformLibrary.Bootstrapping;

using Tracing.IntegrationTests;

using Xunit.Runners.UI;

namespace IntegrationTests.Android
{
    [Activity(Label = "IntegrationTests.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : RunnerActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            // tests can be inside the main assembly
            this.AddTestAssembly(typeof(TracerTests).Assembly);
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}
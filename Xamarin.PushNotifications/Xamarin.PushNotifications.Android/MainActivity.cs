
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Firebase.Messaging;

namespace Xamarin.PushNotifications.Droid
{
    [Activity(Label = "Xamarin.PushNotifications", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            if (IsPlayServicesAvailable())
            {
                FirebaseMessaging.Instance.SubscribeToTopic("xamarin_pushnotifications_test_topic");
            }
        }

        private bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if (resultCode == ConnectionResult.Success)
            {
                Log.Info("GoogleApiAvailability Success", "Google Play Services is available");
                return true;
            }

            string messageText = GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)
                ? GoogleApiAvailability.Instance.GetErrorString(resultCode)
                : "This device is not supported";

            Log.Error("GoogleApiAvailability Error", messageText);
            return false;
        }
    }
}
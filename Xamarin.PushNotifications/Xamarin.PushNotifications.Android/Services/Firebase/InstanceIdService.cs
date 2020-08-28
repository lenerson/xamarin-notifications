
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;

namespace Xamarin.PushNotifications.Droid.Services.Firebase
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class InstanceIdService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendTokenToServer(refreshedToken);
        }

        private void SendTokenToServer(string refreshedToken)
        {
            Log.Debug(PackageName, refreshedToken);
        }
    }
}
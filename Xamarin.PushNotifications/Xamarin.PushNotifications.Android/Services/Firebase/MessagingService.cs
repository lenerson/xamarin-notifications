using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Firebase.Messaging;
using System;
using System.Collections.Generic;

namespace Xamarin.PushNotifications.Droid.Services.Firebase
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MessagingService : FirebaseMessagingService
    {
        private readonly string NOTIFICATION_CHANNEL_ID = "com.companyname.Xamarin.PushNotifications";

        public override void OnMessageReceived(RemoteMessage message)
        {
            if (message.Data.GetEnumerator().MoveNext())
            {
                SendNotification(message.Data);
                return;
            }
            var notification = message.GetNotification();
            SendNotification(notification.Title, notification.Body);
        }

        private void SendNotification(string title, string body)
        {
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Notification Channel", NotificationImportance.Default)
                {
                    Description = "Channel"
                };
                notificationChannel.EnableLights(true);
                notificationChannel.LightColor = Color.Red;
                notificationChannel.SetVibrationPattern(new long[] { 0, 1000, 500, 1000 });

                notificationManager.CreateNotificationChannel(notificationChannel);
            }

            var notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

            notificationBuilder
                .SetAutoCancel(true)
                .SetDefaults(-1)
                .SetWhen(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetContentInfo("Info");

            notificationManager.Notify(new Random().Next(), notificationBuilder.Build());
        }

        private void SendNotification(IDictionary<string, string> data)
        {
            data.TryGetValue("title", out var title);
            data.TryGetValue("body", out var body);

            SendNotification(title, body);
        }
    }

}
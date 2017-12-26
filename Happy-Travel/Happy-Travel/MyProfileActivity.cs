
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HappyTravel
{
    [Activity(Label = "MyProfileActivity")]
    public class MyProfileActivity : Activity
    {
        private Button btnMe;
        private Button btnHome;
        private Button btnPost;

        private TextView txtUsername;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyProfile);

            Bundle bundleUser = Intent.Extras;
            int userId = bundleUser.GetInt("userId");

            var username = UsersDB.Users.GetUserById(userId).username;
            txtUsername = FindViewById<TextView>(Resource.Id.txtMeUsername);
            txtUsername.Text = username;

            btnMe = FindViewById<Button>(Resource.Id.btnMe);
            btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnPost = FindViewById<Button>(Resource.Id.btnPost);

            btnMe.Click += (object sender, EventArgs e) => {
                Intent intentMe = new Intent(this, typeof(MyProfileActivity));
                Bundle bundleMe = new Bundle();
                bundleMe.PutInt("userId", userId);
                intentMe.PutExtras(bundleMe);
                StartActivity(intentMe);
            };

            btnHome.Click += (object sender, EventArgs e) => {
                Intent intentHome = new Intent(this, typeof(ArticleListActivity));
                Bundle bundleHome = new Bundle();
                bundleHome.PutInt("userId", userId);
                intentHome.PutExtras(bundleHome);
                StartActivity(intentHome);
            };

            btnPost.Click += (object sender, EventArgs e) => {
                Intent intentPost = new Intent(this, typeof(ArticlePublishingActivity));
                Bundle bundlePost = new Bundle();
                bundlePost.PutInt("userId", userId);
                intentPost.PutExtras(bundlePost);
                StartActivity(intentPost);
            };
        }
    }
}

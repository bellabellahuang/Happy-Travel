﻿
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
    [Activity(Label = "My Profile")]
    public class MyProfileActivity : Activity
    {
        private Button btnMe;
        private Button btnHome;
        private Button btnPost;
        private Button btnLogout;
        private TextView txtUsername;
        private ListView myPostListView;
        private List<Article> myPosts;
        private ArticleListViewAdapter adapter;
        int userId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyProfile);

            // get the userId
            Bundle bundleUser = Intent.Extras;
            userId = bundleUser.GetInt("userId");

            // initiate the views
            initView();

            // get the username and show it
            var username = UsersDB.Users.GetUserById(userId).username;
            txtUsername.Text = username;

            // call the menuBar method to handle menu buttons click events
            menuBar();

            // logout button click event handler
            btnLogout.Click += (object sender, EventArgs e) => {
                Intent intent = new Intent(this, typeof(MainActivity));
                bundleUser.Clear();
                StartActivity(intent);
            };

            // get articles by user
            myPosts = ArticleDB.Articles.GetArticleByUser(userId);
            adapter = new ArticleListViewAdapter(this, myPosts);
            myPostListView.Adapter = adapter;

            myPostListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Article article = myPosts[(int)e.Id];
                Intent intent = new Intent(this, typeof(ArticleDetailActivity));
                Bundle bundle = new Bundle();
                bundle.PutInt("Article", article.article_id);
                bundle.PutInt("User", userId);
                intent.PutExtras(bundle);
                StartActivity(intent);
            };
        }

        // menu button click event handler
        private void menuBar()
        {
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

        // assign values to widget variables
        private void initView()
        {
            txtUsername = FindViewById<TextView>(Resource.Id.txtMeUsername);
            btnMe = FindViewById<Button>(Resource.Id.btnMe);
            btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnPost = FindViewById<Button>(Resource.Id.btnPost);
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            myPostListView = FindViewById<ListView>(Resource.Id.myPostListView);
        }
    }
}

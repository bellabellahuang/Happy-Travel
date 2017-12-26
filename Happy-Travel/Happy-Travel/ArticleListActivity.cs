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
    [Activity(Label = "Home-Travel")]
    public class ArticleListActivity : Activity
    {
        private ListView articleListView;
        private ArticleDB articleDB = ArticleDB.Articles();
        private List<Article> articleListData;
        private ArticleListViewAdapter articleAdapter;

        private Button btnMe;
        private Button btnHome;
        private Button btnPost;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            Bundle bundleUser = Intent.Extras;
            int userId = bundleUser.GetInt("userId");

            articleListView = FindViewById<ListView>(Resource.Id.articleListView);

            // open or create the database
            articleDB.CreateTable();
            // get all the articles from database
            articleListData = articleDB.GetAriticles();
            articleAdapter = new ArticleListViewAdapter(this, articleListData);
            articleListView.Adapter = articleAdapter;

            articleListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Article article = articleListData[(int)e.Id];
                Intent intent = new Intent(this, typeof(ArticleDetailActivity));
                Bundle bundle = new Bundle();
                bundle.PutInt("Article", article.article_id);
                intent.PutExtras(bundle);
                StartActivity(intent);
            };

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
                Intent intentHome = new Intent(this, typeof(HomeActivity));
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

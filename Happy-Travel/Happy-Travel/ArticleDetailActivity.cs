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
    [Activity(Label = "Happy Travel")]
    public class ArticleDetailActivity : Activity
    {
        private TextView title;
        private TextView author;
        private TextView content;
        private ArticleDB articleDB = ArticleDB.Articles();
        private UsersDB userDB = UsersDB.Users;
        private Button btnMe;
        private Button btnHome;
        private Button btnPost;
        int articleId;
        int userId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ArticleDetail);

            // initiate views
            initView();

            // get article id and user id
            articleId = Intent.Extras.GetInt("Article");
            userId = Intent.Extras.GetInt("User");

            // get article instance and user instance
            var article = articleDB.GetArticleById(articleId);
            var Author = userDB.GetUserById(article.user_id);

            // display the detail of article
            title.Text = article.title;
            author.Text = Author.username;
            content.Text = article.content;

            // call the menuBar method to handle buttons click events
            menuBar();


        }

        // menu buttons click event handler
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
            title = FindViewById<TextView>(Resource.Id.txtDetailTitle);
            author = FindViewById<TextView>(Resource.Id.txtDetailAuthor);
            content = FindViewById<TextView>(Resource.Id.txtDetailContent);
            btnMe = FindViewById<Button>(Resource.Id.btnMe);
            btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnPost = FindViewById<Button>(Resource.Id.btnPost);
        }
    }
}

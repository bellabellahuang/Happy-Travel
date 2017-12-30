
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
    public class ArticleListActivity : Activity
    {
        private ListView articleListView;
        private ArticleDB articleDB = ArticleDB.Articles;
        private List<Article> articleListData;
        private ArticleListViewAdapter articleAdapter;
        private Button btnMe;
        private Button btnHome;
        private Button btnPost;
        int userId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            // get the userId
            Bundle bundleUser = Intent.Extras;
            userId = bundleUser.GetInt("userId");

            // initiate views
            initView();

            // open or create the database
            articleDB.CreateTable();
            articleDB.ClearArticles();
            articleDB.initArticleDB();
            // get all the articles from database
            articleListData = articleDB.GetAriticles();
            articleAdapter = new ArticleListViewAdapter(this, articleListData);
            articleListView.Adapter = articleAdapter;

            articleListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Article article = articleListData[(int)e.Id];
                Intent intent = new Intent(this, typeof(ArticleDetailActivity));
                Bundle bundle = new Bundle();
                bundle.PutInt("Article", article.article_id);
                bundle.PutInt("User", userId);
                intent.PutExtras(bundle);
                StartActivity(intent);
            };

            // call menuBar method to handle menu buttons click events
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

        // asign values to widget variables
        private void initView()
        {
            articleListView = FindViewById<ListView>(Resource.Id.articleListView);
            btnMe = FindViewById<Button>(Resource.Id.btnMe);
            btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnPost = FindViewById<Button>(Resource.Id.btnPost);
        }
    }
}

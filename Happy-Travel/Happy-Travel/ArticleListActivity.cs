
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
    [Activity(Label = "ArticleListActivity")]
    public class ArticleListActivity : Activity
    {
        private ListView articleListView;
        private ArticleDB articleDB = ArticleDB.Articles();
        private List<Article> articleListData;
        private ArticleListViewAdapter articleAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

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
        }
    }
}

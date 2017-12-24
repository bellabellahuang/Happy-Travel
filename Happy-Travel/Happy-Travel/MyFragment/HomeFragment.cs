
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace HappyTravel.MyFragment
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        private ListView articleListView;
        private ArticleDB articleDB = ArticleDB.Articles();
        private List<Article> articleListData;
        private ArticleListViewAdapter articleAdapter;
        private View view;
        public override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            articleListView =view.FindViewById<ListView>(Resource.Id.articleListView);

            // open or create the database
            articleDB.CreateTable();
            // get all the articles from database
            articleListData = articleDB.GetAriticles();
            articleAdapter = new ArticleListViewAdapter(this, articleListData);
            articleListView.Adapter = articleAdapter;

            articleListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                Article article = articleListData[(int)e.Id];
                Intent intent = new Intent(this.Activity.BaseContext, typeof(ArticleDetailActivity));
                Bundle bundle = new Bundle();
                bundle.PutInt("Article", article.article_id);
                intent.PutExtras(bundle);
                StartActivity(intent);
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.ListViewLayout, container, false);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}

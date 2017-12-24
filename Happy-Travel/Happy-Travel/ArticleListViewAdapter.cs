using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.Views;

namespace HappyTravel
{
    public class ArticleListViewAdapter : BaseAdapter<Article>
    {
        private readonly Android.Support.V4.App.Fragment activity;
        private List<Article> articleListData;
        private UsersDB userDB = UsersDB.Users;

        public ArticleListViewAdapter(Android.Support.V4.App.Fragment a, List<Article> articles) : base()
        {
            this.activity = a;
            this.articleListData = articles;
        }

        public override int Count
        {
            get
            {
                return articleListData.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Article this[int position]
        {
            get
            {
                return articleListData[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if(view == null){
                view = activity.Activity.LayoutInflater.Inflate(Resource.Layout.ArticleItemLayout, null, false);
            }
            Article article = this[position];
            view.FindViewById<TextView>(Resource.Id.txtItemTitle).Text = article.title;

            userDB.CreateTable();
            User author = userDB.GetUserById(article.user_id);

            view.FindViewById<TextView>(Resource.Id.txtItemAuthor).Text = author.username;

            return view;
        }
    }
}

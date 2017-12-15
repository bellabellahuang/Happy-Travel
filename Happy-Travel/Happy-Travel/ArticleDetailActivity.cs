
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
    [Activity(Label = "ArticleDetailActivity")]
    public class ArticleDetailActivity : Activity
    {
        private TextView title;
        private TextView author;
        private TextView content;
        private ArticleDB articleDB = ArticleDB.Articles();
        private UsersDB userDB = UsersDB.Users;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ArticleDetail);

            title = FindViewById<TextView>(Resource.Id.txtDetailTitle);
            author = FindViewById<TextView>(Resource.Id.txtDetailAuthor);
            content = FindViewById<TextView>(Resource.Id.txtDetailContent);

            var articleId = Intent.Extras.GetInt("Article");

            articleDB.CreateTable();
            userDB.CreateTable();
            var article = articleDB.GetArticleById(articleId);
            var Author = userDB.GetUserById(article.user_id);

            title.Text = article.title;
            author.Text = Author.username;
            content.Text = article.content;
        }
    }
}

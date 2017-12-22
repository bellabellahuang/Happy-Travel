
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
    [Activity(Label = "ArticlePublishingActivity")]
    public class ArticlePublishingActivity : Activity
    {
        private EditText title;
        private EditText content;
        private Button btnPublish;
        private Article newArticle = new Article();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Post);

            // assign values to variables
            title = FindViewById<EditText>(Resource.Id.txtTitle);
            content = FindViewById<EditText>(Resource.Id.txtContent);
            btnPublish = FindViewById<Button>(Resource.Id.btnPublish);

            // get the current logged in user id
            Bundle bundle = Intent.Extras;
            int userId = bundle.GetInt("userId");

            // publish button click event
            btnPublish.Click += (object sender, EventArgs e) => {
                // show error messages if there is no entry
                if(String.IsNullOrEmpty(title.Text)){
                    title.Error = "Title cannot be empty";
                }else if(String.IsNullOrEmpty(content.Text)){
                    content.Error = "Content cannot be empty";
                }else{
                    // generate the new article object
                    newArticle.title = title.Text;
                    newArticle.content = content.Text;
                    newArticle.user_id = userId; 

                    // save the new article to the database
                    ArticleDB.Articles().SaveArticle(newArticle);
                    // clear the entries
                    title.Text = "";
                    content.Text = "";
                }
            };
        }
    }
}

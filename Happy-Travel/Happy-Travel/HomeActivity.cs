
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
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity 
    {
        private LinearLayout tabMe;
        private LinearLayout tabHome;
        private LinearLayout tabPost;

        private ImageView imgMe;
        private ImageView imgHome;
        private ImageView imgPost;

        private TextView txtMe;
        private TextView txtHome;
        private TextView txtPost;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);


            initView();

            tabPost.Click += (object sender, EventArgs e) => {
                Intent showPost = new Intent(this.BaseContext, typeof(ArticlePublishingActivity));
                StartActivity(showPost);
            };
        }

        private void initView(){
            tabMe = FindViewById<LinearLayout>(Resource.Id.mTabMe);
            tabHome = FindViewById<LinearLayout>(Resource.Id.mTabHome);
            tabPost = FindViewById<LinearLayout>(Resource.Id.mTabPost);

            imgMe = FindViewById<ImageView>(Resource.Id.imgMe);
            imgHome = FindViewById<ImageView>(Resource.Id.imgHome);
            imgPost = FindViewById<ImageView>(Resource.Id.imgPost);

            txtMe = FindViewById<TextView>(Resource.Id.txtMe);
            txtHome = FindViewById<TextView>(Resource.Id.txtHome);
            txtPost = FindViewById<TextView>(Resource.Id.txtPost);
        }


    }
}

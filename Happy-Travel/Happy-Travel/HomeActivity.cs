
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using HappyTravel.MyFragment;

namespace HappyTravel
{
    [Activity(Label = "Home-Travel")]
    public class HomeActivity : FragmentActivity, View.IOnClickListener
    {
        private ViewPager viewPager;
        private LinearLayout tabMe;
        private LinearLayout tabHome;
        private LinearLayout tabPost;

        private ImageView imgMe;
        private ImageView imgHome;
        private ImageView imgPost;

        private TextView txtMe;
        private TextView txtHome;
        private TextView txtPost;

        private Android.Support.V4.App.Fragment meFragment;
        private Android.Support.V4.App.Fragment postFragment;
        private Android.Support.V4.App.Fragment homeFragment;
        private List<Android.Support.V4.App.Fragment> fragmentList;
        private TabFragmentPagerAdapter mAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            // assign values to view wedget variables
            initView();

            initFragment();

            mAdapter = new TabFragmentPagerAdapter(SupportFragmentManager, fragmentList);
            viewPager.Adapter = mAdapter;

            tabMe.SetOnClickListener(this);
            tabHome.SetOnClickListener(this);
            tabPost.SetOnClickListener(this);
        }

        private void initFragment()
        {
            meFragment = new MeFragment();
            postFragment = new PostFragment();
            homeFragment = new HomeFragment();

            fragmentList = new List<Android.Support.V4.App.Fragment>();
            fragmentList.Add(meFragment);
            fragmentList.Add(postFragment);
            fragmentList.Add(homeFragment);
        }

        private void initView(){
            viewPager = FindViewById<ViewPager>(Resource.Id.mViewPager);

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

        public void OnClick(View view){
            switch(view.Id){
                case Resource.Id.mTabHome:
                    viewPager.SetCurrentItem(0, true);
                    break;
                case Resource.Id.mTabMe:
                    viewPager.SetCurrentItem(1, true);
                    break;
                case Resource.Id.mTabPost:
                    viewPager.SetCurrentItem(2, true);
                    break;
            }
        }

    }
}

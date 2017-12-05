﻿using Android.App;
using Android.Widget;
using Android.OS;

namespace HappyTravel
{
    [Activity(Label = "Happy-Travel", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);

            mBtnSignUp.Click += (object sender, System.EventArgs e) => 
            {
                //Pull up the sign up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignUp signUpDialog = new Dialog_SignUp();
                signUpDialog.Show(transaction, "sign up dialog fragment");
            };

            mBtnSignIn.Click += (object sender, System.EventArgs e) =>
            {
                //Pull up the sign in dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignIn signInDialog = new Dialog_SignIn();
                signInDialog.Show(transaction, "sign in dialog fragment");
            };
        }
    }
}

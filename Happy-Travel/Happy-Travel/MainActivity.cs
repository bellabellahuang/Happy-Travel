using Android.App;
using Android.Widget;
using Android.OS;

namespace HappyTravel
{
    [Activity(Label = "Happy-Travel", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnSignUp.Click += (object sender, System.EventArgs e) => 
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignUp signUpDialog = new Dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");
            };
        }
    }
}


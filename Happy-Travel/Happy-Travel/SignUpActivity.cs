
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
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        private EditText username;
        private EditText password1;
        private EditText password2;
        private Button btnSignUp;
        private TextView error;
        private User newUser = new User();
        private UsersDB userDB = UsersDB.Users;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dialog_Sign_Up);

            // get views of EditText
            username = FindViewById<EditText>(Resource.Id.txtUsernameSignUp);
            password1 = FindViewById<EditText>(Resource.Id.txtPassword1);
            password2 = FindViewById<EditText>(Resource.Id.txtPassword2);
            btnSignUp = FindViewById<Button>(Resource.Id.btnDialogSignUp);
            error = FindViewById<TextView>(Resource.Id.textError);
            error.Visibility = ViewStates.Invisible;

            // open user database
            userDB.CreateTable();

            btnSignUp.Click += (object sender, System.EventArgs e) =>
            {
                // compare two password entries to see whether they are the same
                if (password1.Text == password2.Text)
                {
                    newUser.username = username.Text;
                    newUser.password = password1.Text;

                    // insert newUser into user database
                    userDB.SaveUser(newUser);

                    // show main page to login

                    error.Visibility = ViewStates.Invisible;

                }else{
                    // show error message
                    error.Visibility = ViewStates.Visible;
                }

            };

        }
    }
}

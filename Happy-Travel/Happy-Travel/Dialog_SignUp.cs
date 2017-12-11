﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace HappyTravel
{
    public class Dialog_SignUp : DialogFragment
    {
        private EditText username;
        private EditText password1;
        private EditText password2;
        private Button btnSignUp;
        private User newUser = new User();
        private UsersDB userDB = UsersDB.Users;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_Sign_Up, container, false);
            username = view.FindViewById<EditText>(Resource.Id.txtUsernameSignUp);
            password1 = view.FindViewById<EditText>(Resource.Id.txtPassword1);
            password2 = view.FindViewById<EditText>(Resource.Id.txtPassword2);
            btnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogSignUp);

            btnSignUp.Click += (object sender, System.EventArgs e) =>
            {
                if (String.IsNullOrEmpty(username.Text))
                {
                    username.Error = "Username can not be empty";
                }else if(String.IsNullOrEmpty(password1.Text)){
                    password1.Error = "Password can not be empty";
                }else if(String.IsNullOrEmpty(password2.Text)){
                    password2.Error = "Enter password again";
                }else if(password1.Text.Equals(password2.Text)){
                    // generate a new user
                    newUser.username = username.Text;
                    newUser.password = password1.Text;
                    // open the users database
                    userDB.CreateTable();
                    // save the new user
                    userDB.SaveUser(newUser);
                    // close the dialog
                    this.Dismiss();
                }else{
                    password2.Error = "This is different from the first entry";
                }
            };

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);//Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;//set the animation
        }
    }
}

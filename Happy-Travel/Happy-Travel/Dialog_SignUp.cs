using System;
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

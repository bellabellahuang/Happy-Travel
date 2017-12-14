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
    public class Dialog_SignIn : DialogFragment
    {
        private EditText username;
        private EditText password;
        private Button btnSignIn;
        private UsersDB usersDB = UsersDB.Users;
        private List<User> userList = new List<User>();
        private User currentUser = new User();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_Sign_In, container, false);
            username = view.FindViewById<EditText>(Resource.Id.txtUsername);
            password = view.FindViewById<EditText>(Resource.Id.txtPassword);
            btnSignIn = view.FindViewById<Button>(Resource.Id.btnDialogSignIn);

            // sign in button click event
            btnSignIn.Click += (object sender, EventArgs e) => {
                if(String.IsNullOrEmpty(username.Text)){
                    username.Error = "Username cannot be empty";
                }else if (String.IsNullOrEmpty(password.Text)){
                    password.Error = "Password cannot be empty";
                }else{
                    // open user database
                    usersDB.CreateTable();
                    // get users list data
                    userList = usersDB.GetUsersFromCache();

                    currentUser = userList.Find(
                            delegate (User user) {
                                return user.username.Equals(username.Text);
                            }
                        );

                    if(currentUser != null){
                        if(currentUser.password.Equals(password.Text)){
                            this.Dismiss();
                            Intent intent = new Intent(this.Context, typeof(ArticlePublishingActivity));
                            Bundle bundle = new Bundle();
                            bundle.PutInt("userId", currentUser.user_id);
                            intent.PutExtras(bundle);
                            StartActivity(intent);
                        }else{
                            password.Error = "Password is wrong";
                        }
                    }else{
                        username.Error = "Username does not exist";
                    }
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

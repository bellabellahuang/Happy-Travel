using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using System.Collections.Generic;

namespace HappyTravel
{
    [Activity(Label = "Happy-Travel", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;
        private UsersDB userDB = UsersDB.Users;
        private ArticleDB articleDB = ArticleDB.Articles;
        private CommentsDB commentDB = CommentsDB.comments;
        private List<User> userListData = new List<User>();
        private List<Article> articleListData = new List<Article>();
        private List<Comment> commentListData = new List<Comment>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);

            initDB();

            // the click event of the sign up button
            mBtnSignUp.Click += (object sender, System.EventArgs e) => 
            {
                //Pull up the sign up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignUp signUpDialog = new Dialog_SignUp();
                signUpDialog.Show(transaction, "sign up dialog fragment");
            };

            // the click event of the sign in button
            mBtnSignIn.Click += (object sender, System.EventArgs e) =>
            {
                //Pull up the sign in dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignIn signInDialog = new Dialog_SignIn();
                signInDialog.Show(transaction, "sign in dialog fragment");
            };
        }

        private async void DownloadUsersLisyAsync()
        {
            UserService service = new UserService();
            if (!service.isConnected(this))
            {
                Toast toast = Toast.MakeText(this, "Not connected to internet. Please check your device network settings.", ToastLength.Short);
                toast.Show();
            }
            else
            {
                userListData = await service.GetUserListAsync();
                foreach(User user in userListData){
                    userDB.SaveUser(user);
                }
            }
        }

        private async void DownloadArticlesLisyAsync()
        {
            ArticleService service = new ArticleService();
            if (!service.isConnected(this))
            {
                Toast toast = Toast.MakeText(this, "Not connected to internet. Please check your device network settings.", ToastLength.Short);
                toast.Show();
            }
            else
            {
                articleListData = await service.GetArticleListAsync();
                foreach (Article article in articleListData)
                {
                    articleDB.SaveArticle(article);
                }
            }
        }

        private async void DownloadCommentsLisyAsync()
        {
            CommentService service = new CommentService();
            if (!service.isConnected(this))
            {
                Toast toast = Toast.MakeText(this, "Not connected to internet. Please check your device network settings.", ToastLength.Short);
                toast.Show();
            }
            else
            {
                commentListData = await service.GetCommentListAsync();
                foreach (Comment comment in commentListData)
                {
                    commentDB.SaveComment(comment);
                }
            }
        }

        private void initDB()
        {
            userDB.CreateTable();
            userDB.ClearUserCache();
            DownloadUsersLisyAsync();
            //userDB.initUserDB();

            articleDB.CreateTable();
            articleDB.ClearArticles();
            DownloadArticlesLisyAsync();
            //articleDB.initArticleDB();

            commentDB.CreateTable();
            commentDB.DeleteAll();
            DownloadCommentsLisyAsync();
            //commentDB.initCommentDB();
        }
    }
}


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
    [Activity(Label = "Happy Travel")]
    public class ArticleDetailActivity : Activity
    {
        private TextView title;
        private TextView author;
        private TextView content;
        private EditText txtComment;
        private Button btnMe;
        private Button btnHome;
        private Button btnPost;
        private Button btnAddComment;
        private ListView commentsListView;
        private ArticleDB articleDB = ArticleDB.Articles;
        private UsersDB userDB = UsersDB.Users;
        private int articleId;
        private int userId;
        private CommentsDB commentDB = CommentsDB.comments;
        private List<Comment> commentListData;
        private List<Comment> allComments = CommentsDB.comments.GetAllComments();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ArticleDetail);

            // initiate views
            initView();

            // get article id and user id
            articleId = Intent.Extras.GetInt("Article");
            userId = Intent.Extras.GetInt("User");

            // get article instance and user instance
            var article = articleDB.GetArticleById(articleId);
            var Author = userDB.GetUserById(article.user_id);

            // display the detail of article
            title.Text = article.title;
            if (Author == null)
            {
                author.Text = "NO_NAME";
            }
            else
            {
                author.Text = Author.username;
            }
            content.Text = article.content;

            // call the menuBar method to handle buttons click events
            menuBar();

            // display comments
            commentDB.CreateTable();
            updateComments();

            // call the addComment method to handle add comment button click event
            addComment();

        }

        private void updateComments()
        {
            commentListData = commentDB.GetCommentsByArticle(articleId);
            CommentListAdapter adapter = new CommentListAdapter(this, commentListData);
            commentsListView.Adapter = adapter;
        }

        // add comment button click event handler
        private void addComment()
        {
            btnAddComment.Click += (object sender, EventArgs e) => {
                if (String.IsNullOrEmpty(txtComment.Text))
                {
                    txtComment.Error = "Nothing to be commented";
                }
                else
                {
                    Comment newComment = new Comment();
                    newComment.article_id = articleId;
                    newComment.user_id = userId;
                    newComment.comment = txtComment.Text;
                    newComment.comment_id = allComments.ElementAt(allComments.Count - 1).comment_id + 1;

                    commentDB.SaveComment(newComment);
                    Toast toast = Toast.MakeText(this, "Your comment has been added", ToastLength.Long);
                    toast.Show();
                    txtComment.Text = "";
                    updateComments();
                }
            };
        }

        // menu buttons click event handler
        private void menuBar()
        {
            btnMe.Click += (object sender, EventArgs e) => {
                Intent intentMe = new Intent(this, typeof(MyProfileActivity));
                Bundle bundleMe = new Bundle();
                bundleMe.PutInt("userId", userId);
                intentMe.PutExtras(bundleMe);
                StartActivity(intentMe);
            };

            btnHome.Click += (object sender, EventArgs e) => {
                Intent intentHome = new Intent(this, typeof(ArticleListActivity));
                Bundle bundleHome = new Bundle();
                bundleHome.PutInt("userId", userId);
                intentHome.PutExtras(bundleHome);
                StartActivity(intentHome);
            };

            btnPost.Click += (object sender, EventArgs e) => {
                Intent intentPost = new Intent(this, typeof(ArticlePublishingActivity));
                Bundle bundlePost = new Bundle();
                bundlePost.PutInt("userId", userId);
                intentPost.PutExtras(bundlePost);
                StartActivity(intentPost);
            };
        }

        // assign values to widget variables
        private void initView()
        {
            title = FindViewById<TextView>(Resource.Id.txtDetailTitle);
            author = FindViewById<TextView>(Resource.Id.txtDetailAuthor);
            content = FindViewById<TextView>(Resource.Id.txtDetailContent);
            btnMe = FindViewById<Button>(Resource.Id.btnMe);
            btnHome = FindViewById<Button>(Resource.Id.btnHome);
            btnPost = FindViewById<Button>(Resource.Id.btnPost);
            btnAddComment = FindViewById<Button>(Resource.Id.btnAddComment);
            txtComment = FindViewById<EditText>(Resource.Id.txtComment);
            commentsListView = FindViewById<ListView>(Resource.Id.commentsListView);
        }
    }
}

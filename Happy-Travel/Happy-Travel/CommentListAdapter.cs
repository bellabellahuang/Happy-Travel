using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.Views;

namespace HappyTravel
{
    public class CommentListAdapter : BaseAdapter<Comment>
    {
        private readonly Activity activity;
        private List<Comment> commentListData;
        private UsersDB userDB = UsersDB.Users;

        public CommentListAdapter(Activity a, List<Comment> comments) : base()
        {
            this.activity = a;
            this.commentListData = comments;
        }

        public override int Count
        {
            get
            {
                return commentListData.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Comment this[int position]
        {
            get
            {
                return commentListData[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = activity.LayoutInflater.Inflate(Resource.Layout.CommentItemLayout, null, false);
            }
            Comment comment = this[position];
            view.FindViewById<TextView>(Resource.Id.txtCommentItem).Text = comment.comment;

            User byUser = userDB.GetUserById(comment.user_id);

            view.FindViewById<TextView>(Resource.Id.txtByUser).Text = byUser.username;

            return view;
        }
    }
}

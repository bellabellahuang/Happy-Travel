using System;
using System.Collections.Generic;
using SQLite;

namespace HappyTravel
{
    public class CommentsDB
    {
        private static readonly CommentsDB commentsDB = new CommentsDB();
        SQLiteConnection dbConn;
        private const string DB_NAME = "Comments_DB.db3";

        public CommentsDB()
        {
        }

        public static CommentsDB comments{
            get {
                return commentsDB;
            }
        }

        // open database or create a new database if it doesn't exist
        public void CreateTable()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            dbConn = new SQLiteConnection(System.IO.Path.Combine(path, DB_NAME));
            dbConn.CreateTable<Comment>();
        }

        // insert
        public void SaveComment(Comment comment){
            dbConn.Insert(comment);
        }

        // retrieve all records from database
        public List<Comment> GetAllComments()
        {
            var commentList = new List<Comment>();
            IEnumerable<Comment> commentTable = dbConn.Table<Comment>();
            foreach (Comment c in commentTable)
            {
                commentList.Add(c);
            }
            return commentList;
        }

        // get comment list by article id 
        public List<Comment> GetCommentsByArticle(int id)
        {
            var commentsOfArticle = new List<Comment>();
            IEnumerable<Comment> _comments = dbConn.Table<Comment>().Where(a => a.article_id.Equals(id));
            foreach(Comment c in _comments){
                commentsOfArticle.Add(c);
            }
            return commentsOfArticle;
        }

        // delete comment by id
        public void DeleteCommentById(int id){
            dbConn.Delete(id);
        }

        // delete all comments
        public void DeleteAll(){
            dbConn.DeleteAll<Comment>();
        }

    }
}

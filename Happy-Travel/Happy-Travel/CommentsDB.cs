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

        public void initCommentDB(){
            User bob = UsersDB.Users.GetUserByName("bob");
            User mary = UsersDB.Users.GetUserByName("mary");
            User jim = UsersDB.Users.GetUserByName("jim");

            List<Article> bobArticles = ArticleDB.Articles.GetArticleByUser(bob.user_id);
            List<Article> maryArticles = ArticleDB.Articles.GetArticleByUser(mary.user_id);
            List<Article> jimArticles = ArticleDB.Articles.GetArticleByUser(jim.user_id);

            Comment bobComment1 = new Comment();
            bobComment1.user_id = mary.user_id;
            bobComment1.article_id = bobArticles[0].article_id;
            bobComment1.comment = "It sounds great.";
            dbConn.Insert(bobComment1);

            Comment bobComment2 = new Comment();
            bobComment2.user_id = jim.user_id;
            bobComment2.article_id = bobArticles[0].article_id;
            bobComment2.comment = "Thank you for sharing your info.";
            dbConn.Insert(bobComment2);

            Comment maryComment1 = new Comment();
            maryComment1.user_id = bob.user_id;
            maryComment1.article_id = maryArticles[0].article_id;
            maryComment1.comment = "Good ideas.";
            dbConn.Insert(maryComment1);

            Comment maryComment2 = new Comment();
            maryComment2.user_id = jim.user_id;
            maryComment2.article_id = maryArticles[0].article_id;
            maryComment2.comment = "You are so nice.";
            dbConn.Insert(maryComment2);

            Comment jimComment1 = new Comment();
            jimComment1.user_id = bob.user_id;
            jimComment1.article_id = jimArticles[0].article_id;
            jimComment1.comment = "Can you tell me how to rent a motobike?";
            dbConn.Insert(jimComment1);

            Comment jimComment2 = new Comment();
            jimComment2.user_id = mary.user_id;
            jimComment2.article_id = jimArticles[0].article_id;
            jimComment2.comment = "How much did you spend on your trip?";
            dbConn.Insert(jimComment2);
        }

    }
}

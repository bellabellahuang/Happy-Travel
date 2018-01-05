using System;
using System.Collections.Generic;
using SQLite;

namespace HappyTravel
{
    public class ArticleDB
    {
        private static readonly ArticleDB articleDB = new ArticleDB();
        SQLiteConnection dbConn;
        private const string DB_NAME = "Articles_DB.db3";

        public static ArticleDB Articles{
            get{
                return articleDB;
            }

        }

        // open database or create a new database if it doesn't exist
        public void CreateTable()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            dbConn = new SQLiteConnection(System.IO.Path.Combine(path, DB_NAME));
            dbConn.CreateTable<Article>();
        }

        // insert or update a record of article
        public void SaveArticle(Article a)
        {
            dbConn.Insert(a);
        }

        // retrieve all records from database
        public List<Article> GetAriticles()
        {
            var articleList = new List<Article>();
            IEnumerable<Article> articleTable = dbConn.Table<Article>();
            foreach(Article a in articleTable){
                articleList.Add(a);
            }
            return articleList;

        }

        // get an article by id
        public Article GetArticleById(int id){
            Article article = dbConn.Table<Article>().Where(a => a.article_id.Equals(id)).FirstOrDefault();
            return article;
        }

        // get an article list by user id
        public List<Article> GetArticleByUser(int userId){
            var articleData = new List<Article>();
            IEnumerable<Article> articles = dbConn.Table<Article>().Where(a => a.user_id.Equals(userId));
            foreach(Article a in articles){
                articleData.Add(a);
            }
            return articleData;
        }

        // delete an article by id
        public void DeleteArticleById(int id)
        {
            dbConn.Delete<Article>(id);
        }

        // delete all articles
        public void ClearArticles()
        {
            dbConn.DeleteAll<Article>();
        }

    }
}

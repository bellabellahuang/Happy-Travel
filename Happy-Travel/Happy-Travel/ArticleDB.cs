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

        // initiate database
        public void initArticleDB(){
            User bob = UsersDB.Users.GetUserByName("bob");
            User mary = UsersDB.Users.GetUserByName("mary");
            User jim = UsersDB.Users.GetUserByName("jim");

            Article bobArticle = new Article();
            bobArticle.user_id = bob.user_id;
            bobArticle.title = "My trip in Thailand";
            bobArticle.content = "I went to Thailand with my friend this Summer. " +
                "It is really a great trip and I would like to share my story to you." +
                "I spent one week travelling the country. Chingmai was my first stop." +
                "There are a lot of beautiful temples and people are nice." +
                "Two days later, I took a mini van to Pai where is 3 hours far away from Chingmai." +
                "It is a really beautiful village. You can ride a motobike to go around the whole town." +
                "Don't forget to visit the night market on weekends. You can find many interesting things." +
                "Feel free to tell me what you want to know about Thailand in the comment area.";

            Article maryArticle = new Article();
            maryArticle.user_id = mary.user_id;
            maryArticle.title = "A YEAR IN REVIEW";
            maryArticle.content = "As dawn broke on this year, I was excited for a fresh start. Last year, " +
                "I dealt with panic attacks and anxiety from taking on too many projects, " +
                "a breakup that left me heartbroken, and a mini-identity crisis from settling down.But that " +
                "'greatest worst year of my life' set the stage for a year in which I shifted my priorities and " +
                "focused on developing routines. On a personal level, this was a solid year." +
                "And so, as I am off to Thailand and then New Zealand through January, " +
                "I’ve decided to take a mini-break from blogging. In truth, while the panic attacks are gone, " +
                "the conditions that created them still haven’t gone away.";

            Article jimArticle = new Article();
            jimArticle.user_id = jim.user_id;
            jimArticle.title = "6 (NON-MILLENNIAL) SOLO FEMALE TRAVELERS SHARE THEIR TRAVEL WISDOM";
            jimArticle.content = "Kristin Addis from Be My Travel Muse writes our regular column on solo female travel." +
                " It’s an important topic I can’t adequately cover, so I brought in an expert to share her advice " +
                "for other women travelers to help cover the topics important and specific to them! She’s amazing and " +
                "knowledgable. In this column, Kristin shares some insights from solo female travelers who aren’t " +
                "millennials! Every now and then I think about Julie, a 77-year-old woman who stayed at the beach " +
                "bungalow next to me on the island of Gili Air in Indonesia. Today, I want to share some of the " +
                "stories of older women travelers and add their voices to the narratives. " +
                "So I sat down (virtually at least) with seven women and asked them for their travel advice."; 

            dbConn.Insert(bobArticle);
            dbConn.Insert(maryArticle);
            dbConn.Insert(jimArticle);
        }

    }
}

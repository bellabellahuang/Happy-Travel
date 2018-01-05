using System;
using SQLite;

namespace HappyTravel
{
    [Table("ArticleTable")]
    public class Article
    {
        [PrimaryKey]
        public int article_id { get; set; }

        [NotNull]
        public int user_id { get; set; }

        [NotNull]
        public string title { get; set; }

        [NotNull]
        public string content { get; set; }
    }
}

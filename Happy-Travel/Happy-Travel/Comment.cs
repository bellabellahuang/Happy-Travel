using System;
using SQLite;

namespace HappyTravel
{
    [Table("CommentsTable")]
    public class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int comment_id { get; set; }

        [NotNull]
        public int article_id { get; set; }

        [NotNull]
        public int user_id { get; set; }

        [NotNull]
        public string comment { get; set; }
    }
}

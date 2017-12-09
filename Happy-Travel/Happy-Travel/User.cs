using System;
using SQLite;

namespace HappyTravel
{
    [Table("UsersTable")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int user_id { get; set; }

        [NotNull, MaxLength(30)]
        public string username { get; set; }

        [NotNull]
        public string password { get; set; }

        [NotNull]
        public DateTime created_date { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

    }
}

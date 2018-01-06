using System;
using SQLite;

namespace HappyTravel
{
    [Table("UsersTable")]
    public class User
    {
        [PrimaryKey]
        public int user_id { get; set; }

        [NotNull, MaxLength(30)]
        public string username { get; set; }

        [NotNull]
        public string password { get; set; }

    }
}

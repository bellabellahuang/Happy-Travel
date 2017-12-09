using System;
using System.Collections.Generic;
using SQLite;

namespace HappyTravel
{
    public class UsersDB
    {
        private static readonly UsersDB users = new UsersDB();
        SQLiteConnection dbConn;
        private const string DB_NAME = "User_DB.db3";

        private UsersDB()
        {
        }

        public static UsersDB Users{
            get{
                return users;
            }
        }

        // open database or create a new database if it doesn't exist
        public void CreateTable(){
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            dbConn = new SQLiteConnection(System.IO.Path.Combine(path, DB_NAME));
            dbConn.CreateTable<User>();
        }

        // insert or update a record of user
        public void SaveUser(User u){
            dbConn.InsertOrReplace(u);
        }

        // retrieve all records from database
        public List<User> GetUsersFromCache(){
            var userList = new List<User>();
            IEnumerable<User> usersTable = dbConn.Table<User>();
            foreach (User u in usersTable){
                userList.Add(u);
            }
            return userList;
        }

        // get a user by id
        public User GetUserById(int id){
            User user = dbConn.Table<User>().Where(a => a.user_id.Equals(id)).FirstOrDefault();
            return user;
        }

        // delete a user by id
        public void DeleteUserById(int id){
            dbConn.Delete<User>(id);
        }

        // delete all users
        public void ClearUserCache(){
            dbConn.DeleteAll<User>();
        }
    }
}

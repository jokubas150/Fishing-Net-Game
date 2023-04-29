using System;
using System.Collections.Generic;
using System.Text;

namespace CourseworkFish
{
    //user class to get the id and username
    public class User
    {
        int userId;
        string username;

        public User(int id, string uName)
        {
            userId = id;
            username = uName;
        }
        public int UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
    }
}

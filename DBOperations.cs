using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Diagnostics;

namespace CourseworkFish
{
    class DBOperations
    {
        //Create a connections
        private static OleDbConnection GetConnection()
        {
            string connString;
            connString = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Mano\Greenwich\Year 2\Term 2\COMP-1551 Application Development\Coursework\DBFlyingNet.mdb";
            return new OleDbConnection(connString);
        }
        //Load users from the database
        public static List<User> LoadUsers()
        {
            List<User> gameUsers = new List<User>();
            OleDbConnection myConnection = GetConnection();
            
            string myQuery = "SELECT * FROM [user]";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    User u = new User(int.Parse(myReader["uId"].ToString()), myReader["username"].ToString());
                    gameUsers.Add(u);
                }
                return gameUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close(); 
            }
        }
        //Load scores from the database
        public static List<GameScore> LoadScores()
        {
            List<GameScore> scores = new List<GameScore>();
            OleDbConnection myConnection = GetConnection();
            string myQuery = "SELECT scoreId, score, userId, gameDate FROM [score]";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<User> users = LoadUsers();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    User currentUser = FindUser(users, int.Parse(myReader["userId"].ToString()));
                    GameScore s = new GameScore(int.Parse(myReader["scoreId"].ToString()), int.Parse(myReader["score"].ToString()),
                        currentUser, DateTime.Parse(myReader["gameDate"].ToString()));
                    scores.Add(s);
                }
                return scores;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }

        }
        // insert user into the database
        public static void SaveUser(string Name)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "INSERT INTO [user] ( username ) VALUES ('" + Name + "')";
            Debug.WriteLine(myQuery);
            
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }
        //save score into database
        public static void SaveScore(int sc, int ui)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "INSERT INTO [score] ( score, userId, gameDate ) VALUES (" + sc + " , " + ui + " , '" + DateTime.Now + "' )";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
            }
            finally
            {
                myConnection.Close();
            }
        }
        //loop through list of users
        private static User FindUser(List<User> gameUsers, int id)
        {
            foreach (var user in gameUsers)
            {
                if (user.UserId == id)
                {
                    return user;
                }
            }
            return null;
        }
        //loop through list of scores
        private static GameScore FindScore(List<GameScore> gameScores, int id)
        {
            foreach (var gameScore in gameScores)
            {
                if (gameScore.ScoreId == id)
                {
                    return gameScore;
                }
            }
            return null;
        }
    }   
}

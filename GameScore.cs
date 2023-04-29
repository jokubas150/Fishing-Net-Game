using System;
using System.Collections.Generic;
using System.Text;

namespace CourseworkFish
{
    public class GameScore
    {
        int scoreId;
        int score;
        User userId;
        DateTime gameDate;

        public GameScore(int id, int s, User uId, DateTime gD)
        {
            scoreId = id;
            score = s;
            userId = uId;
            gameDate = gD;
        }

        public int ScoreId { get => scoreId; set => scoreId = value; }
        public int Score { get => score; set => score = value; }
        public User UserId { get => userId; set => userId = value; }
        public DateTime GameDate { get => gameDate; set => gameDate = value; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CourseworkFish
{
    public partial class Play : Form
    {
        //fields of play form
        Form1 form1 = new Form1();
        private int fishTimer = 0;
        private int sharkTimer = 0;
        private int chestTimer = 0;
        private int gameTime = 0;
        private int mineTimer = 0;
        private int Score = 0;
        private int Lives = 3;

        Random r = new Random(); // create random object

        private FishingNet flyingNet;
        private List<Fish> fishes;
        private List<Shark> sharks;
        private List<Chest> chests;
        private List<Mine> mines;

        public List<Fish> Fishes{ get => fishes; set => fishes = value; }
        public List<Shark> Sharks { get => sharks; set => sharks = value; }
        public List<Chest> Chests { get => chests; set => chests = value; }
        public List<Mine> Mines { get => mines; set => mines = value; }

        public Play()
        {
            // create new list objects
            InitializeComponent();
            fishes = new List<Fish>(); 
            flyingNet = new FishingNet();
            sharks = new List<Shark>();
            chests = new List<Chest>();
            mines = new List<Mine>();
            
            gameCanvas.Controls.Add(flyingNet); // add flyingnet on the screen
            flyingNet.Location = new Point(50, 250);
            gameTimer.Start(); //start gametimer
        }

        //
        //Fish
        //
        public void AddFish(Fish f)
        {
            fishes.Add(f);
            gameCanvas.Controls.Add(f);
        }
        public void RemoveFish(Fish f)
        {
            this.fishes.Remove(f);
            gameCanvas.Controls.Remove(f);
        }
        //
        //Mine
        //
        public void AddMine(Mine m)
        {
            mines.Add(m);
            gameCanvas.Controls.Add(m);
        }
        public void RemoveMine(Mine m)
        {
            this.mines.Remove(m);
            gameCanvas.Controls.Remove(m);
        }
        //
        //Shark
        //
        public void AddShark(Shark s)
        {
            sharks.Add(s);
            gameCanvas.Controls.Add(s);
        }
        public void RemoveShark(Shark s)
        {
            this.sharks.Remove(s);
            gameCanvas.Controls.Remove(s);
        }
        //
        //Chest
        //
        public void AddChest(Chest c)
        {
            chests.Add(c);
            gameCanvas.Controls.Add(c);
        }
        public void RemoveChest(Chest c)
        {
            this.chests.Remove(c);
            gameCanvas.Controls.Remove(c);
        }
        //wait for the action from user
        private void Play_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                flyingNet.MoveGameEntity(Direction.Right);
            else if (e.KeyCode == Keys.Left)
                flyingNet.MoveGameEntity(Direction.Left);
            else if (e.KeyCode == Keys.Up)
                flyingNet.MoveGameEntity(Direction.Up);
            else if (e.KeyCode == Keys.Down)
                flyingNet.MoveGameEntity(Direction.Down);
        }
        private void Play_Load(object sender, EventArgs e)
        {
            //Load usernames from Form1 into Play Form
            foreach (var gameUser in DBOperations.LoadUsers())
            {
                if (gameUser != null)
                {
                    Form1.User = gameUser;
                }
            }
        }
        //Class to increase timertick 
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            this.gameTime++; //increase game timer
            lblUsername.Text = "Hello " + Form1.User.Username.ToString();
            
            lblTime.Text = "Game Time: " + gameTime.ToString();

            if (gameTime > 0 && gameTime < 199) // Code for Level 1
            {
                // start increasing fish and shark timer
                this.fishTimer++;
                this.sharkTimer++;

                lblTitle.Text = "Level 1"; // change label to Level 1

                // if different timer reaches a number add an object
                if (fishTimer > 25) 
                {
                    AddFish(new Fish());
                    Fish.speed = r.Next(10, 20);
                    this.fishTimer = 0;
                }
                if (sharkTimer > 20)
                {
                    AddShark(new Shark());
                    Shark.speed = r.Next(15, 25);
                    this.sharkTimer = 0;
                }
                // Loop through list of objects
                foreach (var fish in fishes)
                {
                    fish.Update(gameCanvas);
                }
                foreach (var shark in sharks)
                {
                    shark.Update(gameCanvas);
                }
                // if flyingnet collides with the index of object do something...
                for (int index = 0; index < fishes.Count; index++)
                {
                    if (DetectCollisionFish(fishes[index], flyingNet))
                    {
                        this.RemoveFish(fishes[index]);
                        this.Score++;
                        lblScore.Text = "Score: " + this.Score;
                    }
                }
                for (int index = 0; index < sharks.Count; index++)
                {   
                    if (DetectCollisionShark(sharks[index], flyingNet))
                    {
                        this.RemoveShark(sharks[index]);
                        this.Score--;
                        this.Lives -= 1;
                        lblLives.Text = "Lives: " + this.Lives;
                        lblScore.Text = "Score: " + this.Score;
                    }
                }
            } 
            if (gameTime > 200) // if gametimer reaches this number 
            {
                // create random numbers for timers
                chestTimer = r.Next(0, 100);
                fishTimer = r.Next(0, 100);
                mineTimer = r.Next(0, 100);
                sharkTimer = r.Next(0, 100);
                
                lblTitle.Text = "Level 2";

                // if the random number reaches the number do something...
                if (chestTimer == 9) // chest
                {
                    AddChest(new Chest());
                    Chest.speed = r.Next(25, 40);
                    this.chestTimer = 0;
                }
                if (fishTimer > 15 && fishTimer < 20)
                {
                    AddFish(new Fish());
                    Fish.speed = r.Next(15, 25);
                    if (fishTimer == 17) // if timer reaches this number change the shape to heart
                    {
                        Fish.heart = true;
                    }
                    this.fishTimer = 0;
                }
                if (mineTimer > 13 && mineTimer < 15) // mine
                {
                    AddMine(new Mine());
                    Mine.speed = r.Next(18, 30);
                    this.mineTimer = 0;
                }
                if (sharkTimer > 15 && sharkTimer < 20) // shark
                {
                    AddShark(new Shark());
                    Shark.speed = r.Next(18, 30);
                    this.sharkTimer = 0;
                }
                foreach (var fish in fishes) //Fish
                {
                    fish.Update(gameCanvas);
                }
                foreach (var chest in chests) //Chests
                {
                    chest.Update(gameCanvas);
                    
                }
                foreach (var mine in mines) //Mines
                {
                    mine.Update(gameCanvas);
                }
                foreach (var shark in sharks) //Sharks
                {
                    shark.Update(gameCanvas);
                }
                // Chest
                for (int index = 0; index < chests.Count; index++)
                {
                    if (DetectCollisionChest(chests[index], flyingNet))
                    {
                        this.RemoveChest(chests[index]);
                        this.Score += 3;
                        lblScore.Text = "Score: " + this.Score;
                    }
                }
                // Mines
                for (int index = 0; index < mines.Count; index++)
                {
                    if (DetectCollisionMine(mines[index], flyingNet))
                    {
                        this.RemoveMine(mines[index]);
                        this.Score -= 4;
                        this.Lives -= 2;
                        lblScore.Text = "Score: " + this.Score;
                        lblLives.Text = "Lives: " + this.Lives;
                    }
                }
                // Fish
                for (int index = 0; index < fishes.Count; index++)
                {
                    if (DetectCollisionFish(fishes[index], flyingNet))
                    {
                        if (Fish.heart == true)
                        {
                            this.RemoveFish(fishes[index]);
                            this.Lives += 1;
                            lblLives.Text = "Lives: " + this.Lives;
                        }
                        else
                        {
                            this.RemoveFish(fishes[index]);
                            this.Score++;
                            lblScore.Text = "Score: " + this.Score;
                        }
                    } 
                }
                // Shark
                for (int index = 0; index < sharks.Count; index++)
                {
                    if (DetectCollisionShark(sharks[index], flyingNet))
                    {
                        this.RemoveShark(sharks[index]);
                        this.Score--; //this.Score += 5;
                        this.Lives -= 1;
                        lblLives.Text = "Lives: " + this.Lives;
                        lblScore.Text = "Score: " + this.Score;
                    }
                }
            }  
            if (gameTime == 400)
            {
                lblTitle.Text = "Congratulations";
                flyingNet.Dispose();
                gameTimer.Stop();
                DBOperations.SaveScore(this.Score, Form1.User.UserId);
                MessageBox.Show(Form1.User.Username.ToString() + " your score is: " + Score.ToString());
                Close();
            }
            if (Lives == 0)
            {
                lblTitle.Text = "Game Over";
                flyingNet.Dispose();
                gameTimer.Stop();
                DBOperations.SaveScore(this.Score, Form1.User.UserId);
                MessageBox.Show(Form1.User.Username + " your score is: " + Score.ToString());
                Close();
            }
        }
        //Detect collision with different object to become true
        public bool DetectCollisionFish(PictureBox ctrl1, PictureBox ctrl2)
        {
            Rectangle crtl1Rect = new Rectangle(ctrl1.Left, ctrl1.Top, ctrl1.Width, ctrl1.Height);
            Rectangle crtl2Rect = new Rectangle(ctrl2.Left, ctrl2.Top, ctrl2.Width, ctrl2.Height);
            return crtl1Rect.IntersectsWith(crtl2Rect);
        }
        public bool DetectCollisionShark(PictureBox ctrl1, PictureBox ctrl3)
        {
            Rectangle crtl1Rect = new Rectangle(ctrl1.Left, ctrl1.Top, ctrl1.Width, ctrl1.Height);
            Rectangle crtl3Rect = new Rectangle(ctrl3.Left, ctrl3.Top, ctrl3.Width, ctrl3.Height);
            return crtl1Rect.IntersectsWith(crtl3Rect);
        }
        public bool DetectCollisionChest(PictureBox ctrl1, PictureBox ctrl4)
        {
            Rectangle crtl1Rect = new Rectangle(ctrl1.Left, ctrl1.Top, ctrl1.Width, ctrl1.Height);
            Rectangle crtl4Rect = new Rectangle(ctrl4.Left, ctrl4.Top, ctrl4.Width, ctrl4.Height);
            return crtl1Rect.IntersectsWith(crtl4Rect);
        }
        public bool DetectCollisionMine(PictureBox ctrl1, PictureBox ctrl5)
        {
            Rectangle crtl1Rect = new Rectangle(ctrl1.Left, ctrl1.Top, ctrl1.Width, ctrl1.Height);
            Rectangle crtl5Rect = new Rectangle(ctrl5.Left, ctrl5.Top, ctrl5.Width, ctrl5.Height);
            return crtl1Rect.IntersectsWith(crtl5Rect);
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
    }
}


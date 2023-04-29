using System;
using System.Windows.Forms;

namespace CourseworkFish
{
    public partial class Form1 : Form
    {
        private static User user;
        //Create a public User getter, setter object to be used in Play form
        public static User User { get => user; set => user = value; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //Validation to check if the textbox has text in it
            if (String.IsNullOrWhiteSpace(tbUsername.Text))
            {
                lblError.Visible = true;// show this label if it is empty 
            }
            else
            {
                string username = tbUsername.Text.ToString();
                DBOperations.SaveUser(username);

                Play play = new Play();
                play.Show(); 
            }
            tbUsername.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //when form shows do not show error
            lblError.Visible = false;
        }
        private void btnInstructions_Click(object sender, EventArgs e)
        {
            //if the instruction button is clicked show instruction form
            Instructions instructions = new Instructions();
            instructions.Show();
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            //if the results button is clicked show results form
            Results results = new Results();
            results.Show();
        }
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}

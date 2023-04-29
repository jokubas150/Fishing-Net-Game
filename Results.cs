using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CourseworkFish
{
    public partial class Results : Form
    {
        private TableLayoutPanel scrpnl = new TableLayoutPanel();

        public Results()
        {
            InitializeComponent();
        }
        //Button which closes form
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        //The referece for the showResults() method https://stackoverflow.com/questions/13992429/adding-controls-to-tablelayoutpanel-dynamically-during-runtime
        private void showResults()
        {
            scrpnl.ColumnCount = 4;
            scrpnl.RowCount = 10;

            scrpnl.Controls.Clear();
            //Create column names
            scrpnl.Controls.Add(new Label () { Text = "Number", Anchor = AnchorStyles.Left, AutoSize = true }, 0, 0);
            scrpnl.Controls.Add(new Label () { Text = "Score", Anchor = AnchorStyles.Left, AutoSize = true }, 1, 0);
            scrpnl.Controls.Add(new Label () { Text = "Username", Anchor = AnchorStyles.Left, AutoSize = true }, 2, 0);
            scrpnl.Controls.Add(new Label () { Text = "Date", Anchor = AnchorStyles.Left, AutoSize = true }, 3, 0);

            // Reference for foreach loop - https://stackoverflow.com/questions/30268965/using-variable-of-the-foreach-loop-outside-foreach-loop
            int i = 0;
            //Loop through LoadScores data and display it in table
            foreach (var score in DBOperations.LoadScores())
            {
                scrpnl.Controls.Add(new Label() { Text = (i + 1).ToString(), Anchor = AnchorStyles.Left}, 0, i + 1);
                scrpnl.Controls.Add(new Label() { Text = score.UserId.ToString(), Anchor = AnchorStyles.Left }, 1, i + 1);
                scrpnl.Controls.Add(new Label() { Text = score.Score.ToString(), Anchor = AnchorStyles.Left }, 2, i + 1);
                scrpnl.Controls.Add(new Label() { Text = score.GameDate.ToString(), Anchor = AnchorStyles.Left }, 3, i + 1);
                i++;
            }

            this.Controls.Add(scrpnl);
        }

        private void Results_Load(object sender, EventArgs e)
        {
            
        }

        private void Results_VisibleChanged(object sender, EventArgs e)
        {
            showResults();
        }
    }
}

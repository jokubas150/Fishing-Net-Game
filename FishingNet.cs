using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace CourseworkFish
{
    public class FishingNet : PictureBox, IGameEntity
    {
        //velocity to manipulate with with arrow keys
        private int velocity = 10;

        public FishingNet()
        {
            this.Image = Image.FromFile("fishingNet.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            ((Bitmap)this.Image).MakeTransparent(((Bitmap)this.Image).GetPixel(1, 1));
            this.BackColor = Color.Transparent;
            this.Height = 58;
            this.Width = 80;
        }

        public void Update(Panel theCanvas)
        {
            
        }
        // add or remove to velocity depending on direction
        public void MoveGameEntity(Direction direction)
        {
            if (direction == Direction.Right)
                this.Left += velocity;
            else if (direction == Direction.Left)
                this.Left -= velocity;
            else if (direction == Direction.Up)
                this.Top -= velocity;
            else if (direction == Direction.Down)
                this.Top += velocity;
        }
    }
}

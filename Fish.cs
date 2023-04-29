using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkFish
{
    public class Fish : PictureBox, IGameEntity
    {
        
        public static int speed = 0; // create static field to change it in the Play class
        public static bool heart = false;
        public Fish()
        {
            this.Image = Image.FromFile("fish.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            ((Bitmap)this.Image).MakeTransparent(((Bitmap)this.Image).GetPixel(1, 1));
            this.BackColor = Color.Transparent;

            if (heart == true)// change the image of fish to heart.png
            {
                this.Image = Image.FromFile("heart.png");
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                ((Bitmap)this.Image).MakeTransparent(((Bitmap)this.Image).GetPixel(1, 1));
                this.BackColor = Color.Transparent;
            }

            this.Width = 33;
            this.Height = 40;
            this.Left = 900;

            Random random = new Random();
            this.Top = random.Next(50, 590);
            Fish.speed = 0;
        }
        //Update the entity on the panel
        public void Update(Panel c)
        {
            MoveGameEntity(Direction.Left);

            if (this.Left < -30)
            {
                c.Controls.Remove(this);
            }
        }
        public void MoveGameEntity(Direction direction)
        {
            this.Left -= Fish.speed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkFish
{
    public class Mine : PictureBox, IGameEntity
    {
        public static int speed = 0;
        
        public Mine()
        {
            this.Image = Image.FromFile("mine.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            ((Bitmap)this.Image).MakeTransparent(((Bitmap)this.Image).GetPixel(1, 1));
            this.BackColor = Color.Transparent;

            this.Width = 55;
            this.Height = 60;
            this.Left = 900;
            
            Random random = new Random();
            this.Top = random.Next(50, 590);
            Mine.speed = 0;
        }

        public void Update(Panel c)
        {
            MoveGameEntity(Direction.Right);

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
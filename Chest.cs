using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CourseworkFish
{
    public class Chest : PictureBox, IGameEntity
    {
        public static int speed = 0;
        public Chest()
        {
            this.Image = Image.FromFile("chest.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            ((Bitmap)this.Image).MakeTransparent(((Bitmap)this.Image).GetPixel(1, 1));
            this.BackColor = Color.Transparent;

            this.Width = 45;
            this.Height = 50;
            this.Top = 900;

            Random random = new Random();
            this.Top = random.Next(550, 590);
            this.Left = random.Next(10, 1000);
            Chest.speed = 0;
        }

        public void Update(Panel c)
        {
            MoveGameEntity(Direction.Up);

            if (this.Top < -10)
            {
                c.Controls.Remove(this);
            }
        }
        public void MoveGameEntity(Direction direction)
        {
            this.Top -= Chest.speed;
        }
    }
}
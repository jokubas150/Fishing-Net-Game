using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CourseworkFish
{
    interface IGameEntity
    {
        //Update the objects that inherit this class update on the screen
        void Update(Panel theCanvas);
        //move the direction of the object
        void MoveGameEntity(Direction direction);
    }
}

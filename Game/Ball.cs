using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Ball : GameObject
    {
        public int HorizontalSpeed { get; private set; }
        public int VerticalSpeed { get; private set; }

        public Ball() 
            : this(0, 0, 5, 5) { }
        public Ball(int x, int y)
            : this(x, y, 5, 5) { }
        public Ball(int x, int y, int hSpeed, int vSpeed)
            : base(x, y)
        {
            HorizontalSpeed = hSpeed;
            VerticalSpeed = vSpeed;
            CheckZeroSpeed();
        }

        public override void Move(object sender)
        {
            var form = sender as Form;
            X += HorizontalSpeed;
            Y += VerticalSpeed;
            if (CheckHorizntalWallCollision(form))
            {
                ResolveHorizntalWallCollision(form);
                HorizontalSpeed = -HorizontalSpeed;
            }
            if (CheckVerticalWallCollision(form))
            {
                ResolveVerticallWallCollision(form);
                VerticalSpeed = -VerticalSpeed;
            }
        }

        private void CheckZeroSpeed()
        {
            if (HorizontalSpeed == 0)
                HorizontalSpeed++;
            if (VerticalSpeed == 0)
                VerticalSpeed++;
        }
    }
}

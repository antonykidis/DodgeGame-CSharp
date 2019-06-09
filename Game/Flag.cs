using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Flag : GameObject
    {
        public Player Owner { get; private set; }
        private readonly int movingDirection;

        public Flag(int x, int y)
            : this(x, y, 10, 10) { }

        public Flag(int x, int y, int width, int height)
            : base(x, y)
        {
            Speed = new Random().Next(-5, 5);
            movingDirection = new Random().Next(10000);
        }

        public void SetOwner(Player player)
        {
            Owner = player;
            if (player != null && !player.HasFlag)
            {
                player.CaptureFlag();
                player.SetPlayerSize(player.Width * 2, player.Height * 2);
            }
        }

        public override void Move(object sender)
        {
            var form = sender as Form;
            switch (movingDirection % 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        X += Speed;
                        break;
                    }
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    {
                        Y += Speed;
                        break;
                    }
            }
            if (CheckHorizntalWallCollision(form))
            {
                ResolveHorizntalWallCollision(form);
                Speed = -Speed;
            }
            if (CheckVerticalWallCollision(form))
            {
                ResolveHorizntalWallCollision(form);
                Speed = -Speed;
            }
        }

        public void SetFlagOriginalPosition()
        {
            X = OriginalPosition.X;
            Y = OriginalPosition.Y;
        }
    }
}

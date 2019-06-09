using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Player : GameObject
    {
        public readonly int PlayerId;
        public bool HasFlag { get; private set; }
        public int Score { get; private set; }
        public Zone HomeZone { get; private set; }
        public readonly int OriginalSpeed;
        public bool IsAlive { get; private set; }

        public Player()
            : this(0, 0) { }
        public Player(int x, int y)
            : this(0, 0, 0) { }
        public Player(int x, int y, int playerId)
            : base(x, y)
        {
            OriginalSpeed = 3;
            Speed = OriginalSpeed;
            PlayerId = playerId;
            CreateHomeZone();
            IsAlive = true;
        }

        private void CreateHomeZone()
        {
            if (X - Width < 0)
            {
                if (Y - Height < 0)
                    HomeZone = new HomeZone(X, Y, Width * 2, Height * 2);
                else
                    HomeZone = new HomeZone(X, Y - Height, Width * 2, Height * 2);
            }
            else
            {
                if (Y - Height < 0)
                    HomeZone = new HomeZone(X - Width, Y, Width * 2, Height * 2);
                else
                    HomeZone = new HomeZone(X - Width, Y - Height, Width * 2, Height * 2);
            }
        }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void SetScore(int score)
        {
            Score = score;
        }

        public void CaptureFlag()
        {
            Speed--;
            HasFlag = true;
        }

        public void LostFlag()
        {
            Speed++;
            HasFlag = false;
        }

        public void SetPlayerSpeed(int speed)
        {
            Speed = speed;
        }

        public void SetPlayerSize(int newWidth, int newHeight)
        {
            Width = newWidth;
            Height = newHeight;
        }

        public void KillPlayer()
        {
            IsAlive = false;
            SetPlayerSpeed(0);
            SetPosition(OriginalPosition);
            SetPlayerSize(0, 0);
        }
    }
}

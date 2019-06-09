using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Game
{
    class CaptureTheFlagMode : GameForm
    {
        public CaptureTheFlagMode(GameModel model)
            : base(model)
        {
            Game.CreateNewFlag();
        }

        protected override void OnGameTimerTick()
        {
            tickCounter++;
            PlayerMovement();
            BallsCollisions();
            FlagCapture();
            ShowScoreString();
            Invalidate();
        }

        private void FlagCapture()
        {
            Game.Flag.Move(this);
            Game.CheckFlagCapture();
            Game.CheckIfFlagIsAtHomeZone();
        }

        private void BallsCollisions()
        {
            Game.Balls.ForEach(b =>
            {
                b.Move(this);
                Invalidate();
                foreach (var player in Game.Players)
                {
                    if (Game.IsBallHitPlayer(b, player))
                    {
                        player.SetPosition(player.OriginalPosition);
                        if (player.HasFlag)
                            Game.PlayerLostFlag();
                        Invalidate();
                    }
                }
            });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphics.Clear(Color.White);
            foreach (var ball in Game.Balls)
            {
                graphics.FillEllipse(Brushes.Red, ball.X, ball.Y, ball.Width, ball.Height);
                Invalidate();
            }
            var playerColors = new List<Color>
            {
                Color.Blue,
                Color.Green,
                Color.Orange,
                Color.Orchid,
            };
            foreach (var player in Game.Players)
            {
                graphics.FillRectangle(new SolidBrush(playerColors[player.PlayerId]), player.X, player.Y, player.Width, player.Height);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, playerColors[player.PlayerId])), player.HomeZone.Area);
                Invalidate();
            }
            graphics.FillEllipse(Brushes.Aqua, Game.Flag.X, Game.Flag.Y, Game.Flag.Width, Game.Flag.Height);
            Invalidate();
        }
    }
}

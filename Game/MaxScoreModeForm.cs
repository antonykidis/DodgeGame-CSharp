using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class MaxScoreModeForm : GameForm
    {
        public MaxScoreModeForm(GameModel model)
            : base(model) { }

        protected override void OnGameTimerTick()
        {
            tickCounter++;
            PlayerMovement();
            Game.GivePlayerPoints(tickCounter);
            ShowScoreString();
            Game.MaxScoreModeBallsMoves(this);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
            Invalidate();
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
                Invalidate();
            }
            Invalidate();
        }
    }
}

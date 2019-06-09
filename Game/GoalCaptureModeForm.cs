using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class GoalCaptureModeForm : GameForm
    {
        public GoalCaptureModeForm(GameModel model)
            : base(model)
        {
            Game.CreateNewGoal();
        }

        protected override void OnGameTimerTick()
        {
            tickCounter++;
            Game.CheckSlowDownZones();
            Game.CheckSizeIncreaseZones();
            PlayerMovement();
            Game.CheckGoalCapture();
            Game.GoalCaptureModeBallsMoves(this);
            ShowScoreString();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
            foreach (var zone in Game.SlowDownZones)
            {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), zone.Area);
            }
            foreach (var zone in Game.SizeIncreaseZones)
            {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 255, 0)), zone.Area);
            }
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
            graphics.FillEllipse(Brushes.Aqua, Game.Goal.X, Game.Goal.Y, Game.Goal.Width, Game.Goal.Height);
            Invalidate();
        }
    }
}

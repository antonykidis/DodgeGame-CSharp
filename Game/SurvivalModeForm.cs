using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class SurvivalModeForm : GameForm
    {
        public SurvivalModeForm(GameModel model)
            : base(model)
        {
            Game.CreateSafeZone();
        }

        protected override void OnGameTimerTick()
        {
            tickCounter++;
            PlayerMovement();
            Game.SurvivalModeBallsMoves(this);
            SafeZoneActions();
            Game.GivePlayerPoints(tickCounter);
            Invalidate();
        }

        private void SafeZoneActions()
        {
            Game.CheckSafeZone();
            if (tickCounter % 20 == 0)
                Game.ShrinkSafeZone();
            if (Game.CheckAlivePlayers())
                tickCounter = 1000 * 60 * Game.GameTimeInMinutes;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
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
            graphics.DrawRectangle(new Pen(Color.Red, Game.SafeZone.BorderWidth), Game.SafeZone.Area);
            Invalidate();
        }
    }
}

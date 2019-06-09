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
    class GameForm : Form
    {
        protected GameModel Game { get; set; }
        protected List<Keys> pressedKeys = new List<Keys>();
        protected int tickCounter;

        private Thread _gameThread;
        private ManualResetEvent _evExit;

        public GameForm(GameModel model)
        {
            ClientSize = new Size(model.FieldWidth, model.FieldHeight);
            Game = model;

            KeyDown += (sender, key) =>
            {
                if (!pressedKeys.Contains(key.KeyCode))
                    pressedKeys.Add(key.KeyCode);
            };
            KeyUp += (sender, key) => pressedKeys.Remove(key.KeyCode);
            SizeChanged += (sender, args) =>
            {
                Invalidate();
            };
            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint,
                true);
        }

        private void GameThreadProc()
        {
            IAsyncResult tick = null;
            var timerTick = 15;
            while (!_evExit.WaitOne(timerTick))
            {
                if (tick != null)
                {
                    if (!tick.AsyncWaitHandle.WaitOne(0))
                    {
                        if (WaitHandle.WaitAny(
                            new WaitHandle[]
                            {
                                _evExit,
                                tick.AsyncWaitHandle
                            }) == 0)
                        {
                            return;
                        }
                    }
                }
                tick = BeginInvoke(new MethodInvoker(OnGameTimerTick));
                if (tickCounter > 1000 / timerTick * 60 * Game.GameTimeInMinutes)
                    break;
            }
            GameOver();
        }

        protected virtual void OnGameTimerTick()
        {
            return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _evExit = new ManualResetEvent(false);
            _gameThread = new Thread(GameThreadProc)
            {
                Name = "Game Thread"
            };
            _gameThread.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            _evExit.Set();
            _gameThread.Join();
            _evExit.Close();
            base.OnClosed(e);
        }

        protected void PlayerMovement()
        {
            Game.PlayerMovement(pressedKeys, this);
            Invalidate();
        }

        protected void ShowScoreString()
        {
            var scoreString = "";
            foreach (var player in Game.Players)
            {
                scoreString += $"P{player.PlayerId + 1}={player.Score}; ";
            }
            Text = scoreString;
        }

        private void GameOver()
        {
            if (Visible == false)
                return;
            if (InvokeRequired)
                Invoke(new Action<string>((s) => Text = s), "Game over!");
            var winner = Game.Players.OrderByDescending(p => p.Score).First();
            var gameResults = "";
            foreach (var player in Game.Players.OrderByDescending(p => p.Score))
            {
                gameResults += $"Player {player.PlayerId + 1}: {player.Score} points \n";
            }
            var startNewGame = MessageBox.Show(
                $"Player {winner.PlayerId + 1} won! \n \n" +
                $"Game score: \n" +
                gameResults +
                $"\nStart new game?",
                "Game Over!", MessageBoxButtons.YesNo);
            if (startNewGame == DialogResult.Yes)
                Invoke(new Action(() => Close()));
            else
                Application.Exit();
        }
    }
}

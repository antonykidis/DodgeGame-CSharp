using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class GameModel
    {
        public List<Ball> Balls { get; private set; }
        public List<Player> Players { get; private set; }
        public readonly int FieldWidth;
        public readonly int FieldHeight;
        public Flag Flag { get; private set; }
        public int GameTimeInMinutes { get; private set; }
        public List<SlowDownZone> SlowDownZones { get; private set; }
        public List<SizeIncreaseZone> SizeIncreaseZones { get; private set; }
        public SafeZone SafeZone { get; private set; }
        public Goal Goal { get; private set; }

        public GameModel()
            : this(500, 400) { }
        public GameModel(int fieldWidth, int fieldHeight)
            : this(fieldWidth, fieldHeight, 0) { }
        public GameModel(int fieldWidth, int fieldHeight, int ballsCount)
            : this(fieldWidth, fieldHeight, ballsCount, 1) { }
        public GameModel(int fieldWidth, int fieldHeight, int ballsCount,
            int numberOfPlayers)
            : this(fieldWidth, fieldHeight, ballsCount, numberOfPlayers, 0, 0) { }
        public GameModel(int fieldWidth, int fieldHeight, int ballsCount,
            int numberOfPlayers, int firstPlayerX, int firstPlayerY)
        {
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
            CreateRandomBallsList(ballsCount);
            CreatePlayers(numberOfPlayers, firstPlayerX, firstPlayerY);
            GameTimeInMinutes = 3;
            SlowDownZones = new List<SlowDownZone>();
            SizeIncreaseZones = new List<SizeIncreaseZone>();
        }

        private void CreateRandomBallsList(int count)
        {
            var ballMaxSpeed = 3;
            Balls = new List<Ball>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                Balls.Add(new Ball(random.Next(10, FieldWidth - 10),
                    random.Next(10, FieldHeight - 10),
                    random.Next(-ballMaxSpeed, ballMaxSpeed),
                    random.Next(-ballMaxSpeed, ballMaxSpeed)));
            }
        }

        private void CreatePlayers(int count, int firstPlayerX, int firstPlayerY)
        {
            Players = new List<Player>();
            var firstPlayer = new Player(firstPlayerX, firstPlayerY, 0);
            Players.Add(firstPlayer);
            var playerStartPosition = new List<Point>
            {
                new Point(FieldWidth - firstPlayer.Width, FieldHeight - firstPlayer.Height),
                new Point(0, FieldHeight - firstPlayer.Height),
                new Point(FieldWidth - firstPlayer.Width, 0)
            };
            for (int i = 0; i < count - 1; i++)
            {
                Players.Add(new Player(playerStartPosition[i].X, playerStartPosition[i].Y, i + 1));
            }
        }

        public void SetGameTime(int minutes)
        {
            GameTimeInMinutes = minutes;
        }

        public void CreateNewFlag()
        {
            Flag = new Flag(FieldWidth / 2, FieldHeight / 2);
        }

        public void CreateNewGoal()
        {
            var rnd = new Random();
            Goal = new Goal(rnd.Next(30, FieldWidth - 30), rnd.Next(30, FieldHeight - 30));
        }

        private void AddNewSlowDownZone()
        {
            var newSideSize = Goal.Width * 4;
            var newZone = new SlowDownZone(Goal.X - newSideSize / 4, Goal.Y - newSideSize / 4, newSideSize, newSideSize);
            SlowDownZones.Add(newZone);
        }

        private void AddNewSizeIncreaseZone()
        {
            var newSideSize = Goal.Width * 4;
            var newZone = new SizeIncreaseZone(Goal.X - newSideSize / 4, Goal.Y - newSideSize / 4, newSideSize, newSideSize);
            SizeIncreaseZones.Add(newZone);
        }

        public void CreateSafeZone()
        {
            SafeZone = new SafeZone(0, 0, FieldWidth, FieldHeight);
        }

        public void CheckIfFlagIsAtHomeZone()
        {
            if (Flag.Owner == null)
                return;
            var flagHitbox = new Rectangle(Flag.X, Flag.Y, Flag.Width, Flag.Height);
            if (flagHitbox.IntersectsWith(Flag.Owner.HomeZone.Area))
                FlagCaptured();
        }

        private void FlagCaptured()
        {
            Flag.Owner.AddScore(1);
            Flag.Owner.SetOriginalSize();
            Flag.Owner.LostFlag();
            CreateNewFlag();
        }

        public void PlayerLostFlag()
        {
            Flag.Owner.SetOriginalSize();
            Flag.Owner.LostFlag();
            CreateNewFlag();
        }

        public void CheckFlagCapture()
        {
            var flagHitbox = new Rectangle(Flag.X, Flag.Y, Flag.Width, Flag.Height);
            foreach (var player in Players)
            {
                var playerHitbox = new Rectangle(player.X, player.Y, player.Width, player.Height);
                if (playerHitbox.IntersectsWith(flagHitbox))
                {
                    if (Flag.Owner == null)
                        Flag.SetOwner(player);
                    else if (Flag.Owner == player)
                        Flag.SetPosition(player.X + player.Width / 4, player.Y + player.Height / 4);
                }
            }
        }

        public void CheckGoalCapture()
        {
            var goalHitbox = new Rectangle(Goal.X, Goal.Y, Goal.Width, Goal.Height);
            foreach (var player in Players)
            {
                var playerHitbox = new Rectangle(player.X, player.Y, player.Width, player.Height);
                if (playerHitbox.IntersectsWith(goalHitbox))
                {
                    player.AddScore(1);
                    if (new Random().Next(10000) % 10 > 5)
                        AddNewSlowDownZone();
                    else
                        AddNewSizeIncreaseZone();
                    CreateNewGoal();
                }
            }
        }

        #region PlayerControls
        readonly Keys[] firstPlayerControls = new Keys[]
        {
            Keys.W, //Up
            Keys.D, //Right
            Keys.S, //Down
            Keys.A //Left
        };

        readonly Keys[] secondPlayerControls = new Keys[]
        {
            Keys.Up,
            Keys.Right,
            Keys.Down,
            Keys.Left
        };

        readonly Keys[] thirdPlayerControls = new Keys[]
        {
            Keys.I,
            Keys.L,
            Keys.K,
            Keys.J
        };

        readonly Keys[] forthPlayerControls = new Keys[]
        {
            Keys.NumPad8,
            Keys.NumPad6,
            Keys.NumPad5,
            Keys.NumPad4
        };
        #endregion

        public void PlayerMovement(List<Keys> currentPressedKeys, Form form)
        {
            foreach (var key in currentPressedKeys)
            {
                var player = Players.First();
                var controls = firstPlayerControls;
                if (firstPlayerControls.Contains(key))
                {
                    player = Players.First();
                    controls = firstPlayerControls;
                }
                else if (secondPlayerControls.Contains(key) && Players.Count > 1)
                {
                    player = Players[1];
                    controls = secondPlayerControls;
                }
                else if (thirdPlayerControls.Contains(key) && Players.Count > 2)
                {
                    player = Players[2];
                    controls = thirdPlayerControls;
                }
                else if (forthPlayerControls.Contains(key) && Players.Count > 3)
                {
                    player = Players[3];
                    controls = forthPlayerControls;
                }
                if (key == controls[0])
                    player.MoveUp(form);
                else if (key == controls[1])
                    player.MoveRight(form);
                else if (key == controls[2])
                    player.MoveDown(form);
                else if (key == controls[3])
                    player.MoveLeft(form);
            }
        }

        public void CheckSlowDownZones()
        {
            var zone = SlowDownZones.FirstOrDefault();
            foreach (var player in Players)
            {
                var playerHitbox = new Rectangle(player.X, player.Y, player.Width, player.Height);

                if (SlowDownZones.Any(s => s.Area.IntersectsWith(playerHitbox)))
                    player.SetPlayerSpeed(zone.PlayerSpeedInsideZone);
                else
                    player.SetPlayerSpeed(player.OriginalSpeed);
            }
        }

        public void CheckSizeIncreaseZones()
        {
            var zone = SizeIncreaseZones.FirstOrDefault();
            foreach (var player in Players)
            {
                var playerHitbox = new Rectangle(player.X, player.Y, player.Width, player.Height);

                if (SizeIncreaseZones.Any(s => s.Area.IntersectsWith(playerHitbox)))
                    player.SetPlayerSize(zone.PlayerWidthIncreaseValue, zone.PlayerHeightIncreaseValue);
                else
                    player.SetOriginalSize();
            }
        }

        public void CheckSafeZone()
        {
            foreach (var player in Players.Where(p => p.IsAlive))
            {
                var playerHitbox = new Rectangle(player.X, player.Y, player.Width, player.Height);
                if (!playerHitbox.IntersectsWith(SafeZone.Area))
                {
                    player.KillPlayer();
                }
            }
        }

        public void GivePlayerPoints(int tickCounter)
        {
            foreach (var player in Players.Where(p => p.IsAlive))
            {
                if (tickCounter % 100 == 0)
                    player.AddScore(1);
            }
        }

        public bool CheckAlivePlayers()
        {
            return Players.Where(p => p.IsAlive).Count() == 0;
        }

        public void ShrinkSafeZone()
        {
            var player = Players.First();
            if (SafeZone.Area.Height > player.OriginalHeight * 5)
                SafeZone.DecreaseZoneHeight();
            if (SafeZone.Area.Width > player.OriginalWidth * 5)
                SafeZone.DecreaseZoneWidth();
        }

        public void SurvivalModeBallsMoves(Form form)
        {
            Balls.ForEach(ball =>
            {
                ball.Move(form);
                foreach (var player in Players)
                {
                    if (IsBallHitPlayer(ball, player))
                    {
                        player.KillPlayer();
                    }
                }
            });
        }

        public void MaxScoreModeBallsMoves(Form form)
        {
            Balls.ForEach(ball =>
            {
                ball.Move(form);
                foreach (var player in Players)
                {
                    if (IsBallHitPlayer(ball, player))
                    {
                        player.SetPosition(player.OriginalPosition);
                        player.AddScore(-player.Score / 3);
                    }
                }
            });
        }

        public void GoalCaptureModeBallsMoves(Form form)
        {
            Balls.ForEach(ball =>
            {
                ball.Move(form);
                foreach (var player in Players)
                {
                    if (IsBallHitPlayer(ball, player))
                    {
                        player.SetPosition(player.OriginalPosition);
                        if (player.Score > 0)
                            player.AddScore(-1);
                    }
                }
            });
        }

        public void CaptureTheFlagModeBallsMoves(Form form)
        {
            Balls.ForEach(ball =>
            {
                ball.Move(form);
                foreach (var player in Players)
                {
                    if (IsBallHitPlayer(ball, player))
                    {
                        player.SetPosition(player.OriginalPosition);
                        if (player.HasFlag)
                            PlayerLostFlag();
                    }
                }
            });
        }

        private bool IsBallHitPlayer(Ball ball, Player player)
        {
            var playerHitbox = new Rectangle(player.X, player.Y, player.Width, player.Height);
            var ballHitbox = new Rectangle(ball.X, ball.Y, ball.Width, ball.Height);
            return playerHitbox.IntersectsWith(ballHitbox);
        }
    }
}

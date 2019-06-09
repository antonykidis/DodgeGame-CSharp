using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    [TestFixture]
    class GameTests
    {
        [Test]
        public void Should_CreateNewFormWithCertainWidthAndHeight()
        {
            var random = new Random();
            var width = random.Next(50, 100);
            var height = random.Next(50, 100);
            var testGame = new GameModel(width, height);
            var testForm = new MaxScoreModeForm(testGame);

            Assert.AreEqual(width, testForm.ClientSize.Width);
            Assert.AreEqual(height, testForm.ClientSize.Height);
        }

        [Test]
        public void Should_GameContainsPlayer()
        {
            var testGame = new GameModel();

            Assert.NotNull(testGame.Players);
        }

        [Test]
        public void Should_GameHaveCertainBallsCount()
        {
            var count = new Random().Next(1, 100);
            var testGame = new GameModel(300, 300, count);

            Assert.AreEqual(count, testGame.Balls.Count);
        }

        [Test]
        public void Should_PlayerMoveRight()
        {
            var testGame = new GameModel(300, 300);
            var testForm = new MaxScoreModeForm(testGame);
            var player = testGame.Players.First();
            var playerX = player.X;
            var playerY = player.Y;
            var playerSpeed = player.Speed;

            player.MoveRight(testForm);

            Assert.AreEqual(playerX + playerSpeed, player.X);
            Assert.AreEqual(playerY, player.Y);
        }
        [Test]
        public void Should_PlayerMoveLeft()
        {
            var testGame = new GameModel(300, 300, 0, 1, 20, 20);
            var testForm = new MaxScoreModeForm(testGame);
            var player = testGame.Players.First();
            var playerX = player.X;
            var playerY = player.Y;
            var playerSpeed = player.Speed;

            player.MoveLeft(testForm);

            Assert.AreEqual(playerX - playerSpeed, player.X);
            Assert.AreEqual(playerY, player.Y);
        }

        [Test]
        public void Should_PlayerMoveDown()
        {
            var testGame = new GameModel(300, 300);
            var testForm = new MaxScoreModeForm(testGame);
            var player = testGame.Players.First();
            var playerX = player.X;
            var playerY = player.Y;
            var playerSpeed = player.Speed;

            player.MoveDown(testForm);

            Assert.AreEqual(playerX, player.X);
            Assert.AreEqual(playerY + playerSpeed, player.Y);
        }

        [Test]
        public void Should_PlayerMoveUp()
        {
            var testGame = new GameModel(300, 300, 0, 1, 20, 20);
            var testForm = new MaxScoreModeForm(testGame);
            var player = testGame.Players.First();
            var playerX = player.X;
            var playerY = player.Y;
            var playerSpeed = player.Speed;

            player.MoveUp(testForm);

            Assert.AreEqual(playerX, player.X);
            Assert.AreEqual(playerY - playerSpeed, player.Y);
        }

        [Test]
        public void Should_StopIfGameFieldLeftOrTopEdgeReached()
        {
            var testGame = new GameModel(300, 300);
            var testForm = new MaxScoreModeForm(testGame);
            var player = testGame.Players.First();
            var playerX = player.X;
            var playerY = player.Y;
            var playerSpeed = player.Speed;

            player.MoveUp(testForm);
            player.MoveLeft(testForm);

            Assert.AreEqual(playerX, player.X);
            Assert.AreEqual(playerY, player.Y);
        }

        [Test]
        public void Should_StopIfGameFieldRightOrBottomEdgeReached()
        {
            var testGame = new GameModel(300, 300, 0, 1, 290, 290);
            var testForm = new MaxScoreModeForm(testGame);
            var player = testGame.Players.First();
            var playerX = player.X;
            var playerY = player.Y;
            var playerSpeed = player.Speed;

            player.MoveDown(testForm);
            player.MoveRight(testForm);

            Assert.AreEqual(playerX, player.X);
            Assert.AreEqual(playerY, player.Y);
        }

        [Test]
        public void Should_BallMove()
        {
            var testGame = new GameModel(300, 300, 1);
            var testForm = new MaxScoreModeForm(testGame);
            var ballX = testGame.Balls.First().X;
            var ballY = testGame.Balls.First().Y;
            var ballHorSpeed = testGame.Balls.First().HorizontalSpeed;
            var ballVerSpeed = testGame.Balls.First().VerticalSpeed;

            testGame.Balls.First().Move(testForm);

            Assert.AreEqual(ballX + ballHorSpeed, testGame.Balls.First().X);
            Assert.AreEqual(ballY + ballVerSpeed, testGame.Balls.First().Y);
        }

        [Test]
        public void Should_CreateCertainNumberOfBallsInGame()
        {
            var ballsCount = new Random().Next(0, 1000);
            var testGame = new GameModel(300, 300, ballsCount);

            Assert.AreEqual(ballsCount, testGame.Balls.Count);
        }

        [Test]
        public void Should_BallStayInsideGameField()
        {
            var testGame = new GameModel(200, 200, 1000);
            var testForm = new MaxScoreModeForm(testGame);

            for (int i = 0; i < 10000; i++)
                testGame.Balls.ForEach(b => b.Move(testForm));

            foreach (var ball in testGame.Balls)
            {
                Assert.LessOrEqual(0, ball.X);
                Assert.LessOrEqual(0, ball.Y);
                Assert.LessOrEqual(ball.X + ball.Width, testGame.FieldWidth);
                Assert.LessOrEqual(ball.Y + ball.Height, testGame.FieldHeight);
            }
        }

        [Test]
        public void Should_CreateTwoDefaultPlayersWithCertainPosition()
        {
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);

            Assert.AreEqual(2, testGame.Players.Count);

            CheckTwoDefaultPlayersPositions(testGame, testForm);
        }

        private void CheckTwoDefaultPlayersPositions(GameModel game, Form form)
        {
            var firstPlayer = game.Players[0];
            var secondPlayer = game.Players[1];

            Assert.AreEqual(0, firstPlayer.X);
            Assert.AreEqual(0, firstPlayer.Y);
            Assert.AreEqual(form.ClientSize.Width - secondPlayer.Width, secondPlayer.X);
            Assert.AreEqual(form.ClientSize.Height - secondPlayer.Height, secondPlayer.Y);
        }

        [Test]
        public void Should_CreateFourDefaultPlayersWithCertainPosition()
        {
            var testGame = new GameModel(300, 300, 0, 4);
            var testForm = new CaptureTheFlagMode(testGame);

            Assert.AreEqual(4, testGame.Players.Count);

            CheckFourDefaultPlayersPositions(testGame, testForm);
        }

        private void CheckFourDefaultPlayersPositions(GameModel game, Form form)
        {
            CheckTwoDefaultPlayersPositions(game, form);
            var thirdPlayer = game.Players[2];
            var fourthPlayer = game.Players[3];

            Assert.AreEqual(0, thirdPlayer.X);
            Assert.AreEqual(form.ClientSize.Height - fourthPlayer.Height, thirdPlayer.Y);
            Assert.AreEqual(form.ClientSize.Width - fourthPlayer.Width, fourthPlayer.X);
            Assert.AreEqual(0, fourthPlayer.Y);
        }

        [Test]
        public void Should_HaveFlagInTheCenterOfGameField()
        {
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);

            Assert.IsNotNull(testGame.Flag);
            Assert.AreEqual(testForm.ClientRectangle.Width / 2, testGame.Flag.X);
            Assert.AreEqual(testForm.ClientRectangle.Height / 2, testGame.Flag.Y);
        }

        [Test]
        public void Should_CaptureTheFlag()
        {
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPosition(testGame.Flag.X, testGame.Flag.Y);
            testGame.CheckFlagCapture();

            Assert.AreEqual(true, player.HasFlag);
            Assert.AreEqual(testGame.Flag.Owner, player);
        }

        [Test]
        public void Should_ChangePlayerSizeIfFlagIsCaptured()
        {
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPosition(testGame.Flag.X, testGame.Flag.Y);
            testGame.CheckFlagCapture();

            Assert.AreNotEqual(player.Width, player.OriginalWidth);
            Assert.AreNotEqual(player.Height, player.OriginalHeight);
        }

        [Test]
        public void Should_ChangePlayerSpeedIfFlagIsCaptured()
        {
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();
            var startSpeed = player.Speed;

            player.SetPosition(testGame.Flag.X, testGame.Flag.Y);
            testGame.CheckFlagCapture();

            Assert.AreNotEqual(startSpeed, player.Speed);
        }

        [Test]
        public void Should_ChangePlayerSize()
        {
            var newSize = new Random().Next(0, 100);
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPlayerSize(newSize, newSize);

            Assert.AreEqual(newSize, player.Width);
            Assert.AreEqual(newSize, player.Height);
        }

        [Test]
        public void Should_SetOriginalPlayerSize()
        {
            var deltaInc = new Random().Next(0, 20);
            var deltaDec = new Random().Next(0, 20);
            var testGame = new GameModel(300, 300, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPlayerSize(deltaDec, deltaDec);
            player.SetPlayerSize(deltaInc, deltaInc);
            player.SetOriginalSize();

            Assert.AreEqual(player.OriginalWidth, player.Width);
            Assert.AreEqual(player.OriginalHeight, player.Height);
        }

        [Test]
        public void Should_PlayerGetBackToOriginalPositionIfHitByBallInMultiplayerGame()
        {
            var testGame = new GameModel(300, 300, 100, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPosition(testGame.FieldWidth / 2, testGame.FieldHeight / 2);
            for (int i = 0; i < 1000; i++)
            {
                foreach (var ball in testGame.Balls)
                {
                    testGame.CaptureTheFlagModeBallsMoves(testForm);
                }
            }

            Assert.AreEqual(player.OriginalPosition.X, player.X);
            Assert.AreEqual(player.OriginalPosition.Y, player.Y);
        }

        [Test]
        public void Should_IncreasePlayerScoreIfFlagIsAtHomeZone()
        {
            var testGame = new GameModel(100, 100, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPosition(testGame.FieldWidth / 2, testGame.FieldHeight / 2);
            MovePlayerToHomeZone(player, testGame, testForm);

            Assert.AreNotEqual(0, player.Score);
        }

        private void MovePlayerToHomeZone(Player player, GameModel game, Form form)
        {
            for (int i = 0; i < 50; i++)
            {
                player.MoveUp(form);
                player.MoveLeft(form);
                game.CheckFlagCapture();
                game.CheckIfFlagIsAtHomeZone();
            }
        }

        [Test]
        public void Should_SetPlayerSpeedToNormalIfFlagIsAtHomeZone()
        {
            var testGame = new GameModel(100, 100, 0, 2);
            var testForm = new CaptureTheFlagMode(testGame);
            var player = testGame.Players.First();

            player.SetPosition(testGame.FieldWidth / 2, testGame.FieldHeight / 2);
            testGame.CheckFlagCapture();
            var playerChangedSpeed = player.Speed;
            MovePlayerToHomeZone(player, testGame, testForm);

            Assert.AreNotEqual(player.Speed, playerChangedSpeed);
        }

        [Test]
        public void Should_SetPlayerSpeed()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var player = testGame.Players.First();
            var newSpeed = new Random().Next(1, 20);

            player.SetPlayerSpeed(newSpeed);

            Assert.AreNotEqual(player.OriginalSpeed, player.Speed);
            Assert.AreEqual(newSpeed, player.Speed);
        }

        [Test]
        public void Should_KillPlayerIfOutsideOfSafeZone()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var testForm = new SurvivalModeForm(testGame);
            var player = testGame.Players.First();

            for (int i = 0; i < 100; i++)
            {
                testGame.ShrinkSafeZone();
                testGame.CheckSafeZone();
            }

            Assert.AreEqual(false, player.IsAlive);
        }

        [Test]
        public void Should_ChangePlayerSpeedAndSizeWhenKilled()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var player = testGame.Players.First();

            player.KillPlayer();

            Assert.AreEqual(false, player.IsAlive);
            Assert.AreEqual(0, player.Width);
            Assert.AreEqual(0, player.Height);
            Assert.AreEqual(0, player.Speed);
        }

        [Test]
        public void Should_BallKillPlayerInSurvivalMode()
        {
            var testGame = new GameModel(100, 100, 100, 1);
            var testForm = new SurvivalModeForm(testGame);
            var player = testGame.Players.First();

            for (int i = 0; i < 1000; i++)
            {
                testGame.SurvivalModeBallsMoves(testForm);
                if (testGame.CheckAlivePlayers())
                    break;
            }

            Assert.AreEqual(false, player.IsAlive);
        }

        private void CreateNewZoneInGoalCaptureMode(GameModel testGame, Player player)
        {
            testGame.CreateNewGoal();
            player.SetPosition(testGame.Goal.X, testGame.Goal.Y);
            testGame.CheckGoalCapture();
        }

        [Test]
        public void Should_AddPointIfGoalCaptured()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var testForm = new GoalCaptureModeForm(testGame);
            var player = testGame.Players.First();

            CreateNewZoneInGoalCaptureMode(testGame, player);

            Assert.AreEqual(1, player.Score);
        }

        [Test]
        public void Should_SlowDownPlayerInSlowDownZone()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var testForm = new GoalCaptureModeForm(testGame);
            var player = testGame.Players.First();

            while (testGame.SlowDownZones.Count < 1)
            {
                CreateNewZoneInGoalCaptureMode(testGame, player);
            }
            testGame.CheckSlowDownZones();

            Assert.AreEqual(testGame.SlowDownZones.First().PlayerSpeedInsideZone, player.Speed);
        }

        [Test]
        public void Should_ChangeToOriginalPlayerSpeedAfterEscapingSlowDownZone()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var testForm = new GoalCaptureModeForm(testGame);
            var player = testGame.Players.First();

            while (testGame.SlowDownZones.Count < 1)
            {
                CreateNewZoneInGoalCaptureMode(testGame, player);
            }
            testGame.CheckSlowDownZones();
            player.SetPosition(player.OriginalPosition);
            testGame.CheckSlowDownZones();

            Assert.AreEqual(player.OriginalSpeed, player.Speed);
        }

        [Test]
        public void Should_IncreasePlayerSizeInSizeIncreaseZone()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var testForm = new GoalCaptureModeForm(testGame);
            var player = testGame.Players.First();

            while (testGame.SizeIncreaseZones.Count < 1)
            {
                CreateNewZoneInGoalCaptureMode(testGame, player);
            }
            testGame.CheckSizeIncreaseZones();

            Assert.AreEqual(testGame.SizeIncreaseZones.First().PlayerWidthIncreaseValue, player.Width);
            Assert.AreEqual(testGame.SizeIncreaseZones.First().PlayerHeightIncreaseValue, player.Height);
        }

        [Test]
        public void Should_ChangeToOrginalPlayerSizeAfterEscapingSizeIncreaseZone()
        {
            var testGame = new GameModel(100, 100, 0, 1);
            var testForm = new GoalCaptureModeForm(testGame);
            var player = testGame.Players.First();

            while (testGame.SizeIncreaseZones.Count < 1)
            {
                CreateNewZoneInGoalCaptureMode(testGame, player);
            }
            testGame.CheckSizeIncreaseZones();
            player.SetPosition(player.OriginalPosition);
            testGame.CheckSizeIncreaseZones();

            Assert.AreEqual(player.OriginalWidth, player.Width);
            Assert.AreEqual(player.OriginalHeight, player.Height);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class GameModeSelection : Form
    {
        private int playersCountSelection = 1;
        private int ballsCount = 10;
        private int selectedFormWidth = 700;
        private int selectedFormHeight = 600;
        private int selectedGametime = 3;
        private GameModes gameMode = GameModes.MaxScore;

        public GameModeSelection()
        {
            var groupBox = new GroupBox()
            {
                Text = "Game mode",
                Font = new Font("Arial", 10)
            };

            var gameSettingsLabel = new Label
            {
                Text = "Game Settings",
                Font = new Font("Arial", 14, FontStyle.Bold)
            };

            var typeBallsCountLabel = new Label
            {
                Text = "Balls on field: " + ballsCount,
                Font = new Font("Arial", 10)
            };

            var playerCountLabel = new Label
            {
                Text = "Number of players:",
                Font = new Font("Arial", 10)
            };

            var selectGametimeLabel = new Label
            {
                Text = "Game duration:",
                Font = new Font("Arial", 10)
            };

            var selectFormSizeLabel = new Label
            {
                Text = "Set game field width and height:",
                Font = new Font("Arial", 10)
            };

            var controlsHelpButton = new Button
            {
                Text = "?",
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            var startGameButton = new Button
            {
                Text = "Start Game",
            };

            var gameMode1 = new RadioButton()
            {
                Text = "Score",
                Checked = true
            };

            var gameMode2 = new RadioButton()
            {
                Text = "Capture the flag"
            };

            var gameMode3 = new RadioButton()
            {
                Text = "Survival"
            };

            var gameMode4 = new RadioButton()
            {
                Text = "Goal capture"
            };

            var rb1 = new RadioButton()
            {
                Text = "1",
                Checked = true
            };
            var rb2 = new RadioButton()
            {
                Text = "2"
            };
            var rb3 = new RadioButton()
            {
                Text = "3"
            };
            var rb4 = new RadioButton()
            {
                Text = "4"
            };

            var ballsCountTrackBar = new TrackBar
            {
                Maximum = 200,
                Minimum = 10,
                TickStyle = TickStyle.None
            };

            var sizesArray = new object[14];
            var gametimeArray = new object[14];
            for (int i = 0; i < 14; i++)
            {
                sizesArray[i] = i * 100;
                gametimeArray[i] = i;
            }

            var formWidthComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                MaxDropDownItems = 4,
            };
            formWidthComboBox.Items.AddRange(sizesArray.Skip(3).ToArray());
            formWidthComboBox.SelectedIndex = 4;

            var formHeightComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                MaxDropDownItems = 4,
            };
            formHeightComboBox.Items.AddRange(sizesArray.Skip(3).ToArray());
            formHeightComboBox.SelectedIndex = 3;

            var gametimeComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                MaxDropDownItems = 4,
            };
            gametimeComboBox.Items.AddRange(gametimeArray.Skip(1).ToArray());
            gametimeComboBox.SelectedIndex = 2;

            rb1.CheckedChanged += (sender, args) => playersCountSelection = 1;
            rb2.CheckedChanged += (sender, args) => playersCountSelection = 2;
            rb3.CheckedChanged += (sender, args) => playersCountSelection = 3;
            rb4.CheckedChanged += (sender, args) => playersCountSelection = 4;

            gameMode1.CheckedChanged += (sender, args) => gameMode = GameModes.MaxScore;
            gameMode2.CheckedChanged += (sender, args) => gameMode = GameModes.CaptureTheFlag;
            gameMode3.CheckedChanged += (sender, args) => gameMode = GameModes.Survival;
            gameMode4.CheckedChanged += (sender, args) => gameMode = GameModes.GoalCapture;

            controlsHelpButton.Click += ControlsHelpButton_Click;

            startGameButton.Click += StartGameButton_Click;

            formWidthComboBox.SelectedIndexChanged += (sender, args) => selectedFormWidth = (int)formWidthComboBox.SelectedItem;
            formHeightComboBox.SelectedIndexChanged += (sender, args) => selectedFormHeight = (int)formHeightComboBox.SelectedItem;
            gametimeComboBox.SelectedIndexChanged += (sender, args) => selectedGametime = (int)gametimeComboBox.SelectedItem;

            ballsCountTrackBar.ValueChanged += (sender, args) =>
            {
                var userInput = sender as TrackBar;
                ballsCount = userInput.Value;
                typeBallsCountLabel.Text = "Balls on field: " + ballsCount;
            };

            Controls.Add(gameSettingsLabel);
            Controls.Add(typeBallsCountLabel);
            Controls.Add(playerCountLabel);
            Controls.Add(selectGametimeLabel);
            Controls.Add(startGameButton);
            Controls.Add(controlsHelpButton);
            Controls.Add(ballsCountTrackBar);
            Controls.Add(selectFormSizeLabel);
            Controls.Add(rb1);
            Controls.Add(rb2);
            Controls.Add(rb3);
            Controls.Add(rb4);
            Controls.Add(groupBox);
            groupBox.Controls.Add(gameMode1);
            groupBox.Controls.Add(gameMode2);
            groupBox.Controls.Add(gameMode3);
            groupBox.Controls.Add(gameMode4);
            Controls.Add(formWidthComboBox);
            Controls.Add(formHeightComboBox);
            Controls.Add(gametimeComboBox);

            SizeChanged += (sender, args) =>
            {
                var labelHight = 20;
                var buttonHight = 40;
                var margin = 5;

                gameSettingsLabel.Location = new Point(0, ClientSize.Height / 20);
                gameSettingsLabel.Size = new Size(ClientSize.Width, labelHight + margin);
                groupBox.Location = new Point(0, gameSettingsLabel.Bottom + margin);
                groupBox.Size = new Size(ClientSize.Width, 70);
                gameMode1.Location = new Point(margin, labelHight);
                gameMode1.Size = new Size(buttonHight * 4, labelHight);
                gameMode2.Location = new Point(gameMode1.Right, labelHight);
                gameMode2.Size = gameMode1.Size;
                gameMode3.Location = new Point(margin, gameMode1.Bottom);
                gameMode3.Size = gameMode1.Size;
                gameMode4.Location = new Point(gameMode3.Right, gameMode1.Bottom);
                gameMode4.Size = gameMode1.Size;
                playerCountLabel.Location = new Point(0, groupBox.Bottom + margin);
                playerCountLabel.Size = new Size(ClientSize.Width, 20);
                rb1.Location = new Point(margin, playerCountLabel.Bottom);
                rb1.Size = new Size(2 * labelHight, labelHight);
                rb2.Location = new Point(rb1.Right, playerCountLabel.Bottom);
                rb2.Size = rb1.Size;
                rb3.Location = new Point(rb2.Right, playerCountLabel.Bottom);
                rb3.Size = rb1.Size;
                rb4.Location = new Point(rb3.Right, playerCountLabel.Bottom);
                rb4.Size = rb1.Size;
                controlsHelpButton.Location = new Point(rb4.Right + margin, playerCountLabel.Bottom);
                controlsHelpButton.Size = new Size(20, 20);
                typeBallsCountLabel.Location = new Point(0, rb1.Bottom + margin);
                typeBallsCountLabel.Size = playerCountLabel.Size;
                ballsCountTrackBar.Location = new Point(margin, typeBallsCountLabel.Bottom);
                ballsCountTrackBar.Size = new Size(ClientSize.Width - margin * 3, labelHight);
                selectFormSizeLabel.Location = new Point(0, ballsCountTrackBar.Bottom);
                selectFormSizeLabel.Size = playerCountLabel.Size;
                formWidthComboBox.Location = new Point(margin, selectFormSizeLabel.Bottom);
                formHeightComboBox.Location = new Point(formWidthComboBox.Right + 2 * margin, selectFormSizeLabel.Bottom);
                selectGametimeLabel.Location = new Point(0, formWidthComboBox.Bottom + 2 * margin);
                selectGametimeLabel.Size = new Size(100, labelHight);
                gametimeComboBox.Location = new Point(selectGametimeLabel.Right, formWidthComboBox.Bottom + 2 * margin - 1);
                gametimeComboBox.Size = new Size(buttonHight, 10);
                gametimeComboBox.BringToFront();
                startGameButton.Location = new Point(0, gametimeComboBox.Bottom + 2 * margin);
                startGameButton.Size = new Size(ClientSize.Width, buttonHight);
            };

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(300, 400);
            FormClosed += (sender, args) => Application.Exit();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void ControlsHelpButton_Click(object sender, EventArgs e)
        {
            var controlsText =
                "Player 1: \n" +
                "W, A, S, D \n \n" +
                "Player 2: \n" +
                "Up, Left, Down, Right \n \n" +
                "Player 3: \n" +
                "I, J, K, L \n \n" +
                "Player 4: \n" +
                "Num8, Num4, Num5, Num6";
            MessageBox.Show(controlsText,
                "Controls");
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            switch (gameMode)
            {
                case GameModes.MaxScore:
                    {
                        StartMaxScoreGame();
                        break;
                    }
                case GameModes.CaptureTheFlag:
                    {
                        StartCaptureTheFlagGame();
                        break;
                    }
                case GameModes.Survival:
                    {
                        StartSurvivalGame();
                        break;
                    }
                case GameModes.GoalCapture:
                    {
                        StartNewGameModeGame();
                        break;
                    }
            }
        }

        private void StartMaxScoreGame()
        {
            var maxScoreGame = new GameModel(selectedFormWidth, selectedFormHeight, ballsCount, playersCountSelection);
            maxScoreGame.SetGameTime(selectedGametime);
            var maxScoreModeForm = new MaxScoreModeForm(maxScoreGame);
            maxScoreModeForm.ShowDialog();
        }

        private void StartCaptureTheFlagGame()
        {
            var captureTheFlagGame = new GameModel(selectedFormWidth, selectedFormHeight, ballsCount, playersCountSelection);
            captureTheFlagGame.SetGameTime(selectedGametime);
            var captureTheFlagModeForm = new CaptureTheFlagMode(captureTheFlagGame);
            captureTheFlagModeForm.ShowDialog();
        }

        private void StartSurvivalGame()
        {
            var newSurivialGame = new GameModel(selectedFormWidth, selectedFormHeight, ballsCount, playersCountSelection);
            newSurivialGame.SetGameTime(selectedGametime);
            var survivalModeForm = new SurvivalModeForm(newSurivialGame);
            survivalModeForm.ShowDialog();
        }

        private void StartNewGameModeGame()
        {
            var goalCaptureModeGame = new GameModel(selectedFormWidth, selectedFormHeight, ballsCount, playersCountSelection);
            goalCaptureModeGame.SetGameTime(selectedGametime);
            var goalCaptureModeForm = new GoalCaptureModeForm(goalCaptureModeGame);
            goalCaptureModeForm.ShowDialog();
        }
    }
}

```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4DCheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2"; // Game executable name without .exe
        private Timer gameDetectionTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeGameDetection();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat App";
            this.Width = 300;
            this.Height = 200;

            Button toggleCheatButton = new Button
            {
                Text = "Toggle Cheat",
                Location = new System.Drawing.Point(50, 50),
                Width = 200
            };
            toggleCheatButton.Click += ToggleCheatButton_Click;

            this.Controls.Add(toggleCheatButton);
        }

        private void InitializeGameDetection()
        {
            gameDetectionTimer = new Timer();
            gameDetectionTimer.Interval = 2000; // Check every 2 seconds
            gameDetectionTimer.Tick += GameDetectionTimer_Tick;
            gameDetectionTimer.Start();
        }

        private void GameDetectionTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsProcessOpen(GameProcessName);
            UpdateUIForGameState();
        }

        private void UpdateUIForGameState()
        {
            this.Text = isGameRunning ? "Cheat Active - L4D2" : "Left 4 Dead 2 Cheat App";
        }

        private void ToggleCheatButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("Left 4 Dead 2 is not running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Code to toggle the cheat would go here
            MessageBox.Show("Cheat toggled!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static bool IsProcessOpen(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }
    }
}
```
```csharp
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2"; // Game executable name
        private Timer processCheckTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessCheckTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat";
            this.Width = 300;
            this.Height = 200;

            Button toggleCheatButton = new Button();
            toggleCheatButton.Text = "Toggle Cheat";
            toggleCheatButton.Location = new System.Drawing.Point(50, 50);
            toggleCheatButton.Click += ToggleCheatButton_Click;

            Button exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Location = new System.Drawing.Point(50, 100);
            exitButton.Click += ExitButton_Click;

            this.Controls.Add(toggleCheatButton);
            this.Controls.Add(exitButton);
        }

        private void InitializeProcessCheckTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Length > 0;
            if (isGameRunning)
            {
                // Game is running, additional logic can be added here
            }
            else
            {
                // Game is not running, additional logic can be added here
                MessageBox.Show("Left 4 Dead 2 is not running!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToggleCheatButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("Please start Left 4 Dead 2 first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Logic to toggle cheat feature goes here...
            MessageBox.Show("Cheat toggled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
            this.Close();
        }
    }
}
```
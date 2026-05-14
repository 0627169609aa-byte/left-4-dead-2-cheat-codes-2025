```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private Process gameProcess;
        private Timer processCheckTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat";
            this.ClientSize = new System.Drawing.Size(300, 200);

            Button toggleCheatButton = new Button();
            toggleCheatButton.Text = "Toggle Cheat";
            toggleCheatButton.Click += ToggleCheatButton_Click;
            toggleCheatButton.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(toggleCheatButton);

            Button exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Click += ExitButton_Click;
            exitButton.Location = new System.Drawing.Point(50, 100);
            this.Controls.Add(exitButton);

            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeTimer()
        {
            processCheckTimer = new Timer(2000); // Check every 2 seconds
            processCheckTimer.Elapsed += CheckGameProcess;
            processCheckTimer.AutoReset = true;
            processCheckTimer.Enabled = true;
        }

        private void CheckGameProcess(object sender, ElapsedEventArgs e)
        {
            gameProcess = Process.GetProcessesByName(GameProcessName).FirstOrDefault();

            if (gameProcess == null)
            {
                MessageBox.Show("Game is not running!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processCheckTimer.Stop();
            }
        }

        private void ToggleCheatButton_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Logic to toggle cheat goes here
                MessageBox.Show("Cheat toggled!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please start Left 4 Dead 2 first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (processCheckTimer != null)
            {
                processCheckTimer.Stop();
                processCheckTimer.Dispose();
            }
        }
    }
}
```
```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private Timer processDetectTimer;
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat";
            this.Size = new System.Drawing.Size(300, 200);
            
            Button activateCheatButton = new Button();
            activateCheatButton.Text = "Activate Cheat";
            activateCheatButton.Location = new System.Drawing.Point(50, 50);
            activateCheatButton.Click += ActivateCheatButton_Click;

            Button deactivateCheatButton = new Button();
            deactivateCheatButton.Text = "Deactivate Cheat";
            deactivateCheatButton.Location = new System.Drawing.Point(150, 50);
            deactivateCheatButton.Click += DeactivateCheatButton_Click;

            this.Controls.Add(activateCheatButton);
            this.Controls.Add(deactivateCheatButton);
        }

        private void InitializeTimer()
        {
            processDetectTimer = new Timer();
            processDetectTimer.Interval = 1000; // Check every second
            processDetectTimer.Tick += ProcessDetectTimer_Tick;
            processDetectTimer.Start();
        }

        private void ProcessDetectTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            this.Text = isGameRunning ? "Game Running" : "Left 4 Dead 2 Cheat";
        }

        private void ActivateCheatButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Code to activate the cheat
                MessageBox.Show("Cheat Activated!");
            }
            else
            {
                MessageBox.Show("Game not running. Please start Left 4 Dead 2.");
            }
        }

        private void DeactivateCheatButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Code to deactivate the cheat
                MessageBox.Show("Cheat Deactivated!");
            }
            else
            {
                MessageBox.Show("Game not running. Please start Left 4 Dead 2.");
            }
        }
    }
}
```
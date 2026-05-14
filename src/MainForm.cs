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
        private Timer processCheckTimer;
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessCheckTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat";
            this.Size = new System.Drawing.Size(400, 300);

            Button cheatButton = new Button() { Text = "Activate Cheat", Dock = DockStyle.Fill };
            cheatButton.Click += CheatButton_Click;

            this.Controls.Add(cheatButton);
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
            isGameRunning = Process.GetProcessesByName(GameProcessName).Any();
            UpdateUIForGameStatus();
        }

        private void UpdateUIForGameStatus()
        {
            if (isGameRunning)
            {
                this.Text = "Left 4 Dead 2 Cheat - Game Running";
            }
            else
            {
                this.Text = "Left 4 Dead 2 Cheat - Game Not Running";
            }
        }

        private void CheatButton_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("The game must be running to activate cheats!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Placeholder for cheat activation logic
            MessageBox.Show("Cheat Activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            base.OnFormClosing(e);
        }
    }
}
```
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
        private Process _gameProcess;
        private Timer _processDetectionTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessDetection();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat App";
            this.ClientSize = new System.Drawing.Size(300, 200);

            Button btnActivateCheat = new Button
            {
                Text = "Activate Cheat",
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(200, 30)
            };
            btnActivateCheat.Click += BtnActivateCheat_Click;

            Button btnDeactivateCheat = new Button
            {
                Text = "Deactivate Cheat",
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(200, 30)
            };
            btnDeactivateCheat.Click += BtnDeactivateCheat_Click;

            this.Controls.Add(btnActivateCheat);
            this.Controls.Add(btnDeactivateCheat);
        }

        private void InitializeProcessDetection()
        {
            _processDetectionTimer = new Timer
            {
                Interval = 1000 // Check every second
            };
            _processDetectionTimer.Tick += ProcessDetectionTimer_Tick;
            _processDetectionTimer.Start();
        }

        private void ProcessDetectionTimer_Tick(object sender, EventArgs e)
        {
            _gameProcess = Process.GetProcessesByName(GameProcessName).FirstOrDefault();
            if (_gameProcess == null)
            {
                MessageBox.Show("Left 4 Dead 2 is not running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false; // Disable the cheat buttons if game is not running
            }
            else
            {
                this.Enabled = true; // Enable the cheat buttons if game is running
            }
        }

        private void BtnActivateCheat_Click(object sender, EventArgs e)
        {
            if (_gameProcess != null)
            {
                // Code to activate cheat (placeholder)
                MessageBox.Show("Cheat activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDeactivateCheat_Click(object sender, EventArgs e)
        {
            if (_gameProcess != null)
            {
                // Code to deactivate cheat (placeholder)
                MessageBox.Show("Cheat deactivated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
```
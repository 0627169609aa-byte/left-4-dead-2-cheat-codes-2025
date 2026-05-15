```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private Timer processCheckTimer;
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.processCheckTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // processCheckTimer
            // 
            this.processCheckTimer.Interval = 1000; // Check every second
            this.processCheckTimer.Tick += new System.EventHandler(this.ProcessCheckTimer_Tick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Text = "Left 4 Dead 2 Cheat App";
            this.ResumeLayout(false);
            this.Load += new System.EventHandler(this.MainForm_Load);
        }

        private void InitializeTimer()
        {
            processCheckTimer.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckGameProcess();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckGameProcess();
        }

        private void CheckGameProcess()
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Length > 0;
            if (isGameRunning)
            {
                // Enable cheat options
                this.Text = "Left 4 Dead 2 Cheat App - Running";
            }
            else
            {
                // Disable cheat options
                this.Text = "Left 4 Dead 2 Cheat App - Not Running";
            }
        }

        private void EnableCheatsButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Code to enable cheats
                MessageBox.Show("Cheats enabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please start Left 4 Dead 2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableCheatsButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Code to disable cheats
                MessageBox.Show("Cheats disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please start Left 4 Dead 2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```
```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4DCheatApp
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string GameProcessName = "left4dead2";
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.processCheckTimer = new System.Windows.Forms.Timer();
            this.btnActivateCheat = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // btnActivateCheat
            // 
            this.btnActivateCheat.Location = new System.Drawing.Point(50, 50);
            this.btnActivateCheat.Name = "btnActivateCheat";
            this.btnActivateCheat.Size = new System.Drawing.Size(200, 23);
            this.btnActivateCheat.TabIndex = 0;
            this.btnActivateCheat.Text = "Activate Cheat";
            this.btnActivateCheat.UseVisualStyleBackColor = true;
            this.btnActivateCheat.Click += new System.EventHandler(this.BtnActivateCheat_Click);

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(50, 100);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 1;

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnActivateCheat);
            this.Name = "MainForm";
            this.Text = "Left 4 Dead 2 Cheat";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeTimer()
        {
            processCheckTimer.Interval = 2000; // Check every 2 seconds
            processCheckTimer.Tick += new EventHandler(CheckForGameProcess);
            processCheckTimer.Start();
        }

        private void CheckForGameProcess(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Length > 0;
            lblStatus.Text = isGameRunning ? "Game Running" : "Game Not Running";
        }

        private void BtnActivateCheat_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                MessageBox.Show("Please start Left 4 Dead 2 before activating cheats.", "Game Not Running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Call cheat activation logic here
            ActivateCheat();
        }

        private void ActivateCheat()
        {
            // Placeholder for cheat activation logic
            MessageBox.Show("Cheat Activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
```
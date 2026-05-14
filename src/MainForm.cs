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
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // StartButton
            this.StartButton.Location = new System.Drawing.Point(30, 30);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start Cheat";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);

            // StopButton
            this.StopButton.Location = new System.Drawing.Point(120, 30);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop Cheat";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.StopButton);
            this.Name = "MainForm";
            this.Text = "L4D2 CheatApp";
            this.ResumeLayout(false);
        }

        private void InitializeTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = Process.GetProcessesByName(GameProcessName).Length > 0;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Logic to start cheat
                MessageBox.Show("Cheat activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Left 4 Dead 2 is not running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            // Logic to stop cheat
            MessageBox.Show("Cheat deactivated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
    }
}
```
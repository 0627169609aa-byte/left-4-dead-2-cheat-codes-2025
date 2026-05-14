```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private Timer processCheckTimer;
        private const string GameProcessName = "left4dead2";
        private ProcessGameState currentGameState;

        private enum ProcessGameState
        {
            NotRunning,
            Running
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // 1 second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckGameProcess();
        }

        private void CheckGameProcess()
        {
            Process[] gameProcesses = Process.GetProcessesByName(GameProcessName);
            currentGameState = gameProcesses.Length > 0 ? ProcessGameState.Running : ProcessGameState.NotRunning;

            if (currentGameState == ProcessGameState.Running)
            {
                lblStatus.Text = "Game is running.";
                btnActivateCheats.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Game is not running.";
                btnActivateCheats.Enabled = false;
            }
        }

        private void btnActivateCheats_Click(object sender, EventArgs e)
        {
            if (currentGameState == ProcessGameState.Running)
            {
                // Activate cheats (pseudo-code example)
                MessageBox.Show("Cheats activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Add cheat activation logic here
            }
            else
            {
                MessageBox.Show("The game is not running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }

        private void InitializeComponent()
        {
            this.btnActivateCheats = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnActivateCheats
            // 
            this.btnActivateCheats.Location = new System.Drawing.Point(12, 36);
            this.btnActivateCheats.Name = "btnActivateCheats";
            this.btnActivateCheats.Size = new System.Drawing.Size(150, 23);
            this.btnActivateCheats.TabIndex = 1;
            this.btnActivateCheats.Text = "Activate Cheats";
            this.btnActivateCheats.UseVisualStyleBackColor = true;
            this.btnActivateCheats.Click += new System.EventHandler(this.btnActivateCheats_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location
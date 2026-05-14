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
        private const string targetProcessName = "left4dead2";
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += new EventHandler(ProcessCheckTimer_Tick);
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsProcessRunning(targetProcessName);
            updateUI();
        }

        private void updateUI()
        {
            if (isGameRunning)
            {
                lblStatus.Text = "Game is running.";
                btnCheat.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Game is not running.";
                btnCheat.Enabled = false;
            }
        }

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private void btnCheat_Click(object sender, EventArgs e)
        {
            // Call your cheat functions here
            MessageBox.Show("Cheat activated!", "Cheat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }

        private void InitializeComponent()
        {
            this.btnCheat = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCheat
            // 
            this.btnCheat.Location = new System.Drawing.Point(100, 50);
            this.btnCheat.Name = "btnCheat";
            this.btnCheat.Size = new System.Drawing.Size(100, 30);
            this.btnCheat.TabIndex = 1;
            this.btnCheat.Text = "Activate Cheat";
            this.btnCheat.UseVisualStyleBackColor = true;
            this.btnCheat.Click += new System.EventHandler(this.btnCheat_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(97, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCheat);
            this.Name = "MainForm";
            this.Text = "Left 4 Dead 2 Cheat App";
            this.FormClosing += new System
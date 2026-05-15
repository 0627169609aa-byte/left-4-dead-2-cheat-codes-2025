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
        private const string targetProcessName = "left4dead2";
        private bool isGameRunning;

        public MainForm()
        {
            InitializeComponent();
            SetupTimers();
        }

        private void SetupTimers()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 2000; // Check every 2 seconds
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsProcessRunning(targetProcessName);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (isGameRunning)
            {
                lblStatus.Text = "Game Running";
                btnEnableCheats.Enabled = true;
                btnDisableCheats.Enabled = false;
            }
            else
            {
                lblStatus.Text = "Game Not Running";
                btnEnableCheats.Enabled = false;
                btnDisableCheats.Enabled = true;
            }
        }

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private void btnEnableCheats_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Code to enable cheats (placeholder)
                MessageBox.Show("Cheats Enabled!");
            }
        }

        private void btnDisableCheats_Click(object sender, EventArgs e)
        {
            if (isGameRunning)
            {
                // Code to disable cheats (placeholder)
                MessageBox.Show("Cheats Disabled!");
            }
        }

        private void InitializeComponent()
        {
            this.lblStatus = new Label();
            this.btnEnableCheats = new Button();
            this.btnDisableCheats = new Button();
            this.SuspendLayout();

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(114, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Checking Game...";

            // btnEnableCheats
            this.btnEnableCheats.Location = new System.Drawing.Point(12, 40);
            this.btnEnableCheats.Name = "btnEnableCheats";
            this.btnEnableCheats.Size = new System.Drawing.Size(130, 23);
            this.btnEnableCheats.TabIndex = 1;
            this.btnEnableCheats.Text = "Enable Cheats";
            this.btnEnableCheats.UseVisualStyleBackColor = true;
            this.btnEnableCheats.Click += new EventHandler(this.btnEnableCheats_Click);

            // btnDisableCheats
            this.btnDisableCheats.Location = new System.Drawing.Point(148, 40);
            this.btnDisable
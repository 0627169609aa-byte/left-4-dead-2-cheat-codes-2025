```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "Left4Dead2";
        private const string CheatDataAddress = "0x12345678"; // Example address
        private Timer processCheckTimer;
        private bool isGameRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeGameDetection();
        }

        private void InitializeGameDetection()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            isGameRunning = IsProcessOpen(GameProcessName);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (isGameRunning)
            {
                lblStatus.Text = "Game Running";
                btnActivateCheat.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Game Not Running";
                btnActivateCheat.Enabled = false;
            }
        }

        private void btnActivateCheat_Click(object sender, EventArgs e)
        {
            if (!isGameRunning) return;
            
            // Example cheat activation logic
            ActivateCheat(CheatDataAddress);
            MessageBox.Show("Cheat Activated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsProcessOpen(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private void ActivateCheat(string address)
        {
            // Implementation to write to the memory address goes here
            // This is just a placeholder method
        }

        private void InitializeComponent()
        {
            this.lblStatus = new Label();
            this.btnActivateCheat = new Button();

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(80, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Checking...";
            
            // 
            // btnActivateCheat
            // 
            this.btnActivateCheat.Enabled = false;
            this.btnActivateCheat.Location = new System.Drawing.Point(12, 40);
            this.btnActivateCheat.Name = "btnActivateCheat";
            this.btnActivateCheat.Size = new System.Drawing.Size(120, 23);
            this.btnActivateCheat.TabIndex = 1;
            this.btnActivateCheat.Text = "Activate Cheat";
            this.btnActivateCheat.UseVisualStyleBackColor = true;
            this.btnActivateCheat.Click += new EventHandler(btnActivateCheat_Click);
            
            // 
            // MainForm
            // 
            this.ClientSize